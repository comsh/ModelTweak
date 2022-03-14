using System;
using System.Collections.Generic;
using System.IO;
using ModelTweak;

namespace cModelTweak {
class Program {
    const string usage="使い方1: CMT オプション modelファイル名\n"
                      +"オプション:\n"
                      +" /S   シェイプキー名の一覧を出力します\n"
                      +" /T   texファイル名の一覧を出力します\n\n"
                      +"使い方2: CMT オプション modelファイル リストファイル名\n"
                      +"オプション:\n"
                      +" /S   リストファイル内の名称で、modelファイルのシェイプキー名を変更します\n"
                      +" /T   リストファイル内の名称で、modelファイル内のtexファイル名を変更します\n"
                      +" /O 出力ファイル名   更新されたmodelファイルの内容を別ファイルへ出力します\n";

    private static int Usage(){ Console.WriteLine(usage); return 0; }
    private static int NG(string msg){ Console.WriteLine(msg); return -1; }

    static int Main(string[] args) {
        char type=' ';
        var files=new List<string>();
        string outfile=null;
        for(int i=0; i<args.Length; i++){
            if(args[i].Length==0) continue;
            if(args[i][0]!='/') files.Add(args[i]);
            else if(args[i].Length!=2) return Usage();
            else{
                if(args[i][1]=='O'){
                    if(i==args.Length-1) return Usage();
                    outfile=args[++i];
                }else if(args[i][1]=='S' || args[i][1]=='T'){
                    if(type!=' ') return NG("/Sと/Tの両方を指定することはできません");
                    type=args[i][1];
                }else return Usage();
            }
        }
        // ファイル存在チェックだけはやってるけど、その他例外は拾わない
        if(files.Count==1){     // リスト出力
            if(type=='S') return MorphList(files[0]);
            else if(type=='T') return TexList(files[0]);
        }else if(files.Count==2){   // 更新
            if(type=='S') return MorphRename(files[0],files[1],outfile);
            else if(type=='T') return TexRename(files[0],files[1],outfile);
        }
        return Usage();
    }
    private static int MorphList(string modelfile){
        if(!File.Exists(modelfile)) return NG("modelファイルが見つかりません");
        var model=new ModelFile(modelfile);
        var morph=model.smr.morph;
        for(int i=0; i<morph.Count; i++) Console.WriteLine(morph[i].name);
        return 0;
    }
    private static int TexList(string modelfile){
        if(!File.Exists(modelfile)) return NG("modelファイルが見つかりません");
        var model=new ModelFile(modelfile);
        var ti=new TexIter(model.smr.mate);
        ModelFile.PropTexture tx;
        while((tx=ti.Next())!=null) Console.WriteLine(tx.name);
        return 0;
    }
    private static int MorphRename(string modelfile,string namefile,string outfile){
        if(!File.Exists(modelfile)) return NG("modelファイルが見つかりません");
        if(!File.Exists(namefile)) return NG("リストファイルが見つかりません");
        var model=new ModelFile(modelfile);
        var morph=model.smr.morph;
        using(var r=new StreamReader(File.OpenRead(namefile))){
            int line=0;
            while(r.Peek()>=0){
                string name=r.ReadLine();
                if(name=="") return NG($"Line {line+1}: シェイプキー名が空です");
                if(line>=morph.Count) return NG("シェイプキー名の数が多すぎます");
                morph[line++].name=name;
            }
            r.Close();
            if(line<morph.Count) return NG("シェイプキー名の数が少なすぎます");
        }
        WriteFile(model,(outfile==null)?modelfile:outfile);
        return 0;
    }
    private static int TexRename(string modelfile,string namefile,string outfile){
        if(!File.Exists(modelfile)) return NG("modelファイルが見つかりません");
        if(!File.Exists(namefile)) return NG("リストファイルが見つかりません");
        var model=new ModelFile(modelfile);
        using(var r=new StreamReader(File.OpenRead(namefile))){
            var ti=new TexIter(model.smr.mate);

            int tcnt=0; // texファイル数を求めておく
            while(ti.Next()!=null) tcnt++;
            ti.Rewind();
            
            int line=0;
            while(r.Peek()>=0){
                string name=r.ReadLine();
                if(name=="") return NG($"Line {line+1}: texファイル名が空です");
                if(line>=tcnt) return NG("texファイル名の数が多すぎます");
                ti.Next().name=name;
                line++;
            }
            r.Close();
            if(line<tcnt) return NG("texファイル名が少なすぎます");
        }
        WriteFile(model,(outfile==null)?modelfile:outfile);
        return 0;
    }
    private static void WriteFile(ModelFile model,string outfile){
        string tmpfile=Path.GetTempFileName();
        model.Write(tmpfile);
        File.Delete(outfile); File.Move(tmpfile,outfile); // なぜ上書きできない・・・
        File.Delete(tmpfile);  // 一応
    }
    class TexIter {
        int midx;
        int pidx;
        ModelFile.Material[] mate;
        public TexIter(ModelFile.Material[] mate){ this.mate=mate; Rewind(); }
        public void Rewind(){ midx=0; pidx=0; }
        public ModelFile.PropTexture Next(){
            for(; pidx<mate[midx].props.Count; pidx++){
                if(mate[midx].props[pidx].type==ModelFile.Prop.Type.Texture)
                    return (ModelFile.PropTexture)mate[midx].props[pidx++].value;
            }
            for(++midx; midx<mate.Length; midx++){
                for(pidx=0; pidx<mate[midx].props.Count; pidx++){
                    if(mate[midx].props[pidx].type==ModelFile.Prop.Type.Texture)
                        return (ModelFile.PropTexture)mate[midx].props[pidx++].value;
                }
            }
            return null;
        }
    }
}
}

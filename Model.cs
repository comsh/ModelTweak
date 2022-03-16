using System;
using System.Collections.Generic;

namespace ModelTweak {
// ModelFileのデータ加工レイヤ
public class Model {
    private string filename;
    private ModelFile model;
    public ModelFile.SMR smr;

    public static Model Read(string file){
        var model=new Model();
        model.filename=file;
        if(model.Refresh()<0) return null;
        return model;
    }
    public int Write(string file){
        try{ model.Write(file); } catch { return -1; }
        return 0;
    }
    public int Refresh(){
        try{
            model=new ModelFile(filename);
            smr=model.smr;
        }catch{
            return -1;
        }
        MkBoneDic();
        return 0;
    }

    public Bone FindBone(string name){
        if(boneDic.TryGetValue(name,out Bone b)) return b;
        return null;
    }
    public int AddBone(string name,string parent){
        var tr=new ModelFile.Transform(name,0);
        if(parent!=null){
            var p=FindBone(parent);
            if(p==null) return -1;
            (tr.parent=smr.bones[p.idx]).children.Add(tr);
        }else tr.parent=null;
        model.smr.bones.Add(tr);

        // _SCL_対応
        if(tr.parent!=null && tr.parent.hasScl==1){
            tr.parent.hasScl=0;
            tr.name=tr.parent.name+"_SCL_";
            var tr2=new ModelFile.Transform(name,0);
            (tr2.parent=tr).children.Add(tr2);
            model.smr.bones.Add(tr2);
        }

        MkBoneDic();
        return 0;
    }
    public int DelBone(string name){
        var bone=FindBone(name);
        if(bone==null) return -1;

        var tr=smr.bones[bone.idx];
        if(bone.meshIdx>=0){
            foreach(var w in smr.mesh.weights) for(int i=0; i<w.index.Length; i++){
                if(w.index[i]<bone.meshIdx) continue;
                else if(w.index[i]>bone.meshIdx) w.index[i]--;  // 削除対象以降のウェイトの添字を１つ詰める
                else{ w.index[i]=0; w.weight[i]=0; } // このツールではこのケースはない

                // 本当は(汎用処理なら) weightを正規化かつweightの降順にソートしなきゃいけないけど、
                // このツールではindexの付け替えしかやってないので、今のところは正規化等は不要
            }
            smr.mesh.bonelist.RemoveAt(bone.meshIdx);
            smr.mesh.bindpose.RemoveAt(bone.meshIdx);
        }

        if(tr.parent!=null){
            tr.parent.children.Remove(tr);
            model.smr.bones.Remove(tr);
            tr=tr.parent;
            if(tr.name.EndsWith("_SCL_",StringComparison.Ordinal)){ // _SCL_対応
                // (_SCL_がメッシュの方に入る機会はないはず)
                if(tr.parent!=null){
                    tr.parent.children.Remove(tr);
                    tr.parent.hasScl=1;
                }
                model.smr.bones.Remove(tr);
            }
        }else model.smr.bones.Remove(tr);

        MkBoneDic();
        return 0;
    }
    public int AddBoneToMesh(Bone bone){
        if(bone.meshIdx>=0) return 0;
        smr.mesh.bonelist.Add(smr.bones[bone.idx].name);
        bone.meshIdx=smr.mesh.bonelist.Count-1;
        smr.mesh.bindpose.Add(new float[]{1,0,0,0, 0,1,0,0, 0,0,1,0, 0,0,0,1});
        MkBoneDic();
        return 0;
    }

    public class Bone {
        public int idx;          // Transformの添字
        public int meshIdx=-1;   // ウェイトはこの添字で見る。ボーンがメッシュになければ-1
        public Bone(int idx){ this.idx=idx; }
    }
    
    private readonly Dictionary<string,Bone> boneDic=new Dictionary<string,Bone>(); //ボーン名で引く
    private void MkBoneDic(){
        boneDic.Clear();
        var trs=smr.bones;
        for(int i=0; i<trs.Count; i++){
            boneDic.Add(trs[i].name,new Bone(i));
            trs[i].idx=i;
        }
        var mesh=smr.mesh;
        for(int i=0; i<mesh.bonelist.Count; i++)
            if(boneDic.TryGetValue(mesh.bonelist[i],out Bone b)) b.meshIdx=i;
    }
}
}

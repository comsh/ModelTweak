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
        if(boneDic.TryGetValue(name,out int bi)) return trList[bi];
        return null;
    }
    public int AddBone(string name,string parent,ModelFile.Transform orig=null){
        var tr=new ModelFile.Transform(name,0);
        if(parent!=null){
            var p=FindBone(parent);
            if(p==null) return -1;
            tr.parent=smr.bones[p.idx];
            tr.parent.children.Add(tr);
        }else tr.parent=null;
        if(orig==null){
            tr.x=tr.y=tr.z=0; tr.qx=tr.qy=tr.qz=0; tr.qw=1;
            tr.hasScale=false;
        }else{
            tr.x=orig.x; tr.y=orig.y; tr.z=orig.z;
            tr.qx=orig.qx; tr.qy=orig.qy; tr.qz=orig.qz; tr.qw=orig.qw;
            tr.hasScale=orig.hasScale;
            if(tr.hasScale){ tr.scalex=orig.scalex; tr.scaley=orig.scaley; tr.scalez=orig.scalez; }
        }
        model.smr.bones.Add(tr);
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
                if(w.index[i]==bone.meshIdx){ w.index[i]=0; w.weight[i]=0;}
                else w.index[i]--;  // 削除対象以降のウェイトの添字を１つ詰める
            }
            smr.mesh.bonelist.RemoveAt(bone.meshIdx);
            smr.mesh.bindpose.RemoveAt(bone.meshIdx);
        }
        if(tr.parent!=null) tr.parent.children.Remove(tr);
        model.smr.bones.Remove(tr);
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
    
    private readonly List<Bone> trList=new List<Bone>();       // Transformの添字で引く
    private readonly List<Bone> boneList=new List<Bone>();     // Mesh側の添字で引く
    private readonly Dictionary<string,int> boneDic=new Dictionary<string,int>(); //ボーン名で引く
    private void MkBoneDic(){
        trList.Clear();
        boneList.Clear();
        boneDic.Clear();
        var trs=smr.bones;
        for(int i=0; i<trs.Count; i++){
            trList.Add(new Bone(i));
            boneDic.Add(trs[i].name,i);
        }
        var mesh=smr.mesh;
        for(int i=0; i<mesh.bonelist.Count; i++){
            if(boneDic.TryGetValue(mesh.bonelist[i],out int bi)){
                var b=trList[bi];
                boneList.Add(b);
                b.meshIdx=i;
            }
        }
    }
}
}

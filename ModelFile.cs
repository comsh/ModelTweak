using System;
using System.Collections.Generic;
using System.IO;

namespace ModelTweak {
public class ModelFile {
    public int format;
    public string smname;
    public string basebone;    // SkinnedMeshRendererがAddComponentされるbone

    public SMR smr;
    public class SMR {          // UnityのSkinnedMeshRendererに相当
        public Transform[] bones;
        public Mesh mesh;
        public Material[] mate;
        public List<Morph> morph;   // いわゆる「シェイプキー」
    }

    public int Read(string filename){
        var f=File.Open(filename,FileMode.Open);
        using (BinaryReader r=new BinaryReader(f)){
            smr=new SMR();
            ReadHeader(r);
            ReadBones(r,smr);
            smr.mesh=new Mesh(r);
            ReadMaterial(r,smr);
            ReadMorph(r,smr);
        }
        return 0;
    }
    public int Write(string filename){
        var f=File.Open(filename,FileMode.Create);
        using (BinaryWriter w = new BinaryWriter(f)){
            WriteHeader(w);
            WriteBones(w,smr);
            smr.mesh.Write(w);
            WriteMaterial(w,smr);
            WriteMorph(w,smr);
        }
        return 0;
    }
    public void ReadHeader(BinaryReader br){
        br.ReadBytes(11);
        format=br.ReadInt32();
        smname=br.ReadString();
        basebone=br.ReadString();
    }
    public void WriteHeader(BinaryWriter bw){
        bw.Write("CM3D2_MESH");
        bw.Write(format);
        bw.Write(smname);
        bw.Write(basebone);
    }

    public int ReadBones(BinaryReader br,SMR smr){
        int bn=br.ReadInt32();
        var bones=smr.bones=new Transform[bn];
        for(int i=0; i<bn; i++)
            bones[i]=new Transform(br.ReadString(),br.ReadByte());

        for(int i=0; i<bn; i++){
            int p=br.ReadInt32();
            if(p<0) continue;
            bones[i].parent=bones[p];
            bones[p].children.Add(bones[i]);
        }
        for(int i=0; i<bn; i++){
            bones[i].x=br.ReadSingle(); bones[i].y=br.ReadSingle(); bones[i].z=br.ReadSingle();
            bones[i].qx=br.ReadSingle(); bones[i].qy=br.ReadSingle(); bones[i].qz=br.ReadSingle(); bones[i].qw=br.ReadSingle();
            if(2001<=format){
                bones[i].hasScale=br.ReadBoolean();
                if(bones[i].hasScale){
                    bones[i].scalex=br.ReadSingle(); bones[i].scaley=br.ReadSingle(); bones[i].scalez=br.ReadSingle();
                }
            }
        }
        return 0;
    }

    public void WriteBones(BinaryWriter bw,SMR smr){
        Transform[] bones=smr.bones;
        int bn=bones.Length;
        bw.Write(bn);
        var dic=new Dictionary<string,int>();
        for(int i=0; i<bn; i++){
            bw.Write(bones[i].name);
            bw.Write(bones[i].hasScl);
            dic.Add(bones[i].name,i);
        }
        for(int i=0; i<bn; i++){
            if(bones[i].parent==null){
                bw.Write((int)-1);
            }else{
                string pname=bones[i].parent.name;
                bw.Write(dic[pname]);
            }
        }
        for(int i=0; i<bn; i++){
            bw.Write(bones[i].x); bw.Write(bones[i].y); bw.Write(bones[i].z);
            bw.Write(bones[i].qx); bw.Write(bones[i].qy); bw.Write(bones[i].qz); bw.Write(bones[i].qw);
            if(2001<=format){
                bw.Write(bones[i].hasScale);
                if(bones[i].hasScale){
                    bw.Write(bones[i].scalex);
                    bw.Write(bones[i].scaley);
                    bw.Write(bones[i].scalez);
                }
            }
        }
    }

    public void ReadMaterial(BinaryReader br,SMR smr){
        int mcnt=br.ReadInt32();
        smr.mate=new Material[mcnt];
        for(int i=0; i<mcnt; i++) smr.mate[i]=new Material(br);
    }
    public void WriteMaterial(BinaryWriter bw,SMR smr){
        bw.Write(smr.mate.Length);
        for(int i=0; i<smr.mate.Length; i++) smr.mate[i].Write(bw);
    }

    public void ReadMorph(BinaryReader br,SMR smr){
        smr.morph=new List<Morph>();
        while(br.PeekChar()!=-1){
            var cmd=br.ReadString();
            if(cmd=="end") break;
            else if(cmd=="morph") smr.morph.Add(new Morph(br));
        }
    }
    public void WriteMorph(BinaryWriter bw,SMR smr){
        foreach(Morph m in smr.morph){
            bw.Write("morph");
            m.Write(bw);
        }
        bw.Write("end");
    }
    public class Transform {
        public string name;
        public byte hasScl;
        public Transform parent;
        public List<Transform> children;
        public float x; public float y; public float z;
        public float qx; public float qy; public float qz; public float qw;
        public bool hasScale;
        public float scalex; public float scaley; public float scalez;
        public Transform(string name,byte hasScl){
            this.name=name; this.hasScl=hasScl;
            children=new List<Transform>();
        }
    }
    public class Mesh {
        public string[] bonelist;
        public float[][] vertex;
        public float[][] normal;
        public float[][] uv;
        public float[][] bindpose;
        public BoneWeight[] weights;
        public float[][] tangents;
        public ushort[][] triangles;

        public Mesh(BinaryReader br){
            int vertcount=br.ReadInt32();
            int submeshcount=br.ReadInt32();
            int bonecount=br.ReadInt32();
            bonelist=new string[bonecount];
            vertex=new float[vertcount][];
            normal=new float[vertcount][];
            uv=new float[vertcount][];
            bindpose=new float[bonecount][];
            weights=new BoneWeight[vertcount];
            triangles=new ushort[submeshcount][];
    
            for(int i=0; i<bonecount; i++) bonelist[i]=br.ReadString();
            for(int i=0; i<bonecount; i++){
                bindpose[i]=new float[16];
                for(int j=0;j<16;j++) bindpose[i][j]=br.ReadSingle();
            }
    
            for(int i=0; i<vertcount; i++){
                vertex[i]=new float[]{ br.ReadSingle(),br.ReadSingle(),br.ReadSingle() };
                normal[i]=new float[]{ br.ReadSingle(),br.ReadSingle(),br.ReadSingle() };
                uv[i]=new float[]{ br.ReadSingle(),br.ReadSingle() };
            }
            int tancount=br.ReadInt32();
            if(tancount>0){
                tangents=new float[tancount][];
                for(int i=0; i<tancount; i++)
                    tangents[i]=new float[]{
                        br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle()
                    };
            }
            for(int i=0; i<vertcount; i++)
                weights[i]=new BoneWeight(
                    br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(), br.ReadUInt16(),
                    br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle()
                );
            for(int i=0; i<submeshcount; i++){
                int n=br.ReadInt32();
                var vs=new ushort[n];
                for(int j=0; j<n; j++) vs[j]=br.ReadUInt16();
                triangles[i]=vs;
            }
        }
        public void Write(BinaryWriter bw){
            int vn=vertex.Length, bn=bonelist.Length,sn=triangles.Length,i,j;
            bw.Write(vn);
            bw.Write(sn);
            bw.Write(bn);
            for(i=0; i<bn; i++) bw.Write(bonelist[i]);
            for(i=0; i<bn; i++) for(j=0; j<16; j++) bw.Write(bindpose[i][j]);
            for(i=0; i<vn; i++){
                bw.Write(vertex[i][0]); bw.Write(vertex[i][1]); bw.Write(vertex[i][2]);
                bw.Write(normal[i][0]); bw.Write(normal[i][1]); bw.Write(normal[i][2]);
                bw.Write(uv[i][0]); bw.Write(uv[i][1]);
            }
            int tn=(tangents==null)?0:tangents.Length;
            bw.Write(tn);
            for(i=0; i<tn; i++){
                bw.Write(tangents[i][0]); bw.Write(tangents[i][1]); bw.Write(tangents[i][2]); bw.Write(tangents[i][3]);
            }
            for(i=0; i<vn; i++){
                bw.Write(weights[i].index[0]);
                bw.Write(weights[i].index[1]);
                bw.Write(weights[i].index[2]);
                bw.Write(weights[i].index[3]);
                bw.Write(weights[i].weight[0]);
                bw.Write(weights[i].weight[1]);
                bw.Write(weights[i].weight[2]);
                bw.Write(weights[i].weight[3]);
            }
            for(i=0; i<sn; i++){
                int n=triangles[i].Length;
                bw.Write(n);
                for(j=0; j<n; j++) bw.Write(triangles[i][j]);
            }
        }
    }
    public class Material {
        public string name;
        public string shader;
        public string defMaterial;
        public List<Prop> props;

        public Material(BinaryReader br){
            name=br.ReadString();
            shader=br.ReadString();
            defMaterial=br.ReadString();
            props=new List<Prop>();
            while(br.PeekChar()!=-1){
                var type=br.ReadString();
                if(type=="end") break;
                var name=br.ReadString();
                if(type=="tex"){
                    var tf=br.ReadString();
                    if(tf=="null") {
                        var prop=new Prop(name,Prop.Type.Texture2);
                        prop.texture2=new PropTexture2();
                        prop.texture2.dummy1=null;
                        props.Add(prop);
                    }else if(tf=="tex2d"){
                        var prop=new Prop(name,Prop.Type.Texture);
                        var tex=new PropTexture();
                        prop.texture=tex;
                        tex.name=br.ReadString();
                        tex.dummy=br.ReadString(); // 使われてない
                        tex.offsetX=br.ReadSingle(); tex.offsetY=br.ReadSingle();
                        tex.scaleX=br.ReadSingle(); tex.scaleY=br.ReadSingle();
                        props.Add(prop);
                    }else if(tf=="texRT"){ // 使われない。そのまま書き出すだけ
                        var prop=new Prop(name,Prop.Type.Texture2);
                        prop.texture2=new PropTexture2();
                        prop.texture2.dummy1=br.ReadString();
                        prop.texture2.dummy2=br.ReadString();
                        props.Add(prop);
                    }
                }else if(type=="col"){
                    var prop=new Prop(name,Prop.Type.Color);
                    prop.color=new PropColor(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
                    props.Add(prop);
                }else if(type=="vec"){
                    var prop=new Prop(name,Prop.Type.Vector);
                    prop.vector=new PropVector(br.ReadSingle(),br.ReadSingle(),br.ReadSingle(),br.ReadSingle());
                    props.Add(prop);
                }else if(type=="f"){
                    var prop=new Prop(name,Prop.Type.Value);
                    prop.value=br.ReadSingle();
                    props.Add(prop);
                }
            }
        }

        public void Write(BinaryWriter bw){
            bw.Write(name);
            bw.Write(shader);
            bw.Write(defMaterial);
            foreach(var prop in props){
                if(prop.type==Prop.Type.Texture){
                    bw.Write("tex");
                    bw.Write(prop.name);
                    bw.Write("tex2d");
                    bw.Write(prop.texture.name);
                    bw.Write(prop.texture.dummy);
                    bw.Write(prop.texture.offsetX);
                    bw.Write(prop.texture.offsetY);
                    bw.Write(prop.texture.scaleX);
                    bw.Write(prop.texture.scaleY);
                } else if(prop.type==Prop.Type.Texture2){
                    bw.Write("tex");
                    bw.Write(prop.name);
                    if(prop.texture2.dummy1==null) bw.Write("null");
                    else{
                        bw.Write("texRT");
                        bw.Write(prop.texture2.dummy1);
                        bw.Write(prop.texture2.dummy2);
                    }
                } else if(prop.type==Prop.Type.Color){
                    bw.Write("col");
                    bw.Write(prop.name);
                    bw.Write(prop.color.r);
                    bw.Write(prop.color.g);
                    bw.Write(prop.color.b);
                    bw.Write(prop.color.a);
                } else if(prop.type==Prop.Type.Vector){
                    bw.Write("vec");
                    bw.Write(prop.name);
                    bw.Write(prop.vector.x);
                    bw.Write(prop.vector.y);
                    bw.Write(prop.vector.z);
                    bw.Write(prop.vector.w);
                } else if(prop.type==Prop.Type.Value){
                    bw.Write("f");
                    bw.Write(prop.name);
                    bw.Write(prop.value);
                }
            }
            bw.Write("end");
        }
    }
    public class Prop{
        public string name;
        public Type type;
        public PropTexture texture;
        public PropTexture2 texture2;
        public PropColor color;
        public PropVector vector;
        public float value;
        public Prop(string name,Type type){
            this.name=name; this.type=type;
        }

        public enum Type { Texture,Texture2,Color,Vector,Value };
    }
    public class PropColor{
        public float r;
        public float g;
        public float b;
        public float a;
        public PropColor(float r,float g,float b,float a){
           this.r=r; this.g=g; this.b=b; this.a=a;
        }
    }
    public class PropVector{
        public float x;
        public float y;
        public float z;
        public float w;
        public PropVector(float x,float y,float z,float w){
            this.x=x; this.y=y; this.z=z; this.w=w;
        }
    }
    public class PropTexture {
        public string name;
        public string dummy;    // つかわれてない。そのまま書き出す用
        public float offsetX;
        public float offsetY;
        public float scaleX;
        public float scaleY;
        public override string ToString(){return name; }
    }
    public class PropTexture2 { // texRTまたはnullのとき用
        public string dummy1;
        public string dummy2;
    }
    public class BoneWeight {
        public ushort[] index;
        public float[] weight;
        public BoneWeight(ushort i1,ushort i2,ushort i3,ushort i4,float w1,float w2,float w3,float w4){
            index=new ushort[]{i1,i2,i3,i4};
            weight=new float[]{w1,w2,w3,w4};
        }
    }
    public class Morph {
        public string name;
        public ushort[] idx;
        public float[][] v;
        public float[][] norm;
        public Morph(){}
        public Morph(Morph m){
            this.name=m.name;
            int n=m.idx.Length;
            idx=new ushort[n];
            v=new float[n][];
            norm=new float[n][];
            for(int i=0; i<n; i++){
                idx[i]=m.idx[i];
                v[i]=new float[]{m.v[i][0],m.v[i][1],m.v[i][2]};
                norm[i]=new float[]{m.norm[i][0],m.norm[i][1],m.norm[i][2]};
            }
        }
        public Morph(BinaryReader br){
            this.name=br.ReadString();
            int n=br.ReadInt32();
            idx=new ushort[n];
            v=new float[n][];
            norm=new float[n][];
            for(int i=0; i<n; i++){
                idx[i]=br.ReadUInt16();
                v[i]=new float[]{br.ReadSingle(),br.ReadSingle(),br.ReadSingle()};
                norm[i]=new float[]{br.ReadSingle(),br.ReadSingle(),br.ReadSingle()};
            }
        }
        public void Write(BinaryWriter bw){
            bw.Write(name);
            bw.Write(idx.Length);
            for(int i=0; i<idx.Length; i++){
                bw.Write(idx[i]);
                bw.Write(v[i][0]); bw.Write(v[i][1]); bw.Write(v[i][2]);
                bw.Write(norm[i][0]); bw.Write(norm[i][1]); bw.Write(norm[i][2]);
            }
        }
        public override string ToString(){return name; }
    }
}
}

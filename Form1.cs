using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ModelTweak {
    public partial class Form1:Form {
        public Form1() { InitializeComponent(); }
        private void Form1_Load(object sender,EventArgs e) {
            cmbMorphLRType.DropDownStyle=ComboBoxStyle.DropDownList;
            cmbMorphParentBone.DropDownStyle=ComboBoxStyle.DropDownList;

#if DEBUG
            tabControl1.SuspendLayout();
            var pageDebug=new TabPage("Debug");
            tabControl1.TabPages.Add(pageDebug);
            txtDebug=new TextBox();
            txtDebug.Multiline=true;
            txtDebug.MinimumSize=new Size(500,440);
            txtDebug.MaximumSize=new Size(500,440);
            pageDebug.Controls.Add(txtDebug);
            tabControl1.ResumeLayout();
#endif
        }
#if DEBUG
        private TextBox txtDebug;
#endif

        private void btnFileSel_Click(object sender,EventArgs e) {
            string fname = fileDialog();
            if (fname != "") handleInputFileSelected(fname);
        }
        private void Form1_DragEnter(object sender,DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            bool acceptq = true;
            for(int i = 0; i < files.Length; i++)
                if(!files[i].EndsWith(".model")){ acceptq = false; break; }
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) acceptq = false;
            e.Effect = acceptq ? DragDropEffects.All : DragDropEffects.None;
        }
        private void Form1_DragDrop(object sender,DragEventArgs e) {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            handleInputFileSelected(files[0]);
        }

        private Model model;
        private void handleInputFileSelected(string fname) {
            if (!fname.EndsWith(".model")) return;
            txtFile.Text = fname;
            lastPath=System.IO.Path.GetDirectoryName(fname);

            if((model=Model.Read(fname))==null){
                MessageBox.Show("modelファイルが読み込めないか、未対応の形式です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int i=0;
            lstMorph.SuspendLayout();
            lstMorph.Sorting=SortOrder.None; // 随時ソートを避け、後で並び替える。どちらが速いかはわからない
            lstMorph.Items.Clear();
            foreach(var mph in model.smr.morph) lstMorph.Items.Add(new LVItem(mph.name,mph,i++));
            width(lstMorph);
            MorphListSort();
            lstMorph.ResumeLayout();

            lstTex.SuspendLayout();
            lstTex.Items.Clear();
            for(int m=0; m<model.smr.mate.Length; m++)
                foreach(var prop in model.smr.mate[m].props) 
                    if(prop.type==ModelFile.Prop.Type.Texture){
                        var tex=(ModelFile.PropTexture)prop.value;
                        lstTex.Items.Add(new LVItem(tex.name,new MateInfo(m,model.smr.mate[m],prop)));
                    }

            width(lstTex);
            lstTex.ResumeLayout();

            lstBone.SuspendLayout();
            lstBone.Sorting=SortOrder.None;
            lstBone.Items.Clear();
            foreach(var bone in model.smr.bones){
                var item=new LVItem(bone.name,bone);
                lstBone.Items.Add(item);

                if(AnyWeight(bone.name)){ // ウェイト持ちボーンを強調
                    item.Font=new Font(item.Font,FontStyle.Bold);
                    item.ForeColor=Color.Black;
                }else{
                    item.ForeColor=Color.DimGray;
                }
            }
            BoneListSort();
            width(lstBone);
            lstBone.ResumeLayout();

            cmbMorphParentBone.SuspendLayout();
            cmbMorphParentBone.Items.Clear();
            foreach(var bone in model.smr.bones) cmbMorphParentBone.Items.Add(bone.name);
            cmbMorphParentBone.ResumeLayout();

            EnableInputs();

            void width(ListView lv){
                if(lv.Items.Count==0) return;
                lv.Columns[0].Width=-1; // 項目サイズにあわせる
                int w=lv.Items[0].Bounds.Width;
                int scr=0;
                if(lv.Items[lv.Items.Count-1].Position.Y>lv.Height-lv.Margin.Bottom)
                    scr=SystemInformation.VerticalScrollBarWidth;
                var lw=lv.Width-lv.Margin.Left-lv.Margin.Right-scr;;
                if(w<lw) lv.Columns[0].Width=lw;
            }
        }
        private void EnableInputs(){
            EnableInputsMorph(null,null);
            EnableInputsTex(null,null);
            EnableInputsBone(null,null);
        }

        private string lastPath="";
        private string fileDialog() {
            string fname = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "入力modelファイル選択";
            dialog.Filter = "modelファイル|*.model";
            if(lastPath!="") dialog.InitialDirectory=lastPath;
            //dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
            dialog.Dispose();
            return fname;
        }
        private string outFileDialog() {
            string fname = null;
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = System.IO.Path.GetFileNameWithoutExtension(txtFile.Text)+"_modified";
            dialog.Title = "出力modelファイル選択";
            dialog.Filter = "modelファイル|*.model";
            dialog.InitialDirectory=System.IO.Path.GetDirectoryName(txtFile.Text);
            //dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK) fname = dialog.FileName;
            dialog.Dispose();
            return fname;
        }


        // シェイプキー

        private void btnMorphExec_Click(object sender,EventArgs e) {
            if(!MorphValidate()) return;
            try{
                if(chkMorphOW.Checked){
                    string tmpfile=System.IO.Path.GetTempFileName();
                    MorphExec(tmpfile);
                    System.IO.File.Copy(tmpfile,txtFile.Text,true);
                    System.IO.File.Delete(tmpfile);
                }else{
                    string outfile=outFileDialog();
                    if(!string.IsNullOrEmpty(outfile)) MorphExec(outfile);
                }
                handleInputFileSelected(txtFile.Text);
            }catch{
                MessageBox.Show("書き込みに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool CheckMorphName(string name){
            for(int i=0; i<lstMorph.Items.Count; i++)
                if(name==GetMorph(lstMorph.Items[i]).name) return false;
            return true;
        }
        private bool MorphValidate(){
            if(rdoMorphRen.Checked){
                if(txtMorphNewName.Text==""){
                    MessageBox.Show("新しい名前が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(!CheckMorphName(txtMorphNewName.Text)){
                    MessageBox.Show("その名前は使われています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if(rdoMorphLR.Checked){
                if(txtMorphLKey.Text=="" || txtMorphRKey.Text==""){
                    MessageBox.Show("新しい名前が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }else if(txtMorphLKey.Text==txtMorphRKey.Text){
                    MessageBox.Show("左右の名前が同一です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(!CheckMorphName(txtMorphLKey.Text) || !CheckMorphName(txtMorphRKey.Text)){
                    MessageBox.Show("その名前は使われています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if(rdoMorphBone.Checked){
                if(txtMorphNewBoneName.Text==""){
                    MessageBox.Show("ボーン名が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(txtMorphNewBoneName.Text.EndsWith("_SCL_",StringComparison.Ordinal)){
                    MessageBox.Show("そのボーン名は使用できません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(!CheckBoneName(txtMorphNewBoneName.Text)){
                    MessageBox.Show("そのボーン名は使われています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(cmbMorphParentBone.Text==""){
                    MessageBox.Show("親ボーンを指定してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private void EnableInputsMorph(object sender,EventArgs e){
            int n=lstMorph.SelectedItems.Count;
            if(n==0){
                txtMorphNewName.Text=txtMorphLKey.Text=txtMorphRKey.Text="";
                txtMorphNewName.Text=txtMorphLKey.Text=txtMorphRKey.Text="";
            }
            if(n>1) rdoMorphDel.Checked=true;
            rdoMorphDel.Enabled=(n>0);
            rdoMorphBone.Enabled=rdoMorphLR.Enabled=rdoMorphRen.Enabled=(n==1);
            txtMorphNewName.Enabled=rdoMorphRen.Checked;
            txtMorphRKey.Enabled=txtMorphLKey.Enabled=cmbMorphLRType.Enabled=rdoMorphLR.Checked;
            btnMorphExec.Enabled=chkMorphOW.Enabled=(n>0);
            cmbMorphParentBone.Enabled=txtMorphNewBoneName.Enabled=rdoMorphBone.Checked;
        }
        private void lstMorph_SelectedIndexChanged(object sender,EventArgs e) {
            EnableInputsMorph(null,null);
            if(lstMorph.SelectedItems.Count!=1) return;
            var morph=GetMorph(lstMorph.SelectedItems[0]);
            string name=morph.name;
            txtMorphNewName.Text=name;
            txtMorphLKey.Text=name+"_l";
            txtMorphRKey.Text=name+"_r";

#if DEBUG
            txtDebug.Text="";
            txtDebug.SuspendLayout();
            for(int i=0; i<morph.idx.Length; i++)
                txtDebug.Text+=$"{morph.idx[i]} {xyz(morph.v[i])} {xyz(morph.norm[i])} {xyz(model.smr.mesh.vertex[morph.idx[i]])} {xyz(model.smr.mesh.normal[morph.idx[i]])}\r\n";
            txtDebug.ResumeLayout();
#endif
        }
#if DEBUG
        private string xyz(float[] p){
            return string.Concat(p[0].ToString("F6"),",",p[1].ToString("F6"),",",p[2].ToString("F6"));
        }
#endif

        private static Regex leftBone=new Regex(@"L\b|_L_",RegexOptions.Compiled);
        private static Regex rightBone=new Regex(@"R\b|_R_",RegexOptions.Compiled);
        private ModelFile.Morph FilterMorph(ModelFile.Morph morph,Regex reg){
            var ret=new ModelFile.Morph();
            List<int> pick=new List<int>();
            if(cmbMorphLRType.SelectedText=="ボーン"){
                for(int i=0; i<morph.idx.Length; i++){
                    var bones=model.smr.mesh.weights[morph.idx[i]];
                    for(int j=0; j<bones.index.Length; j++){
                        if(bones.weight[j]<0.01f) continue;
                        string bonename=model.smr.mesh.bonelist[bones.index[j]];
                        if(reg.Match(bonename).Success){ pick.Add(i); break; }
                    }
                }
            }else{ // 座標
                if(reg==leftBone){
                    for(int i=0; i<morph.idx.Length; i++)
                        if(model.smr.mesh.vertex[morph.idx[i]][0]<=0) pick.Add(i);
                }else{
                    for(int i=0; i<morph.idx.Length; i++)
                        if(model.smr.mesh.vertex[morph.idx[i]][0]>0) pick.Add(i);
                }
            }
            ret.idx=new ushort[pick.Count];
            ret.v=new float[pick.Count][];
            ret.norm=new float[pick.Count][];
            int n=0;
            foreach(int p in pick){
                ret.idx[n]=morph.idx[p];
                ret.v[n]=new float[]{morph.v[p][0],morph.v[p][1],morph.v[p][2]};
                ret.norm[n]=new float[]{morph.norm[p][0],morph.norm[p][1],morph.norm[p][2]};
                n++;
            }
            return ret;
        }
        private void MorphExec(string ofile){
            // modelを書き換えちゃうので後で読み込み直してね
            if(rdoMorphDel.Checked){
                foreach(LVItem d in lstMorph.SelectedItems){
                    model.smr.morph.Remove(GetMorph(d));
                }
            } else if(rdoMorphRen.Checked){
                ModelFile.Morph morph=GetMorph(lstMorph.SelectedItems[0]);
                morph.name=txtMorphNewName.Text;
            } else if(rdoMorphLR.Checked){
                ModelFile.Morph morph=GetMorph(lstMorph.SelectedItems[0]);
                var l=FilterMorph(morph,leftBone);
                l.name=txtMorphLKey.Text;
                model.smr.morph.Add(l);
                var r=FilterMorph(morph,rightBone);
                r.name=txtMorphRKey.Text;
                model.smr.morph.Add(r);
            } else if(rdoMorphBone.Checked){
                ModelFile.Morph morph=GetMorph(lstMorph.SelectedItems[0]);
                model.AddBone(txtMorphNewBoneName.Text,cmbMorphParentBone.Text);
                var to=model.FindBone(txtMorphNewBoneName.Text);
                var parent=model.FindBone(cmbMorphParentBone.Text);
                var mesh=model.smr.mesh;
                model.AddBoneToMesh(to);
                mesh.bindpose[to.meshIdx]=mesh.bindpose[parent.meshIdx];
                for(int i=0; i<morph.idx.Length; i++){
                    if(  Math.Abs(morph.v[i][0])<0.001
                      && Math.Abs(morph.v[i][1])<0.001
                      && Math.Abs(morph.v[i][2])<0.001
                      /* && Math.Abs(morph.norm[i][0])<0.001
                      && Math.Abs(morph.norm[i][1])<0.001
                      && Math.Abs(morph.norm[i][2])<0.001 */ ) continue;
                    var wt=mesh.weights[morph.idx[i]];
                    wt.index[0]=(ushort)to.meshIdx; wt.weight[0]=1.0f;
                    wt.index[1]=0; wt.weight[1]=0.0f;
                    wt.index[2]=0; wt.weight[2]=0.0f;
                    wt.index[3]=0; wt.weight[3]=0.0f;
                }
            }
            model.Write(ofile);
        }

        // texファイル

        private void btnTexExec_Click(object sender,EventArgs e) {
            if(!TexValidate()) return;
            try{
                if(chkTexOW.Checked){
                    string tmpfile=System.IO.Path.GetTempFileName();
                    TexExec(tmpfile);
                    System.IO.File.Copy(tmpfile,txtFile.Text,true);
                    System.IO.File.Delete(tmpfile);
                }else{
                    string outfile=outFileDialog();
                    if(!string.IsNullOrEmpty(outfile)) TexExec(outfile);
                }
                handleInputFileSelected(txtFile.Text);
            }catch{
                MessageBox.Show("書き込みに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TexValidate(){
            if(txtTex.Text==""){
                MessageBox.Show("texファイル名が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(!float.TryParse(txtTexOffX.Text,out float ox)
            || !float.TryParse(txtTexOffY.Text,out float oy)
            || !float.TryParse(txtTexSclX.Text,out float sx)
            || !float.TryParse(txtTexSclY.Text,out float sy)){
                MessageBox.Show("数値が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void EnableInputsTex(object sender,EventArgs e){
            int n=lstTex.SelectedItems.Count;
            btnTexExec.Enabled=chkTexOW.Enabled=(n>0);
            if(n==0){
                lblMateName.Text=lblMateShader1.Text=lblMateProp.Text="";
                txtTex.Text=txtTexOffX.Text=txtTexOffY.Text=txtTexSclX.Text=txtTexSclY.Text="";
            }
            txtTex.Enabled=txtTexOffX.Enabled=txtTexOffY.Enabled=txtTexSclX.Enabled=txtTexSclY.Enabled=(n>0);
        }

        private void lstTex_SelectedIndexChanged(object sender,EventArgs e) {
            EnableInputsTex(null,null);
            if(lstTex.SelectedItems.Count==0) return;
            var mi=GetMate(lstTex.SelectedItems[0]);
            var tex=(ModelFile.PropTexture)mi.prop.value;
            lblMateNo.Text=mi.no.ToString();
            lblMateName.Text=mi.mate.name;
            lblMateShader1.Text=mi.mate.shader;
            lblMateProp.Text=mi.prop.name;
            txtTex.Text=tex.name;
            txtTexOffX.Text=tex.offsetX.ToString("F4");
            txtTexOffY.Text=tex.offsetY.ToString("F4");
            txtTexSclX.Text=tex.scaleX.ToString("F4");
            txtTexSclY.Text=tex.scaleY.ToString("F4");
        }

        private void TexExec(string ofile){
            if(!float.TryParse(txtTexOffX.Text,out float ox)
            || !float.TryParse(txtTexOffY.Text,out float oy)
            || !float.TryParse(txtTexSclX.Text,out float sx)
            || !float.TryParse(txtTexSclY.Text,out float sy)) return;
            
            var mi=GetMate(lstTex.SelectedItems[0]);
            var tex=(ModelFile.PropTexture)mi.prop.value;
            tex.name=txtTex.Text;
            if(tex.name.EndsWith(".tex",StringComparison.Ordinal)) tex.name=tex.name.Substring(0,tex.name.Length-4);
            tex.offsetX=ox; tex.offsetY=oy; tex.scaleX=sx; tex.scaleY=sy;
            model.Write(ofile);
        }

        // ボーン

        private void btnBoneAdd_Click(object sender,EventArgs e) {
            if(!BoneValidate()) return;
            try{
                if(chkBoneOW.Checked){
                    string tmpfile=System.IO.Path.GetTempFileName();
                    BoneExec(tmpfile);
                    System.IO.File.Copy(tmpfile,txtFile.Text,true);
                    System.IO.File.Delete(tmpfile);
                }else{
                    string outfile=outFileDialog();
                    if(!string.IsNullOrEmpty(outfile)) BoneExec(outfile);
                }
                handleInputFileSelected(txtFile.Text);
            }catch{
                MessageBox.Show("書き込みに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckBoneName(string name){
            var lst=model.smr.bones;
            for(int i=0; i<lst.Count; i++) if(name==lst[i].name) return false;
            return true;
        }
        private struct BoneFormData {
            public float x0,x1;
            public float y0,y1;
            public float z0,z1;
            public float lx,ly,lz;
        }
        private BoneFormData bfd;
        private bool BoneValidate(){
            if(rdoBoneAddChild.Checked){
                if(txtNewBone.Text==""){
                    MessageBox.Show("ボーン名が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(txtNewBone.Text.EndsWith("_SCL_",StringComparison.Ordinal)){
                    MessageBox.Show("そのボーン名は使用できません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(!CheckBoneName(txtNewBone.Text)){
                    MessageBox.Show("そのボーン名は使われています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if( !float.TryParse(txNewBoneX0.Text,out bfd.x0) ||
                    !float.TryParse(txNewBoneX1.Text,out bfd.x1) ||
                    !float.TryParse(txNewBoneY0.Text,out bfd.y0) ||
                    !float.TryParse(txNewBoneY1.Text,out bfd.y1) ||
                    !float.TryParse(txNewBoneZ0.Text,out bfd.z0) ||
                    !float.TryParse(txNewBoneZ1.Text,out bfd.z1) ){
                    MessageBox.Show("数値が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(bfd.x0>bfd.x1){ float t=bfd.x0; bfd.x0=bfd.x1; bfd.x1=t; txNewBoneX0.Text=bfd.x0.ToString(); txNewBoneX1.Text=bfd.x1.ToString(); }
                if(bfd.y0>bfd.y1){ float t=bfd.y0; bfd.y0=bfd.y1; bfd.y1=t; txNewBoneY0.Text=bfd.y0.ToString(); txNewBoneY1.Text=bfd.y1.ToString(); }
                if(bfd.z0>bfd.z1){ float t=bfd.z0; bfd.z0=bfd.z1; bfd.z1=t; txNewBoneZ0.Text=bfd.z0.ToString(); txNewBoneZ1.Text=bfd.z1.ToString(); }
                int ret;
                if((ret=ParseSubMeshFilter(txtBoneSubMeshNo.Text))<0){
                    submeshFilter=null;
                    if(ret==-2) MessageBox.Show("サブメッシュ番号が範囲外です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("サブメッシュ番号の指定が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if(rdoBoneMove.Checked){
                if( !float.TryParse(txtBoneMoveX.Text,out bfd.lx) ||
                    !float.TryParse(txtBoneMoveY.Text,out bfd.ly) ||
                    !float.TryParse(txtBoneMoveZ.Text,out bfd.lz) ){
                    MessageBox.Show("数値が正しくありません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private int[] submeshFilter=null;
        private int ParseSubMeshFilter(string txt){
            submeshFilter=null;
            int submeshcnt=model.smr.mesh.triangles.Length;
            var ret=new HashSet<int>();
            string[] sa=txt.Split(',');
            for(int i=0; i<sa.Length; i++){
                string si=sa[i].Trim();
                int minus=si.IndexOf('-');
                if(minus==0) return -1;
                else if(minus>0){
                    if(!int.TryParse(si.Substring(0,minus),out int n)) return -1;
                    if(!int.TryParse(si.Substring(minus+1),out int n1)||n1<0) return -1;
                    if(n>n1){ int t=n; n=n1; n1=t; }
                    for(; n<=n1; n++) if(n<submeshcnt) ret.Add(n); else return -2;
                }else{
                    if(!int.TryParse(si,out int n)) return -1;
                    if(n<submeshcnt) ret.Add(n); else return -2;
                }
            }
            if(ret.Count==0) return 0; // 空ならnullのままにする
            submeshFilter=new int[ret.Count];
            ret.CopyTo(submeshFilter);
            return 0;
        }

        private void BoneExec(string ofile){
            var basebone=GetBone(lstBone.SelectedItems[0]);
            var mesh=model.smr.mesh;

            if(rdoBoneAddChild.Checked){ // 子ボーン追加
                model.AddBone(txtNewBone.Text,basebone.name);
                weight(basebone.name,txtNewBone.Text,new float[]{bfd.x0,bfd.x1,bfd.y0,bfd.y1,bfd.z0,bfd.z1},submeshFilter);
            }else if(rdoBoneDel.Checked){ // ボーン削除
                if(basebone.parent!=null){
                    if(basebone.parent.name.EndsWith("_SCL_",StringComparison.Ordinal))
                        weight(basebone.name,basebone.parent.parent.name,null,null); // SCLの親は必ずあるはず
                    else 
                        weight(basebone.name,basebone.parent.name,null,null);
                }
                model.DelBone(basebone.name);
            }else if(rdoBoneMove.Checked){
                float ox=basebone.x, oy=basebone.y, oz=basebone.z;
                basebone.x=bfd.lx; basebone.y=bfd.ly; basebone.z=bfd.lz;
                float[] v=new float[]{ bfd.lx-ox, bfd.ly-oy, bfd.lz-oz};

                var b=model.FindBone(basebone.name);

                // bindposeの変位部分は localPositionと同じ座標系
                // なので、localPositionの差分は、単にそのまま引けばいい
                var w2l=mesh.bindpose[b.meshIdx];
                w2l[12]-=v[0]; w2l[13]-=v[1]; w2l[14]-=v[2];
            }
            model.Write(ofile);

            void weight(string name1,string name2,float[] range,int[] submesh){
                var from=model.FindBone(name1);
                if(from.meshIdx<0) return;
                var to=model.FindBone(name2);
                if(to.meshIdx<0){
                    model.AddBoneToMesh(to);
                    // すぐ書き出して読み直すからシャローコピーでいい
                    mesh.bindpose[to.meshIdx]=mesh.bindpose[from.meshIdx];
                }
                // ウェイト付け替え
                for(int v=0; v<mesh.weights.Length; v++){
                   if(submesh!=null){
                        int j,k;
                        for(j=0; j<submesh.Length; j++){
                            var tj=mesh.triangles[submesh[j]];
                            for(k=0; k<tj.Length; k++) if(tj[k]==v) break;
                            if(k<tj.Length) break;
                        }
                        if(j>=submesh.Length) continue;
                   }
                    var wt=mesh.weights[v];
                    for(int i=0; i<wt.index.Length; i++) if(wt.index[i]==from.meshIdx && wt.weight[i]>0){
                        if(range!=null){
                            var p=model.W2L(v,from.meshIdx);
                            if(p[0]<range[0]||p[0]>range[1] || p[1]<range[2]||p[1]>range[3] || p[2]<range[4]||p[2]>range[5]) continue;
                        }
                        wt.index[i]=(ushort)to.meshIdx;
                    }
                }
            }
        }

        private int CountWeight(string name,float tt=0f){
            var b=model.FindBone(name);
            if(b==null||b.meshIdx<0) return 0;
            int cnt=0;
            foreach(var wt in model.smr.mesh.weights)
                for(int i=0; i<wt.index.Length; i++) if(wt.index[i]==b.meshIdx && wt.weight[i]>tt){ cnt++; break; }
            return cnt;
        }
        private bool AnyWeight(string name,float tt=0f){
            var b=model.FindBone(name);
            if(b==null||b.meshIdx<0) return false;
            foreach(var wt in model.smr.mesh.weights)
                for(int i=0; i<wt.index.Length; i++) if(wt.index[i]==b.meshIdx && wt.weight[i]>tt){ return true; }
            return false;
        }

        private void EnableInputsBone(object sender,EventArgs e){
            int n=lstBone.SelectedItems.Count;
            if(n==0){
                btnBoneAdd.Enabled=chkBoneOW.Enabled=
                rdoBoneAddChild.Enabled=rdoBoneDel.Enabled=txtNewBone.Enabled=rdoBoneMove.Enabled=false;
                txtBaseBone.Text=txtNewBone.Text=lblBoneSCL.Text="";
                txNewBoneX0.Enabled=txNewBoneX1.Enabled=txNewBoneY0.Enabled=txNewBoneY1.Enabled=txNewBoneZ0.Enabled=txNewBoneZ1.Enabled=false;
                txtBoneSubMeshNo.Enabled=false;
                txtBoneMoveX.Enabled=txtBoneMoveY.Enabled=txtBoneMoveZ.Enabled=false;
            }else{
                btnBoneAdd.Enabled=chkBoneOW.Enabled=rdoBoneAddChild.Enabled=true;
                rdoBoneDel.Enabled=rdoBoneMove.Enabled=(boneCCnt==0);
                txtNewBone.Enabled=rdoBoneAddChild.Checked;
                txNewBoneX0.Enabled=txNewBoneX1.Enabled=txNewBoneY0.Enabled=txNewBoneY1.Enabled=txNewBoneZ0.Enabled=txNewBoneZ1.Enabled=rdoBoneAddChild.Checked;
                txtBoneSubMeshNo.Enabled=rdoBoneAddChild.Checked;
                txtBoneMoveX.Enabled=txtBoneMoveY.Enabled=txtBoneMoveZ.Enabled=rdoBoneMove.Checked;
            }
        }

        private int boneWCnt=0;
        private int boneCCnt=0;
        private void lstBone_SelectedIndexChanged(object sender,EventArgs e) {
            if(lstBone.SelectedItems.Count==0){
                boneWCnt=boneCCnt=0;
                EnableInputsBone(null,null);
                return;
            }
            var bone=GetBone(lstBone.SelectedItems[0]);
            boneWCnt=CountWeight(bone.name);
            boneCCnt=bone.children.Count;
            if(bone.hasScl==1) lblBoneSCL.Text="[_SCL_有]"; else lblBoneSCL.Text="";
            lblBoneWeightCnt.Text=boneWCnt.ToString();
            lblBoneChildCnt.Text=boneCCnt.ToString();
            txtBaseBone.Text=bone.name;
            txtNewBone.Text="";
            txtBoneMoveX.Text=bone.x.ToString("F4");
            txtBoneMoveY.Text=bone.y.ToString("F4");
            txtBoneMoveZ.Text=bone.z.ToString("F4");
            EnableInputsBone(null,null);
        }


        //    リストのソート関連

        public static ModelFile.Morph GetMorph(ListViewItem lvi) { return (ModelFile.Morph)((LVItem)lvi).item; }
        public static MateInfo GetMate(ListViewItem lvi) { return (MateInfo)((LVItem)lvi).item; }
        public static ModelFile.Transform GetBone(ListViewItem lvi) { return (ModelFile.Transform)((LVItem)lvi).item; }
        public class LVItem:ListViewItem {
            public int idx;
            public object item;
            public LVItem(string str,object item,int i=0) : base(str) { this.item=item; this.idx=i; }
        }
        public class MateInfo {
            public int no;
            public ModelFile.Material mate;
            public ModelFile.Prop prop;
            public MateInfo(int no,ModelFile.Material mate,ModelFile.Prop prop){
                this.no=no; this.mate=mate; this.prop=prop;
            }
        }

        private void rdoMorphSortAsc_CheckedChanged(object sender,EventArgs e) {
            if(rdoMorphSortAsc.Checked){
                lstMorph.ListViewItemSorter=null;
                lstMorph.Sorting=SortOrder.Ascending;
            }
        }
        private void rdoMorphSortDsc_CheckedChanged(object sender,EventArgs e) {
            if(rdoMorphSortDsc.Checked){
                lstMorph.ListViewItemSorter=null;
                lstMorph.Sorting=SortOrder.Descending;
            }
        }
        private class LVItemComparer : IComparer {
            public int Compare(object a,object b){
                return ((LVItem)a).idx-((LVItem)b).idx;
            }
        }
        private void rdoMorphNoSort_CheckedChanged(object sender,EventArgs e) {
            if(rdoMorphNoSort.Checked){
                lstMorph.Sorting=SortOrder.None;
                lstMorph.ListViewItemSorter=new LVItemComparer();
            }
        }
        private void MorphListSort(){
            rdoMorphNoSort_CheckedChanged(null,null);
            rdoMorphSortAsc_CheckedChanged(null,null);
            rdoMorphSortDsc_CheckedChanged(null,null);
        }
        private void btnMorphReverse_Click(object sender,EventArgs e) {
            for(int i=0; i<lstMorph.Items.Count; i++)
                lstMorph.Items[i].Selected=!lstMorph.Items[i].Selected;
            lstMorph.Focus();
            EnableInputsMorph(null,null);
        }

        private void tabControl1_SelectedIndexChanged(object sender,EventArgs e) {
            switch(tabControl1.SelectedIndex){
            case 0: EnableInputsMorph(null,null); lstMorph.Focus(); break;
            case 1: EnableInputsTex(null,null); lstTex.Focus(); break;
            case 2: EnableInputsBone(null,null); lstBone.Focus(); break;
            }
        }
        private void rdoBoneSortNormal_CheckedChanged(object sender,EventArgs e) {
            if(rdoBoneSortNormal.Checked){
                lstBone.ListViewItemSorter=null;
                lstBone.Sorting=SortOrder.Ascending;
            }
        }

        private class BoldBoneComparer : IComparer {
            public int Compare(object a,object b){
                var ia=(ListViewItem)a; var ib=(ListViewItem)b;
                var ba=GetBone(ia); var bb=GetBone(ib);
                int ret=(ia.Font.Bold?0:1)-(ib.Font.Bold?0:1);
                if(ret==0) return string.CompareOrdinal(ba.name,bb.name);
                else return ret;
            }
        }
        private void rdoBoneSortWeight_CheckedChanged(object sender,EventArgs e) {
            if(rdoBoneSortWeight.Checked){
                lstBone.Sorting=SortOrder.None;
                lstBone.ListViewItemSorter=new BoldBoneComparer();
            }
        }
        private class ChildBoneComparer : IComparer {
            public int Compare(object a,object b){
                var ba=GetBone((ListViewItem)a);
                var bb=GetBone((ListViewItem)b);
                int ret=(ba.children.Count>0?1:0)-(bb.children.Count>0?1:0);
                if(ret==0) return string.CompareOrdinal(ba.name,bb.name);
                else return ret;
            }
        }
        private void rdoBoneSortChild_CheckedChanged(object sender,EventArgs e) {
            if(rdoBoneSortChild.Checked){
                lstBone.Sorting=SortOrder.None;
                lstBone.ListViewItemSorter=new ChildBoneComparer();
            }
        }
        private void BoneListSort(){
            rdoBoneSortNormal_CheckedChanged(null,null);
            rdoBoneSortWeight_CheckedChanged(null,null);
            rdoBoneSortChild_CheckedChanged(null,null);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ModelTweak {
    public partial class Form1:Form {
        public Form1() { InitializeComponent(); }

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
            lstMorph.Items.Clear();
            foreach(var mph in model.smr.morph) lstMorph.Items.Add(new LVItem(mph.name,mph,i++));
            width(lstMorph);

            lstTex.Items.Clear();
            foreach(var mat in model.smr.mate) foreach(var prop in mat.props) if(prop.type==ModelFile.Prop.Type.Texture){
                var tex=(ModelFile.PropTexture)prop.value;
                lstTex.Items.Add(new LVItem(tex.name,new MateInfo(mat,prop)));
            }
            width(lstTex);

            lstBone.Items.Clear();
            cmbBoneWeightMoveTarget.Items.Clear();
            foreach(var bone in model.smr.bones){
                var item=new LVItem(bone.name,bone);
                lstBone.Items.Add(item);

                // ウェイト移動先コンボボックスにも項目(名前のみ)追加
                cmbBoneWeightMoveTarget.Items.Add(bone.name);

                if(AnyWeight(bone.name)){ // ウェイト持ちボーンを強調
                    item.Font=new Font(item.Font,FontStyle.Bold);
                    item.ForeColor=Color.Black;
                }else{
                    item.ForeColor=Color.DimGray;
                }
            }
            width(lstBone);
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
                    MorphExec(outfile);
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
            rdoMorphLR.Enabled=rdoMorphRen.Enabled=(n==1);
            txtMorphNewName.Enabled=rdoMorphRen.Checked;
            txtMorphRKey.Enabled=txtMorphLKey.Enabled=rdoMorphLR.Checked;
            btnMorphExec.Enabled=chkMorphOW.Enabled=(n>0);
        }
        private void lstMorph_SelectedIndexChanged(object sender,EventArgs e) {
            EnableInputsMorph(null,null);
            if(lstMorph.SelectedItems.Count!=1) return;
            string name=GetMorph(lstMorph.SelectedItems[0]).name;
            txtMorphNewName.Text=name;
            txtMorphLKey.Text=name+"_l";
            txtMorphRKey.Text=name+"_r";
        }

        private static Regex leftBone=new Regex(@"L\b|_L_",RegexOptions.Compiled);
        private static Regex rightBone=new Regex(@"R\b|_R_",RegexOptions.Compiled);
        private ModelFile.Morph FilterMorph(ModelFile.Morph morph,Regex reg){
            var ret=new ModelFile.Morph();
            List<int> pick=new List<int>();
            for(int i=0; i<morph.idx.Length; i++){
                var bones=model.smr.mesh.weights[morph.idx[i]];
                for(int j=0; j<bones.index.Length; j++){
                    if(bones.weight[j]<0.01f) continue;
                    string bonename=model.smr.mesh.bonelist[bones.index[j]];
                    if(reg.Match(bonename).Success){ pick.Add(i); break; }
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
                    TexExec(outfile);
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
        }

        private void lstTex_SelectedIndexChanged(object sender,EventArgs e) {
            EnableInputsTex(null,null);
            if(lstTex.SelectedItems.Count==0) return;
            var mi=GetMate(lstTex.SelectedItems[0]);
            var tex=(ModelFile.PropTexture)mi.prop.value;
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
                    BoneExec(outfile);
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
        private bool BoneValidate(){
            if(rdoBoneAddChild.Checked){
                if(txtNewBone.Text==""){
                    MessageBox.Show("ボーン名が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(!CheckBoneName(txtNewBone.Text)){
                    MessageBox.Show("その名前は使われています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }else if(rdoBoneAddSibling.Checked){
                if(txtBoneNewSibling.Text==""){
                    MessageBox.Show("ボーン名が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(!CheckBoneName(txtBoneNewSibling.Text)){
                    MessageBox.Show("その名前は使われています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            } else if(rdoBoneDel.Checked && boneWCnt>0){
                if(cmbBoneWeightMoveTarget.Text==""){
                    MessageBox.Show("相続ボーン名が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                } else if(CheckBoneName(cmbBoneWeightMoveTarget.Text)){
                    MessageBox.Show("対象ボーンが存在しません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }
        private void BoneExec(string ofile){
            var basebone=GetBone(lstBone.SelectedItems[0]);
            var mesh=model.smr.mesh;

            if(rdoBoneAddChild.Checked){ // 子ボーン追加
                model.AddBone(txtNewBone.Text,basebone.name);
                weight(basebone.name,txtNewBone.Text);
            }else if(rdoBoneAddSibling.Checked){ // 兄弟ボーン追加
                string pname=(basebone.parent==null)?null:basebone.parent.name;
                model.AddBone(txtBoneNewSibling.Text,pname,basebone);
                weight(basebone.name,txtBoneNewSibling.Text);
            }else if(rdoBoneDel.Checked){ // ボーン削除
                weight(basebone.name,cmbBoneWeightMoveTarget.Text);
                model.DelBone(basebone.name);
            }
            model.Write(ofile);

            void weight(string name1,string name2){
                var from=model.FindBone(name1);
                if(from.meshIdx<0) return;
                var to=model.FindBone(name2);
                if(to.meshIdx<0) model.AddBoneToMesh(to);
                // すぐ書き出して読み直すからシャローコピーでいい
                mesh.bindpose[to.meshIdx]=mesh.bindpose[from.meshIdx];
                // ウェイト付け替え
                foreach(var wt in mesh.weights)
                    for(int i=0; i<wt.index.Length; i++)
                        if(wt.index[i]==from.meshIdx && wt.weight[i]>0) wt.index[i]=(ushort)to.meshIdx;
            }
        }
        private int CountWeight(string name){
            var list=model.smr.mesh.bonelist;
            for(int i=0; i<list.Count; i++) if(list[i]==name) return CountWeight(i);
            return 0;
        }
        private int CountWeight(int idx){
            int cnt=0;
            foreach(var wt in model.smr.mesh.weights)
                for(int i=0; i<wt.index.Length; i++) if(wt.index[i]==idx && wt.weight[i]>0f){ cnt++; break; }
            return cnt;
        }
        private bool AnyWeight(string name, float tt=0f){
            var list=model.smr.mesh.bonelist;
            for(int i=0; i<list.Count; i++) if(list[i]==name) return AnyWeight(i,tt);
            return false;
        }
        private bool AnyWeight(int idx,float tt=0f){
            foreach(var wt in model.smr.mesh.weights)
                for(int i=0; i<wt.index.Length; i++) if(wt.index[i]==idx && wt.weight[i]>tt){ return true; }
            return false;
        }

        private void EnableInputsBone(object sender,EventArgs e){
            int n=lstBone.SelectedItems.Count;
            if(n==0){
                btnBoneAdd.Enabled=chkBoneOW.Enabled=
                rdoBoneAddChild.Enabled=rdoBoneAddSibling.Enabled=rdoBoneDel.Enabled=
                txtNewBone.Enabled=txtBoneNewSibling.Enabled=cmbBoneWeightMoveTarget.Enabled=false;
                txtBaseBone.Text=txtNewBone.Text=lblBoneSCL.Text=cmbBoneWeightMoveTarget.Text="";
            }else{
                btnBoneAdd.Enabled=chkBoneOW.Enabled=rdoBoneAddChild.Enabled=rdoBoneAddSibling.Enabled=true;
                rdoBoneDel.Enabled=(boneCCnt==0);
                txtNewBone.Enabled=rdoBoneAddChild.Checked;
                txtBoneNewSibling.Enabled=rdoBoneAddSibling.Checked;
                cmbBoneWeightMoveTarget.Enabled=(rdoBoneDel.Enabled && rdoBoneDel.Checked);
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
            EnableInputsBone(null,null);

            cmbBoneWeightMoveTarget.Items.Clear();
            if(boneWCnt==0) return;
            if(bone.parent!=null){ // 親あり→親か兄弟が相続候補　
                cmbBoneWeightMoveTarget.Items.Add(bone.parent.name);
                foreach(var b in bone.parent.children)
                    if(b.name!=bone.name) cmbBoneWeightMoveTarget.Items.Add(b.name);
            }else{  // 親なし→トップレベルのボーンが相続候補
                foreach(var b in model.smr.bones)
                    if(b.parent==null && b.name!=bone.name) cmbBoneWeightMoveTarget.Items.Add(b.name);
            }
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
            public ModelFile.Material mate;
            public ModelFile.Prop prop;
            public MateInfo(ModelFile.Material mate,ModelFile.Prop prop){this.mate=mate; this.prop=prop;}
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

    }
}

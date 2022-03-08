using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ModelTweak {
    public partial class Form1:Form {
        public Form1() {
            InitializeComponent();
        }

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

        ModelFile model;

        private void handleInputFileSelected(string fname) {
            if (!fname.EndsWith(".model")) return;
            txtFile.Text = fname;
            lastPath=System.IO.Path.GetDirectoryName(fname);

            model=new ModelFile();
            try{
                model.Read(fname);
            }catch{
                MessageBox.Show("modelファイルが読み込めないか、未対応の形式です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lstMorph.Items.Clear();
            foreach(var mph in model.smr.morph) lstMorph.Items.Add(mph);

            lstTex.Items.Clear();
            foreach(var mat in model.smr.mate) foreach(var prop in mat.props)
                if(prop.texture!=null) lstTex.Items.Add(prop.texture);

            rdoDel.Checked=true;
            EnableInputs();
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

        private void btnMorphExec_Click(object sender,EventArgs e) {
            if(!MorphValidate()) return;
            try{
                string outfile=outFileDialog();
                MorphExec(outfile);
                handleInputFileSelected(txtFile.Text);
            }catch{
                MessageBox.Show("書き込みに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMorphExecOW_Click(object sender,EventArgs e) {
            if(!MorphValidate()) return;
            try{
                string tmpfile=System.IO.Path.GetTempFileName();
                MorphExec(tmpfile);
                System.IO.File.Copy(tmpfile,txtFile.Text,true);
                System.IO.File.Delete(tmpfile);
                handleInputFileSelected(txtFile.Text);
            }catch{
                MessageBox.Show("書き込みに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTexExec_Click(object sender,EventArgs e) {
            if(!TexValidate()) return;
            try{
                string outfile=outFileDialog();
                TexExec(outfile);
                handleInputFileSelected(txtFile.Text);
            }catch{
                MessageBox.Show("書き込みに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnTexExecOW_Click(object sender,EventArgs e) {
            if(!TexValidate()) return;
            try{
                string tmpfile=System.IO.Path.GetTempFileName();
                TexExec(tmpfile);
                System.IO.File.Copy(tmpfile,txtFile.Text,true);
                System.IO.File.Delete(tmpfile);
                handleInputFileSelected(txtFile.Text);
            }catch{
                MessageBox.Show("書き込みに失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CheckMorphName(string name){
            for(int i=0; i<lstMorph.Items.Count; i++)
                if(name==lstMorph.Items[i].ToString()) return false;
            return true;
        }
        private bool MorphValidate(){
            if(rdoRen.Checked){
                if(txtMorphNewName.Text==""){
                    MessageBox.Show("新しい名前が空です", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                if(!CheckMorphName(txtMorphNewName.Text)){
                    MessageBox.Show("その名前は使われています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            if(rdoLR.Checked){
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
            if(rdoDel.Checked){
                ModelFile.Morph morph=(ModelFile.Morph)lstMorph.SelectedItem;
                model.smr.morph.Remove(morph);
            } else if(rdoRen.Checked){
                ModelFile.Morph morph=(ModelFile.Morph)lstMorph.SelectedItem;
                morph.name=txtMorphNewName.Text;
            } else if(rdoLR.Checked){
                ModelFile.Morph morph=(ModelFile.Morph)lstMorph.SelectedItem;
                var l=FilterMorph(morph,leftBone);
                l.name=txtMorphLKey.Text;
                model.smr.morph.Add(l);
                var r=FilterMorph(morph,rightBone);
                r.name=txtMorphRKey.Text;
                model.smr.morph.Add(r);
            }
            model.Write(ofile);
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
        private void TexExec(string ofile){
            if(!float.TryParse(txtTexOffX.Text,out float ox)
            || !float.TryParse(txtTexOffY.Text,out float oy)
            || !float.TryParse(txtTexSclX.Text,out float sx)
            || !float.TryParse(txtTexSclY.Text,out float sy)) return;
            
            var tex=(ModelFile.PropTexture)lstTex.SelectedItem;
            tex.name=txtTex.Text;
            if(tex.name.EndsWith(".tex",StringComparison.Ordinal)) tex.name=tex.name.Substring(0,tex.name.Length-4);
            tex.offsetX=ox; tex.offsetY=oy; tex.scaleX=sx; tex.scaleY=sy;
            model.Write(ofile);
        }

        private void EnableInputs(){ // 冗長だけど面倒なので１箇所に全部書く
            txtMorphNewName.Enabled=rdoRen.Checked;
            txtMorphRKey.Enabled=txtMorphLKey.Enabled=rdoLR.Checked;
            btnMorphExec.Enabled=btnMorphExecOW.Enabled=(lstMorph.SelectedIndex!=-1);
            btnTexExec.Enabled=btnTexExecOW.Enabled=(lstTex.SelectedIndex!=-1);
            if(!btnMorphExec.Enabled){
                txtMorphNewName.Text=txtMorphLKey.Text=txtMorphRKey.Text="";
                txtMorphNewName.Text=txtMorphLKey.Text=txtMorphRKey.Text="";
            }
            if(!btnTexExec.Enabled){
                txtTex.Text=txtTexOffX.Text=txtTexOffY.Text=txtTexSclX.Text=txtTexSclY.Text="";
            }
        }
        private void rdoDel_CheckedChanged(object sender,EventArgs e) { EnableInputs(); }
        private void rdoRen_CheckedChanged(object sender,EventArgs e) { EnableInputs(); }
        private void rdoLR_CheckedChanged(object sender,EventArgs e) { EnableInputs(); }

        private void lstMorph_SelectedIndexChanged(object sender,EventArgs e) {
            EnableInputs();
            if(lstMorph.SelectedIndex==-1) return;
            string name=lstMorph.SelectedItem.ToString();  
            txtMorphNewName.Text=name;
            txtMorphLKey.Text=name+"_l";
            txtMorphRKey.Text=name+"_r";
        }
        private void lstTex_SelectedIndexChanged(object sender,EventArgs e) {
            EnableInputs();
            if(lstTex.SelectedIndex==-1) return;
            var tex=(ModelFile.PropTexture)lstTex.SelectedItem;
            txtTex.Text=tex.name;
            txtTexOffX.Text=tex.offsetX.ToString("G5");
            txtTexOffY.Text=tex.offsetY.ToString("G5");
            txtTexSclX.Text=tex.scaleX.ToString("G5");
            txtTexSclY.Text=tex.scaleY.ToString("G5");
        }
    }
}

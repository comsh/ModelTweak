namespace ModelTweak {
    partial class Form1 {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.btnFileSel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chkMorphOW = new System.Windows.Forms.CheckBox();
            this.btnMorphReverse = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoMorphNoSort = new System.Windows.Forms.RadioButton();
            this.rdoMorphSortDsc = new System.Windows.Forms.RadioButton();
            this.rdoMorphSortAsc = new System.Windows.Forms.RadioButton();
            this.lstMorph = new System.Windows.Forms.ListView();
            this.morph = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtMorphNewName = new System.Windows.Forms.TextBox();
            this.rdoMorphRen = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMorphRKey = new System.Windows.Forms.TextBox();
            this.txtMorphLKey = new System.Windows.Forms.TextBox();
            this.rdoMorphLR = new System.Windows.Forms.RadioButton();
            this.rdoMorphDel = new System.Windows.Forms.RadioButton();
            this.btnMorphExec = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkTexOW = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblMateProp = new System.Windows.Forms.Label();
            this.lblMateShader1 = new System.Windows.Forms.Label();
            this.lblMateName = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lstTex = new System.Windows.Forms.ListView();
            this.tex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTexSclY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnTexExec = new System.Windows.Forms.Button();
            this.txtTexSclX = new System.Windows.Forms.TextBox();
            this.txtTexOffY = new System.Windows.Forms.TextBox();
            this.txtTexOffX = new System.Windows.Forms.TextBox();
            this.txtTex = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lblBoneChildCnt = new System.Windows.Forms.Label();
            this.lblBoneWeightCnt = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.chkBoneOW = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rdoBoneSortChild = new System.Windows.Forms.RadioButton();
            this.rdoBoneSortWeight = new System.Windows.Forms.RadioButton();
            this.rdoBoneSortNormal = new System.Windows.Forms.RadioButton();
            this.rdoBoneDel = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.rdoBoneAddChild = new System.Windows.Forms.RadioButton();
            this.lblBoneSCL = new System.Windows.Forms.Label();
            this.lstBone = new System.Windows.Forms.ListView();
            this.bone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtBaseBone = new System.Windows.Forms.TextBox();
            this.btnBoneAdd = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNewBone = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "ファイル";
            // 
            // txtFile
            // 
            this.txtFile.Enabled = false;
            this.txtFile.Location = new System.Drawing.Point(55, 7);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(401, 19);
            this.txtFile.TabIndex = 1;
            // 
            // btnFileSel
            // 
            this.btnFileSel.Location = new System.Drawing.Point(462, 4);
            this.btnFileSel.Name = "btnFileSel";
            this.btnFileSel.Size = new System.Drawing.Size(56, 23);
            this.btnFileSel.TabIndex = 2;
            this.btnFileSel.Text = "選択...";
            this.btnFileSel.UseVisualStyleBackColor = true;
            this.btnFileSel.Click += new System.EventHandler(this.btnFileSel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(522, 405);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chkMorphOW);
            this.tabPage1.Controls.Add(this.btnMorphReverse);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Controls.Add(this.lstMorph);
            this.tabPage1.Controls.Add(this.txtMorphNewName);
            this.tabPage1.Controls.Add(this.rdoMorphRen);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtMorphRKey);
            this.tabPage1.Controls.Add(this.txtMorphLKey);
            this.tabPage1.Controls.Add(this.rdoMorphLR);
            this.tabPage1.Controls.Add(this.rdoMorphDel);
            this.tabPage1.Controls.Add(this.btnMorphExec);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(514, 379);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "シェイプキー";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chkMorphOW
            // 
            this.chkMorphOW.AutoSize = true;
            this.chkMorphOW.Location = new System.Drawing.Point(264, 330);
            this.chkMorphOW.Name = "chkMorphOW";
            this.chkMorphOW.Size = new System.Drawing.Size(81, 16);
            this.chkMorphOW.TabIndex = 18;
            this.chkMorphOW.Text = "上書き保存";
            this.chkMorphOW.UseVisualStyleBackColor = true;
            // 
            // btnMorphReverse
            // 
            this.btnMorphReverse.Location = new System.Drawing.Point(190, 353);
            this.btnMorphReverse.Margin = new System.Windows.Forms.Padding(2);
            this.btnMorphReverse.Name = "btnMorphReverse";
            this.btnMorphReverse.Size = new System.Drawing.Size(40, 20);
            this.btnMorphReverse.TabIndex = 17;
            this.btnMorphReverse.Text = "反転";
            this.btnMorphReverse.UseVisualStyleBackColor = true;
            this.btnMorphReverse.Click += new System.EventHandler(this.btnMorphReverse_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoMorphNoSort);
            this.panel1.Controls.Add(this.rdoMorphSortDsc);
            this.panel1.Controls.Add(this.rdoMorphSortAsc);
            this.panel1.Location = new System.Drawing.Point(10, 352);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(179, 22);
            this.panel1.TabIndex = 16;
            // 
            // rdoMorphNoSort
            // 
            this.rdoMorphNoSort.AutoSize = true;
            this.rdoMorphNoSort.Checked = true;
            this.rdoMorphNoSort.Location = new System.Drawing.Point(3, 3);
            this.rdoMorphNoSort.Name = "rdoMorphNoSort";
            this.rdoMorphNoSort.Size = new System.Drawing.Size(62, 16);
            this.rdoMorphNoSort.TabIndex = 15;
            this.rdoMorphNoSort.TabStop = true;
            this.rdoMorphNoSort.Text = "ソート無";
            this.rdoMorphNoSort.UseVisualStyleBackColor = true;
            this.rdoMorphNoSort.CheckedChanged += new System.EventHandler(this.rdoMorphNoSort_CheckedChanged);
            // 
            // rdoMorphSortDsc
            // 
            this.rdoMorphSortDsc.AutoSize = true;
            this.rdoMorphSortDsc.Location = new System.Drawing.Point(124, 3);
            this.rdoMorphSortDsc.Name = "rdoMorphSortDsc";
            this.rdoMorphSortDsc.Size = new System.Drawing.Size(47, 16);
            this.rdoMorphSortDsc.TabIndex = 14;
            this.rdoMorphSortDsc.Text = "降順";
            this.rdoMorphSortDsc.UseVisualStyleBackColor = true;
            this.rdoMorphSortDsc.CheckedChanged += new System.EventHandler(this.rdoMorphSortDsc_CheckedChanged);
            // 
            // rdoMorphSortAsc
            // 
            this.rdoMorphSortAsc.AutoSize = true;
            this.rdoMorphSortAsc.Location = new System.Drawing.Point(71, 3);
            this.rdoMorphSortAsc.Name = "rdoMorphSortAsc";
            this.rdoMorphSortAsc.Size = new System.Drawing.Size(47, 16);
            this.rdoMorphSortAsc.TabIndex = 13;
            this.rdoMorphSortAsc.Text = "昇順";
            this.rdoMorphSortAsc.UseVisualStyleBackColor = true;
            this.rdoMorphSortAsc.CheckedChanged += new System.EventHandler(this.rdoMorphSortAsc_CheckedChanged);
            // 
            // lstMorph
            // 
            this.lstMorph.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.morph});
            this.lstMorph.FullRowSelect = true;
            this.lstMorph.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstMorph.HideSelection = false;
            this.lstMorph.Location = new System.Drawing.Point(10, 10);
            this.lstMorph.Name = "lstMorph";
            this.lstMorph.Size = new System.Drawing.Size(220, 339);
            this.lstMorph.TabIndex = 4;
            this.lstMorph.UseCompatibleStateImageBehavior = false;
            this.lstMorph.View = System.Windows.Forms.View.Details;
            this.lstMorph.SelectedIndexChanged += new System.EventHandler(this.lstMorph_SelectedIndexChanged);
            // 
            // morph
            // 
            this.morph.Width = 186;
            // 
            // txtMorphNewName
            // 
            this.txtMorphNewName.Location = new System.Drawing.Point(314, 69);
            this.txtMorphNewName.Name = "txtMorphNewName";
            this.txtMorphNewName.Size = new System.Drawing.Size(180, 19);
            this.txtMorphNewName.TabIndex = 7;
            // 
            // rdoMorphRen
            // 
            this.rdoMorphRen.AutoSize = true;
            this.rdoMorphRen.Location = new System.Drawing.Point(245, 70);
            this.rdoMorphRen.Name = "rdoMorphRen";
            this.rdoMorphRen.Size = new System.Drawing.Size(47, 16);
            this.rdoMorphRen.TabIndex = 6;
            this.rdoMorphRen.Text = "改名";
            this.rdoMorphRen.UseVisualStyleBackColor = true;
            this.rdoMorphRen.CheckedChanged += new System.EventHandler(this.EnableInputsMorph);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(259, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "右キー名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "左キー名";
            // 
            // txtMorphRKey
            // 
            this.txtMorphRKey.Location = new System.Drawing.Point(314, 172);
            this.txtMorphRKey.Name = "txtMorphRKey";
            this.txtMorphRKey.Size = new System.Drawing.Size(180, 19);
            this.txtMorphRKey.TabIndex = 10;
            // 
            // txtMorphLKey
            // 
            this.txtMorphLKey.Location = new System.Drawing.Point(314, 140);
            this.txtMorphLKey.Name = "txtMorphLKey";
            this.txtMorphLKey.Size = new System.Drawing.Size(180, 19);
            this.txtMorphLKey.TabIndex = 9;
            // 
            // rdoMorphLR
            // 
            this.rdoMorphLR.AutoSize = true;
            this.rdoMorphLR.Location = new System.Drawing.Point(245, 113);
            this.rdoMorphLR.Name = "rdoMorphLR";
            this.rdoMorphLR.Size = new System.Drawing.Size(80, 16);
            this.rdoMorphLR.TabIndex = 8;
            this.rdoMorphLR.Text = "左右に分割";
            this.rdoMorphLR.UseVisualStyleBackColor = true;
            this.rdoMorphLR.CheckedChanged += new System.EventHandler(this.EnableInputsMorph);
            // 
            // rdoMorphDel
            // 
            this.rdoMorphDel.AutoSize = true;
            this.rdoMorphDel.Checked = true;
            this.rdoMorphDel.Location = new System.Drawing.Point(245, 25);
            this.rdoMorphDel.Name = "rdoMorphDel";
            this.rdoMorphDel.Size = new System.Drawing.Size(47, 16);
            this.rdoMorphDel.TabIndex = 5;
            this.rdoMorphDel.TabStop = true;
            this.rdoMorphDel.Text = "削除";
            this.rdoMorphDel.UseVisualStyleBackColor = true;
            this.rdoMorphDel.CheckedChanged += new System.EventHandler(this.EnableInputsMorph);
            // 
            // btnMorphExec
            // 
            this.btnMorphExec.Location = new System.Drawing.Point(351, 326);
            this.btnMorphExec.Name = "btnMorphExec";
            this.btnMorphExec.Size = new System.Drawing.Size(143, 23);
            this.btnMorphExec.TabIndex = 11;
            this.btnMorphExec.Text = "実行";
            this.btnMorphExec.UseVisualStyleBackColor = true;
            this.btnMorphExec.Click += new System.EventHandler(this.btnMorphExec_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkTexOW);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.lblMateProp);
            this.tabPage2.Controls.Add(this.lblMateShader1);
            this.tabPage2.Controls.Add(this.lblMateName);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.lstTex);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtTexSclY);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.btnTexExec);
            this.tabPage2.Controls.Add(this.txtTexSclX);
            this.tabPage2.Controls.Add(this.txtTexOffY);
            this.tabPage2.Controls.Add(this.txtTexOffX);
            this.tabPage2.Controls.Add(this.txtTex);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(514, 379);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "texファイル";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkTexOW
            // 
            this.chkTexOW.AutoSize = true;
            this.chkTexOW.Location = new System.Drawing.Point(258, 330);
            this.chkTexOW.Name = "chkTexOW";
            this.chkTexOW.Size = new System.Drawing.Size(81, 16);
            this.chkTexOW.TabIndex = 32;
            this.chkTexOW.Text = "上書き保存";
            this.chkTexOW.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label16.Location = new System.Drawing.Point(245, 85);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 12);
            this.label16.TabIndex = 31;
            this.label16.Text = "プロパティ名";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label15.Location = new System.Drawing.Point(245, 51);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 30;
            this.label15.Text = "シェーダ";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.label14.Location = new System.Drawing.Point(245, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 12);
            this.label14.TabIndex = 29;
            this.label14.Text = "マテリアル名";
            // 
            // lblMateProp
            // 
            this.lblMateProp.AutoSize = true;
            this.lblMateProp.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.lblMateProp.Location = new System.Drawing.Point(313, 85);
            this.lblMateProp.Name = "lblMateProp";
            this.lblMateProp.Size = new System.Drawing.Size(0, 12);
            this.lblMateProp.TabIndex = 28;
            // 
            // lblMateShader1
            // 
            this.lblMateShader1.AutoSize = true;
            this.lblMateShader1.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.lblMateShader1.Location = new System.Drawing.Point(313, 51);
            this.lblMateShader1.Name = "lblMateShader1";
            this.lblMateShader1.Size = new System.Drawing.Size(0, 12);
            this.lblMateShader1.TabIndex = 27;
            // 
            // lblMateName
            // 
            this.lblMateName.AutoSize = true;
            this.lblMateName.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.lblMateName.Location = new System.Drawing.Point(313, 25);
            this.lblMateName.Name = "lblMateName";
            this.lblMateName.Size = new System.Drawing.Size(0, 12);
            this.lblMateName.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(245, 147);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 12);
            this.label13.TabIndex = 25;
            this.label13.Text = "オフセット";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(245, 176);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 12);
            this.label10.TabIndex = 24;
            this.label10.Text = "スケール";
            // 
            // lstTex
            // 
            this.lstTex.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.tex});
            this.lstTex.FullRowSelect = true;
            this.lstTex.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstTex.HideSelection = false;
            this.lstTex.Location = new System.Drawing.Point(9, 9);
            this.lstTex.MultiSelect = false;
            this.lstTex.Name = "lstTex";
            this.lstTex.Size = new System.Drawing.Size(220, 364);
            this.lstTex.TabIndex = 14;
            this.lstTex.UseCompatibleStateImageBehavior = false;
            this.lstTex.View = System.Windows.Forms.View.Details;
            this.lstTex.SelectedIndexChanged += new System.EventHandler(this.lstTex_SelectedIndexChanged);
            // 
            // tex
            // 
            this.tex.Width = 162;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(476, 116);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = ".tex";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(393, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "Y";
            // 
            // txtTexSclY
            // 
            this.txtTexSclY.Location = new System.Drawing.Point(408, 173);
            this.txtTexSclY.Name = "txtTexSclY";
            this.txtTexSclY.Size = new System.Drawing.Size(68, 19);
            this.txtTexSclY.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(300, 176);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(12, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(393, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(300, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(245, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "ファイル名";
            // 
            // btnTexExec
            // 
            this.btnTexExec.Location = new System.Drawing.Point(345, 326);
            this.btnTexExec.Name = "btnTexExec";
            this.btnTexExec.Size = new System.Drawing.Size(143, 23);
            this.btnTexExec.TabIndex = 20;
            this.btnTexExec.Text = "変更";
            this.btnTexExec.UseVisualStyleBackColor = true;
            this.btnTexExec.Click += new System.EventHandler(this.btnTexExec_Click);
            // 
            // txtTexSclX
            // 
            this.txtTexSclX.Location = new System.Drawing.Point(313, 173);
            this.txtTexSclX.Name = "txtTexSclX";
            this.txtTexSclX.Size = new System.Drawing.Size(68, 19);
            this.txtTexSclX.TabIndex = 18;
            // 
            // txtTexOffY
            // 
            this.txtTexOffY.Location = new System.Drawing.Point(408, 144);
            this.txtTexOffY.Name = "txtTexOffY";
            this.txtTexOffY.Size = new System.Drawing.Size(68, 19);
            this.txtTexOffY.TabIndex = 17;
            // 
            // txtTexOffX
            // 
            this.txtTexOffX.Location = new System.Drawing.Point(313, 144);
            this.txtTexOffX.Name = "txtTexOffX";
            this.txtTexOffX.Size = new System.Drawing.Size(68, 19);
            this.txtTexOffX.TabIndex = 16;
            // 
            // txtTex
            // 
            this.txtTex.Location = new System.Drawing.Point(313, 112);
            this.txtTex.Name = "txtTex";
            this.txtTex.Size = new System.Drawing.Size(163, 19);
            this.txtTex.TabIndex = 15;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label17);
            this.tabPage3.Controls.Add(this.lblBoneChildCnt);
            this.tabPage3.Controls.Add(this.lblBoneWeightCnt);
            this.tabPage3.Controls.Add(this.label21);
            this.tabPage3.Controls.Add(this.label20);
            this.tabPage3.Controls.Add(this.chkBoneOW);
            this.tabPage3.Controls.Add(this.panel2);
            this.tabPage3.Controls.Add(this.rdoBoneDel);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.rdoBoneAddChild);
            this.tabPage3.Controls.Add(this.lblBoneSCL);
            this.tabPage3.Controls.Add(this.lstBone);
            this.tabPage3.Controls.Add(this.txtBaseBone);
            this.tabPage3.Controls.Add(this.btnBoneAdd);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.txtNewBone);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(514, 379);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "ボーン";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lblBoneChildCnt
            // 
            this.lblBoneChildCnt.Location = new System.Drawing.Point(430, 62);
            this.lblBoneChildCnt.Name = "lblBoneChildCnt";
            this.lblBoneChildCnt.Size = new System.Drawing.Size(58, 12);
            this.lblBoneChildCnt.TabIndex = 46;
            this.lblBoneChildCnt.Text = "0";
            this.lblBoneChildCnt.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblBoneWeightCnt
            // 
            this.lblBoneWeightCnt.Location = new System.Drawing.Point(430, 44);
            this.lblBoneWeightCnt.Name = "lblBoneWeightCnt";
            this.lblBoneWeightCnt.Size = new System.Drawing.Size(58, 15);
            this.lblBoneWeightCnt.TabIndex = 45;
            this.lblBoneWeightCnt.Text = "0";
            this.lblBoneWeightCnt.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(374, 46);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(50, 12);
            this.label21.TabIndex = 44;
            this.label21.Text = "ウェイト数";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(374, 61);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(58, 12);
            this.label20.TabIndex = 43;
            this.label20.Text = "子ボーン数";
            // 
            // chkBoneOW
            // 
            this.chkBoneOW.AutoSize = true;
            this.chkBoneOW.Location = new System.Drawing.Point(258, 330);
            this.chkBoneOW.Name = "chkBoneOW";
            this.chkBoneOW.Size = new System.Drawing.Size(81, 16);
            this.chkBoneOW.TabIndex = 39;
            this.chkBoneOW.Text = "上書き保存";
            this.chkBoneOW.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rdoBoneSortChild);
            this.panel2.Controls.Add(this.rdoBoneSortWeight);
            this.panel2.Controls.Add(this.rdoBoneSortNormal);
            this.panel2.Location = new System.Drawing.Point(9, 353);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 21);
            this.panel2.TabIndex = 38;
            // 
            // rdoBoneSortChild
            // 
            this.rdoBoneSortChild.AutoSize = true;
            this.rdoBoneSortChild.Location = new System.Drawing.Point(147, 2);
            this.rdoBoneSortChild.Name = "rdoBoneSortChild";
            this.rdoBoneSortChild.Size = new System.Drawing.Size(80, 16);
            this.rdoBoneSortChild.TabIndex = 2;
            this.rdoBoneSortChild.TabStop = true;
            this.rdoBoneSortChild.Text = "子無し優先";
            this.rdoBoneSortChild.UseVisualStyleBackColor = true;
            this.rdoBoneSortChild.CheckedChanged += new System.EventHandler(this.rdoBoneSortChild_CheckedChanged);
            // 
            // rdoBoneSortWeight
            // 
            this.rdoBoneSortWeight.AutoSize = true;
            this.rdoBoneSortWeight.Location = new System.Drawing.Point(51, 2);
            this.rdoBoneSortWeight.Name = "rdoBoneSortWeight";
            this.rdoBoneSortWeight.Size = new System.Drawing.Size(92, 16);
            this.rdoBoneSortWeight.TabIndex = 1;
            this.rdoBoneSortWeight.TabStop = true;
            this.rdoBoneSortWeight.Text = "ウェイト持優先";
            this.rdoBoneSortWeight.UseVisualStyleBackColor = true;
            this.rdoBoneSortWeight.CheckedChanged += new System.EventHandler(this.rdoBoneSortWeight_CheckedChanged);
            // 
            // rdoBoneSortNormal
            // 
            this.rdoBoneSortNormal.AutoSize = true;
            this.rdoBoneSortNormal.Checked = true;
            this.rdoBoneSortNormal.Location = new System.Drawing.Point(3, 2);
            this.rdoBoneSortNormal.Name = "rdoBoneSortNormal";
            this.rdoBoneSortNormal.Size = new System.Drawing.Size(47, 16);
            this.rdoBoneSortNormal.TabIndex = 0;
            this.rdoBoneSortNormal.TabStop = true;
            this.rdoBoneSortNormal.Text = "通常";
            this.rdoBoneSortNormal.UseVisualStyleBackColor = true;
            this.rdoBoneSortNormal.CheckedChanged += new System.EventHandler(this.rdoBoneSortNormal_CheckedChanged);
            // 
            // rdoBoneDel
            // 
            this.rdoBoneDel.AutoSize = true;
            this.rdoBoneDel.Location = new System.Drawing.Point(247, 172);
            this.rdoBoneDel.Name = "rdoBoneDel";
            this.rdoBoneDel.Size = new System.Drawing.Size(198, 16);
            this.rdoBoneDel.TabIndex = 37;
            this.rdoBoneDel.TabStop = true;
            this.rdoBoneDel.Text = "ボーン削除(子のいないボーンのみ可)";
            this.rdoBoneDel.UseVisualStyleBackColor = true;
            this.rdoBoneDel.CheckedChanged += new System.EventHandler(this.EnableInputsBone);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(261, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 12);
            this.label12.TabIndex = 33;
            this.label12.Text = "ボーン名";
            // 
            // rdoBoneAddChild
            // 
            this.rdoBoneAddChild.AutoSize = true;
            this.rdoBoneAddChild.Checked = true;
            this.rdoBoneAddChild.Location = new System.Drawing.Point(247, 98);
            this.rdoBoneAddChild.Name = "rdoBoneAddChild";
            this.rdoBoneAddChild.Size = new System.Drawing.Size(208, 16);
            this.rdoBoneAddChild.TabIndex = 30;
            this.rdoBoneAddChild.TabStop = true;
            this.rdoBoneAddChild.Text = "子ボーンを追加してウェイトをすべて渡す";
            this.rdoBoneAddChild.UseVisualStyleBackColor = true;
            this.rdoBoneAddChild.CheckedChanged += new System.EventHandler(this.EnableInputsBone);
            // 
            // lblBoneSCL
            // 
            this.lblBoneSCL.AutoSize = true;
            this.lblBoneSCL.Location = new System.Drawing.Point(308, 47);
            this.lblBoneSCL.Name = "lblBoneSCL";
            this.lblBoneSCL.Size = new System.Drawing.Size(0, 12);
            this.lblBoneSCL.TabIndex = 29;
            // 
            // lstBone
            // 
            this.lstBone.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.bone});
            this.lstBone.FullRowSelect = true;
            this.lstBone.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstBone.HideSelection = false;
            this.lstBone.Location = new System.Drawing.Point(9, 9);
            this.lstBone.MultiSelect = false;
            this.lstBone.Name = "lstBone";
            this.lstBone.Size = new System.Drawing.Size(220, 340);
            this.lstBone.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstBone.TabIndex = 23;
            this.lstBone.UseCompatibleStateImageBehavior = false;
            this.lstBone.View = System.Windows.Forms.View.Details;
            this.lstBone.SelectedIndexChanged += new System.EventHandler(this.lstBone_SelectedIndexChanged);
            // 
            // bone
            // 
            this.bone.Width = 186;
            // 
            // txtBaseBone
            // 
            this.txtBaseBone.Location = new System.Drawing.Point(309, 22);
            this.txtBaseBone.Name = "txtBaseBone";
            this.txtBaseBone.ReadOnly = true;
            this.txtBaseBone.Size = new System.Drawing.Size(179, 19);
            this.txtBaseBone.TabIndex = 24;
            // 
            // btnBoneAdd
            // 
            this.btnBoneAdd.Location = new System.Drawing.Point(345, 326);
            this.btnBoneAdd.Name = "btnBoneAdd";
            this.btnBoneAdd.Size = new System.Drawing.Size(143, 23);
            this.btnBoneAdd.TabIndex = 27;
            this.btnBoneAdd.Text = "実行";
            this.btnBoneAdd.UseVisualStyleBackColor = true;
            this.btnBoneAdd.Click += new System.EventHandler(this.btnBoneAdd_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(245, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "選択ボーン";
            // 
            // txtNewBone
            // 
            this.txtNewBone.Location = new System.Drawing.Point(309, 120);
            this.txtNewBone.Name = "txtNewBone";
            this.txtNewBone.Size = new System.Drawing.Size(179, 19);
            this.txtNewBone.TabIndex = 25;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(267, 193);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(199, 12);
            this.label17.TabIndex = 47;
            this.label17.Text = "※(あれば)ウェイトはすべて親に渡されます";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 441);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnFileSel);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(540, 480);
            this.MinimumSize = new System.Drawing.Size(540, 480);
            this.Name = "Form1";
            this.Text = "ModelTweak";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button btnFileSel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtMorphNewName;
        private System.Windows.Forms.RadioButton rdoMorphRen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMorphRKey;
        private System.Windows.Forms.TextBox txtMorphLKey;
        private System.Windows.Forms.RadioButton rdoMorphLR;
        private System.Windows.Forms.RadioButton rdoMorphDel;
        private System.Windows.Forms.Button btnMorphExec;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTexSclY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTexExec;
        private System.Windows.Forms.TextBox txtTexSclX;
        private System.Windows.Forms.TextBox txtTexOffY;
        private System.Windows.Forms.TextBox txtTexOffX;
        private System.Windows.Forms.TextBox txtTex;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtBaseBone;
        private System.Windows.Forms.Button btnBoneAdd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNewBone;
        private System.Windows.Forms.ListView lstBone;
        private System.Windows.Forms.ColumnHeader bone;
        private System.Windows.Forms.ListView lstMorph;
        private System.Windows.Forms.ColumnHeader morph;
        private System.Windows.Forms.ListView lstTex;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblMateProp;
        private System.Windows.Forms.Label lblMateShader1;
        private System.Windows.Forms.Label lblMateName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ColumnHeader tex;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoMorphNoSort;
        private System.Windows.Forms.RadioButton rdoMorphSortDsc;
        private System.Windows.Forms.RadioButton rdoMorphSortAsc;
        private System.Windows.Forms.Button btnMorphReverse;
        private System.Windows.Forms.Label lblBoneSCL;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rdoBoneAddChild;
        private System.Windows.Forms.RadioButton rdoBoneDel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rdoBoneSortChild;
        private System.Windows.Forms.RadioButton rdoBoneSortWeight;
        private System.Windows.Forms.RadioButton rdoBoneSortNormal;
        private System.Windows.Forms.CheckBox chkMorphOW;
        private System.Windows.Forms.CheckBox chkTexOW;
        private System.Windows.Forms.CheckBox chkBoneOW;
        private System.Windows.Forms.Label lblBoneChildCnt;
        private System.Windows.Forms.Label lblBoneWeightCnt;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label17;
    }
}


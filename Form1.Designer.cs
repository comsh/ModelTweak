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
            this.txtMorphNewName = new System.Windows.Forms.TextBox();
            this.rdoRen = new System.Windows.Forms.RadioButton();
            this.btnMorphExecOW = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMorphRKey = new System.Windows.Forms.TextBox();
            this.txtMorphLKey = new System.Windows.Forms.TextBox();
            this.rdoLR = new System.Windows.Forms.RadioButton();
            this.rdoDel = new System.Windows.Forms.RadioButton();
            this.btnMorphExec = new System.Windows.Forms.Button();
            this.lstMorph = new System.Windows.Forms.ListBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTexExecOW = new System.Windows.Forms.Button();
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
            this.lstTex = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
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
            this.txtFile.Size = new System.Drawing.Size(305, 19);
            this.txtFile.TabIndex = 1;
            // 
            // btnFileSel
            // 
            this.btnFileSel.Location = new System.Drawing.Point(366, 5);
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
            this.tabControl1.Location = new System.Drawing.Point(0, 34);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(433, 375);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtMorphNewName);
            this.tabPage1.Controls.Add(this.rdoRen);
            this.tabPage1.Controls.Add(this.btnMorphExecOW);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtMorphRKey);
            this.tabPage1.Controls.Add(this.txtMorphLKey);
            this.tabPage1.Controls.Add(this.rdoLR);
            this.tabPage1.Controls.Add(this.rdoDel);
            this.tabPage1.Controls.Add(this.btnMorphExec);
            this.tabPage1.Controls.Add(this.lstMorph);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(425, 349);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "シェイプキー";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtMorphNewName
            // 
            this.txtMorphNewName.Location = new System.Drawing.Point(259, 61);
            this.txtMorphNewName.Name = "txtMorphNewName";
            this.txtMorphNewName.Size = new System.Drawing.Size(147, 19);
            this.txtMorphNewName.TabIndex = 7;
            // 
            // rdoRen
            // 
            this.rdoRen.AutoSize = true;
            this.rdoRen.Location = new System.Drawing.Point(184, 63);
            this.rdoRen.Name = "rdoRen";
            this.rdoRen.Size = new System.Drawing.Size(47, 16);
            this.rdoRen.TabIndex = 6;
            this.rdoRen.Text = "改名";
            this.rdoRen.UseVisualStyleBackColor = true;
            this.rdoRen.CheckedChanged += new System.EventHandler(this.rdoRen_CheckedChanged);
            // 
            // btnMorphExecOW
            // 
            this.btnMorphExecOW.Location = new System.Drawing.Point(218, 293);
            this.btnMorphExecOW.Name = "btnMorphExecOW";
            this.btnMorphExecOW.Size = new System.Drawing.Size(138, 23);
            this.btnMorphExecOW.TabIndex = 12;
            this.btnMorphExecOW.Text = "実行(上書き)";
            this.btnMorphExecOW.UseVisualStyleBackColor = true;
            this.btnMorphExecOW.Click += new System.EventHandler(this.btnMorphExecOW_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(193, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "右側キー名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(193, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "左側キー名";
            // 
            // txtMorphRKey
            // 
            this.txtMorphRKey.Location = new System.Drawing.Point(260, 170);
            this.txtMorphRKey.Name = "txtMorphRKey";
            this.txtMorphRKey.Size = new System.Drawing.Size(146, 19);
            this.txtMorphRKey.TabIndex = 10;
            // 
            // txtMorphLKey
            // 
            this.txtMorphLKey.Location = new System.Drawing.Point(260, 133);
            this.txtMorphLKey.Name = "txtMorphLKey";
            this.txtMorphLKey.Size = new System.Drawing.Size(146, 19);
            this.txtMorphLKey.TabIndex = 9;
            // 
            // rdoLR
            // 
            this.rdoLR.AutoSize = true;
            this.rdoLR.Location = new System.Drawing.Point(184, 98);
            this.rdoLR.Name = "rdoLR";
            this.rdoLR.Size = new System.Drawing.Size(80, 16);
            this.rdoLR.TabIndex = 8;
            this.rdoLR.Text = "左右に分割";
            this.rdoLR.UseVisualStyleBackColor = true;
            this.rdoLR.CheckedChanged += new System.EventHandler(this.rdoLR_CheckedChanged);
            // 
            // rdoDel
            // 
            this.rdoDel.AutoSize = true;
            this.rdoDel.Checked = true;
            this.rdoDel.Location = new System.Drawing.Point(184, 30);
            this.rdoDel.Name = "rdoDel";
            this.rdoDel.Size = new System.Drawing.Size(47, 16);
            this.rdoDel.TabIndex = 5;
            this.rdoDel.TabStop = true;
            this.rdoDel.Text = "削除";
            this.rdoDel.UseVisualStyleBackColor = true;
            this.rdoDel.CheckedChanged += new System.EventHandler(this.rdoDel_CheckedChanged);
            // 
            // btnMorphExec
            // 
            this.btnMorphExec.Location = new System.Drawing.Point(218, 242);
            this.btnMorphExec.Name = "btnMorphExec";
            this.btnMorphExec.Size = new System.Drawing.Size(138, 23);
            this.btnMorphExec.TabIndex = 11;
            this.btnMorphExec.Text = "実行(別名保存)";
            this.btnMorphExec.UseVisualStyleBackColor = true;
            this.btnMorphExec.Click += new System.EventHandler(this.btnMorphExec_Click);
            // 
            // lstMorph
            // 
            this.lstMorph.FormattingEnabled = true;
            this.lstMorph.HorizontalScrollbar = true;
            this.lstMorph.ItemHeight = 12;
            this.lstMorph.Location = new System.Drawing.Point(10, 12);
            this.lstMorph.Name = "lstMorph";
            this.lstMorph.Size = new System.Drawing.Size(150, 328);
            this.lstMorph.TabIndex = 4;
            this.lstMorph.SelectedIndexChanged += new System.EventHandler(this.lstMorph_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.btnTexExecOW);
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
            this.tabPage2.Controls.Add(this.lstTex);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(425, 349);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "texファイル";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnTexExecOW
            // 
            this.btnTexExecOW.Location = new System.Drawing.Point(233, 294);
            this.btnTexExecOW.Name = "btnTexExecOW";
            this.btnTexExecOW.Size = new System.Drawing.Size(133, 23);
            this.btnTexExecOW.TabIndex = 21;
            this.btnTexExecOW.Text = "変更(上書き)";
            this.btnTexExecOW.UseVisualStyleBackColor = true;
            this.btnTexExecOW.Click += new System.EventHandler(this.btnTexExecOW_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(194, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 12);
            this.label8.TabIndex = 11;
            this.label8.Text = "スケールY";
            // 
            // txtTexSclY
            // 
            this.txtTexSclY.Location = new System.Drawing.Point(251, 173);
            this.txtTexSclY.Name = "txtTexSclY";
            this.txtTexSclY.Size = new System.Drawing.Size(137, 19);
            this.txtTexSclY.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(194, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "スケールX";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(194, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "オフセットY";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "オフセットX";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "ファイル名";
            // 
            // btnTexExec
            // 
            this.btnTexExec.Location = new System.Drawing.Point(233, 243);
            this.btnTexExec.Name = "btnTexExec";
            this.btnTexExec.Size = new System.Drawing.Size(133, 23);
            this.btnTexExec.TabIndex = 20;
            this.btnTexExec.Text = "変更(別名保存)";
            this.btnTexExec.UseVisualStyleBackColor = true;
            this.btnTexExec.Click += new System.EventHandler(this.btnTexExec_Click);
            // 
            // txtTexSclX
            // 
            this.txtTexSclX.Location = new System.Drawing.Point(251, 139);
            this.txtTexSclX.Name = "txtTexSclX";
            this.txtTexSclX.Size = new System.Drawing.Size(137, 19);
            this.txtTexSclX.TabIndex = 18;
            // 
            // txtTexOffY
            // 
            this.txtTexOffY.Location = new System.Drawing.Point(251, 105);
            this.txtTexOffY.Name = "txtTexOffY";
            this.txtTexOffY.Size = new System.Drawing.Size(137, 19);
            this.txtTexOffY.TabIndex = 17;
            // 
            // txtTexOffX
            // 
            this.txtTexOffX.Location = new System.Drawing.Point(251, 71);
            this.txtTexOffX.Name = "txtTexOffX";
            this.txtTexOffX.Size = new System.Drawing.Size(137, 19);
            this.txtTexOffX.TabIndex = 16;
            // 
            // txtTex
            // 
            this.txtTex.Location = new System.Drawing.Point(251, 30);
            this.txtTex.Name = "txtTex";
            this.txtTex.Size = new System.Drawing.Size(137, 19);
            this.txtTex.TabIndex = 15;
            // 
            // lstTex
            // 
            this.lstTex.FormattingEnabled = true;
            this.lstTex.HorizontalScrollbar = true;
            this.lstTex.ItemHeight = 12;
            this.lstTex.Location = new System.Drawing.Point(9, 12);
            this.lstTex.Name = "lstTex";
            this.lstTex.Size = new System.Drawing.Size(172, 328);
            this.lstTex.TabIndex = 14;
            this.lstTex.SelectedIndexChanged += new System.EventHandler(this.lstTex_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(390, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = ".tex";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnFileSel);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(450, 450);
            this.MinimumSize = new System.Drawing.Size(450, 450);
            this.Name = "Form1";
            this.Text = "ModelTweak";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
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
        private System.Windows.Forms.RadioButton rdoRen;
        private System.Windows.Forms.Button btnMorphExecOW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMorphRKey;
        private System.Windows.Forms.TextBox txtMorphLKey;
        private System.Windows.Forms.RadioButton rdoLR;
        private System.Windows.Forms.RadioButton rdoDel;
        private System.Windows.Forms.Button btnMorphExec;
        private System.Windows.Forms.ListBox lstMorph;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnTexExecOW;
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
        private System.Windows.Forms.ListBox lstTex;
        private System.Windows.Forms.Label label9;
    }
}


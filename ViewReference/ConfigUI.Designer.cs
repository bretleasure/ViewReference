namespace ViewReference
{
    partial class ConfigUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigUI));
            this.btn_ViewName = new System.Windows.Forms.Button();
            this.btn_ViewSheetNumber = new System.Windows.Forms.Button();
            this.btn_ViewSheetName = new System.Windows.Forms.Button();
            this.btn_ParentSheetNumber = new System.Windows.Forms.Button();
            this.btn_ParentSheetName = new System.Windows.Forms.Button();
            this.btn_LineBreak = new System.Windows.Forms.Button();
            this.btn_Delim = new System.Windows.Forms.Button();
            this.btn_Scale = new System.Windows.Forms.Button();
            this.ckb_Detail = new System.Windows.Forms.CheckBox();
            this.ckb_Section = new System.Windows.Forms.CheckBox();
            this.ckb_Aux = new System.Windows.Forms.CheckBox();
            this.ckb_Projected = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txb_CalloutStyle = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_SaveSettings = new System.Windows.Forms.Button();
            this.btn_Callout4 = new System.Windows.Forms.Button();
            this.btn_Callout3 = new System.Windows.Forms.Button();
            this.btn_Callout2 = new System.Windows.Forms.Button();
            this.btn_Callout1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.pnl_DetailStyle = new System.Windows.Forms.Panel();
            this.txb_DetailStyle = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_AuxStyle = new System.Windows.Forms.Panel();
            this.txb_AuxStyle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnl_SectionStyle = new System.Windows.Forms.Panel();
            this.txb_SectionStyle = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnl_ProjectedStyle = new System.Windows.Forms.Panel();
            this.txb_ProjectedStyle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_Label3 = new System.Windows.Forms.Button();
            this.btn_Label2 = new System.Windows.Forms.Button();
            this.btn_Label1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.ckb_UpdateBeforeSave = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.pnl_DetailStyle.SuspendLayout();
            this.pnl_AuxStyle.SuspendLayout();
            this.pnl_SectionStyle.SuspendLayout();
            this.pnl_ProjectedStyle.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ViewName
            // 
            this.btn_ViewName.Location = new System.Drawing.Point(21, 292);
            this.btn_ViewName.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ViewName.Name = "btn_ViewName";
            this.btn_ViewName.Size = new System.Drawing.Size(200, 31);
            this.btn_ViewName.TabIndex = 0;
            this.btn_ViewName.TabStop = false;
            this.btn_ViewName.Text = "<VIEW>";
            this.btn_ViewName.UseVisualStyleBackColor = true;
            this.btn_ViewName.Click += new System.EventHandler(this.Add_AttributeToTextBox);
            // 
            // btn_ViewSheetNumber
            // 
            this.btn_ViewSheetNumber.Location = new System.Drawing.Point(21, 330);
            this.btn_ViewSheetNumber.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ViewSheetNumber.Name = "btn_ViewSheetNumber";
            this.btn_ViewSheetNumber.Size = new System.Drawing.Size(200, 31);
            this.btn_ViewSheetNumber.TabIndex = 1;
            this.btn_ViewSheetNumber.TabStop = false;
            this.btn_ViewSheetNumber.Text = "<VIEW SHEET #>";
            this.btn_ViewSheetNumber.UseVisualStyleBackColor = true;
            this.btn_ViewSheetNumber.Click += new System.EventHandler(this.Add_AttributeToTextBox);
            // 
            // btn_ViewSheetName
            // 
            this.btn_ViewSheetName.Location = new System.Drawing.Point(21, 368);
            this.btn_ViewSheetName.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ViewSheetName.Name = "btn_ViewSheetName";
            this.btn_ViewSheetName.Size = new System.Drawing.Size(200, 31);
            this.btn_ViewSheetName.TabIndex = 2;
            this.btn_ViewSheetName.TabStop = false;
            this.btn_ViewSheetName.Text = "<VIEW SHEET NAME>";
            this.btn_ViewSheetName.UseVisualStyleBackColor = true;
            this.btn_ViewSheetName.Click += new System.EventHandler(this.Add_AttributeToTextBox);
            // 
            // btn_ParentSheetNumber
            // 
            this.btn_ParentSheetNumber.Location = new System.Drawing.Point(21, 406);
            this.btn_ParentSheetNumber.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ParentSheetNumber.Name = "btn_ParentSheetNumber";
            this.btn_ParentSheetNumber.Size = new System.Drawing.Size(200, 31);
            this.btn_ParentSheetNumber.TabIndex = 3;
            this.btn_ParentSheetNumber.TabStop = false;
            this.btn_ParentSheetNumber.Text = "<PARENT SHEET #>";
            this.btn_ParentSheetNumber.UseVisualStyleBackColor = true;
            this.btn_ParentSheetNumber.Click += new System.EventHandler(this.Add_AttributeToTextBox);
            // 
            // btn_ParentSheetName
            // 
            this.btn_ParentSheetName.Location = new System.Drawing.Point(21, 444);
            this.btn_ParentSheetName.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ParentSheetName.Name = "btn_ParentSheetName";
            this.btn_ParentSheetName.Size = new System.Drawing.Size(200, 31);
            this.btn_ParentSheetName.TabIndex = 4;
            this.btn_ParentSheetName.TabStop = false;
            this.btn_ParentSheetName.Text = "<PARENT SHEET NAME>";
            this.btn_ParentSheetName.UseVisualStyleBackColor = true;
            this.btn_ParentSheetName.Click += new System.EventHandler(this.Add_AttributeToTextBox);
            // 
            // btn_LineBreak
            // 
            this.btn_LineBreak.Location = new System.Drawing.Point(21, 521);
            this.btn_LineBreak.Margin = new System.Windows.Forms.Padding(4);
            this.btn_LineBreak.Name = "btn_LineBreak";
            this.btn_LineBreak.Size = new System.Drawing.Size(200, 31);
            this.btn_LineBreak.TabIndex = 5;
            this.btn_LineBreak.TabStop = false;
            this.btn_LineBreak.Text = "<Br/>";
            this.btn_LineBreak.UseVisualStyleBackColor = true;
            this.btn_LineBreak.Click += new System.EventHandler(this.Add_AttributeToTextBox);
            // 
            // btn_Delim
            // 
            this.btn_Delim.Location = new System.Drawing.Point(21, 559);
            this.btn_Delim.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Delim.Name = "btn_Delim";
            this.btn_Delim.Size = new System.Drawing.Size(200, 31);
            this.btn_Delim.TabIndex = 6;
            this.btn_Delim.TabStop = false;
            this.btn_Delim.Text = "<DELIM>";
            this.btn_Delim.UseVisualStyleBackColor = true;
            this.btn_Delim.Click += new System.EventHandler(this.Add_AttributeToTextBox);
            // 
            // btn_Scale
            // 
            this.btn_Scale.Location = new System.Drawing.Point(21, 482);
            this.btn_Scale.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Scale.Name = "btn_Scale";
            this.btn_Scale.Size = new System.Drawing.Size(200, 31);
            this.btn_Scale.TabIndex = 7;
            this.btn_Scale.TabStop = false;
            this.btn_Scale.Text = "<SCALE>";
            this.btn_Scale.UseVisualStyleBackColor = true;
            this.btn_Scale.Click += new System.EventHandler(this.Add_AttributeToTextBox);
            // 
            // ckb_Detail
            // 
            this.ckb_Detail.AutoSize = true;
            this.ckb_Detail.Location = new System.Drawing.Point(4, 4);
            this.ckb_Detail.Margin = new System.Windows.Forms.Padding(4);
            this.ckb_Detail.Name = "ckb_Detail";
            this.ckb_Detail.Size = new System.Drawing.Size(101, 20);
            this.ckb_Detail.TabIndex = 8;
            this.ckb_Detail.Text = "Detail Views";
            this.ckb_Detail.UseVisualStyleBackColor = true;
            this.ckb_Detail.CheckedChanged += new System.EventHandler(this.ckb_Detail_CheckedChanged);
            // 
            // ckb_Section
            // 
            this.ckb_Section.AutoSize = true;
            this.ckb_Section.Location = new System.Drawing.Point(4, 32);
            this.ckb_Section.Margin = new System.Windows.Forms.Padding(4);
            this.ckb_Section.Name = "ckb_Section";
            this.ckb_Section.Size = new System.Drawing.Size(111, 20);
            this.ckb_Section.TabIndex = 9;
            this.ckb_Section.Text = "Section Views";
            this.ckb_Section.UseVisualStyleBackColor = true;
            this.ckb_Section.CheckedChanged += new System.EventHandler(this.ckb_Section_CheckedChanged);
            // 
            // ckb_Aux
            // 
            this.ckb_Aux.AutoSize = true;
            this.ckb_Aux.Location = new System.Drawing.Point(4, 60);
            this.ckb_Aux.Margin = new System.Windows.Forms.Padding(4);
            this.ckb_Aux.Name = "ckb_Aux";
            this.ckb_Aux.Size = new System.Drawing.Size(116, 20);
            this.ckb_Aux.TabIndex = 10;
            this.ckb_Aux.Text = "Auxiliary Views";
            this.ckb_Aux.UseVisualStyleBackColor = true;
            this.ckb_Aux.CheckedChanged += new System.EventHandler(this.ckb_Aux_CheckedChanged);
            // 
            // ckb_Projected
            // 
            this.ckb_Projected.AutoSize = true;
            this.ckb_Projected.Location = new System.Drawing.Point(4, 89);
            this.ckb_Projected.Margin = new System.Windows.Forms.Padding(4);
            this.ckb_Projected.Name = "ckb_Projected";
            this.ckb_Projected.Size = new System.Drawing.Size(124, 20);
            this.ckb_Projected.TabIndex = 14;
            this.ckb_Projected.Text = "Projected Views";
            this.ckb_Projected.UseVisualStyleBackColor = true;
            this.ckb_Projected.CheckedChanged += new System.EventHandler(this.ckb_Projected_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Add References To:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ckb_Detail);
            this.panel1.Controls.Add(this.ckb_Section);
            this.panel1.Controls.Add(this.ckb_Projected);
            this.panel1.Controls.Add(this.ckb_Aux);
            this.panel1.Location = new System.Drawing.Point(21, 94);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 128);
            this.panel1.TabIndex = 17;
            // 
            // txb_CalloutStyle
            // 
            this.txb_CalloutStyle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_CalloutStyle.Location = new System.Drawing.Point(16, 85);
            this.txb_CalloutStyle.Margin = new System.Windows.Forms.Padding(4);
            this.txb_CalloutStyle.Name = "txb_CalloutStyle";
            this.txb_CalloutStyle.Size = new System.Drawing.Size(799, 22);
            this.txb_CalloutStyle.TabIndex = 19;
            this.txb_CalloutStyle.Click += new System.EventHandler(this.Set_ActiveTextbox);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.Location = new System.Drawing.Point(912, 604);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(177, 28);
            this.btn_Cancel.TabIndex = 20;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_SaveSettings
            // 
            this.btn_SaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SaveSettings.Location = new System.Drawing.Point(727, 604);
            this.btn_SaveSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btn_SaveSettings.Name = "btn_SaveSettings";
            this.btn_SaveSettings.Size = new System.Drawing.Size(177, 28);
            this.btn_SaveSettings.TabIndex = 21;
            this.btn_SaveSettings.Text = "Save Settings";
            this.btn_SaveSettings.UseVisualStyleBackColor = true;
            this.btn_SaveSettings.Click += new System.EventHandler(this.btn_SaveSettings_Click);
            // 
            // btn_Callout4
            // 
            this.btn_Callout4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Callout4.Location = new System.Drawing.Point(440, 14);
            this.btn_Callout4.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Callout4.Name = "btn_Callout4";
            this.btn_Callout4.Size = new System.Drawing.Size(133, 37);
            this.btn_Callout4.TabIndex = 33;
            this.btn_Callout4.TabStop = false;
            this.btn_Callout4.Text = "A1 SH2";
            this.btn_Callout4.UseVisualStyleBackColor = true;
            this.btn_Callout4.Click += new System.EventHandler(this.btn_Callout4_Click);
            // 
            // btn_Callout3
            // 
            this.btn_Callout3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Callout3.Location = new System.Drawing.Point(299, 14);
            this.btn_Callout3.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Callout3.Name = "btn_Callout3";
            this.btn_Callout3.Size = new System.Drawing.Size(133, 37);
            this.btn_Callout3.TabIndex = 32;
            this.btn_Callout3.TabStop = false;
            this.btn_Callout3.Text = "A (SH 2)";
            this.btn_Callout3.UseVisualStyleBackColor = true;
            this.btn_Callout3.Click += new System.EventHandler(this.btn_Callout3_Click);
            // 
            // btn_Callout2
            // 
            this.btn_Callout2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Callout2.Location = new System.Drawing.Point(157, 14);
            this.btn_Callout2.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Callout2.Name = "btn_Callout2";
            this.btn_Callout2.Size = new System.Drawing.Size(133, 37);
            this.btn_Callout2.TabIndex = 31;
            this.btn_Callout2.TabStop = false;
            this.btn_Callout2.Text = "A (2)";
            this.btn_Callout2.UseVisualStyleBackColor = true;
            this.btn_Callout2.Click += new System.EventHandler(this.btn_Callout2_Click);
            // 
            // btn_Callout1
            // 
            this.btn_Callout1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Callout1.Location = new System.Drawing.Point(16, 14);
            this.btn_Callout1.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Callout1.Name = "btn_Callout1";
            this.btn_Callout1.Size = new System.Drawing.Size(133, 37);
            this.btn_Callout1.TabIndex = 30;
            this.btn_Callout1.TabStop = false;
            this.btn_Callout1.Text = "A (Sh. 2)";
            this.btn_Callout1.UseVisualStyleBackColor = true;
            this.btn_Callout1.Click += new System.EventHandler(this.btn_Callout1_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 65);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 16);
            this.label10.TabIndex = 29;
            this.label10.Text = "Style String:";
            // 
            // pnl_DetailStyle
            // 
            this.pnl_DetailStyle.Controls.Add(this.txb_DetailStyle);
            this.pnl_DetailStyle.Controls.Add(this.label3);
            this.pnl_DetailStyle.Enabled = false;
            this.pnl_DetailStyle.Location = new System.Drawing.Point(8, 71);
            this.pnl_DetailStyle.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_DetailStyle.Name = "pnl_DetailStyle";
            this.pnl_DetailStyle.Size = new System.Drawing.Size(812, 55);
            this.pnl_DetailStyle.TabIndex = 23;
            // 
            // txb_DetailStyle
            // 
            this.txb_DetailStyle.Location = new System.Drawing.Point(8, 25);
            this.txb_DetailStyle.Margin = new System.Windows.Forms.Padding(4);
            this.txb_DetailStyle.Name = "txb_DetailStyle";
            this.txb_DetailStyle.Size = new System.Drawing.Size(799, 22);
            this.txb_DetailStyle.TabIndex = 19;
            this.txb_DetailStyle.Click += new System.EventHandler(this.Set_ActiveTextbox);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Detail View:";
            // 
            // pnl_AuxStyle
            // 
            this.pnl_AuxStyle.Controls.Add(this.txb_AuxStyle);
            this.pnl_AuxStyle.Controls.Add(this.label4);
            this.pnl_AuxStyle.Enabled = false;
            this.pnl_AuxStyle.Location = new System.Drawing.Point(8, 197);
            this.pnl_AuxStyle.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_AuxStyle.Name = "pnl_AuxStyle";
            this.pnl_AuxStyle.Size = new System.Drawing.Size(812, 55);
            this.pnl_AuxStyle.TabIndex = 25;
            // 
            // txb_AuxStyle
            // 
            this.txb_AuxStyle.Location = new System.Drawing.Point(8, 25);
            this.txb_AuxStyle.Margin = new System.Windows.Forms.Padding(4);
            this.txb_AuxStyle.Name = "txb_AuxStyle";
            this.txb_AuxStyle.Size = new System.Drawing.Size(799, 22);
            this.txb_AuxStyle.TabIndex = 19;
            this.txb_AuxStyle.Click += new System.EventHandler(this.Set_ActiveTextbox);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Auxiliary View:";
            // 
            // pnl_SectionStyle
            // 
            this.pnl_SectionStyle.Controls.Add(this.txb_SectionStyle);
            this.pnl_SectionStyle.Controls.Add(this.label5);
            this.pnl_SectionStyle.Enabled = false;
            this.pnl_SectionStyle.Location = new System.Drawing.Point(8, 134);
            this.pnl_SectionStyle.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_SectionStyle.Name = "pnl_SectionStyle";
            this.pnl_SectionStyle.Size = new System.Drawing.Size(812, 55);
            this.pnl_SectionStyle.TabIndex = 24;
            // 
            // txb_SectionStyle
            // 
            this.txb_SectionStyle.Location = new System.Drawing.Point(8, 25);
            this.txb_SectionStyle.Margin = new System.Windows.Forms.Padding(4);
            this.txb_SectionStyle.Name = "txb_SectionStyle";
            this.txb_SectionStyle.Size = new System.Drawing.Size(799, 22);
            this.txb_SectionStyle.TabIndex = 19;
            this.txb_SectionStyle.Click += new System.EventHandler(this.Set_ActiveTextbox);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "Section View:";
            // 
            // pnl_ProjectedStyle
            // 
            this.pnl_ProjectedStyle.Controls.Add(this.txb_ProjectedStyle);
            this.pnl_ProjectedStyle.Controls.Add(this.label9);
            this.pnl_ProjectedStyle.Enabled = false;
            this.pnl_ProjectedStyle.Location = new System.Drawing.Point(8, 260);
            this.pnl_ProjectedStyle.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_ProjectedStyle.Name = "pnl_ProjectedStyle";
            this.pnl_ProjectedStyle.Size = new System.Drawing.Size(812, 55);
            this.pnl_ProjectedStyle.TabIndex = 27;
            // 
            // txb_ProjectedStyle
            // 
            this.txb_ProjectedStyle.Location = new System.Drawing.Point(8, 25);
            this.txb_ProjectedStyle.Margin = new System.Windows.Forms.Padding(4);
            this.txb_ProjectedStyle.Name = "txb_ProjectedStyle";
            this.txb_ProjectedStyle.Size = new System.Drawing.Size(799, 22);
            this.txb_ProjectedStyle.TabIndex = 19;
            this.txb_ProjectedStyle.Click += new System.EventHandler(this.Set_ActiveTextbox);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 5);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 16);
            this.label9.TabIndex = 18;
            this.label9.Text = "Projected View:";
            // 
            // btn_Label3
            // 
            this.btn_Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Label3.Location = new System.Drawing.Point(352, 14);
            this.btn_Label3.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Label3.Name = "btn_Label3";
            this.btn_Label3.Size = new System.Drawing.Size(160, 49);
            this.btn_Label3.TabIndex = 33;
            this.btn_Label3.TabStop = false;
            this.btn_Label3.Text = "DETAIL A1";
            this.btn_Label3.UseVisualStyleBackColor = true;
            this.btn_Label3.Click += new System.EventHandler(this.btn_Label3_Click);
            // 
            // btn_Label2
            // 
            this.btn_Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Label2.Location = new System.Drawing.Point(184, 14);
            this.btn_Label2.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Label2.Name = "btn_Label2";
            this.btn_Label2.Size = new System.Drawing.Size(160, 49);
            this.btn_Label2.TabIndex = 32;
            this.btn_Label2.TabStop = false;
            this.btn_Label2.Text = "DETAIL A (1)";
            this.btn_Label2.UseVisualStyleBackColor = true;
            this.btn_Label2.Click += new System.EventHandler(this.btn_Label2_Click);
            // 
            // btn_Label1
            // 
            this.btn_Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Label1.Location = new System.Drawing.Point(16, 14);
            this.btn_Label1.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Label1.Name = "btn_Label1";
            this.btn_Label1.Size = new System.Drawing.Size(160, 49);
            this.btn_Label1.TabIndex = 31;
            this.btn_Label1.TabStop = false;
            this.btn_Label1.Text = "DETAIL\r\n(SHEET 1)";
            this.btn_Label1.UseVisualStyleBackColor = true;
            this.btn_Label1.Click += new System.EventHandler(this.btn_Label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 242);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 32);
            this.label2.TabIndex = 33;
            this.label2.Text = "Click Attribute Below to \r\nAdd to Current Style:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(544, 14);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(248, 48);
            this.label11.TabIndex = 34;
            this.label11.Text = "*LABEL MAY NOT INCLUDE \"DETAIL\"\r\nOR \"SECTION\" DEPENDING ON YOUR\r\nVIEW ANNOTATION " +
    "STYLE";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(17, 17);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(649, 32);
            this.label12.TabIndex = 34;
            this.label12.Text = "Configure View Reference by selecting view types to include references and either" +
    " choosing a preset style or\r\nbuilding your own by using the Attribute Buttons on" +
    " the left.";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btn_Callout4);
            this.panel2.Controls.Add(this.btn_Callout1);
            this.panel2.Controls.Add(this.btn_Callout3);
            this.panel2.Controls.Add(this.btn_Callout2);
            this.panel2.Controls.Add(this.txb_CalloutStyle);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Location = new System.Drawing.Point(248, 94);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(839, 128);
            this.panel2.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 74);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 16);
            this.label6.TabIndex = 37;
            this.label6.Text = "View Callout Style:";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.btn_Label1);
            this.panel3.Controls.Add(this.btn_Label3);
            this.panel3.Controls.Add(this.pnl_ProjectedStyle);
            this.panel3.Controls.Add(this.pnl_SectionStyle);
            this.panel3.Controls.Add(this.btn_Label2);
            this.panel3.Controls.Add(this.pnl_AuxStyle);
            this.panel3.Controls.Add(this.pnl_DetailStyle);
            this.panel3.Location = new System.Drawing.Point(248, 258);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(839, 331);
            this.panel3.TabIndex = 38;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(244, 239);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 16);
            this.label7.TabIndex = 39;
            this.label7.Text = "View Label Style:";
            // 
            // ckb_UpdateBeforeSave
            // 
            this.ckb_UpdateBeforeSave.AutoSize = true;
            this.ckb_UpdateBeforeSave.Location = new System.Drawing.Point(341, 609);
            this.ckb_UpdateBeforeSave.Margin = new System.Windows.Forms.Padding(4);
            this.ckb_UpdateBeforeSave.Name = "ckb_UpdateBeforeSave";
            this.ckb_UpdateBeforeSave.Size = new System.Drawing.Size(223, 20);
            this.ckb_UpdateBeforeSave.TabIndex = 40;
            this.ckb_UpdateBeforeSave.Text = "Update References Before Save";
            this.ckb_UpdateBeforeSave.UseVisualStyleBackColor = true;
            // 
            // ConfigUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1105, 647);
            this.Controls.Add(this.ckb_UpdateBeforeSave);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_SaveSettings);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Scale);
            this.Controls.Add(this.btn_Delim);
            this.Controls.Add(this.btn_LineBreak);
            this.Controls.Add(this.btn_ParentSheetName);
            this.Controls.Add(this.btn_ParentSheetNumber);
            this.Controls.Add(this.btn_ViewSheetName);
            this.Controls.Add(this.btn_ViewSheetNumber);
            this.Controls.Add(this.btn_ViewName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Reference - Configure";
            this.Load += new System.EventHandler(this.ConfigUI_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnl_DetailStyle.ResumeLayout(false);
            this.pnl_DetailStyle.PerformLayout();
            this.pnl_AuxStyle.ResumeLayout(false);
            this.pnl_AuxStyle.PerformLayout();
            this.pnl_SectionStyle.ResumeLayout(false);
            this.pnl_SectionStyle.PerformLayout();
            this.pnl_ProjectedStyle.ResumeLayout(false);
            this.pnl_ProjectedStyle.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ViewName;
        private System.Windows.Forms.Button btn_ViewSheetNumber;
        private System.Windows.Forms.Button btn_ViewSheetName;
        private System.Windows.Forms.Button btn_ParentSheetNumber;
        private System.Windows.Forms.Button btn_ParentSheetName;
        private System.Windows.Forms.Button btn_LineBreak;
        private System.Windows.Forms.Button btn_Delim;
        private System.Windows.Forms.Button btn_Scale;
        private System.Windows.Forms.CheckBox ckb_Detail;
        private System.Windows.Forms.CheckBox ckb_Section;
        private System.Windows.Forms.CheckBox ckb_Aux;
        private System.Windows.Forms.CheckBox ckb_Projected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txb_CalloutStyle;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_SaveSettings;
        private System.Windows.Forms.Panel pnl_DetailStyle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txb_DetailStyle;
        private System.Windows.Forms.Panel pnl_AuxStyle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txb_AuxStyle;
        private System.Windows.Forms.Panel pnl_SectionStyle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txb_SectionStyle;
        private System.Windows.Forms.Panel pnl_ProjectedStyle;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txb_ProjectedStyle;
        private System.Windows.Forms.Button btn_Callout4;
        private System.Windows.Forms.Button btn_Callout3;
        private System.Windows.Forms.Button btn_Callout2;
        private System.Windows.Forms.Button btn_Callout1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_Label3;
        private System.Windows.Forms.Button btn_Label2;
        private System.Windows.Forms.Button btn_Label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox ckb_UpdateBeforeSave;
	}
}
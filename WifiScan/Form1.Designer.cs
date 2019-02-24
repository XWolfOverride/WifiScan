namespace WifiScan
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tvTree = new System.Windows.Forms.TreeView();
            this.imageListDevices = new System.Windows.Forms.ImageList(this.components);
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.pwinfo = new System.Windows.Forms.Panel();
            this.lbInfo2 = new System.Windows.Forms.Label();
            this.cbVisible = new System.Windows.Forms.CheckBox();
            this.lbInfo = new System.Windows.Forms.Label();
            this.btColor = new System.Windows.Forms.Button();
            this.pbSignal = new System.Windows.Forms.ProgressBar();
            this.WifiTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.SuspendLayout();
            this.pwinfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvTree
            // 
            this.tvTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTree.FullRowSelect = true;
            this.tvTree.HideSelection = false;
            this.tvTree.ImageIndex = 0;
            this.tvTree.ImageList = this.imageListDevices;
            this.tvTree.Location = new System.Drawing.Point(0, 0);
            this.tvTree.Name = "tvTree";
            this.tvTree.SelectedImageIndex = 0;
            this.tvTree.Size = new System.Drawing.Size(350, 437);
            this.tvTree.StateImageList = this.imageListDevices;
            this.tvTree.TabIndex = 0;
            this.tvTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTree_AfterSelect);
            // 
            // imageListDevices
            // 
            this.imageListDevices.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDevices.ImageStream")));
            this.imageListDevices.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDevices.Images.SetKeyName(0, "iconfinder_network_card_82824.png");
            this.imageListDevices.Images.SetKeyName(1, "iconfinder_wifi_2639756.ico");
            this.imageListDevices.Images.SetKeyName(2, "no wifi.png");
            this.imageListDevices.Images.SetKeyName(3, "hidden_invisible_hide_eye_private-512.png");
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tvTree);
            this.scMain.Panel1.Controls.Add(this.pwinfo);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.scMain.Size = new System.Drawing.Size(1237, 567);
            this.scMain.SplitterDistance = 350;
            this.scMain.SplitterWidth = 15;
            this.scMain.TabIndex = 1;
            // 
            // pwinfo
            // 
            this.pwinfo.Controls.Add(this.lbInfo2);
            this.pwinfo.Controls.Add(this.cbVisible);
            this.pwinfo.Controls.Add(this.lbInfo);
            this.pwinfo.Controls.Add(this.btColor);
            this.pwinfo.Controls.Add(this.pbSignal);
            this.pwinfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pwinfo.Location = new System.Drawing.Point(0, 437);
            this.pwinfo.Name = "pwinfo";
            this.pwinfo.Size = new System.Drawing.Size(350, 130);
            this.pwinfo.TabIndex = 2;
            // 
            // lbInfo2
            // 
            this.lbInfo2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo2.AutoSize = true;
            this.lbInfo2.Location = new System.Drawing.Point(12, 23);
            this.lbInfo2.Name = "lbInfo2";
            this.lbInfo2.Size = new System.Drawing.Size(21, 20);
            this.lbInfo2.TabIndex = 4;
            this.lbInfo2.Text = "   ";
            // 
            // cbVisible
            // 
            this.cbVisible.AutoSize = true;
            this.cbVisible.Location = new System.Drawing.Point(12, 56);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(139, 24);
            this.cbVisible.TabIndex = 3;
            this.cbVisible.Text = "Show graphics";
            this.cbVisible.UseVisualStyleBackColor = true;
            this.cbVisible.CheckedChanged += new System.EventHandler(this.cbVisible_CheckedChanged);
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.AutoSize = true;
            this.lbInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbInfo.Location = new System.Drawing.Point(12, 3);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(24, 20);
            this.lbInfo.TabIndex = 2;
            this.lbInfo.Text = "   ";
            // 
            // btColor
            // 
            this.btColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btColor.Location = new System.Drawing.Point(315, 86);
            this.btColor.Name = "btColor";
            this.btColor.Size = new System.Drawing.Size(32, 32);
            this.btColor.TabIndex = 1;
            this.btColor.UseVisualStyleBackColor = false;
            this.btColor.Click += new System.EventHandler(this.btColor_Click);
            // 
            // pbSignal
            // 
            this.pbSignal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSignal.Location = new System.Drawing.Point(12, 86);
            this.pbSignal.Name = "pbSignal";
            this.pbSignal.Size = new System.Drawing.Size(297, 32);
            this.pbSignal.TabIndex = 0;
            // 
            // WifiTimer
            // 
            this.WifiTimer.Enabled = true;
            this.WifiTimer.Interval = 1000;
            this.WifiTimer.Tick += new System.EventHandler(this.WifiTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 567);
            this.Controls.Add(this.scMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WifiScan V1.1";
            this.scMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.pwinfo.ResumeLayout(false);
            this.pwinfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvTree;
        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.ImageList imageListDevices;
        private System.Windows.Forms.Timer WifiTimer;
        private System.Windows.Forms.Panel pwinfo;
        private System.Windows.Forms.Button btColor;
        private System.Windows.Forms.ProgressBar pbSignal;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.CheckBox cbVisible;
        private System.Windows.Forms.Label lbInfo2;
    }
}


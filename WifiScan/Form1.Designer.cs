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
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.SuspendLayout();
            this.pwinfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvTree
            // 
            this.tvTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvTree.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.tvTree.FullRowSelect = true;
            this.tvTree.HideSelection = false;
            this.tvTree.ImageIndex = 0;
            this.tvTree.ImageList = this.imageListDevices;
            this.tvTree.Location = new System.Drawing.Point(0, 0);
            this.tvTree.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tvTree.Name = "tvTree";
            this.tvTree.SelectedImageIndex = 0;
            this.tvTree.ShowLines = false;
            this.tvTree.ShowPlusMinus = false;
            this.tvTree.ShowRootLines = false;
            this.tvTree.Size = new System.Drawing.Size(238, 400);
            this.tvTree.StateImageList = this.imageListDevices;
            this.tvTree.TabIndex = 0;
            this.tvTree.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tvTree_DrawNode);
            this.tvTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTree_AfterSelect);
            this.tvTree.DoubleClick += new System.EventHandler(this.tvTree_DoubleClick);
            this.tvTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tvTree_KeyPress);
            // 
            // imageListDevices
            // 
            this.imageListDevices.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDevices.ImageStream")));
            this.imageListDevices.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDevices.Images.SetKeyName(0, "Net-card.png");
            this.imageListDevices.Images.SetKeyName(1, "Wifi-ok.png");
            this.imageListDevices.Images.SetKeyName(2, "Wifi-2-3.png");
            this.imageListDevices.Images.SetKeyName(3, "Wifi-1-3.png");
            this.imageListDevices.Images.SetKeyName(4, "Wifi-ko.png");
            this.imageListDevices.Images.SetKeyName(5, "hide.png");
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.tvTree);
            this.scMain.Panel1.Controls.Add(this.pwinfo);
            this.scMain.Panel1.Controls.Add(this.panel1);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.scMain.Size = new System.Drawing.Size(1078, 519);
            this.scMain.SplitterDistance = 238;
            this.scMain.SplitterWidth = 10;
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
            this.pwinfo.Location = new System.Drawing.Point(0, 400);
            this.pwinfo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pwinfo.Name = "pwinfo";
            this.pwinfo.Size = new System.Drawing.Size(238, 84);
            this.pwinfo.TabIndex = 2;
            // 
            // lbInfo2
            // 
            this.lbInfo2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo2.AutoSize = true;
            this.lbInfo2.Location = new System.Drawing.Point(8, 15);
            this.lbInfo2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInfo2.Name = "lbInfo2";
            this.lbInfo2.Size = new System.Drawing.Size(16, 13);
            this.lbInfo2.TabIndex = 4;
            this.lbInfo2.Text = "   ";
            // 
            // cbVisible
            // 
            this.cbVisible.AutoSize = true;
            this.cbVisible.Location = new System.Drawing.Point(8, 36);
            this.cbVisible.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(102, 17);
            this.cbVisible.TabIndex = 3;
            this.cbVisible.Text = "Show in graphic";
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
            this.lbInfo.Location = new System.Drawing.Point(8, 2);
            this.lbInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(19, 13);
            this.lbInfo.TabIndex = 2;
            this.lbInfo.Text = "   ";
            // 
            // btColor
            // 
            this.btColor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btColor.Location = new System.Drawing.Point(215, 56);
            this.btColor.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btColor.Name = "btColor";
            this.btColor.Size = new System.Drawing.Size(21, 21);
            this.btColor.TabIndex = 1;
            this.btColor.UseVisualStyleBackColor = false;
            this.btColor.Click += new System.EventHandler(this.btColor_Click);
            // 
            // pbSignal
            // 
            this.pbSignal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSignal.Location = new System.Drawing.Point(8, 56);
            this.pbSignal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pbSignal.Name = "pbSignal";
            this.pbSignal.Size = new System.Drawing.Size(203, 21);
            this.pbSignal.TabIndex = 0;
            // 
            // WifiTimer
            // 
            this.WifiTimer.Enabled = true;
            this.WifiTimer.Interval = 1000;
            this.WifiTimer.Tick += new System.EventHandler(this.WifiTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 484);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 35);
            this.panel1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 519);
            this.Controls.Add(this.scMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WifiScan V1.2 by XWolf Override";
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
        private System.Windows.Forms.Panel panel1;
    }
}


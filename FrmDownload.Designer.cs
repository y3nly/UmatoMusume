namespace UmatoMusume
{
    partial class FrmDownload
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
            groupBox1 = new GroupBox();
            btnCrawlSupport = new Button();
            btnCrawlUma = new Button();
            btnDownloadSupport = new Button();
            btnDownloadUma = new Button();
            pbDownload = new ProgressBar();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnCrawlSupport);
            groupBox1.Controls.Add(btnCrawlUma);
            groupBox1.Controls.Add(btnDownloadSupport);
            groupBox1.Controls.Add(btnDownloadUma);
            groupBox1.Controls.Add(pbDownload);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(576, 156);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Downloader options";
            // 
            // btnCrawlSupport
            // 
            btnCrawlSupport.Location = new Point(290, 61);
            btnCrawlSupport.Name = "btnCrawlSupport";
            btnCrawlSupport.Size = new Size(280, 29);
            btnCrawlSupport.TabIndex = 4;
            btnCrawlSupport.Text = "Crawl support data";
            btnCrawlSupport.UseVisualStyleBackColor = true;
            btnCrawlSupport.Click += btnCrawlSupport_Click;
            // 
            // btnCrawlUma
            // 
            btnCrawlUma.Location = new Point(6, 61);
            btnCrawlUma.Name = "btnCrawlUma";
            btnCrawlUma.Size = new Size(280, 29);
            btnCrawlUma.TabIndex = 3;
            btnCrawlUma.Text = "Crawl uma data";
            btnCrawlUma.UseVisualStyleBackColor = true;
            btnCrawlUma.Click += btnCrawlUma_Click;
            // 
            // btnDownloadSupport
            // 
            btnDownloadSupport.Location = new Point(290, 26);
            btnDownloadSupport.Name = "btnDownloadSupport";
            btnDownloadSupport.Size = new Size(280, 29);
            btnDownloadSupport.TabIndex = 2;
            btnDownloadSupport.Text = "Download support data";
            btnDownloadSupport.UseVisualStyleBackColor = true;
            btnDownloadSupport.Click += btnDownloadSupport_Click;
            // 
            // btnDownloadUma
            // 
            btnDownloadUma.Location = new Point(6, 26);
            btnDownloadUma.Name = "btnDownloadUma";
            btnDownloadUma.Size = new Size(280, 29);
            btnDownloadUma.TabIndex = 1;
            btnDownloadUma.Text = "Download uma data";
            btnDownloadUma.UseVisualStyleBackColor = true;
            btnDownloadUma.Click += btnDownloadUma_Click;
            // 
            // pbDownload
            // 
            pbDownload.Location = new Point(6, 121);
            pbDownload.Name = "pbDownload";
            pbDownload.Size = new Size(564, 29);
            pbDownload.TabIndex = 0;
            // 
            // FrmDownload
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(600, 176);
            Controls.Add(groupBox1);
            Name = "FrmDownload";
            Text = "Umamusume Data Downloader";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnCrawlSupport;
        private Button btnCrawlUma;
        private Button btnDownloadSupport;
        private Button btnDownloadUma;
        private ProgressBar pbDownload;
    }
}
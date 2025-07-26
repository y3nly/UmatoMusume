using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UmatoMusume.Utils;

namespace UmatoMusume
{
    public partial class FrmDownload : Form
    {
        private Label lblProgress;

        public FrmDownload()
        {
            InitializeComponent();
            
            lblProgress = new Label
            {
                AutoSize = true,
                Location = new Point(pbDownload.Location.X, pbDownload.Location.Y - 20),
                Name = "lblProgress",
                Size = new Size(pbDownload.Width, 20),
                Text = "Ready"
            };
            groupBox1.Controls.Add(lblProgress);
        }

        private async void btnCrawlUma_Click(object sender, EventArgs e)
        {
            SetControlsEnabled(false);
            pbDownload.Value = 0;

            try
            {
                var progress = new Progress<(int Current, int Total, string Message)>(progressData =>
                {
                    var (current, total, message) = progressData;
                    pbDownload.Value = Math.Min(current, 100);
                    lblProgress.Text = message;
                });

                await GameTora.DownloadUmaData(progress);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}", "Download Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetControlsEnabled(true);
            }
        }

        private async void btnCrawlSupport_Click(object sender, EventArgs e)
        {
            SetControlsEnabled(false);
            pbDownload.Value = 0;

            try
            {
                var progress = new Progress<(int Current, int Total, string Message)>(progressData =>
                {
                    var (current, total, message) = progressData;
                    pbDownload.Value = Math.Min(current, 100);
                    lblProgress.Text = message;
                });

                await GameTora.DownloadSupportData(progress);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error occurred: {ex.Message}", "Download Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetControlsEnabled(true);
            }
        }

        private void btnDownloadUma_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature not implemented yet", "Coming Soon", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDownloadSupport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Feature not implemented yet", "Coming Soon", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetControlsEnabled(bool enabled)
        {
            btnCrawlUma.Enabled = enabled;
            btnCrawlSupport.Enabled = enabled;
            btnDownloadUma.Enabled = enabled;
            btnDownloadSupport.Enabled = enabled;
        }
    }
}

namespace UmatoMusume
{
    partial class FrmMain
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
            btnCaptureEventType = new Button();
            btnCaptureCharInfo = new Button();
            btnCaptureEvent = new Button();
            groupBox2 = new GroupBox();
            lblCharacterInfo = new Label();
            label4 = new Label();
            lblEventName = new Label();
            lblEventType = new Label();
            label2 = new Label();
            label1 = new Label();
            groupBox3 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnCaptureEventType);
            groupBox1.Controls.Add(btnCaptureCharInfo);
            groupBox1.Controls.Add(btnCaptureEvent);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(423, 63);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Capture options";
            // 
            // btnCaptureEventType
            // 
            btnCaptureEventType.Location = new Point(6, 22);
            btnCaptureEventType.Name = "btnCaptureEventType";
            btnCaptureEventType.Size = new Size(130, 23);
            btnCaptureEventType.TabIndex = 2;
            btnCaptureEventType.Text = "Capture event type";
            btnCaptureEventType.UseVisualStyleBackColor = true;
            btnCaptureEventType.Click += btnCaptureEventType_Click;
            // 
            // btnCaptureCharInfo
            // 
            btnCaptureCharInfo.Location = new Point(253, 22);
            btnCaptureCharInfo.Name = "btnCaptureCharInfo";
            btnCaptureCharInfo.Size = new Size(164, 23);
            btnCaptureCharInfo.TabIndex = 1;
            btnCaptureCharInfo.Text = "Capture character info";
            btnCaptureCharInfo.UseVisualStyleBackColor = true;
            btnCaptureCharInfo.Click += btnCaptureCharInfo_Click;
            // 
            // btnCaptureEvent
            // 
            btnCaptureEvent.Location = new Point(142, 22);
            btnCaptureEvent.Name = "btnCaptureEvent";
            btnCaptureEvent.Size = new Size(105, 23);
            btnCaptureEvent.TabIndex = 0;
            btnCaptureEvent.Text = "Capture event";
            btnCaptureEvent.UseVisualStyleBackColor = true;
            btnCaptureEvent.Click += btnCaptureEvent_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblCharacterInfo);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(lblEventName);
            groupBox2.Controls.Add(lblEventType);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label1);
            groupBox2.Location = new Point(12, 81);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(417, 256);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Event selector";
            // 
            // lblCharacterInfo
            // 
            lblCharacterInfo.AutoSize = true;
            lblCharacterInfo.Location = new Point(87, 69);
            lblCharacterInfo.Name = "lblCharacterInfo";
            lblCharacterInfo.Padding = new Padding(0, 5, 0, 5);
            lblCharacterInfo.Size = new Size(69, 25);
            lblCharacterInfo.TabIndex = 5;
            lblCharacterInfo.Text = "Capturing...";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 69);
            label4.Name = "label4";
            label4.Padding = new Padding(0, 5, 0, 5);
            label4.Size = new Size(59, 25);
            label4.TabIndex = 4;
            label4.Text = "Char info:";
            // 
            // lblEventName
            // 
            lblEventName.AutoSize = true;
            lblEventName.Location = new Point(87, 44);
            lblEventName.Name = "lblEventName";
            lblEventName.Padding = new Padding(0, 5, 0, 5);
            lblEventName.Size = new Size(69, 25);
            lblEventName.TabIndex = 3;
            lblEventName.Text = "Capturing...";
            // 
            // lblEventType
            // 
            lblEventType.AutoSize = true;
            lblEventType.Location = new Point(87, 19);
            lblEventType.Name = "lblEventType";
            lblEventType.Padding = new Padding(0, 5, 0, 5);
            lblEventType.Size = new Size(69, 25);
            lblEventType.TabIndex = 2;
            lblEventType.Text = "Capturing...";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Padding = new Padding(0, 5, 0, 5);
            label2.Size = new Size(68, 25);
            label2.TabIndex = 1;
            label2.Text = "Event type: ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 44);
            label1.Name = "label1";
            label1.Padding = new Padding(0, 5, 0, 5);
            label1.Size = new Size(75, 25);
            label1.TabIndex = 0;
            label1.Text = "Event name: ";
            // 
            // groupBox3
            // 
            groupBox3.Location = new Point(12, 343);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(417, 476);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Objectives";
            // 
            // FrmMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(447, 831);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "FrmMain";
            Text = "FrmMain";
            FormClosing += FrmMain_FormClosing;
            Load += FrmMain_Load;
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnCaptureCharInfo;
        private Button btnCaptureEvent;
        private GroupBox groupBox2;
        private Label label1;
        private Label label2;
        private Button btnCaptureEventType;
        private Label lblEventName;
        private Label lblEventType;
        private Label lblCharacterInfo;
        private Label label4;
        private GroupBox groupBox3;
    }
}
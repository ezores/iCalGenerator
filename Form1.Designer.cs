namespace iCalGenerator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnGenerateICal = new Button();
            btnSelectScreenshot = new Button();
            dateTimePickerEnd = new DateTimePicker();
            dateTimePickerStart = new DateTimePicker();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnGenerateICal
            // 
            btnGenerateICal.Location = new System.Drawing.Point(259, 292);
            btnGenerateICal.Name = "btnGenerateICal";
            btnGenerateICal.Size = new System.Drawing.Size(250, 63);
            btnGenerateICal.TabIndex = 0;
            btnGenerateICal.Text = "Generate";
            btnGenerateICal.UseVisualStyleBackColor = true;
            btnGenerateICal.Click += btnGenerateICal_Click;
            // 
            // btnSelectScreenshot
            // 
            btnSelectScreenshot.Location = new System.Drawing.Point(259, 56);
            btnSelectScreenshot.Name = "btnSelectScreenshot";
            btnSelectScreenshot.Size = new System.Drawing.Size(250, 63);
            btnSelectScreenshot.TabIndex = 1;
            btnSelectScreenshot.Text = "Upload";
            btnSelectScreenshot.UseVisualStyleBackColor = true;
            btnSelectScreenshot.Click += btnSelectScreenshot_Click;
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.Location = new System.Drawing.Point(483, 181);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new System.Drawing.Size(200, 23);
            dateTimePickerEnd.TabIndex = 2;
            // 
            // dateTimePickerStart
            // 
            dateTimePickerStart.Location = new System.Drawing.Point(126, 181);
            dateTimePickerStart.Name = "dateTimePickerStart";
            dateTimePickerStart.Size = new System.Drawing.Size(200, 23);
            dateTimePickerStart.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(247, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(277, 15);
            label1.TabIndex = 4;
            label1.Text = "Upload the image and select start and ending dates";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(label1);
            Controls.Add(dateTimePickerStart);
            Controls.Add(dateTimePickerEnd);
            Controls.Add(btnSelectScreenshot);
            Controls.Add(btnGenerateICal);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnGenerateICal;
        private Button btnSelectScreenshot;
        private DateTimePicker dateTimePickerEnd;
        private DateTimePicker dateTimePickerStart;
        private Label label1;
    }
}
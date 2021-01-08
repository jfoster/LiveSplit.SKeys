namespace LiveSplit.SKeys
{
    partial class SKeysSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sizeLabel = new System.Windows.Forms.Label();
            this.sizeUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.sizeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(5, 6);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(27, 13);
            this.sizeLabel.TabIndex = 0;
            this.sizeLabel.Text = "Size";
            // 
            // sizeUpDown
            // 
            this.sizeUpDown.Location = new System.Drawing.Point(46, 4);
            this.sizeUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.sizeUpDown.Name = "sizeUpDown";
            this.sizeUpDown.Size = new System.Drawing.Size(120, 20);
            this.sizeUpDown.TabIndex = 1;
            // 
            // SKeysSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sizeUpDown);
            this.Controls.Add(this.sizeLabel);
            this.Name = "SKeysSettings";
            this.Size = new System.Drawing.Size(450, 150);
            this.Load += new System.EventHandler(this.SKeysSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sizeUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.NumericUpDown sizeUpDown;
    }
}

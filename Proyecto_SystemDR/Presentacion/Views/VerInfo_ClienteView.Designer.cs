namespace Presentacion.Views
{
    partial class VerInfo_ClienteView
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Button_ShowM = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(37, 66);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(798, 221);
            this.dataGridView1.TabIndex = 0;
            // 
            // Button_ShowM
            // 
            this.Button_ShowM.Location = new System.Drawing.Point(375, 379);
            this.Button_ShowM.Name = "Button_ShowM";
            this.Button_ShowM.Size = new System.Drawing.Size(153, 23);
            this.Button_ShowM.TabIndex = 1;
            this.Button_ShowM.Text = "Show Message";
            this.Button_ShowM.UseVisualStyleBackColor = true;
            // 
            // VerInfo_ClienteView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 611);
            this.Controls.Add(this.Button_ShowM);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "VerInfo_ClienteView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VerInfo_ClienteView";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public System.Windows.Forms.Button Button_ShowM;
        public System.Windows.Forms.DataGridView dataGridView1;
    }
}
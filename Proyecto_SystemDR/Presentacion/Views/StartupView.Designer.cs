namespace Presentacion.Views
{
    partial class StartupView
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
            this.DGV_Persona = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Persona)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV_Persona
            // 
            this.DGV_Persona.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Persona.Location = new System.Drawing.Point(38, 26);
            this.DGV_Persona.Name = "DGV_Persona";
            this.DGV_Persona.Size = new System.Drawing.Size(702, 224);
            this.DGV_Persona.TabIndex = 0;
            // 
            // StartupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DGV_Persona);
            this.Name = "StartupView";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.StartupView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Persona)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGV_Persona;
    }
}


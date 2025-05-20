namespace Campo_minato3
{
    partial class finestra_iniziale
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(finestra_iniziale));
            this.lbl_titolo = new System.Windows.Forms.Label();
            this.lbl_titoloDifficolta = new System.Windows.Forms.Label();
            this.cmb_difficolta = new System.Windows.Forms.ComboBox();
            this.btn_inserisci = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_titolo
            // 
            this.lbl_titolo.AutoSize = true;
            this.lbl_titolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titolo.Location = new System.Drawing.Point(113, 23);
            this.lbl_titolo.Name = "lbl_titolo";
            this.lbl_titolo.Size = new System.Drawing.Size(212, 20);
            this.lbl_titolo.TabIndex = 0;
            this.lbl_titolo.Text = "Benvenuto al prato fiorito";
            this.lbl_titolo.Click += new System.EventHandler(this.lbl_titolo_Click);
            // 
            // lbl_titoloDifficolta
            // 
            this.lbl_titoloDifficolta.AutoSize = true;
            this.lbl_titoloDifficolta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titoloDifficolta.Location = new System.Drawing.Point(140, 75);
            this.lbl_titoloDifficolta.Name = "lbl_titoloDifficolta";
            this.lbl_titoloDifficolta.Size = new System.Drawing.Size(158, 20);
            this.lbl_titoloDifficolta.TabIndex = 1;
            this.lbl_titoloDifficolta.Text = "Scegliere difficoltà";
            this.lbl_titoloDifficolta.Click += new System.EventHandler(this.lbl_titoloDifficolta_Click);
            // 
            // cmb_difficolta
            // 
            this.cmb_difficolta.FormattingEnabled = true;
            this.cmb_difficolta.Location = new System.Drawing.Point(80, 132);
            this.cmb_difficolta.Name = "cmb_difficolta";
            this.cmb_difficolta.Size = new System.Drawing.Size(121, 21);
            this.cmb_difficolta.TabIndex = 2;
            this.cmb_difficolta.SelectedIndexChanged += new System.EventHandler(this.cmb_difficolta_SelectedIndexChanged);
            // 
            // btn_inserisci
            // 
            this.btn_inserisci.Location = new System.Drawing.Point(283, 131);
            this.btn_inserisci.Name = "btn_inserisci";
            this.btn_inserisci.Size = new System.Drawing.Size(75, 23);
            this.btn_inserisci.TabIndex = 3;
            this.btn_inserisci.Text = "inserisci";
            this.btn_inserisci.UseVisualStyleBackColor = true;
            this.btn_inserisci.Click += new System.EventHandler(this.btn_inserisci_Click);
            // 
            // finestra_iniziale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 241);
            this.Controls.Add(this.btn_inserisci);
            this.Controls.Add(this.cmb_difficolta);
            this.Controls.Add(this.lbl_titoloDifficolta);
            this.Controls.Add(this.lbl_titolo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "finestra_iniziale";
            this.Text = "finestra_iniziale";
            this.Load += new System.EventHandler(this.finestra_iniziale_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_titolo;
        private System.Windows.Forms.Label lbl_titoloDifficolta;
        private System.Windows.Forms.ComboBox cmb_difficolta;
        private System.Windows.Forms.Button btn_inserisci;
    }
}
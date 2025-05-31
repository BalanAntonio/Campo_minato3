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
            this.cmb_tema = new System.Windows.Forms.ComboBox();
            this.lbl_temaTitolo = new System.Windows.Forms.Label();
            this.lbl_migliorTempoTitolo = new System.Windows.Forms.Label();
            this.lbl_TempoCorrenteTitolo = new System.Windows.Forms.Label();
            this.lbl_migliorTempo = new System.Windows.Forms.Label();
            this.lbl_tempoCorrente = new System.Windows.Forms.Label();
            this.btn_resetta = new System.Windows.Forms.Button();
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
            this.lbl_titoloDifficolta.Location = new System.Drawing.Point(37, 112);
            this.lbl_titoloDifficolta.Name = "lbl_titoloDifficolta";
            this.lbl_titoloDifficolta.Size = new System.Drawing.Size(158, 20);
            this.lbl_titoloDifficolta.TabIndex = 1;
            this.lbl_titoloDifficolta.Text = "Scegliere difficoltà";
            this.lbl_titoloDifficolta.Click += new System.EventHandler(this.lbl_titoloDifficolta_Click);
            // 
            // cmb_difficolta
            // 
            this.cmb_difficolta.FormattingEnabled = true;
            this.cmb_difficolta.Location = new System.Drawing.Point(74, 135);
            this.cmb_difficolta.Name = "cmb_difficolta";
            this.cmb_difficolta.Size = new System.Drawing.Size(121, 21);
            this.cmb_difficolta.TabIndex = 2;
            this.cmb_difficolta.SelectedIndexChanged += new System.EventHandler(this.cmb_difficolta_SelectedIndexChanged);
            // 
            // btn_inserisci
            // 
            this.btn_inserisci.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_inserisci.Location = new System.Drawing.Point(251, 135);
            this.btn_inserisci.Name = "btn_inserisci";
            this.btn_inserisci.Size = new System.Drawing.Size(75, 29);
            this.btn_inserisci.TabIndex = 3;
            this.btn_inserisci.Text = "gioca";
            this.btn_inserisci.UseVisualStyleBackColor = true;
            this.btn_inserisci.Click += new System.EventHandler(this.btn_inserisci_Click);
            // 
            // cmb_tema
            // 
            this.cmb_tema.FormattingEnabled = true;
            this.cmb_tema.Location = new System.Drawing.Point(74, 204);
            this.cmb_tema.Name = "cmb_tema";
            this.cmb_tema.Size = new System.Drawing.Size(121, 21);
            this.cmb_tema.TabIndex = 4;
            this.cmb_tema.SelectedIndexChanged += new System.EventHandler(this.cmb_tema_SelectedIndexChanged);
            // 
            // lbl_temaTitolo
            // 
            this.lbl_temaTitolo.AutoSize = true;
            this.lbl_temaTitolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_temaTitolo.Location = new System.Drawing.Point(66, 181);
            this.lbl_temaTitolo.Name = "lbl_temaTitolo";
            this.lbl_temaTitolo.Size = new System.Drawing.Size(129, 20);
            this.lbl_temaTitolo.TabIndex = 5;
            this.lbl_temaTitolo.Text = "Scegliere tema";
            this.lbl_temaTitolo.Click += new System.EventHandler(this.lbl_temaTitolo_Click);
            // 
            // lbl_migliorTempoTitolo
            // 
            this.lbl_migliorTempoTitolo.AutoSize = true;
            this.lbl_migliorTempoTitolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_migliorTempoTitolo.Location = new System.Drawing.Point(220, 54);
            this.lbl_migliorTempoTitolo.Name = "lbl_migliorTempoTitolo";
            this.lbl_migliorTempoTitolo.Size = new System.Drawing.Size(105, 16);
            this.lbl_migliorTempoTitolo.TabIndex = 6;
            this.lbl_migliorTempoTitolo.Text = "miglior tempo:";
            this.lbl_migliorTempoTitolo.Click += new System.EventHandler(this.lbl_migliorTempoTitolo_Click);
            // 
            // lbl_TempoCorrenteTitolo
            // 
            this.lbl_TempoCorrenteTitolo.AutoSize = true;
            this.lbl_TempoCorrenteTitolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TempoCorrenteTitolo.Location = new System.Drawing.Point(234, 87);
            this.lbl_TempoCorrenteTitolo.Name = "lbl_TempoCorrenteTitolo";
            this.lbl_TempoCorrenteTitolo.Size = new System.Drawing.Size(91, 16);
            this.lbl_TempoCorrenteTitolo.TabIndex = 7;
            this.lbl_TempoCorrenteTitolo.Text = "il tuo tempo:";
            this.lbl_TempoCorrenteTitolo.Click += new System.EventHandler(this.lbl_TempoCorrenteTitolo_Click);
            // 
            // lbl_migliorTempo
            // 
            this.lbl_migliorTempo.AutoSize = true;
            this.lbl_migliorTempo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_migliorTempo.Location = new System.Drawing.Point(331, 54);
            this.lbl_migliorTempo.Name = "lbl_migliorTempo";
            this.lbl_migliorTempo.Size = new System.Drawing.Size(17, 16);
            this.lbl_migliorTempo.TabIndex = 8;
            this.lbl_migliorTempo.Text = "--";
            this.lbl_migliorTempo.Click += new System.EventHandler(this.lbl_migliorTempo_Click);
            // 
            // lbl_tempoCorrente
            // 
            this.lbl_tempoCorrente.AutoSize = true;
            this.lbl_tempoCorrente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_tempoCorrente.Location = new System.Drawing.Point(331, 87);
            this.lbl_tempoCorrente.Name = "lbl_tempoCorrente";
            this.lbl_tempoCorrente.Size = new System.Drawing.Size(17, 16);
            this.lbl_tempoCorrente.TabIndex = 9;
            this.lbl_tempoCorrente.Text = "--";
            this.lbl_tempoCorrente.Click += new System.EventHandler(this.lbl_tempoCorrente_Click);
            // 
            // btn_resetta
            // 
            this.btn_resetta.Location = new System.Drawing.Point(251, 202);
            this.btn_resetta.Name = "btn_resetta";
            this.btn_resetta.Size = new System.Drawing.Size(97, 23);
            this.btn_resetta.TabIndex = 10;
            this.btn_resetta.Text = "resetta tempo";
            this.btn_resetta.UseVisualStyleBackColor = true;
            this.btn_resetta.Click += new System.EventHandler(this.btn_resetta_Click);
            // 
            // finestra_iniziale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 241);
            this.Controls.Add(this.btn_resetta);
            this.Controls.Add(this.lbl_tempoCorrente);
            this.Controls.Add(this.lbl_migliorTempo);
            this.Controls.Add(this.lbl_TempoCorrenteTitolo);
            this.Controls.Add(this.lbl_migliorTempoTitolo);
            this.Controls.Add(this.lbl_temaTitolo);
            this.Controls.Add(this.cmb_tema);
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
        private System.Windows.Forms.ComboBox cmb_tema;
        private System.Windows.Forms.Label lbl_temaTitolo;
        private System.Windows.Forms.Label lbl_migliorTempoTitolo;
        private System.Windows.Forms.Label lbl_TempoCorrenteTitolo;
        private System.Windows.Forms.Label lbl_migliorTempo;
        private System.Windows.Forms.Label lbl_tempoCorrente;
        private System.Windows.Forms.Button btn_resetta;
    }
}
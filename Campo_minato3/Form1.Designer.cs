namespace Campo_minato3
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dtg_campo = new System.Windows.Forms.DataGridView();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lbl_titolo = new System.Windows.Forms.Label();
            this.lbl_nMineTitolo = new System.Windows.Forms.Label();
            this.lbl_nMine = new System.Windows.Forms.Label();
            this.pnl_titolo = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dtg_campo)).BeginInit();
            this.pnl_titolo.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtg_campo
            // 
            this.dtg_campo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtg_campo.Location = new System.Drawing.Point(41, 129);
            this.dtg_campo.Name = "dtg_campo";
            this.dtg_campo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dtg_campo.Size = new System.Drawing.Size(687, 403);
            this.dtg_campo.TabIndex = 0;
            this.dtg_campo.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dtg_campo_CellMouseClick);
            // 
            // lbl_titolo
            // 
            this.lbl_titolo.AutoSize = true;
            this.lbl_titolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_titolo.Location = new System.Drawing.Point(48, 15);
            this.lbl_titolo.Name = "lbl_titolo";
            this.lbl_titolo.Size = new System.Drawing.Size(144, 24);
            this.lbl_titolo.TabIndex = 1;
            this.lbl_titolo.Text = "Campo minato";
            this.lbl_titolo.Click += new System.EventHandler(this.lbl_titolo_Click);
            // 
            // lbl_nMineTitolo
            // 
            this.lbl_nMineTitolo.AutoSize = true;
            this.lbl_nMineTitolo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nMineTitolo.Location = new System.Drawing.Point(25, 39);
            this.lbl_nMineTitolo.Name = "lbl_nMineTitolo";
            this.lbl_nMineTitolo.Size = new System.Drawing.Size(154, 24);
            this.lbl_nMineTitolo.TabIndex = 2;
            this.lbl_nMineTitolo.Text = "mine rimanenti:";
            this.lbl_nMineTitolo.Click += new System.EventHandler(this.lbl_nMineTitolo_Click);
            // 
            // lbl_nMine
            // 
            this.lbl_nMine.AutoSize = true;
            this.lbl_nMine.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nMine.Location = new System.Drawing.Point(195, 39);
            this.lbl_nMine.Name = "lbl_nMine";
            this.lbl_nMine.Size = new System.Drawing.Size(24, 24);
            this.lbl_nMine.TabIndex = 3;
            this.lbl_nMine.Text = "--";
            this.lbl_nMine.Click += new System.EventHandler(this.lbl_nMine_Click);
            // 
            // pnl_titolo
            // 
            this.pnl_titolo.Controls.Add(this.lbl_titolo);
            this.pnl_titolo.Controls.Add(this.lbl_nMine);
            this.pnl_titolo.Controls.Add(this.lbl_nMineTitolo);
            this.pnl_titolo.Location = new System.Drawing.Point(281, 12);
            this.pnl_titolo.Name = "pnl_titolo";
            this.pnl_titolo.Size = new System.Drawing.Size(239, 74);
            this.pnl_titolo.TabIndex = 4;
            this.pnl_titolo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_titolo_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 544);
            this.Controls.Add(this.pnl_titolo);
            this.Controls.Add(this.dtg_campo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Campo minato";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg_campo)).EndInit();
            this.pnl_titolo.ResumeLayout(false);
            this.pnl_titolo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtg_campo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lbl_titolo;
        private System.Windows.Forms.Label lbl_nMineTitolo;
        private System.Windows.Forms.Label lbl_nMine;
        private System.Windows.Forms.Panel pnl_titolo;
    }
}


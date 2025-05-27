using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_minato3
{
    public partial class finestra_iniziale : Form
    {
        public int lunghezza_latoIn { get; set; }
        public int altezza_latoIn { get; set; }
        public int NmineIn { get; set; }
        public CTema tema { get; set; }

        // LISTA DI TUTTI I TEMI DEL GIOCO


        Color[] coloriDefault = new Color[]
        {
            Color.FromArgb(0, 0, 255),       // 1 - Blu
            Color.FromArgb(0, 128, 0),       // 2 - Verde
            Color.FromArgb(255, 0, 0),       // 3 - Rosso
            Color.FromArgb(0, 0, 139),       // 4 - Blu scuro
            Color.FromArgb(139, 69, 19),     // 5 - Marrone scuro
            Color.FromArgb(72, 209, 204),    // 6 - Ciano scuro
            Color.FromArgb(0, 0, 0),         // 7 - Nero
            Color.FromArgb(105, 105, 105)    // 8 - Grigio scuro
        };


        CTema[] temi = new CTema[]
        {

        };

        public finestra_iniziale(string messaggio)
        {
            InitializeComponent();

            lbl_titolo.Text = messaggio;
            lbl_titolo.Location = new Point((this.ClientSize.Width - lbl_titolo.Width) / 2, lbl_titolo.Location.Y); // Centra il titolo

            cmb_difficolta.Items.Add("Facile");
            cmb_difficolta.Items.Add("Medio");
            cmb_difficolta.Items.Add("Difficile");

            cmb_difficolta.SelectedIndex = 0;
        }

        private void btn_inserisci_Click(object sender, EventArgs e)
        {
            string difficolta = cmb_difficolta.SelectedItem.ToString().ToLower();

            if (difficolta == "facile")
            {
                lunghezza_latoIn = 9;
                altezza_latoIn = 9;
                NmineIn = 10;
            }
            else if (difficolta == "medio")
            {
                lunghezza_latoIn = 16;
                altezza_latoIn = 16;
                NmineIn = 40;
            }
            else if (difficolta == "difficile")
            {
                lunghezza_latoIn = 30;
                altezza_latoIn = 16;
                NmineIn = 99;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void finestra_iniziale_Load(object sender, EventArgs e)
        {

        }

        private void lbl_titolo_Click(object sender, EventArgs e)
        {

        }

        private void lbl_titoloDifficolta_Click(object sender, EventArgs e)
        {

        }

        private void cmb_difficolta_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbl_migliorTempoTitolo_Click(object sender, EventArgs e)
        {

        }

        private void lbl_TempoCorrenteTitolo_Click(object sender, EventArgs e)
        {

        }

        private void lbl_migliorTempo_Click(object sender, EventArgs e)
        {

        }

        private void lbl_tempoCorrente_Click(object sender, EventArgs e)
        {

        }

        private void lbl_temaTitolo_Click(object sender, EventArgs e)
        {

        }

        private void cmb_tema_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

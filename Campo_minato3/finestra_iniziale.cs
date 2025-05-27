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
    }
}

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
        public finestra_iniziale()
        {
            InitializeComponent();

            cmb_difficolta.Items.Add("Facile");
            cmb_difficolta.Items.Add("Medio");
            cmb_difficolta.Items.Add("Difficile");
        }

        private void btn_inserisci_Click(object sender, EventArgs e)
        {

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

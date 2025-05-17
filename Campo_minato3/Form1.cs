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
    public partial class Form1 : Form
    {
        //  0   ->  Cella vuota non scoperta
        //  9   ->  Cella vuota scoperta
        //  10  ->  Cella con mina
        //  -10 ->  Cella con mina nascosta
        //  Altri numeri positivi    ->  Numero bombe adiacenti scoperti
        //  Altri numeri negativi   ->  Numero bombe adiacenti non scoperti

        static void ScopriCasella(int x, int y, ref int[,] Area)
        {
            if (Area[y, x] == 0)
            {
                Area[y, x] = 1; // Rende cella vuota scoperta e non cliccabile
                ScopriCasella(x, y + 1, ref Area);
                ScopriCasella(x, y - 1, ref Area);
                ScopriCasella(x + 1, y, ref Area);
                ScopriCasella(x - 1, y, ref Area);
            }
            else if (Area[y, x] <= -1 && Area[y, x] >= -8)
            {
                Area[y, x] *= -1;   // Rende il numero visibile
            }
            else if (Area[y, x] == 10)
            {
                // Codice di game over
            }
        }



        public Form1()
        {
            InitializeComponent();

            creaCelle(10);
        }

        public void creaCelle(int lato)
        {
            

            // creazione colonne
            for (int i = 0; i < lato; i++)
            {
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.Width = 35;
                btnColumn.FlatStyle = FlatStyle.Flat; // per un bottone più personalizzabile

                btnColumn.DefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

                dtg_campo.Columns.Add(btnColumn);
            }

            // creazione righe
            for (int i = 0; i < lato; i++)
            {
                dtg_campo.Rows.Add();
                dtg_campo.Rows[i].Height = 35;

                // da ad ogni cella il colore e il tag 0 che indica l'acqua
                for (int j = 0; j < lato; j++)
                {
                    dtg_campo.Rows[i].Cells[j].Style.BackColor = Color.LightBlue;
                    dtg_campo.Rows[i].Cells[j].Tag = 0;
                }
            }

            dtg_campo.ColumnHeadersVisible = false;
            dtg_campo.RowHeadersVisible = false;

            // non permette all'utente di uscire dagli schemi
            dtg_campo.AllowUserToAddRows = false;
            dtg_campo.AllowUserToResizeColumns = false;
            dtg_campo.AllowUserToResizeRows = false;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dtg_campo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

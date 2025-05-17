using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_minato3
{
    public partial class Form1 : Form
    {

        int lughezzaLato;
        int Nmine;

        int[,] campo;

        //  0   ->  Cella vuota non scoperta
        //  9   ->  Cella vuota scoperta
        //  10  ->  Cella con mina
        //  -10 ->  Cella con mina nascosta
        //  Altri numeri positivi    ->  Numero bombe adiacenti scoperti
        //  Altri numeri negativi   ->  Numero bombe adiacenti non scoperti

        // io farei:
        //  -1   ->  Cella vuota scoperta
        //  -2   ->  Cella vuota scoperta

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
        public void AggiornaDatagrid()
        {

        }

        public Form1()
        {
            InitializeComponent();

            PrendiDifficolta();

            creaCelle();

            riordinaGrandezze();
        }

        public void creaCelle()
        {
            // pulisce il datagridview
            dtg_campo.Columns.Clear();
            dtg_campo.Rows.Clear();

            // creazione colonne
            for (int i = 0; i < lughezzaLato; i++)
            {
                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.Width = 25;
                btnColumn.FlatStyle = FlatStyle.Flat; // per un bottone più personalizzabile

                btnColumn.DefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

                dtg_campo.Columns.Add(btnColumn);
            }

            // creazione righe
            for (int i = 0; i < lughezzaLato; i++)
            {
                dtg_campo.Rows.Add();
                dtg_campo.Rows[i].Height = 25;

                // da ad ogni cella il colore e il tag 0 che indica l'acqua
                for (int j = 0; j < lughezzaLato; j++)
                {
                    dtg_campo.Rows[i].Cells[j].Style.BackColor = Color.LightBlue;
                    dtg_campo.Rows[i].Cells[j].Tag = 0;
                }
            }

            dtg_campo.ColumnHeadersVisible = false;
            dtg_campo.RowHeadersVisible = false;

            // non permette all'utente di uscire dagli schemi
            dtg_campo.AllowUserToAddRows = false; // non permette di aggiungere righe
            dtg_campo.AllowUserToAddRows = false; // non permette di aggiungere righe
            dtg_campo.AllowUserToResizeColumns = false; // non permette di ridimensionare le righe e le colonne
            dtg_campo.AllowUserToResizeRows = false; // non permette di ridimensionare le righe e le colonne    
            dtg_campo.MultiSelect = false; // non permette di selezionare più celle contemporaneamente
            dtg_campo.ReadOnly = true; // rende le celle non modificabili
            dtg_campo.AllowUserToDeleteRows = false; // non permette di eliminare righe
            dtg_campo.AllowUserToOrderColumns = false; // non permette di ordinare le colonne

            // tgliere il bordo tra le celle
            dtg_campo.CellBorderStyle = DataGridViewCellBorderStyle.None; 
        }

        public void riordinaGrandezze()
        {
            // ridimensiona la grandezza del datagridview in base alla grandezza delle celle
            dtg_campo.Height = (int)25 * lughezzaLato + 2;
            dtg_campo.Width = (int)25 * lughezzaLato + 2;
            
            // ridimensiona la grandezza della finestra in base alla grandezza del datagridview
            this.ClientSize = new Size(dtg_campo.Width + 20, dtg_campo.Height + 20); // +20 per il bordo della finestra
            
            // centra il datagridview nella finestra
            dtg_campo.Location = new Point((this.ClientSize.Width - dtg_campo.Width) / 2, (this.ClientSize.Height - dtg_campo.Height) / 2);
        }

        public void PrendiDifficolta()
        {
            finestra_iniziale finestra = new finestra_iniziale();

            if(finestra.ShowDialog() == DialogResult.OK)
            {
                lughezzaLato = finestra.lunghezza_latoIn; // prendi il numero di celle per lato
                Nmine = finestra.NmineIn; // prendi il numero di mine
            }

            campo = new int[lughezzaLato, lughezzaLato]; // crea il campo di gioco
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void dtg_campo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

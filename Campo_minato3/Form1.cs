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
        //  -2   ->  Cella con mina nascosta

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

            PrendiDifficolta();

            creaCelle();

            riordinaGrandezze();

            posizionaMine();
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

        public void posizionaMine()
        {
            Random random = new Random(Environment.TickCount);
            int minePos = 0;
            while (minePos < Nmine)
            {
                int x = random.Next(0, lughezzaLato);
                int y = random.Next(0, lughezzaLato);
                if (campo[x, y] != -2) // se la cella non è già occupata da una mina
                {
                    campo[x, y] = -2; // posiziona la mina


                    // incrementa il numero di mine adiacenti
                    incrementaNumeroMineVicine(x, y);
                    minePos++;
                }
            }
        }

        public void incrementaNumeroMineVicine(int xIn, int yIn)
        {

            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    if (x != 0 || y != 0)
                    {
                        int Posx = xIn + x;
                        int Posy = yIn + y;

                        if (controlloBordi(Posx, Posy))
                        {
                            if (campo[Posx, Posy] != -2)// se la cella non è una mina
                            {
                                campo[Posx, Posy]++;
                            } 

                        }
                    }
                }
            }
        }

        private void dtg_campo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(e)
            // prende le coordinate della cella cliccata
            int riga = e.RowIndex;
            int colonna = e.ColumnIndex;
            dtg_campo.ClearSelection();
            if (campo[colonna, riga] != -1){

                if (campo[colonna, riga] > 0)
                {
                    dtg_campo.Rows[riga].Cells[colonna].Value = campo[colonna, riga]; // fa vedere all'utente il valore
                    dtg_campo.Rows[riga].Cells[colonna].Style.BackColor = Color.White;
                }
                else if (campo[colonna, riga] == -2)
                {
                    // partita persa
                }
                else
                {
                    // implementare funzione per scoprire tutte le celle fino a quando non trova quelle con i numeri, flood fill
                    IndicaCelleVuote(colonna, riga);
                }

            }
        }

        public void IndicaCelleVuote(int xIn, int yIn)
        {
            if(controlloBordi(xIn, yIn))
            {
                if (campo[xIn, yIn] == 0)
                {

                    dtg_campo.Rows[yIn].Cells[xIn].Style.BackColor = Color.White; // cambia il colore della cella in bianco
                    campo[xIn, yIn] = -1; // rende la cella scoperta

                    IndicaCelleVuote(xIn + 1, yIn); // cella a destra
                    IndicaCelleVuote(xIn - 1, yIn); // cella a sinistra
                    IndicaCelleVuote(xIn, yIn + 1); // cella sotto
                    IndicaCelleVuote(xIn, yIn - 1); // cella sopra

                }
                else if (campo[xIn, yIn] > 0)
                {
                    dtg_campo.Rows[yIn].Cells[xIn].Style.BackColor = Color.White; // cambia il colore della cella in bianco
                    dtg_campo.Rows[yIn].Cells[xIn].Value = campo[xIn, yIn];
                    
                    campo[xIn, yIn] -= 10; // rende la cella scoperta e non cliccabile
                }
            }
        }

        public bool controlloBordi(int xIn, int yIn)
        {
            if (xIn < 0 || xIn >= lughezzaLato || yIn < 0 || yIn >= lughezzaLato)
            {
                return false; // esci dalla funzione se le coordinate sono fuori dai bordi
            }
            else
            {
                return true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}

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

        int BandiereGiuste = 0;
        int BandiereSbagliate = 0;

        //  0   ->  Cella vuota non scoperta
        //  1 a 8 ->  Cella con numero di mine adiacenti coperto
        //  10    ->  Cella vuota scoperta
        //  10 a 18  ->  Cella NON coperta
        //  -1   ->  Cella con mina nascosta

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
                    dtg_campo.Rows[i].Cells[j].Value = " ";
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
                if (campo[x, y] != -1) // se la cella non è già occupata da una mina
                {
                    campo[x, y] = -1; // posiziona la mina


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
                            if (campo[Posx, Posy] != -1)// se la cella non è una mina
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
            /*
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

            }*/
        }

        public void IndicaCelleVuote(int xIn, int yIn)
        {
            if(controlloBordi(xIn, yIn))
            {
                if (campo[xIn, yIn] == 0)
                {

                    dtg_campo.Rows[yIn].Cells[xIn].Style.BackColor = Color.White; // cambia il colore della cella in bianco
                    campo[xIn, yIn] = 10; // rende la cella scoperta

                    IndicaCelleVuote(xIn + 1, yIn); // cella a destra
                    IndicaCelleVuote(xIn - 1, yIn); // cella a sinistra
                    IndicaCelleVuote(xIn, yIn + 1); // cella sotto
                    IndicaCelleVuote(xIn, yIn - 1); // cella sopra

                    IndicaCelleVuote(xIn + 1, yIn+1); // cella in alto a destra
                    IndicaCelleVuote(xIn - 1, yIn+1); // cella in alto a sinistra
                    IndicaCelleVuote(xIn+1, yIn -1); // cella in basso a destra
                    IndicaCelleVuote(xIn-1, yIn - 1); // cella in basso a sinistra


                }
                else if (campo[xIn, yIn] > 0 && campo[xIn, yIn]<10)
                {
                    dtg_campo.Rows[yIn].Cells[xIn].Style.BackColor = Color.White; // cambia il colore della cella in bianco
                    dtg_campo.Rows[yIn].Cells[xIn].Value = campo[xIn, yIn];
                    
                    campo[xIn, yIn] += 10; // rende la cella scoperta e non cliccabile
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

        private void dtg_campo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int riga = e.RowIndex;
            int colonna = e.ColumnIndex;

            if (e.Button == MouseButtons.Right)// CLICK DESTRO PER BANDIERE
            {
                if (campo[colonna, riga] == -1) // se c'e la mina dove hai fatto click destro
                {
                    if (dtg_campo.Rows[riga].Cells[colonna].Value == " ") // messa bandiera su mina nascosta
                    {
                        dtg_campo.Rows[riga].Cells[colonna].Value = "🚩";
                        BandiereGiuste++;
                    }
                    else if (dtg_campo.Rows[riga].Cells[colonna].Value == "🚩") // tolto bandiera su mina nascosta
                    {
                        dtg_campo.Rows[riga].Cells[colonna].Value = " ";
                        BandiereGiuste--;
                    }
                }
                else if (campo[colonna, riga] >= 0 && campo[colonna, riga] <= 8)
                {
                    if (dtg_campo.Rows[riga].Cells[colonna].Value == " ") // messa bandiera su una cella nascosta che non ha una mina
                    {
                        dtg_campo.Rows[riga].Cells[colonna].Value = "🚩";
                        BandiereSbagliate++;
                    }
                    else if (dtg_campo.Rows[riga].Cells[colonna].Value == "🚩") // tolta bandiera su una cella nascosta che non ha una mina
                    {
                        dtg_campo.Rows[riga].Cells[colonna].Value = " ";
                        BandiereSbagliate--;
                    }
                }
                MessageBox.Show("Giuste: " + BandiereGiuste.ToString() + "\nSbagliate: " + BandiereSbagliate.ToString());
                return;
            }

            dtg_campo.ClearSelection(); // togli la selezione cosi la cella cliccata non rimane blu

            if (dtg_campo.Rows[riga].Cells[colonna].Value == "🚩") // Se fai click sinistro su una bandiera ferma subito la funzione 
            {
                return;
            }

            if (!(campo[colonna, riga] >= 10))
            {

                if (campo[colonna, riga] > 0 && campo[colonna, riga] < 9)
                {
                    dtg_campo.Rows[riga].Cells[colonna].Value = campo[colonna, riga]; // fa vedere all'utente il valore
                    dtg_campo.Rows[riga].Cells[colonna].Style.BackColor = Color.White;
                }
                else if (campo[colonna, riga] == -1)
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
    }
}

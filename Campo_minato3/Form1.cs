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
        public bool chiudiForm { get; private set; } = false; // per chiudere la funestra iniziale se l'utente non seleziona una difficoltà

        int lughezzaLato;
        int altezzaLato = 1;
        int Nmine;

        bool partitaPersa = false;

        Cmina[] mine;
        List<Cbandiere> bandiere = new List<Cbandiere>();

        int[,] campo;

        int Nbandiere = 0; // numero di bandiere messe

        bool fattoClickIniziale = false;

        int BlocchiScoperti = 0;

        //  0   ->  Cella vuota non scoperta
        //  1 a 8 ->  Cella con numero di mine adiacenti coperto
        //  10    ->  Cella vuota scoperta
        //  11 a 18  ->  Cella con numero scoperta
        //  -1   ->  Cella con mina nascosta
        // quando si mette una bandiera si fa valore presente -10

        public Form1()
        {
            InitializeComponent();

            inizio();
        }

        public void inizio()
        {
            if (PrendiDifficolta())
            {
                chiudiForm = true;
                this.Close(); // chiude Form1
                return;
            }

            fattoClickIniziale = false;
            partitaPersa = false;
            Nbandiere = 0; // numero di bandiere messe
            BlocchiScoperti = 0;

            Array.Clear(campo, 0, campo.Length); // imposta tutto a 0
            bandiere.Clear(); // svuota la lista delle bandiere


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
            for (int i = 0; i < altezzaLato; i++)
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
            dtg_campo.Height = (int)25 * altezzaLato + 2;
            dtg_campo.Width = (int)25 * lughezzaLato + 2;
            
            // ridimensiona la grandezza della finestra in base alla grandezza del datagridview
            this.ClientSize = new Size(dtg_campo.Width + 40, dtg_campo.Height + 120); // +20 per il bordo della finestra
            
            // centra il datagridview nella finestra
            dtg_campo.Location = new Point((this.ClientSize.Width - dtg_campo.Width) / 2, (this.ClientSize.Height - dtg_campo.Height) / 2 + 50);
            pnl_titolo.Location = new Point((this.ClientSize.Width - pnl_titolo.Width) / 2, 10); // centra il pannello del titolo nella finestra
        }

        public bool PrendiDifficolta()
        {
            finestra_iniziale finestra = new finestra_iniziale();

            if (finestra.ShowDialog() == DialogResult.OK)
            {
                lughezzaLato = finestra.lunghezza_latoIn; // prendi il numero di celle per lato
                altezzaLato = finestra.altezza_latoIn;
                Nmine = finestra.NmineIn; // prendi il numero di mine

                campo = new int[lughezzaLato, altezzaLato]; // crea il campo di gioco
                mine = new Cmina[Nmine]; // crea l'array delle mine

                lbl_nMine.Text = $"{Nmine}"; // mostra il numero di mine nella label

                return false;
            }

            return true;
        }

        public void posizionaMine()
        {
            Random random = new Random(Environment.TickCount);
            int minePos = 0;
            while (minePos < Nmine)
            {
                int x = random.Next(0, lughezzaLato);
                int y = random.Next(0, altezzaLato);
                if (campo[x, y] != -1 && controlloClick(x,y)) // se la cella non è già occupata da una mina
                {
                    campo[x, y] = -1; // posiziona la mina
                    mine[minePos] = new Cmina(x, y);


                    // incrementa il numero di mine adiacenti
                    incrementaNumeroMineVicine(x, y);
                    minePos++;
                }
            }
        }

        public bool controlloClick(int xIn, int yIn) 
        {
            // falso = non mettere
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    int Posx = xIn + x;
                    int Posy = yIn + y;

                    if (controlloBordi(Posx, Posy))
                    {
                        if (campo[Posx, Posy] == 100)// se la cella non è una mina
                        {
                            return false;
                        }

                    }
                }
            }
            return true;
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

        public void IndicaCelleVuote(int xIn, int yIn)
        {
            if(controlloBordi(xIn, yIn))
            {
                

                if (campo[xIn, yIn] == 0)
                {
                    BlocchiScoperti++;
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
                    BlocchiScoperti++;
                    dtg_campo.Rows[yIn].Cells[xIn].Style.BackColor = Color.White; // cambia il colore della cella in bianco
                    dtg_campo.Rows[yIn].Cells[xIn].Value = campo[xIn, yIn];
                    
                    campo[xIn, yIn] += 10; // rende la cella scoperta e non cliccabile
                }
            }
        }

        public bool controlloBordi(int xIn, int yIn)
        {
            if (xIn < 0 || xIn >= lughezzaLato || yIn < 0 || yIn >= altezzaLato)
            {
                return false; // esci dalla funzione se le coordinate sono fuori dai bordi
            }
            else
            {
                return true;
            }
        }

        private void dtg_campo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int riga = e.RowIndex;
            int colonna = e.ColumnIndex;

            if (e.Button == MouseButtons.Middle) { // per debug: fa vedere il contenuto della cella nell'array
                MessageBox.Show(campo[colonna, riga].ToString());
                return;
            }

            if (!fattoClickIniziale && e.Button==MouseButtons.Left)
            {
                campo[colonna, riga] = 100; // do il valore 100 per indicare il click iniziale per i controlli su posizionaMine()
                posizionaMine();
                campo[colonna, riga] = 0; // rimetto come cella vuota
                fattoClickIniziale = true;
            }

            if (e.Button == MouseButtons.Right)// CLICK DESTRO PER BANDIERE
            {
                if (campo[colonna, riga] == -1) // se c'e la mina dove hai fatto click destro
                {
                    Nbandiere += ControlloBandiere(riga, colonna, true);
                }
                else if ((campo[colonna, riga] >= 0 && campo[colonna, riga] <= 8) || campo[colonna, riga] < -1)
                {
                    Nbandiere += ControlloBandiere(riga, colonna, false);
                }
                lbl_nMine.Text = $"{Nmine - Nbandiere}"; // aggiorna il numero di mine rimaste
                //MessageBox.Show("Giuste: " + BandiereGiuste.ToString() + "\nSbagliate: " + BandiereSbagliate.ToString());
                return;
            }

            dtg_campo.ClearSelection(); // togli la selezione cosi la cella cliccata non rimane blu

            if (campo[colonna, riga] < -1) // Se fai click sinistro su una bandiera ferma subito la funzione 
            {
                return;
            }

            if (campo[colonna, riga] <= 8 && campo[colonna, riga] >= -1 && !partitaPersa)
            {

                if (campo[colonna, riga] > 0 && campo[colonna, riga] < 9)
                {
                    dtg_campo.Rows[riga].Cells[colonna].Value = campo[colonna, riga]; // fa vedere all'utente il valore
                    dtg_campo.Rows[riga].Cells[colonna].Style.BackColor = Color.White;
                    IndicaCelleVuote(colonna, riga);
                }
                else if (campo[colonna, riga] == -1)
                {
                    finePartita(); // partita persa
                }
                else if (campo[colonna, riga] > 10 && !partitaPersa)
                {
                    int valore = campo[colonna, riga] - 10; // prendi il valore della cella senza il 10
                }
                else
                {
                    // implementare funzione per scoprire tutte le celle fino a quando non trova quelle con i numeri, flood fill
                    IndicaCelleVuote(colonna, riga);
                }

            }

            if(BlocchiScoperti==lughezzaLato* altezzaLato - Nmine)
            {
                MessageBox.Show("Hai vinto!");
                inizio();
            }
        }

        public int ControlloBandiere(int riga, int colonna, bool bandieraGiusta)
        {
            if (campo[colonna, riga] >= -1 && campo[colonna, riga] <= 8 && Nbandiere < Nmine) // messa bandiera su mina nascosta
            {
                dtg_campo.Rows[riga].Cells[colonna].Value = "🚩";
                campo [colonna, riga] -= 10; // metti bandiera e cambia il valore della cella
                bandiere.Add(new Cbandiere(colonna, riga, bandieraGiusta)); // aggiungi la bandiera alla lista
                return 1;
            }
            else if (campo[colonna, riga] < -1) // tolto bandiera su mina nascosta
            {
                dtg_campo.Rows[riga].Cells[colonna].Value = " ";
                campo[colonna, riga] += 10; // togli bandiera e cambia il valore della cella
                bandiere.RemoveAll(b => b.posX == colonna && b.posY == riga); // rimuovi la bandiera dalla lista
                return -1;
            }

            return 0;
        }

        public void finePartita()
        {
            foreach (var parametro in mine)
            {
                dtg_campo.Rows[parametro.posY].Cells[parametro.posX].Value = "💣"; // mostra la mina
                dtg_campo.Rows[parametro.posY].Cells[parametro.posX].Style.BackColor = Color.Red; // cambia il colore della cella in rosso
            }

            foreach (var parametro in bandiere)
            {
                if (!parametro.postoGiusto)
                {
                    dtg_campo.Rows[parametro.posY].Cells[parametro.posX].Value = "❌"; // mostra la bandiera sbagliata
                }
                else
                {
                    dtg_campo.Rows[parametro.posY].Cells[parametro.posX].Value = "🚩"; // mostra la bandiera giusta
                }
            }

            partitaPersa = true;

            MessageBox.Show("Hai perso!"); // mostra il messaggio di fine partita


            // ricomincia da capo
            inizio();
        }
        
        public int contaBandiereAdiacenti(int xIn, int yIn)
        {
            int bandiere = 0;
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
                            //MessageBox.Show(campo[Posx, Posy].ToString());
                            if (campo[Posx, Posy]==-11 || (campo[Posx,Posy]<=-2 && campo[Posx, Posy] >= -9)) // se la cella è una bandiera
                            {
                                bandiere++;
                            }

                        }
                    }
                }
            }
            return bandiere;
        }

        private void lbl_titolo_Click(object sender, EventArgs e)
        {

        }

        private void lbl_nMineTitolo_Click(object sender, EventArgs e)
        {

        }

        private void lbl_nMine_Click(object sender, EventArgs e)
        {

        }

        private void pnl_titolo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dtg_campo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dtg_campo.ClearSelection(); // togli la selezione cosi la cella cliccata non rimane blu
            int riga = e.RowIndex;
            int colonna = e.ColumnIndex;

            //MessageBox.Show(contaBandiereAdiacenti(colonna, riga).ToString() + ", " + (campo[colonna, riga] - 10));

            if (campo[colonna, riga] >= 11 && campo[colonna, riga] <= 18 && contaBandiereAdiacenti(colonna,riga) == campo[colonna, riga] - 10)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        
                        if (x != 0 || y != 0)
                        {
                            int Posx = colonna + x;
                            int Posy = riga + y;
                            if (controlloBordi(Posx, Posy))
                            {
                                if(!((campo[Posx, Posy] <= -2 && campo[Posx, Posy] >= -9) || campo[Posx, Posy]==-11))
                                {

                                    if (campo[Posx, Posy] <= 8 && campo[Posx, Posy] >= -1 && !partitaPersa)
                                    {

                                        if (campo[Posx, Posy] > 0 && campo[Posx, Posy] < 9)
                                        {
                                            dtg_campo.Rows[Posy].Cells[Posx].Value = campo[Posx, Posy]; // fa vedere all'utente il valore
                                            dtg_campo.Rows[Posy].Cells[Posx].Style.BackColor = Color.White;
                                            IndicaCelleVuote(Posx, Posy);
                                        }
                                        else if (campo[Posx, Posy] == -1)
                                        {
                                            finePartita(); // partita persa
                                            return;
                                        }
                                        else
                                        {
                                            // implementare funzione per scoprire tutte le celle fino a quando non trova quelle con i numeri, flood fill
                                            IndicaCelleVuote(Posx, Posy);
                                        }

                                    }

                                    if (BlocchiScoperti == lughezzaLato * altezzaLato - Nmine)
                                    {
                                        MessageBox.Show("Hai vinto!");
                                        inizio();
                                        return;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            
        }
    }
}

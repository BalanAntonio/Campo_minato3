using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.IO;

namespace Campo_minato3
{
    public partial class Form1 : Form
    {
        
        public bool chiudiForm { get; private set; } = false; // per chiudere la funestra iniziale se l'utente non seleziona una difficoltà

        Timer cronometro;
        int secondi = 0;
        int migliorTempo; // per il miglior tempo, viene letto dal file PunteggioMax.csv

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

        SoundPlayer[] souni;

        CTema tema;

        //  0   ->  Cella vuota non scoperta
        //  1 a 8 ->  Cella con numero di mine adiacenti coperto
        //  10    ->  Cella vuota scoperta
        //  11 a 18  ->  Cella con numero scoperta
        //  -1   ->  Cella con mina nascosta
        // quando si mette una bandiera si fa valore presente -10

        public Form1()
        {
            InitializeComponent();

            inizio("Benvenuto al campo minato", "--");
        }

        private void inizio(string messaggio, string punteggio)
        {
            if (PrendiDifficolta(messaggio, punteggio))
            {
                chiudiForm = true;
                this.Close(); // chiude Form1
                return;
            }

            // resetta il tempo
            lbl_tempo.Text = "0 s";
            secondi = 0;

            
            // inizia a contare il tempo
            cronometro = new Timer();
            cronometro.Interval = 1000; // ogni secondo
            cronometro.Tick += contaTempo;
            cronometro.Start();

            // resetta variabili
            fattoClickIniziale = false;
            partitaPersa = false;
            Nbandiere = 0; // numero di bandiere messe
            BlocchiScoperti = 0;

            // resetta liste e array
            Array.Clear(campo, 0, campo.Length); // imposta tutto a 0
            bandiere.Clear(); // svuota la lista delle bandiere


            creaCelle();
            riordinaGrandezze();
        }

        private void contaTempo(object sender, EventArgs e)
        {
            secondi++; // aumenta il conteggio del tempo

            //lbl_tempo.Invoke((MethodInvoker)(() => lbl_tempo.Text = $"{secondi} s"));
            lbl_tempo.Text = $"{secondi} s";
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

                btnColumn.DefaultCellStyle.Font = tema.font;


                //btnColumn.DefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);

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
                    dtg_campo.Rows[i].Cells[j].Style.BackColor = tema.colori[8];
                    dtg_campo.Rows[i].Cells[j].Value = " ";
                    dtg_campo.Rows[i].Cells[j].Style.ForeColor = tema.colori[11];
                }
            }

            // rende non visibili prima colonna e la prina riga che sono inutili per il nostro gioco
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
            // da font alle label
            lbl_titolo.Font = tema.font;
            lbl_tempo.Font = tema.font;
            lbl_nMine.Font = tema.font;
            lbl_nMineTitolo.Font = tema.font;
            lbl_tempoTitolo.Font = tema.font;

            // ridimensiona la grandezza del datagridview in base alla grandezza delle celle
            dtg_campo.Height = (int)25 * altezzaLato + 2;
            dtg_campo.Width = (int)25 * lughezzaLato + 2;
            
            // ridimensiona la grandezza della finestra in base alla grandezza del datagridview
            this.ClientSize = new Size(dtg_campo.Width + 40, dtg_campo.Height + 120); // +20 per il bordo della finestra
            
            // centra il datagridview nella finestra
            dtg_campo.Location = new Point((this.ClientSize.Width - dtg_campo.Width) / 2, (this.ClientSize.Height - dtg_campo.Height) / 2 + 50);
            pnl_titolo.Location = new Point((this.ClientSize.Width - pnl_titolo.Width) / 2, 10); // centra il pannello del titolo nella finestra

        }

        public bool PrendiDifficolta(string messaggio, string punteggio)
        {
            finestra_iniziale finestra = new finestra_iniziale(messaggio, punteggio);

            if (finestra.ShowDialog() == DialogResult.OK)
            {
                lughezzaLato = finestra.lunghezza_latoIn; // prendi il numero di celle per lato
                altezzaLato = finestra.altezza_latoIn;
                Nmine = finestra.NmineIn; // prendi il numero di mine
                migliorTempo = finestra.migliorPunteggioPubblico;

                campo = new int[lughezzaLato, altezzaLato]; // crea il campo di gioco
                mine = new Cmina[Nmine]; // crea l'array delle mine

                lbl_nMine.Text = $"{Nmine}"; // mostra il numero di mine nella label
                tema = finestra.tema;
                souni = tema.Suoni;
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
                if (campo[x, y] != -1 && controlloClick(x,y)) // se la cella non è già occupata da una mina e non è il click iniziale
                {
                    campo[x, y] = -1; // posiziona la mina
                    mine[minePos] = new Cmina(x, y);


                    // incrementa il numero di mine adiacenti
                    incrementaNumeroMineVicine(x, y);
                    minePos++;
                }
            }
        }


        // le prossime due funzioni sono sono molto simili, ma abbiamo deciso di tenerle separate per chiarezza
        public bool controlloClick(int xIn, int yIn) 
        {
            // falso = non mettere
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    int Posx = xIn + x;
                    int Posy = yIn + y;

                    if (controlloBordi(Posx, Posy) && campo[Posx, Posy] == 100) // se la cella è il click iniziale
                    {
                        return false; // se la cella è il click iniziale non mettere la mina

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

        public void IndicaCelleVuote(int xIn, int yIn) // flood fill
        {
            if(controlloBordi(xIn, yIn))
            {
                if (campo[xIn, yIn] == 0)
                {
                    BlocchiScoperti++;
                    dtg_campo.Rows[yIn].Cells[xIn].Style.BackColor = tema.colori[9]; // cambia il colore
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
                    dtg_campo.Rows[yIn].Cells[xIn].Style.BackColor = tema.colori[9];
                    dtg_campo.Rows[yIn].Cells[xIn].Style.ForeColor = tema.colori[campo[xIn, yIn]];
                    dtg_campo.Rows[yIn].Cells[xIn].Value = campo[xIn, yIn];
                    
                    campo[xIn, yIn] += 10; // rende la cella scoperta e non cliccabile
                }
            }
        }

        public bool controlloBordi(int xIn, int yIn)
        {
            if (xIn < 0 || xIn >= lughezzaLato || yIn < 0 || yIn >= altezzaLato)
            {
                return false; // esci dalla funzione se le coordinate sono fuori dai bordi e rorna false
            }
            else
            {
                return true; // le coordinate sono valide, ritorna true
            }
        }

        private void dtg_campo_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int riga = e.RowIndex;
            int colonna = e.ColumnIndex;

            /*
            if (e.Button == MouseButtons.Middle) { // per debug: fa vedere il contenuto della cella nell'array
                MessageBox.Show(campo[colonna, riga].ToString());
                return;
            }
            */

            if (!fattoClickIniziale && e.Button==MouseButtons.Left)
            {
                campo[colonna, riga] = 100; // do il valore 100 per indicare il click iniziale per i controlli su posizionaMine()
                posizionaMine();
                campo[colonna, riga] = 0; // rimetto come cella vuota
                fattoClickIniziale = true;
            } else if (!fattoClickIniziale)
            {
                MessageBox.Show("Devi prima cliccare con il tasto sinistro su una cella per iniziare la partita!");
                return;
            }

            if (e.Button == MouseButtons.Right)// CLICK DESTRO PER BANDIERE
            {

                if (campo[colonna, riga] == -1) // se c'e la mina dove hai fatto click destro
                {
                    souni[1].Play(); // suono bandiera

                    // metti bandiera e cambia il valore della cella, inoltre aumenta / diminuisce il numero di bandiere
                    Nbandiere += ControlloBandiere(riga, colonna, true);
                }
                else if ((campo[colonna, riga] >= 0 && campo[colonna, riga] <= 8) || campo[colonna, riga] < -1)
                {
                    souni[1].Play(); // suono bandiera
                    // metti bandiera e cambia il valore della cella, inoltre aumenta / diminuisce il numero di bandiere
                    Nbandiere += ControlloBandiere(riga, colonna, false);
                }
                lbl_nMine.Text = $"{Nmine - Nbandiere}"; // aggiorna il numero di mine rimaste
                //MessageBox.Show("Giuste: " + BandiereGiuste.ToString() + "\nSbagliate: " + BandiereSbagliate.ToString());
                return;
            }

            if (e.Button != MouseButtons.Left) // se non è il click sinistro esci dalla funzione
            {
                return;
            }

            dtg_campo.ClearSelection(); // togli la selezione cosi la cella cliccata non rimane blu

            if (campo[colonna, riga] < -1) // Se fai click sinistro su una bandiera ferma subito la funzione 
            {
                return;
            }

            // arrivati a questo punto significa che si è fatto click sinistro su una cella coperta, quindi si scopre la cella
            souni[3].Play(); // suono cliccato
            cellaCliccata(colonna, riga);
        }

        public int ControlloBandiere(int riga, int colonna, bool bandieraGiusta)
        {
            if (campo[colonna, riga] >= -1 && campo[colonna, riga] <= 8 && Nbandiere < Nmine) // se non c'è ancora una bandiera e non si è superato il numero di mine
            {
                dtg_campo.Rows[riga].Cells[colonna].Value = tema.bandiera; // mostra la bandiera
                campo [colonna, riga] -= 10; // metti bandiera e cambia il valore della cella
                bandiere.Add(new Cbandiere(colonna, riga, bandieraGiusta)); // aggiungi la bandiera alla lista
                return 1;
            }
            else if (campo[colonna, riga] < -1) // tolto bandiera su mina nascosta
            {
                dtg_campo.Rows[riga].Cells[colonna].Value = " "; // rimuovi la bandiera dalla cella
                campo[colonna, riga] += 10; // togli bandiera e cambia il valore della cella che rotorna al valore originale
                bandiere.RemoveAll(b => b.posX == colonna && b.posY == riga); // rimuovi la bandiera dalla lista
                return -1;
            }

            return 0;
        }

        public void finePartitaPersa()
        {
            // indica la posizione delle mina
            foreach (var parametro in mine)
            {
                dtg_campo.Rows[parametro.posY].Cells[parametro.posX].Value = tema.bomba; // mostra la mina
                dtg_campo.Rows[parametro.posY].Cells[parametro.posX].Style.BackColor = tema.colori[10]; // cambia il colore della cella in rosso
            }

            // indica la posizione delle bandiere sbagliate con una X
            foreach (var parametro in bandiere)
            {
                if (!parametro.postoGiusto)
                {
                    dtg_campo.Rows[parametro.posY].Cells[parametro.posX].Value = "❌"; // mostra la bandiera sbagliata
                }
                else
                {
                    dtg_campo.Rows[parametro.posY].Cells[parametro.posX].Value = tema.bandiera; // mostra la bandiera giusta
                }
            }

            // funzioni vitali per la fine della partita
            partitaPersa = true; // non permette di scoprire altre celle
            souni[0].Play(); // suono di fine partita persa
            cronometro.Stop(); // ferma il cronometro

            // ricomincia da capo richiamando la funzione inizio
            inizio("Hai perso!!!", "--");
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
                            if (campo[Posx, Posy] < -1) // se la cella è una bandiera
                            {
                                bandiere++;
                            }

                        }
                    }
                }
            }
            return bandiere;
        }

        // se l'utente fa doppio click su una cella scoperta con un numero pari al numero di bandiere adiacenti, si scoprono tutte le celle adiacenti che non sono bandiere
        private void dtg_campo_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dtg_campo.ClearSelection(); // togli la selezione cosi la cella cliccata non rimane blu
            int riga = e.RowIndex; // prendi la riga della cella cliccata
            int colonna = e.ColumnIndex; // prendi la colonna della cella cliccata

            //MessageBox.Show(contaBandiereAdiacenti(colonna, riga).ToString() + ", " + (campo[colonna, riga] - 10));

            if (campo[colonna, riga] >= 11 && campo[colonna, riga] <= 18 && contaBandiereAdiacenti(colonna,riga) == campo[colonna, riga] - 10) // se la cella è scoperta e il numero di bandiere adiacenti è uguale al numero della cella
            {
                souni[2].Play(); // suono di doppio cliccato

                // scopri tutte le celle adiacenti che non sono bandiere
                for (int y = -1; y <= 1; y++)
                {
                    for (int x = -1; x <= 1; x++)
                    {
                        
                        if (x != 0 || y != 0) // per non controllare la cella stessa
                        {
                            int Posx = colonna + x;
                            int Posy = riga + y;

                            if (controlloBordi(Posx, Posy)) // se le coordinate sono nei bordi del campo
                            {
                                if(!(campo[Posx, Posy] < -1)) // se la cella non è una bandiera
                                {
                                    if(cellaCliccata(Posx, Posy)) // se la funzione ritorna true vuol dire che la partita è finita
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            
        }

        public bool cellaCliccata(int Posx, int Posy)
        {

            if (campo[Posx, Posy] <= 8 && campo[Posx, Posy] >= -1 && !partitaPersa) // se la cella è coperta e la partita non è persa
            {

                if (campo[Posx, Posy] > 0 && campo[Posx, Posy] < 9) // se la cella è coperta e contiene un numeri, NON una mina
                {
                    dtg_campo.Rows[Posy].Cells[Posx].Value = campo[Posx, Posy]; // fa vedere all'utente il numero
                    dtg_campo.Rows[Posy].Cells[Posx].Style.BackColor = tema.colori[9]; // colore sfondo
                    dtg_campo.Rows[Posy].Cells[Posx].Style.ForeColor = tema.colori[campo[Posx, Posy]]; // colore numero

                    // applica la funzione flood fill per scoprire le celle vuote adiacenti, però in queste cirsostanze verrà scoperta solo la cella cliccata
                    IndicaCelleVuote(Posx, Posy);
                }
                else if (campo[Posx, Posy] == -1)
                {
                    finePartitaPersa(); // partita persa
                    return true;
                }
                else
                {
                    // implementare funzione per scoprire tutte le celle fino a quando non trova quelle con i numeri, flood fill
                    IndicaCelleVuote(Posx, Posy);
                }

            }
            
            if (BlocchiScoperti == lughezzaLato * altezzaLato - Nmine) // se sono state scoperte tutte le celle tranne le mine
            {
                cronometro.Stop(); // ferma il cronometro
                souni[4].Play(); // suono di vittoria

                migliorPunteggio(); // controlla se il punteggio è migliore del precedente

                inizio("Hai vinto!!!", $"{secondi}");
                return true;
            }

            return false;

        }

        public void migliorPunteggio()
        {
            if (migliorTempo > secondi) // se il tempo attuale è migliore del miglior tempo
            {
                try // permette di far andare avanti il programma anche se si trova un errore
                {
                    using (StreamWriter sw = new StreamWriter(@"tempoMin.csv")) // apre il file
                    {
                        sw.WriteLine($"{secondi}"); // sovrascrive il punteggio
                    }
                }
                catch // se c'è un errore durante la scrittura del file
                {
                    MessageBox.Show("errore");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
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
        private int migliorPunteggioPrivato;
        public int migliorPunteggioPubblico
        {
            get
            {
                return migliorPunteggioPrivato;
            }
        }
        public CTema tema { get; set; }
        List<CTema> temiPrivati = new List<CTema>();

        public finestra_iniziale(string messaggio, string punteggio)
        {
            InitializeComponent();

            lbl_titolo.Text = messaggio;
            lbl_titolo.Location = new Point((this.ClientSize.Width - lbl_titolo.Width) / 2, lbl_titolo.Location.Y); // Centra il titolo

            lbl_tempoCorrente.Text = $"{punteggio} s";


            migliorTempo();

            leggiTemi();

            cmb_difficolta.Items.Add("Facile");
            cmb_difficolta.Items.Add("Medio");
            cmb_difficolta.Items.Add("Difficile");

            cmb_difficolta.SelectedIndex = 0;


            cmb_tema.SelectedIndex = 0;
        }

        public void migliorTempo()
        {
            try // permette di far andare avanti il programma anche se si trova un errore
            {
                // apre il file e prende il valore del miglior punteggio
                using (StreamReader sr = new StreamReader(@"tempoMin.csv"))
                {
                    string riga = sr.ReadLine(); // legge la riga

                    if (int.TryParse(riga, out migliorPunteggioPrivato)) // cerca di trasformarlo in intero e se ci riesce assegna il valore a migliorPunteggio
                    {
                        lbl_migliorTempo.Text = $"{migliorPunteggioPrivato} s";  // il miglior punteggio viene scritto nel form
                    }
                }
            }
            catch
            {
                MessageBox.Show("errore");
            }
        }

        public void leggiTemi()
        {

            try // permette di far andare avanti il programma anche se si trova un errore
            {
                bool inizio = true; // variabile per verificare se è la prima riga del file
                using (StreamReader sr = new StreamReader(@"temi.csv")) // apre il file
                {
                    string riga;

                    while (!sr.EndOfStream)
                    {
                        riga = sr.ReadLine(); // legge la riga

                        if (inizio) // se non è la prima riga
                        {
                            riga = sr.ReadLine(); // legge la riga successiva
                            inizio = false; // non è più la prima riga
                        }


                        if (riga != null) // se la riga non è vuota
                        {
                            string[] campi = riga.Split(';'); // divide la riga in base al carattere ';'

                            if (campi.Length == 6)
                            {
                                // prende i colori
                                string[] coloreStringhe = campi[4].Split(',');
                                Color[] colori = coloreStringhe.Select(c => ColorTranslator.FromHtml(c)).ToArray();
                                // come fare:
                                // Color[] colori = new Color[coloreStringhe.Length];
                                // for (int i = 0; i < coloreStringhe.Length; i++)
                                // {
                                //     colori[i] = ColorTranslator.FromHtml(coloreStringhe[i]);
                                // }


                                // prende i suoni
                                string[] suonoStringhe = campi[5].Split(',');
                                SoundPlayer[] suoni = suonoStringhe.Select(nomeFile =>
                                    new SoundPlayer($"media/{nomeFile}") // path relativo o assoluto a tua scelta
                                ).ToArray();
                                //string[] suonoStringhe = campi[5].Split(',');
                                //SoundPlayer[] suoni = new SoundPlayer[suonoStringhe.Length];
                                //for (int i = 0; i < suonoStringhe.Length; i++)
                                //{
                                //    suoni[i] = prendeSuono(suonoStringhe[i].Trim()); // chiama la funzione prendeSuono per ogni suono
                                //}


                                // prende il font
                                string[] partiFont = campi[3].Split(',');

                                string nomeFont = partiFont[0];
                                float grandezzaFont = float.Parse(partiFont[1]);
                                FontStyle stile = FontStyle.Regular;
                                if (Enum.TryParse(partiFont[2], true, out FontStyle parsedStyle))
                                {
                                    stile = parsedStyle;
                                }

                                Font font = new Font(nomeFont, grandezzaFont, stile);

                                temiPrivati.Add(new CTema(campi[0], font, colori, campi[1], campi[2], suoni));
                                cmb_tema.Items.Add(campi[0]);
                            }
                        }
                    }

                }
            }
            catch
            {
                MessageBox.Show("errore");
            }
        }

        private void btn_inserisci_Click(object sender, EventArgs e)
        {
            string difficolta = cmb_difficolta.SelectedItem.ToString().ToLower();
            int nTema = cmb_tema.SelectedIndex;

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

            tema = temiPrivati[nTema]; // assegna il tema selezionato alla variabile tema
            Close();
        }

        private void btn_resetta_Click(object sender, EventArgs e)
        {

            migliorPunteggioPrivato = 2000;

            try // permette di far andare avanti il programma anche se si trova un errore
            {
                using (StreamWriter sw = new StreamWriter(@"tempoMin.csv")) // apre il file
                {
                    sw.WriteLine($"{migliorPunteggioPrivato}"); // sovrascrive il punteggio
                }
            }
            catch
            {
                MessageBox.Show("errore");
            }

            lbl_migliorTempo.Text = $"{migliorPunteggioPrivato}  s";  // il punteggio viene scritto nel form
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

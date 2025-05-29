using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private int migliorPunteggioPrivato;
        public int migliorPunteggioPubblico
        {
            get
            {
                return migliorPunteggioPrivato;
            }
        }
        public CTema tema { get; set; }

        //
        // LISTA DI TUTTI I TEMI DEL GIOCO
        //

        // NATALE

        Color[] ColoriNatale = new Color[]
        {
            Color.FromArgb(0, 0, 255),       // 1 - Blu
            Color.FromArgb(0, 128, 0),       // 2 - Verde
            Color.FromArgb(255, 0, 0),       // 3 - Rosso
            Color.FromArgb(0, 0, 139),       // 4 - Blu scuro
            Color.FromArgb(139, 69, 19),     // 5 - Marrone scuro
            Color.FromArgb(72, 209, 204),    // 6 - Ciano scuro
            Color.FromArgb(0, 0, 0),         // 7 - Nero
            Color.FromArgb(105, 105, 105),   // 8 - Grigio scuro
            Color.FromArgb(255,70,70),       // Cella non scoperta
            Color.FromArgb(200,255,200),     // Cella scoperta
            Color.FromArgb(50,50,255),       // Cella perdita
            Color.FromArgb(0,0,0)            // Colore default
        };
        Font fontnatale = new Font("Algerian", 14, FontStyle.Bold);
        CTema natale;

        // DEFAULT

        Color[] ColoriDefault = new Color[]
        {
            Color.FromArgb(0, 0, 255),       // 1 - Blu
            Color.FromArgb(0, 128, 0),       // 2 - Verde
            Color.FromArgb(255, 0, 0),       // 3 - Rosso
            Color.FromArgb(0, 0, 139),       // 4 - Blu scuro
            Color.FromArgb(139, 69, 19),     // 5 - Marrone scuro
            Color.FromArgb(72, 209, 204),    // 6 - Ciano scuro
            Color.FromArgb(0, 0, 0),         // 7 - Nero
            Color.FromArgb(105, 105, 105),   // 8 - Grigio scuro
            Color.FromArgb(170,170,170),     // Cella non scoperta
            Color.FromArgb(240,240,240),     // Cella scoperta
            Color.FromArgb(255,20,40),       // Cella perdita
            Color.FromArgb(0,0,0)            // Colore default
        };
        Font fontdefault = new Font("Arial", 14, FontStyle.Bold);
        CTema Classico;

        CTema[] temi;

        public finestra_iniziale(string messaggio, string punteggio)
        {
            InitializeComponent();

            lbl_titolo.Text = messaggio;
            lbl_titolo.Location = new Point((this.ClientSize.Width - lbl_titolo.Width) / 2, lbl_titolo.Location.Y); // Centra il titolo

            lbl_tempoCorrente.Text = $"{punteggio} s";


            migliorTempo();

            cmb_difficolta.Items.Add("Facile");
            cmb_difficolta.Items.Add("Medio");
            cmb_difficolta.Items.Add("Difficile");

            cmb_difficolta.SelectedIndex = 0;

            cmb_tema.Items.Add("Classico");
            cmb_tema.Items.Add("Natale");

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

            tema = temi[cmb_tema.SelectedIndex];

            DialogResult = DialogResult.OK;
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
            natale = new CTema(fontnatale, ColoriNatale, "🍬", "🎄");
            Classico = new CTema(fontdefault, ColoriDefault, "💣", "🏴");

            // LISTA DI TUTTI I TEMI
            temi = new CTema[]
            {
                Classico,
                natale
            };
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

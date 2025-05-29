using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;

namespace Campo_minato3
{
    public class CTema
    {
        public string nomeTema { get; set; }
        public Font font { get; set; }
        public Color[] colori { get; set; }
        public string bomba { get; set; }
        public string bandiera { get; set; }
        public SoundPlayer[] Suoni { get; set; }

        public CTema()
        {
            nomeTema = "Default";
            font = new Font(FontFamily.GenericSansSerif, 14.25f, FontStyle.Regular);

            colori = new Color[]
            {
                ColorTranslator.FromHtml("#0000FF"), // 1 - blu
                ColorTranslator.FromHtml("#008000"), // 2 - verde
                ColorTranslator.FromHtml("#FF0000"), // 3 - rosso
                ColorTranslator.FromHtml("#00008B"), // 4 - blu scuro
                ColorTranslator.FromHtml("#8B4513"), // 5 - marrone scuro
                ColorTranslator.FromHtml("#48D1CC"), // 6 - ciano scuro
                ColorTranslator.FromHtml("#000000"), // 7 - nero
                ColorTranslator.FromHtml("#696969"), // 8 - grigio scuro
                ColorTranslator.FromHtml("#AAAAAA"), // cella non scoperta
                ColorTranslator.FromHtml("#F0F0F0"), // cella scoperta
                ColorTranslator.FromHtml("#FF1428"), // cella perdita
                ColorTranslator.FromHtml("#000000")  // colore default
            };

            bomba = "💣";
            bandiera = "🚩";
        }

        public CTema( string nomeTema, Font font, Color[] colori, string bomba, string bandiera, SoundPlayer[] suoni)
        {
            this.nomeTema = nomeTema;
            this.font = font;
            this.colori = colori;
            this.bomba = bomba;
            this.bandiera = bandiera;
            this.Suoni = suoni;
        }
    }
}

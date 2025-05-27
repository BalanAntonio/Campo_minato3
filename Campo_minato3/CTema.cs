using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Campo_minato3
{
    internal class CTema
    {
        Color[] defaultC = new Color[]
        {
            Color.FromArgb(0, 0, 255),       // 1 - Blu
            Color.FromArgb(0, 128, 0),       // 2 - Verde
            Color.FromArgb(255, 0, 0),       // 3 - Rosso
            Color.FromArgb(0, 0, 139),       // 4 - Blu scuro
            Color.FromArgb(139, 69, 19),     // 5 - Marrone scuro
            Color.FromArgb(72, 209, 204),    // 6 - Ciano scuro
            Color.FromArgb(0, 0, 0),         // 7 - Nero
            Color.FromArgb(105, 105, 105)    // 8 - Grigio scuro
        };
        
        public Font font { get; set; }
        public Color[] colori { get; set; }
        public string bomba { get; set; }
        public string bandiera { get; set; }

        public CTema(Font font, Color[] colori, string bomba, string bandiera)
        {
            this.font = font;
            this.colori = colori;
            this.bomba = bomba;
            this.bandiera = bandiera;
        }

        public CTema()
        {
            font = new Font(FontFamily.GenericSansSerif,14.25f, FontStyle.Regular);
            colori = defaultC;
            bomba = "💣";
            bandiera = "🚩";
        }
    }
}

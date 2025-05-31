using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campo_minato3
{
    internal class Cmina
    {
        // Proprietà per le coordinate della mina
        public int posX { get; set; }
        public int posY { get; set; }

        // Costruttore di default
        public Cmina()
        {
            posX = 0;
            posY = 0;
        }

        // Costruttore con parametri
        public Cmina(int xIn, int yIn)
        {
            posX = xIn;
            posY = yIn;
        }
    }
}

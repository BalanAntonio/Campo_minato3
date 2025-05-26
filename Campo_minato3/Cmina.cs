using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campo_minato3
{
    internal class Cmina
    {
        public int posX { get; set; }
        public int posY { get; set; }

        public Cmina()
        {
            posX = 0;
            posY = 0;
        }

        public Cmina(int xIn, int yIn)
        {
            posX = xIn;
            posY = yIn;
        }
    }
}

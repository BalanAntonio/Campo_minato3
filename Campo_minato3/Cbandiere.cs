using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campo_minato3
{
    internal class Cbandiere
    {

        public int posX { get; set; }
        public int posY { get; set; }
        public bool postoGiusto { get; set; }

        public Cbandiere()
        {
            posX = 0;
            posY = 0;
            postoGiusto = false;
        }

        public Cbandiere(int xIn, int yIn, bool giustaIn)
        {
            posX = xIn;
            posY = yIn;
            postoGiusto = giustaIn;
        }
    }
}

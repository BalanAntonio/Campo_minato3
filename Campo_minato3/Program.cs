using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_minato3
{
    internal static class Program
    {
        //  0   ->  Cella vuota non scoperta
        // -1   ->  Cella vuota scoperta
        //  -2  ->  Cella con mina
        //  Altri numeri    ->  Numero celle adiacenti

        static void ScopriCaselle(int x, int y, int[,] Area)
        {

        }

        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}

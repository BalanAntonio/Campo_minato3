﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Campo_minato3
{
    internal static class Program
    {
        static void Main()
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            var form = new Form1();
            if (!form.chiudiForm)
            {
                Application.Run(form);
            }
        }
    }
}

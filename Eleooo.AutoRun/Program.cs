using System;
using System.Collections.Generic;
using System.Text;
using Eleooo.DAL;

namespace Eleooo.AutoRun
{
    class Program
    {
        static void Main(string[] args)
        {
            SP_.SpAutoProcessTakeAwayOutOfStockMenu( ).Execute( );
        }
    }
}

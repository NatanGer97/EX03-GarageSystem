using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.ConsoleUI
{
    // $G$ CSS-027 (-15) Spaces are not kept as required before return statement in all the solution.
    // $G$ DSN-999 (-20) Every file should contain only 1 class/enum (regarding all files)
    // $G$ CSS-027 (-15) Spaces are not kept as required after defying variables in all the solution.
    public class Program
    {
        public static void Main(string[] args)
        {
            GarageUi garageUi = new GarageUi();
            garageUi.Menu();
        }
    }
}

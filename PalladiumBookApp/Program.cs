using System;
using PalladiumBookApp.Models;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using PalladiumBookApp.Controllers;


namespace PalladiumBookApp
{
    class Program
    {



        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.Start();
        }
    }
}

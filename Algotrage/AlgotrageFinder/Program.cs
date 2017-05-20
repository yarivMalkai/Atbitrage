using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlgotrageDAL.Entities;
using System.Threading;

namespace AlgotrageFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbitrageManager man = new ArbitrageManager();

            while (true)
            {
                man.checkForUpdates();
                man.findArbitrage();

                Thread.Sleep(1000);
            }
        }
    }
}

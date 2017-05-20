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
            while (true)
            {
                ArbitrageManager man = new ArbitrageManager();

                man.checkForUpdates();
                man.findArbitrage();

                Thread.Sleep(1000);
            }
        }
    }
}

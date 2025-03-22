using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace battaglia
{
    internal class program
    {
        static void Main(string[] args)
        {
            Arena campo = new Arena();
            List<Giocatore> giocatori = new List<Giocatore>();
            for (int i = 0; i < 10; i++)
            {
                Giocatore G = new Giocatore("player" + (i + 1));
                giocatori.Add(G);
                Thread g = new Thread(G.Lotta);
                g.Start(campo);
            }
        }
    }
}

// ricorda che combattenti[0] è il difensore, mentre combattenti[1] è l'attaccante
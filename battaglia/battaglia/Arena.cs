using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battaglia
{
    internal class Arena
    {
        public List<Giocatore> combattenti = new List<Giocatore>();
        public Semaphore accessi = new Semaphore(2,2);
    }
}

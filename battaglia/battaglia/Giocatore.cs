using System;
using System.Threading;

namespace battaglia
{
    internal class Giocatore
    {
        public int PuntiFeritaTotali { get; private set; }
        public int PuntiFerita { get; private set; }
        public int DannoAttacco { get; private set; }
        public int PercentualeDifesa { get; private set; }
        public string Nome { get; private set; }
        private Random rnd = new Random();

        public Giocatore(string nome)
        {
            Nome = nome;
            PuntiFeritaTotali = rnd.Next(3000, 8000);
            PuntiFerita = PuntiFeritaTotali;
            DannoAttacco = rnd.Next(500, 2000);
            PercentualeDifesa = rnd.Next(1, 101);
        }

        public void Difendi(Giocatore attaccante)
        {
            int danno;
            if (attaccante.Attacca() <= PercentualeDifesa)
            {
                danno = attaccante.DannoAttacco * rnd.Next(50, 101) / 100;
            }
            else
            {
                danno = attaccante.DannoAttacco;
            }

            PuntiFerita -= danno;
            Console.WriteLine($"{attaccante.Nome} ha inflitto {danno} danni a {Nome}. Punti ferita rimanenti: {PuntiFerita}");

            if (PuntiFerita <= 0)
            {
                Console.WriteLine($"{Nome} è stato sconfitto!");
            }
        }

        public int Attacca()
        {
            return rnd.Next(10, 100);
        }

        public void Ripristino()
        {
            PuntiFerita = PuntiFeritaTotali;
        }

        public void Lotta(Arena arena)
        {
            Console.WriteLine($"{Nome} è pronto a combattere.");
            arena.EntraNellArena(this);
        }
    }
}

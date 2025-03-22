using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace battaglia
{
    internal class Giocatore
    {
        private int puntiFeritaTotali {  get; set; }
        private int puntiFerita { get; set; }
        private int dannoAttacco { get; set; }
        private int percentualeDifesa { get; set; }
        private string nome { get; set; }
        private Random rnd = new Random();

        public Giocatore(string nome)
        {
            this.nome = nome;
            this.puntiFerita = puntiFeritaTotali;
            this.puntiFeritaTotali = rnd.Next(3000, 8000);
            this.dannoAttacco = rnd.Next(500, 2000);
            this.percentualeDifesa = rnd.Next(0, 100);
        }

        public void Difendi(Giocatore attaccante)
        {
            int danno;
            if(attaccante.Attacca()<=percentualeDifesa)
            {
                danno = attaccante.dannoAttacco * (rnd.Next(50, 100) / 100);
                puntiFerita = puntiFerita - danno;
                Console.WriteLine(attaccante.nome + " ha inflitto " + danno + " danni a " + nome);
                Console.WriteLine(nome + " ha " + puntiFerita + " punti ferita rimanenti");
            }
            else
            {
                danno = attaccante.dannoAttacco;
                puntiFerita = puntiFerita - danno;
                Console.WriteLine(attaccante.nome + " ha inflitto " + danno + " danni a " + nome);
                Console.WriteLine(nome + " ha " + puntiFerita + " punti ferita rimanenti");
            }
        }

        public int Attacca()
        {
            return rnd.Next(10, 100);
        }

        public void Ripristino()
        {
            puntiFerita = puntiFeritaTotali;
        }

        public void Lotta(object arenaobj)
        {
            Arena arena = (Arena) arenaobj;
            Console.WriteLine(nome + ", sono pronto a combattere");
            arena.accessi.WaitOne(); //accedo all'arena
            Console.WriteLine(nome +", sono entrato nell'arena");
            arena.combattenti.Add(this);
            while (arena.combattenti.Count < 2) { Console.WriteLine(nome + ", attendo il mio avversario"); } //attendo l'avversario
            Console.WriteLine(arena.combattenti[0].nome + " vs " + arena.combattenti[1].nome); //porto avanti il combattimento
            Thread.Sleep(2000);
            Console.WriteLine(nome + ", terminato il combattimento");
            arena.accessi.Release();
        }
    }
}

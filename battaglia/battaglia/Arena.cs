using System;
using System.Threading;

namespace battaglia
{
    internal class Arena
    {
        private Giocatore vincitore = null;  // Il vincitore rimane per il prossimo scontro
        private object lockObj = new object();
        public Semaphore accessi = new Semaphore(1, 1); // Solo un nuovo sfidante alla volta

        public void EntraNellArena(Giocatore nuovoGiocatore)
        {
            accessi.WaitOne(); // Aspetta il turno per entrare nell'arena

            lock (lockObj)
            {
                if (vincitore == null)
                {
                    vincitore = nuovoGiocatore;
                    Console.WriteLine($"{vincitore.Nome} è in attesa di uno sfidante.");
                    accessi.Release(); // Libera subito per accettare un avversario
                    return;
                }
            }

            // Se c'è già un vincitore, combattono
            Combatti(nuovoGiocatore);
            accessi.Release(); // Libera l'accesso all'arena per un nuovo sfidante
        }

        private void Combatti(Giocatore sfidante)
        {
            Console.WriteLine($"Combattimento iniziato: {vincitore.Nome} vs {sfidante.Nome}");
            Thread.Sleep(1000);

            while (vincitore.PuntiFerita > 0 && sfidante.PuntiFerita > 0)
            {
                vincitore.Difendi(sfidante);
                if (vincitore.PuntiFerita > 0)
                    sfidante.Difendi(vincitore);
            }

            lock (lockObj)
            {
                if (vincitore.PuntiFerita > 0)
                {
                    Console.WriteLine($"{vincitore.Nome} ha vinto e aspetta il prossimo sfidante!");
                    vincitore.Ripristino();
                }
                else
                {
                    Console.WriteLine($"{sfidante.Nome} ha vinto e aspetta il prossimo sfidante!");
                    vincitore = sfidante;
                    vincitore.Ripristino();
                }
            }
        }
    }
}

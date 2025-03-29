using System;
using System.Collections.Generic;
using System.Threading;

namespace battaglia
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Arena campo = new Arena();
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 10; i++)
            {
                Giocatore g = new Giocatore("Player" + (i + 1));
                Thread t = new Thread(() => g.Lotta(campo));
                threads.Add(t);
                t.Start();
            }

            foreach (Thread t in threads)
            {
                t.Join(); // Aspetta che tutti i combattimenti finiscano
            }

            Console.WriteLine("Tutti i combattimenti sono terminati.");
        }
    }
}

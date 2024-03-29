﻿using Area51.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Area51
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the trip..");

            var elevator = new Elevator(1);

            var agents = new List<BaseAgent>
            {
                new BaseAgent("Didi",SecurityLevel.TopSecret,elevator),
                new BaseAgent("Another Didi",SecurityLevel.Secret,elevator)
            };

            var threads = agents
                .Select(a => new Thread(a.DoWork))
                .ToArray();

            foreach (var thread in threads)
            {
                thread.Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("\nArea51 Trip is over!");
            Console.ReadLine();
        }
    }
}

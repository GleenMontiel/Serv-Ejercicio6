using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejercicio6
{
  
    class Player
    {
        Random r = new Random();
        public static bool displayRunning = true;
        public static bool playing = true;
        public static bool winner = false;
        static int cont = 0;
        public static readonly object l = new object();
        public string Name { set; get; }
        public int PositionX  { set; get; }
        public int PositionY { set; get; }
        public int Goal { set; get; }
        
        public Player(string name ,int positionX, int positionY, int goal)
        {
            this.Name = name;
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.Goal = goal;
        }

        public void Play()
        {
            int n;
            while (playing)
            {
              
                lock (l) 
                {
                     n = r.Next(1, 11);
                    Console.SetCursorPosition(this.PositionX, this.PositionY);
                    if (n == 5 || n == 7)
                    {
                        if (this.Goal > 0)
                        {
                            cont++;
                            displayRunning = false;
                        }
                        else
                        {
                            cont--;
                            displayRunning = true;
                            Monitor.Pulse(l);
                        }
                        Console.WriteLine("{0}: {1,2}", this.Name, n);
                        if (cont == this.Goal)
                        {
                            playing = false;
                            winner = true;
                            Console.WriteLine("Winner: " + this.Name);
                        }
                    }
                    else 
                    {
                        Console.WriteLine("{0}: {1,2}", this.Name, n);
                    }
                    
                }
                Thread.Sleep(r.Next(100, (100 * n)));
            }
        }
    }
}

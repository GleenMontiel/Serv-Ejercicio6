using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ejercicio6
{
    class Program
    {
        static Random r = new Random();
        public static bool displayRunning = true;
        public static bool playing = true;
        public static bool winner = false;
        static int cont = 0;
        public static readonly object l = new object();
        static void Main(string[] args)
        {
         
            Thread t1 = new Thread(Player1);
            Thread t2 = new Thread(Player2);
            Thread t3 = new Thread(Display);
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();

            t3.Join();
            Console.SetCursorPosition(0, 10);
            Console.WriteLine("ya");
            Console.ReadLine();
        }
        static void Player1()
        {
            int n;
            while (playing)
            {

                lock (l)
                {
                    n = r.Next(1, 11);
                    Console.SetCursorPosition(0, 0);
                    if (n == 5 || n == 7)
                    {

                        cont++;
                        displayRunning = false;

                        Console.WriteLine("{0}: {1,2}", "Player1", n);

                        if (cont == 20)
                        {
                            playing = false;
                            winner = true;
                            Console.WriteLine("Winner: " + "Player1");
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0}: {1,2}", "Player1", n);
                    }

                }
                Thread.Sleep(r.Next(100, (100 * n)));
            }
        }

        static void Player2()
        {
            int n;
            while (playing)
            {

                lock (l)
                {
                    n = r.Next(1, 11);
                    Console.SetCursorPosition(50, 0);
                    if (n == 5 || n == 7)
                    {

                        cont--;
                        displayRunning = true;
                        Monitor.Pulse(l);

                        Console.WriteLine("{0}: {1,2}", "Player2", n);

                        if (cont == -20)
                        {
                            playing = false;
                            winner = true;
                            Console.WriteLine("Winner: " + "Player2");
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0}: {1,2}", "Player2", n);
                    }

                }
                Thread.Sleep(r.Next(100, (100 * n)));
            }
        }
        static void Display()
        {

            Char[] d = { '|', '/', '-', '\\' };
            int i = 0;
            while (playing)
            {
                lock (l)
                {
                    if (!displayRunning) 
                    {
                        Monitor.Wait(l);
                    }

                    Console.SetCursorPosition(25, 0);
                    Console.WriteLine(d[i]);
                    i++;
                    if (i == d.Length) i = 0;

                }
                Thread.Sleep(200);
            }
        }
    }
}

/*
 
 Ejercicio 6
Crea tres hilos con las siguientes características:

Dos de ellos (llamado player1 y player2) sacan aleatoriamente un número entre 1 y 10 y lo hacen de
forma continua y lo van mostrando. Cada número que sacan descansan un tiempo aleatorio entre
100ms y 100*número_sacado milisegundos.


Un tercer proceso (denominado display) tiene que ir cambiando el aspecto de un carácter situado en
otra zona de la consola entre los caracteres |, /, -, \ como si hiciese efecto de giro. El cambio de
carácter será será cada 200ms.


Si player1 saca un 5 o un 7 entonces pausa el display y se incrementa un contador común en 1 punto.
Si el display ya estaba parado entonces la aumenta en 5 puntos. Si player2 saca un 5 o un 7 entonces
vuelve a arrancar el display y se decrementa la puntuación común en un punto y si ya estaba en
funcionamiento (excepto al principio del juego) se decrementa en 5 puntos. Gana jugador1 si llega a
20 puntos o jugador 2 si llega a -20 puntos.

  
Este programa puede realizarse en consola usando setCursor para escribir en distintas zonas de la
pantalla para cada jugador y el display. O si prefieres hazlo en Windows Forms usando Labels o
TextBox para cada elemento, pero ten en cuenta las notas que van a continuación pues se complica el
problema. En el caso de hacerlo en Windows Forms sustituye el display de consola por una etiqueta
que cambie de color haciendo efecto de parpadeo entre varios colores aleatorios
 */
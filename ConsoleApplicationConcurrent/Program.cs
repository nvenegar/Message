using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ConcurrencyVisualizer.Instrumentation;
using System.IO;
using System.Diagnostics;


namespace ConsoleApplicationConcurrent
{
    class Program
    {
        static void Main(string[] args)
        {
            //EJEMPLO_SPAN();
           // EJEMPLO_FLAG();
            EJEMPLO_MESSAGE();

        }

        private static void EJEMPLO_MESSAGE()
        {
            ObtenerArchivos();

        }

        private static void ObtenerArchivos()
        {
            try
            {
                string[] dirs = Directory.GetDirectories(@"D:\");
                Console.WriteLine("El Numero de archivos encontrados son {0}.", dirs.Length);
                Stopwatch sw = new Stopwatch();
                foreach (string dir in dirs)
                {
                    sw.Restart();
                    writeFile(dir);
                    sw.Stop();
                    long duration = sw.ElapsedMilliseconds;
                    Markers.WriteMessage(dir+ ":" + duration);

                    Console.WriteLine(dir);                             
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("Procesos fallido: {0}", e.ToString());

            }
           



        }

        private static void writeFile(string dir)
        {
     
            StreamWriter writer = new StreamWriter("D:\\archivos.txt", true);
            writer.WriteLine(dir);
            // close the stream
            writer.Close();

        }

        private static void EJEMPLO_FLAG()
        {
            Metodo1();

            Markers.WriteFlag("FinMetodo1");

            Metodo2();
            Markers.WriteFlag("FinMetodo2");

        }

        private static void EJEMPLO_SPAN()
        {
            using (Markers.EnterSpan("Metodo 1"))
            {
                Metodo1();
            }
            Console.WriteLine("Terminado Metodo 1");
            var span = Markers.EnterSpan("Metodo 2");
            Metodo2();
            span.Leave();
            Console.WriteLine("Terminado Metodo 2");
        }

       
        private static void Metodo1()
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
            }
        }

        private static void Metodo2()
        {
            for (int i = 100; i < 200; i++)
            {
                Console.WriteLine(i);
            }
        }


    }
}

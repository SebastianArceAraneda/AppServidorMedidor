using AppServidorMedidor.Comunicacion;
using MedidorModel.DAL;
using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Sebastian Arce - Daniel Santos Seccion 170

namespace AppServidorMedidor
{
    public partial class Program
    {
            private static IMedidorDAL medidorDAL = MedidorDALArchivos.GetIntancia();

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("¿Que quiere hacer?");
            Console.WriteLine("1. Ingresar \n 2. Mostrar \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Ingresar();
                    break;
                case "2":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }

        static void Mostrar()
        {
            List<Medidor> medidores = null;
            lock (medidorDAL)
            {
                medidores = medidorDAL.ObtenerMedidores();
            }
            foreach (Medidor medidor in medidores)
            {
                Console.WriteLine(medidor);
            }
        }

        static void Ingresar()
        {
            Console.WriteLine("Ingrese medidor: ");
            string medidor = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese consumo :");
            string consumo = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese fecha :");
            string fecha = Console.ReadLine().Trim();
            Medidor medidor1 = new Medidor()
            {
                IdMedidor = medidor,
                Consumo = consumo,
                Fecha = fecha
            };
            //nuestro proyecto es ThreadSafe que significa que nos hacemos cargo de la concurrencia
            lock (medidorDAL)
            {
                medidorDAL.AgregarMedidor(medidor1);
            }

        }


        static void Main(string[] args)
            {

                HebraServidor hebra = new HebraServidor();
                Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
                t.Start();
                while (Menu()) ;
        }
        }
    }


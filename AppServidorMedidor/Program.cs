using AppServidorMedidor.Comunicacion;
using MedidorModel.DAL;
using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppServidorMedidor
{
    public partial class Program
    {
            private static IMedidorDAL medidorDAL = MedidorDALArchivos.GetIntancia();
            static void Main(string[] args)
            {

                HebraServidor hebra = new HebraServidor();
                Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
                t.Start();

            }

        

    }
    }


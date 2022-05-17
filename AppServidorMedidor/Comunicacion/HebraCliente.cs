
using MedidorModel.DAL;
using MedidorModel.DTO;
using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServidorMedidor.Comunicacion
{
    class HebraCliente
    {

        private IMedidorDAL medidorDAL = MedidorDALArchivos.GetIntancia();
        private ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }
        
        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese medidor: ");
            string idMedidor = clienteCom.Leer();
            clienteCom.Escribir("Ingrese consumo: ");
            string consumo = clienteCom.Leer();
            clienteCom.Escribir("Ingrese fecha: ");
            string fecha = clienteCom.Leer();
            bool Validar = BuscarMedidores(idMedidor);
            if (Validar)
            {
                Medidor medidor = new Medidor()
            {
                IdMedidor = idMedidor,
                Consumo = consumo,
                Fecha = fecha
            };
            lock (medidorDAL)
            {
                medidorDAL.AgregarMedidor(medidor);
            }
            clienteCom.Escribir("OK.");
            Console.WriteLine("Se agrego correctamente.");
            clienteCom.Desconectar();
            }
            else
            {
                Console.WriteLine("Id de medidor incorrecto");
                clienteCom.Escribir("Medidor no encontrado. ");
                clienteCom.Desconectar();
            }

        }

        public bool BuscarMedidores(string respuesta)
        {
            List<Medidor> filtrar = medidorDAL.FiltrarMedidor(respuesta);
            bool codigo = false;
            filtrar.ForEach(p => codigo = true);
            if (codigo)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}

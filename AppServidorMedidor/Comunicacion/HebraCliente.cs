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
            bool Validar = BuscarMedidor(idMedidor);
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
            clienteCom.Escribir("OK ");
            clienteCom.Desconectar();
            }
            else
            {
                Console.WriteLine("Id de medidor incorrecto");
                clienteCom.Escribir("Medidor no encontrado. ");
                clienteCom.Desconectar();
            }

        }

        public bool BuscarMedidor(string respuesta)
        {
            List<Medidor> filtradas = medidorDAL.FiltrarMedidor(respuesta);
            bool codigo = false;
            filtradas.ForEach(p => codigo = true);
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

using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DAL
{
    public class MedidorDALArchivos : IMedidorDAL
    {

        //patron Singleton
        private MedidorDALArchivos()
        {

        }

        private static MedidorDALArchivos instancia;

        public static IMedidorDAL GetIntancia()
        {
            if (instancia == null)
            {
                instancia = new MedidorDALArchivos();
            }
            return instancia;
        }

        private static string url = Directory.GetCurrentDirectory();

        private static string archivo = url + "/medidor.txt";
        public void AgregarMedidor(Medidor medidor)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(medidor.IdMedidor + ";" + medidor.Consumo + ";" + medidor.Fecha);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public List<Medidor> ObtenerMedidor()
        {
            List<Medidor> lista = new List<Medidor>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split(';');
                            Medidor medidor = new Medidor()
                            {
                                IdMedidor = arr[0],
                                Consumo = arr[1],
                                Fecha = arr[2]
                            };
                            lista.Add(medidor);
                        }
                    } while (texto != null);
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
        public List<Medidor> FiltrarMedidor(string nombre)
        {
            return ObtenerMedidor().FindAll(p => p.IdMedidor == nombre);

        }

        public void AgregarMedidor(string medidor)
        {
            ((IMedidorDAL)instancia).AgregarMedidor(medidor);
        }

        public List<Medidor> ObtenerMedidores()
        {
            return ((IMedidorDAL)instancia).ObtenerMedidores();
        }
    }
}

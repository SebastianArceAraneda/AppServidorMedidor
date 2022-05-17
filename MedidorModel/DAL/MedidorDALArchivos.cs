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
        
        private static string archivo = url + "/medidor.txt";
        
        private static string url = Directory.GetCurrentDirectory() + "/" + archivo;

        
        public void AgregarMedidor(Medidor medidor)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(url, true))
                {
                    writer.WriteLine(medidor.IdMedidor + ";" + medidor.Consumo + ";" + medidor.Fecha + ";");
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<Medidor> FiltrarMedidor(string nombre)
        {
            return ObtenerMedidores().FindAll(p => p.IdMedidor == nombre);

        }
        
        public List<Medidor> ObtenerMedidores()
        {
            List<Medidor> medidores = new List<Medidor>();
            using (StreamReader reader = new StreamReader(url))
            {
                string texto;
                do
                {
                    //Leer desde el archivo hasta que no haya nada
                    texto = reader.ReadLine();
                    if (texto != null)
                    {
                        string[] textoarr = texto.Trim().Split(';');
                        string nombre = textoarr[0];
                        string consumo = (textoarr[1]);
                        string fecha = (textoarr[2]);
                        //crear una persona
                        Medidor p = new Medidor()
                        {
                            IdMedidor = nombre,
                            Consumo = consumo,
                            Fecha = fecha
                        };

                        medidores.Add(p);
                    }

                } while (texto != null);

                //crear una persona
                // calcule su IMC
                // agregar a la lista
            }
            return medidores;
        }
    }
}

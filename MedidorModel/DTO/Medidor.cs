using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DTO
{
    public class Medidor
    {

        private string idMedidor;
        private string consumo;
        private string fecha;

        public string Consumo { get => consumo; set => consumo = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string IdMedidor { get => idMedidor; set => idMedidor = value; }
        
    }
}

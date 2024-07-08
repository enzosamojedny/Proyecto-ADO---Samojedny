using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Datos.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string? NombreCompleto { get; set; }
        public string? DNI { get; set; }
        public int Edad { get; set; }
        public bool Casado { get; set; }
        public decimal? Salario { get; set; }

    }

}

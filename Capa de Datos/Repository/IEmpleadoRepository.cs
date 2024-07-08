using Capa_de_Datos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Datos.Repository
{
    public interface IEmpleadoRepository
    {
        Task<Empleado> GetEmpleado(int id);
    }

    public interface IEmpleadosRepository
    {
        Task<List<Empleado>> GetAllEmpleados();
    }
    public interface ICreateEmpleadoRepository
    {
        Task<Empleado> CreateEmpleado(string NombreCompleto, string DNI, int Edad, bool Casado, decimal Salario);
    }
}

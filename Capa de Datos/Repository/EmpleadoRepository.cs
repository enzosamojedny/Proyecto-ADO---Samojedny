using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Capa_de_Datos.Settings;
using Capa_de_Datos.Entities;
using Microsoft.Extensions.Options;
//acceso a las clases de ADO.net
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Net;

namespace Capa_de_Datos.Repository
{
    public class EmpleadosRepository : IEmpleadosRepository
    {
        //aca viene el connection string
        private readonly Settings.DbConnection _connection;


        //creo el constructor
        //se encapsula en IOptions

        public EmpleadosRepository(IOptions<Settings.DbConnection> connection)
        {
            _connection = connection.Value; //recibo el valor del connection string
        }

        public async Task<List<Empleado>> GetAllEmpleados()
        {
            List<Empleado> list = new List<Empleado>();

            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_getEmpleados", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        list.Add(new Empleado()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                            DNI = reader.GetString(reader.GetOrdinal("DNI")),
                            Edad = reader.GetInt32(reader.GetOrdinal("Edad")),
                            Casado = reader.GetBoolean(reader.GetOrdinal("Casado")),
                            Salario = reader.GetDecimal(reader.GetOrdinal("Salario"))

                        });
                    }
                }
            }
            return list;
        }
    }

    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly Settings.DbConnection _connection;

        public EmpleadoRepository(IOptions<Settings.DbConnection> connection)
        {
            _connection = connection.Value;
        }
        public async Task<Empleado> GetEmpleado(int id)
        {
            Empleado? empleado = null;

            using (var connection = new SqlConnection(_connection.ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_getEmpleadoByID", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Id", id));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        empleado = new Empleado()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            NombreCompleto = reader.GetString(reader.GetOrdinal("NombreCompleto")),
                            DNI = reader.GetString(reader.GetOrdinal("DNI")),
                            Edad = reader.GetInt32(reader.GetOrdinal("Edad")),
                            Casado = reader.GetBoolean(reader.GetOrdinal("Casado")),
                            Salario = reader.GetDecimal(reader.GetOrdinal("Salario"))

                        };
                    }
                }
            }
            return empleado;
        }
    }
    public class CreateEmpleadoRepository : ICreateEmpleadoRepository
    {
        private readonly Settings.DbConnection _connection;

        public CreateEmpleadoRepository(IOptions<Settings.DbConnection> connection)
        {
            _connection = connection.Value;
        }
        public async Task<Empleado> CreateEmpleado(string NombreCompleto, string DNI, int Edad, bool Casado, decimal Salario)
        {
            Empleado? empleado = null;

            using (var connection = new SqlConnection(_connection.ConnectionString))
            {

                await connection.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_CreateEmpleado", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@NombreCompleto", SqlDbType.NVarChar, 100)).Value = NombreCompleto;
                cmd.Parameters.Add(new SqlParameter("@DNI", SqlDbType.NVarChar, 20)).Value = DNI;
                cmd.Parameters.Add(new SqlParameter("@Edad", SqlDbType.Int)).Value = Edad;
                cmd.Parameters.Add(new SqlParameter("@Casado", SqlDbType.Bit)).Value = Casado;
                cmd.Parameters.Add(new SqlParameter("@Salario", SqlDbType.Decimal)).Value = Salario;

                await cmd.ExecuteNonQueryAsync();
            }
            return new Empleado
            {
                NombreCompleto = NombreCompleto,
                DNI = DNI,
                Edad = Edad,
                Casado = Casado,
                Salario = Salario
            };
        }
    }

}

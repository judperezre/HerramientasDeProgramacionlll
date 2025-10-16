using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class Roles
    {
        public int IdRolEmpleado { get; set; }
        public string StrDescripcion { get; set; } = "";

        public string InsertarRol()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@StrDescripcion", StrDescripcion)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("SP_INSERTAR_ROL", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ActualizarRol()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdRolEmpleado", IdRolEmpleado),
                    new Parametros("@StrDescripcion", StrDescripcion)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("SP_ACTUALIZAR_ROL", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string EliminarRol()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdRolEmpleado", IdRolEmpleado)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("SP_ELIMINAR_ROL", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public DataTable ConsultarRoles()
        {
            try
            {
                string consulta = "SELECT IdRolEmpleado, StrDescripcion FROM TBLROLES";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarRol(int idRol)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLROLES WHERE IdRolEmpleado = {idRol}";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
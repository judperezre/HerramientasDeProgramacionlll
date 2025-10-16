using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class Empleados
    {
        public int IdEmpleado { get; set; }
        public string StrNombre { get; set; } = "";
        public long NumDocumento { get; set; }
        public string StrDireccion { get; set; } = "";
        public string StrTelefono { get; set; } = "";
        public string StrEmail { get; set; } = "";
        public int IdRolEmpleado { get; set; }
        public DateTime DtmIngreso { get; set; }
        public DateTime? DtmRetiro { get; set; }
        public string StrDatosAdicionales { get; set; } = "";
        public DateTime DtmFechaModifica { get; set; }
        public string StrUsuarioModifico { get; set; } = "";

        public string InsertarEmpleado()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdEmpleado", 0), // Para insertar nuevo
                    new Parametros("@strNombre", StrNombre),
                    new Parametros("@NumDocumento", NumDocumento),
                    new Parametros("@StrDireccion", StrDireccion),
                    new Parametros("@StrTelefono", StrTelefono),
                    new Parametros("@StrEmail", StrEmail),
                    new Parametros("@IdRolEmpleado", IdRolEmpleado),
                    new Parametros("@DtmIngreso", DtmIngreso),
                    new Parametros("@DtmRetiro", DtmRetiro ?? (object)DBNull.Value),
                    new Parametros("@strDatosAdicionales", StrDatosAdicionales),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifico", StrUsuarioModifico)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_Empleado", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ActualizarEmpleado()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdEmpleado", IdEmpleado),
                    new Parametros("@strNombre", StrNombre),
                    new Parametros("@NumDocumento", NumDocumento),
                    new Parametros("@StrDireccion", StrDireccion),
                    new Parametros("@StrTelefono", StrTelefono),
                    new Parametros("@StrEmail", StrEmail),
                    new Parametros("@IdRolEmpleado", IdRolEmpleado),
                    new Parametros("@DtmIngreso", DtmIngreso),
                    new Parametros("@DtmRetiro", DtmRetiro ?? (object)DBNull.Value),
                    new Parametros("@strDatosAdicionales", StrDatosAdicionales),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifico", StrUsuarioModifico)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_Empleado", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string EliminarEmpleado()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdEmpleado", IdEmpleado)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("Eliminar_Empleado", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public DataTable ConsultarEmpleados()
        {
            try
            {
                string consulta = "SELECT e.IdEmpleado, e.strNombre, e.NumDocumento, e.StrDireccion, e.StrTelefono, e.StrEmail, " +
                                "r.StrDescripcion as Rol, e.DtmIngreso, e.DtmRetiro, e.strDatosAdicionales " +
                                "FROM TBLEMPLEADO e " +
                                "LEFT JOIN TBLROLES r ON e.IdRolEmpleado = r.IdRolEmpleado";
                
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarEmpleado(int idEmpleado)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLEMPLEADO WHERE IdEmpleado = {idEmpleado}";
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
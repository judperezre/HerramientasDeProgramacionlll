using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class Seguridad
    {
        public int IdSeguridad { get; set; }
        public int IdEmpleado { get; set; }
        public string StrUsuario { get; set; } = "";
        public string StrClave { get; set; } = "";
        public DateTime DtmFechaModifica { get; set; }
        public string StrUsuarioModifico { get; set; } = "";

        public string InsertarUsuario()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdEmpleado", IdEmpleado),
                    new Parametros("@StrUsuario", StrUsuario),
                    new Parametros("@StrClave", StrClave),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifico", StrUsuarioModifico)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("SP_INSERTAR_USUARIO", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ActualizarUsuario()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdSeguridad", IdSeguridad),
                    new Parametros("@IdEmpleado", IdEmpleado),
                    new Parametros("@StrUsuario", StrUsuario),
                    new Parametros("@StrClave", StrClave),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifico", StrUsuarioModifico)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("SP_ACTUALIZAR_USUARIO", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string EliminarUsuario()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdSeguridad", IdSeguridad)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("SP_ELIMINAR_USUARIO", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public DataTable ConsultarUsuarios()
        {
            try
            {
                string consulta = "SELECT s.IdSeguridad, e.strNombre as Empleado, s.StrUsuario, s.StrClave, " +
                                "s.DtmFechaModifica, s.StrUsuarioModifico " +
                                "FROM TBLSEGURIDAD s " +
                                "INNER JOIN TBLEMPLEADO e ON s.IdEmpleado = e.IdEmpleado";
                
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarUsuario(int idSeguridad)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLSEGURIDAD WHERE IdSeguridad = {idSeguridad}";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ValidarUsuarioExistente(string usuario)
        {
            try
            {
                string consulta = $"SELECT COUNT(*) FROM TBLSEGURIDAD WHERE StrUsuario = '{usuario}'";
                AccesoDatos acceso = new AccesoDatos();
                DataTable dt = acceso.EjecutarConsulta(consulta);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    int count = Convert.ToInt32(dt.Rows[0][0]);
                    return count > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
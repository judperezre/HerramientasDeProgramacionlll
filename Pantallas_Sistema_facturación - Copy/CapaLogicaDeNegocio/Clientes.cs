using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class Clientes
    {
        public int IdCliente { get; set; }
        public string StrNombre { get; set; } = "";
        public long NumDocumento { get; set; }
        public string StrDireccion { get; set; } = "";
        public string StrTelefono { get; set; } = "";
        public string StrEmail { get; set; } = "";
        public DateTime DtmFechaModifica { get; set; }
        public string StrUsuarioModifica { get; set; } = "";

        public string InsertarCliente()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdCliente", 0), // Para insertar nuevo
                    new Parametros("@StrNombre", StrNombre),
                    new Parametros("@NumDocumento", NumDocumento),
                    new Parametros("@StrDireccion", StrDireccion),
                    new Parametros("@StrTelefono", StrTelefono),
                    new Parametros("@StrEmail", StrEmail),
                    new Parametros("@StrUsuarioModifica", StrUsuarioModifica),
                    new Parametros("@DtmFechaModifica", DateTime.Now)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_Cliente", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ActualizarCliente()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdCliente", IdCliente),
                    new Parametros("@StrNombre", StrNombre),
                    new Parametros("@NumDocumento", NumDocumento),
                    new Parametros("@StrDireccion", StrDireccion),
                    new Parametros("@StrTelefono", StrTelefono),
                    new Parametros("@StrEmail", StrEmail),
                    new Parametros("@StrUsuarioModifica", StrUsuarioModifica),
                    new Parametros("@DtmFechaModifica", DateTime.Now)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_Cliente", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string EliminarCliente()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdCliente", IdCliente)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("Eliminar_Cliente", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public DataTable ConsultarClientes()
        {
            try
            {
                string consulta = "SELECT IdCliente, StrNombre, NumDocumento, StrDireccion, StrTelefono, StrEmail, " +
                                "DtmFechaModifica, StrUsuarioModifica FROM TBLCLIENTES ORDER BY StrNombre";
                
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarCliente(int idCliente)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLCLIENTES WHERE IdCliente = {idCliente}";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarClientePorDocumento(long documento)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLCLIENTES WHERE NumDocumento = {documento}";
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class CategoriasProductos
    {
        public int IdCategoria { get; set; }
        public string StrDescripcion { get; set; } = "";

        public string InsertarCategoria()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdCategoria", 0), // Para insertar nuevo
                    new Parametros("@StrDescripcion", StrDescripcion),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifico", "Admin")
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_CategoriaProducto", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ActualizarCategoria()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdCategoria", IdCategoria),
                    new Parametros("@StrDescripcion", StrDescripcion),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifico", "Admin")
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_CategoriaProducto", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string EliminarCategoria()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdCategoria", IdCategoria)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("Eliminar_CategoriaProducto", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public DataTable ConsultarCategorias()
        {
            try
            {
                string consulta = "SELECT IdCategoria, StrDescripcion FROM TBLCATEGORIA_PROD ORDER BY StrDescripcion";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarCategoria(int idCategoria)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLCATEGORIA_PROD WHERE IdCategoria = {idCategoria}";
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
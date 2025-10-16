using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class Productos
    {
        public int IdProducto { get; set; }
        public string StrNombre { get; set; } = "";
        public string StrCodigo { get; set; } = "";
        public double NumPrecioCompra { get; set; }
        public double NumPrecioVenta { get; set; }
        public int IdCategoria { get; set; }
        public string StrDetalle { get; set; } = "";
        public string StrFoto { get; set; } = "";
        public int NumStock { get; set; }
        public DateTime DtmFechaModifica { get; set; }
        public string StrUsuarioModifica { get; set; } = "";

        public string InsertarProducto()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdProducto", 0), // Para insertar nuevo
                    new Parametros("@StrNombre", StrNombre),
                    new Parametros("@StrCodigo", StrCodigo),
                    new Parametros("@NumPrecioCompra", NumPrecioCompra),
                    new Parametros("@NumPrecioVenta", NumPrecioVenta),
                    new Parametros("@IdCategoria", IdCategoria),
                    new Parametros("@StrDetalle", StrDetalle),
                    new Parametros("@strFoto", StrFoto),
                    new Parametros("@NumStock", NumStock),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifica", StrUsuarioModifica)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_Producto", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ActualizarProducto()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdProducto", IdProducto),
                    new Parametros("@StrNombre", StrNombre),
                    new Parametros("@StrCodigo", StrCodigo),
                    new Parametros("@NumPrecioCompra", NumPrecioCompra),
                    new Parametros("@NumPrecioVenta", NumPrecioVenta),
                    new Parametros("@IdCategoria", IdCategoria),
                    new Parametros("@StrDetalle", StrDetalle),
                    new Parametros("@strFoto", StrFoto),
                    new Parametros("@NumStock", NumStock),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifica", StrUsuarioModifica)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_Producto", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string EliminarProducto()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdProducto", IdProducto)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("Eliminar_Producto", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public DataTable ConsultarProductos()
        {
            try
            {
                string consulta = "SELECT p.IdProducto, p.StrNombre, p.StrCodigo, p.NumPrecioCompra, p.NumPrecioVenta, " +
                                "c.StrDescripcion as Categoria, p.StrDetalle, p.strFoto, p.NumStock, " +
                                "p.DtmFechaModifica, p.StrUsuarioModifica " +
                                "FROM TBLPRODUCTO p " +
                                "LEFT JOIN TBLCATEGORIA_PROD c ON p.IdCategoria = c.IdCategoria " +
                                "ORDER BY p.StrNombre";
                
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarProducto(int idProducto)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLPRODUCTO WHERE IdProducto = {idProducto}";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarProductoPorCodigo(string codigo)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLPRODUCTO WHERE StrCodigo = '{codigo}'";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string ActualizarStock(int cantidad)
        {
            try
            {
                string sentencia = $"UPDATE TBLPRODUCTO SET NumStock = NumStock - {cantidad}, " +
                                 $"DtmFechaModifica = '{DateTime.Now}', StrUsuarioModifica = '{StrUsuarioModifica}' " +
                                 $"WHERE IdProducto = {IdProducto}";
                
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarComando(sentencia);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
    }
}
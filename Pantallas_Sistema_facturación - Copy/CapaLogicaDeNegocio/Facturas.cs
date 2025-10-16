using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class Facturas
    {
        public int IdFactura { get; set; }
        public DateTime DtmFecha { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public double NumDescuento { get; set; }
        public double NumImpuesto { get; set; }
        public double NumValorTotal { get; set; }
        public int IdEstado { get; set; }
        public DateTime DtmFechaModifica { get; set; }
        public string StrUsuarioModifica { get; set; } = "";

        public string InsertarFactura()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdFactura", 0), // Para insertar nuevo
                    new Parametros("@DtmFecha", DtmFecha),
                    new Parametros("@IdCliente", IdCliente),
                    new Parametros("@IdEmpleado", IdEmpleado),
                    new Parametros("@NumDescuento", NumDescuento),
                    new Parametros("@NumImpuesto", NumImpuesto),
                    new Parametros("@NumValorTotal", NumValorTotal),
                    new Parametros("@IdEstado", IdEstado),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifica", StrUsuarioModifica)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_Factura", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string ActualizarFactura()
        {
            try
            {
                List<Parametros> parametros = new List<Parametros>
                {
                    new Parametros("@IdFactura", IdFactura),
                    new Parametros("@DtmFecha", DtmFecha),
                    new Parametros("@IdCliente", IdCliente),
                    new Parametros("@IdEmpleado", IdEmpleado),
                    new Parametros("@NumDescuento", NumDescuento),
                    new Parametros("@NumImpuesto", NumImpuesto),
                    new Parametros("@NumValorTotal", NumValorTotal),
                    new Parametros("@IdEstado", IdEstado),
                    new Parametros("@DtmFechaModifica", DateTime.Now),
                    new Parametros("@StrUsuarioModifica", StrUsuarioModifica)
                };

                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarProcedimiento("actualizar_Factura", parametros);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string EliminarFactura()
        {
            try
            {
                string sentencia = $"DELETE FROM TBLFACTURA WHERE IdFactura = {IdFactura}";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarComando(sentencia);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public DataTable ConsultarFacturas()
        {
            try
            {
                string consulta = "SELECT f.IdFactura, f.DtmFecha, c.StrNombre as Cliente, e.strNombre as Empleado, " +
                                "f.NumDescuento, f.NumImpuesto, f.NumValorTotal, est.StrDescripcion as Estado, " +
                                "f.DtmFechaModifica, f.StrUsuarioModifica " +
                                "FROM TBLFACTURA f " +
                                "LEFT JOIN TBLCLIENTES c ON f.IdCliente = c.IdCliente " +
                                "LEFT JOIN TBLEMPLEADO e ON f.IdEmpleado = e.IdEmpleado " +
                                "LEFT JOIN TBLESTADO_FACTURA est ON f.IdEstado = est.IdEstadoFactura " +
                                "ORDER BY f.DtmFecha DESC";
                
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable BuscarFactura(int idFactura)
        {
            try
            {
                string consulta = $"SELECT * FROM TBLFACTURA WHERE IdFactura = {idFactura}";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarConsulta(consulta);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public int ObtenerUltimaFactura()
        {
            try
            {
                string consulta = "SELECT MAX(IdFactura) FROM TBLFACTURA";
                AccesoDatos acceso = new AccesoDatos();
                DataTable dt = acceso.EjecutarConsulta(consulta);
                
                if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                {
                    return Convert.ToInt32(dt.Rows[0][0]);
                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }

    public class DetalleFactura
    {
        public int IdDetalle { get; set; }
        public int IdFactura { get; set; }
        public int NumCantidad { get; set; }
        public int IdProducto { get; set; }
        public double NumPrecio { get; set; }

        public string InsertarDetalle()
        {
            try
            {
                string sentencia = $"INSERT INTO TBLDETALLE_FACTURA (IdFactura, NumCantidad, IdProducto, NumPrecio) " +
                                 $"VALUES ({IdFactura}, {NumCantidad}, {IdProducto}, {NumPrecio})";
                
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarComando(sentencia);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public string EliminarDetallesPorFactura()
        {
            try
            {
                string sentencia = $"DELETE FROM TBLDETALLE_FACTURA WHERE IdFactura = {IdFactura}";
                AccesoDatos acceso = new AccesoDatos();
                return acceso.EjecutarComando(sentencia);
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }

        public DataTable ConsultarDetallesPorFactura()
        {
            try
            {
                string consulta = $"SELECT d.IdDetalle, d.NumCantidad, p.StrNombre as Producto, d.NumPrecio, " +
                                $"(d.NumCantidad * d.NumPrecio) as Subtotal " +
                                $"FROM TBLDETALLE_FACTURA d " +
                                $"LEFT JOIN TBLPRODUCTO p ON d.IdProducto = p.IdProducto " +
                                $"WHERE d.IdFactura = {IdFactura}";
                
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
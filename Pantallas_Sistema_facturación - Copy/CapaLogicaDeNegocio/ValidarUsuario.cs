using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaAccesoDatos;

namespace CapaLogicaDeNegocio
{
    public class ValidarUsuario
    {
        public string C_StrUsuario { get; set; } = "";
        public string C_StrClave { get; set; } = "";
        public Int32 C_IdEmpleado { get; set; }

        public void ValidarUsuarios()
        {
            try
            {
                string sentencia = $"SELECT IdEmpleado FROM dbo.TBLSEGURIDAD WHERE StrUsuario ='{C_StrUsuario}'AND StrClave = '{C_StrClave}'";
                DataTable dt = new DataTable();
                AccesoDatos acceso = new AccesoDatos();
                dt = acceso.EjecutarConsulta(sentencia);

                foreach (DataRow row in dt.Rows)
                {
                    C_IdEmpleado = int.Parse(row[0].ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la consulta" + ex);
            }
        }
    }

}

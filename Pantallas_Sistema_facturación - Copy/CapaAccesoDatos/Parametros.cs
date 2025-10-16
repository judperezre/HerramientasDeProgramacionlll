using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaAccesoDatos
{
    public class Parametros

    {

        public string Nombre { get; set; }

        public object Valor { get; set; }

        public SqlDbType TipoDato { get; set; }

        public Int32 Tamaño { get; set; }

        public ParameterDirection DireccionParametro { get; set; }

        //ENTRADA

        public Parametros(string objNombre, object objValor)

        {

            Nombre = objNombre;

            Valor = objValor;

            DireccionParametro = ParameterDirection.Input;

        }

        //SALIDA

        public Parametros(string objNombre, SqlDbType objTipoDato, Int32 objTamaño)

        {

            Nombre = objNombre;

            TipoDato = objTipoDato;

            Tamaño = objTamaño;

            DireccionParametro = ParameterDirection.Input;

        }

    }

    public class AccesoDatos

    {

        SqlConnection conexion;

        SqlCommand cmd;

        SqlDataReader lectorDatos = null;

        SqlDataAdapter da;

        DataTable dt;

        public string AbrirBd()

        {

            string error = "";

            try

            {

                conexion = new SqlConnection("Data Source =DESKTOP-VBP8H8N; Initial Catalog=DBFACTURAS;Integrated Security = True");

                conexion.Open();

                Console.WriteLine("Tamos conectados");

            }

            catch (Exception ex)

            {

                error = "Error: no se establecio la conexion con la base de datos - " + ex;



            }

            return error;

        }

        public string CerrarBd()

        {

            string error = "";

            try

            {

                conexion.Close();

            }

            catch (Exception ex)

            {

                error = "Error: Fallo al cerrar la conexion - " + ex;

            }

            return error;

        }

        public string EjecutarProcedimiento(string procedimiento, List<Parametros> list)

        {

            string salida = "";

            try

            {

                int retornado;

                AbrirBd();

                SqlCommand comando = new SqlCommand(procedimiento, conexion);

                comando.CommandType = CommandType.StoredProcedure;

                if (list != null)

                {

                    for (int i = 0; i < list.Count; i++)

                    {

                        if (list[i].DireccionParametro == ParameterDirection.Input)

                        {

                            comando.Parameters.AddWithValue(list[i].Nombre, list[i].Valor);

                        }

                        if (list[i].DireccionParametro == ParameterDirection.Output)

                        {

                            comando.Parameters.Add(list[i].Nombre, list[i].TipoDato, list[i].Tamaño).Direction = ParameterDirection.Output;

                        }

                    }

                }

                retornado = comando.ExecuteNonQuery();

                CerrarBd();

                if (retornado > 0)

                {

                    salida = "Los Datos fueron actualizados!";

                }

                else

                {

                    salida = "Los Datos no fueron actualizados!";

                }

            }

            catch (Exception ex)

            {

                salida = "Error: falló la operación - " + ex;

            }

            return salida;

        }

        public string EjecutarComando(string sentencia)

        {

            string salida = "";

            try

            {

                int retornado;

                AbrirBd();

                cmd = new SqlCommand(sentencia, conexion);

                retornado = cmd.ExecuteNonQuery();

                CerrarBd();

                if (retornado > 0)

                {

                    salida = "Los Datos fueron actualizados!";

                }

                else

                {

                    salida = "Los Datos no fueron actualizados!";

                }

            }

            catch (Exception ex)

            {

                salida = "Error: Falló operación - " + ex;

            }

            return salida;

        }

        public DataTable EjecutarConsulta(string cmd)

        {

            try

            {

                AbrirBd();

                da = new SqlDataAdapter(cmd, conexion);

                dt = new DataTable();

                da.Fill(dt);

                CerrarBd();

                return dt;

            }

            catch (Exception ex)

            {

                return null;

            }

        }

    }
}

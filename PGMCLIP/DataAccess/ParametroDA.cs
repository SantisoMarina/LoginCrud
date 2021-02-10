using PGMCLIP.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PGMCLIP.DataAccess
{
    public class ParametroDA
    {

        public static List<ParametroConfiguracion> obtenerListaParametros()
        {
            List<ParametroConfiguracion> listaParametros = new List<ParametroConfiguracion>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string selectParametros = "SELECT tc.codigo_parametro, tc.nombre, tc.valor, tc.habilitado " +
                    " FROM TablaConfiguracion tc;";
                command.Parameters.Clear();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = selectParametros;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        ParametroConfiguracion par = new ParametroConfiguracion();
                        par.codigo_parametro = int.Parse(dataReader["codigo_parametro"].ToString());
                        par.nombre = dataReader["nombre"].ToString();
                        par.valor = dataReader["valor"].ToString();
                        par.habilitado = bool.Parse(dataReader["habilitado"].ToString());

                        listaParametros.Add(par);
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return listaParametros;
        }



        public static ParametroConfiguracion obtenerParametro(int codigo_parametro)
        {
            ParametroConfiguracion parametro = new ParametroConfiguracion();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string selectParametro = "SELECT tc.codigo_parametro, tc.nombre, tc.valor, tc.habilitado " +
                    " FROM TablaConfiguracion tc " +
                    " WHERE tc.codigo_parametro = @codigo_parametro;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@codigo_parametro", codigo_parametro);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = selectParametro;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        parametro.codigo_parametro = int.Parse(dataReader["codigo_parametro"].ToString());
                        parametro.nombre = dataReader["nombre"].ToString();
                        parametro.valor = dataReader["valor"].ToString();
                        parametro.habilitado = bool.Parse(dataReader["habilitado"].ToString());
                    }
                }
            }

            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }

            return parametro;
        }


        public static bool modificarParametro(ParametroConfiguracion parametroM)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string updateParametro = "UPDATE TablaConfiguracion " +
                    " SET valor = @valor, habilitado = @habilitado " +
                    " WHERE codigo_parametro = @codigo_parametro;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@valor", parametroM.valor);
                command.Parameters.AddWithValue("@habilitado", parametroM.habilitado);
                command.Parameters.AddWithValue("@codigo_parametro", parametroM.codigo_parametro);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = updateParametro;
                conexion.Open();
                command.Connection = conexion;
                command.ExecuteNonQuery();
                resultado = true;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.Close();
            }
            return resultado;
        }


    }
}
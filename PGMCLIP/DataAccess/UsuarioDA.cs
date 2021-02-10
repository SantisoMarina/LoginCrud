using PGMCLIP.Models;
using PGMCLIP.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PGMCLIP.DataAccess
{
    public class UsuarioDA
    {
        public static bool nuevoUsuario(Usuario usuarioN)
        {
            bool resultado = false;
            bool resultadoPersona = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string insertPersona = "INSERT INTO Personas " +
                    " output INSERTED.id_persona " +
                    " VALUES " +
                    " (@nombre, @apellido, @email, @fecha_nacimiento, @telefono, @id_provincia);";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@nombre", usuarioN.persona.nombre);
                command.Parameters.AddWithValue("@apellido", usuarioN.persona.apellido);
                command.Parameters.AddWithValue("@email", usuarioN.persona.email);
                command.Parameters.AddWithValue("@fecha_nacimiento", usuarioN.persona.fecha_nacimiento);
                command.Parameters.AddWithValue("@telefono", usuarioN.persona.telefono);
                command.Parameters.AddWithValue("@id_provincia", usuarioN.persona.id_provincia);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = insertPersona;
                conexion.Open();
                command.Connection = conexion;
                int id_persona = (int)command.ExecuteScalar();
                resultadoPersona = true;

                if (resultadoPersona)
                {
                    string insertUser = "INSERT INTO Usuarios VALUES" +
                        " (@usuario, @password, @id_persona);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@usuario", usuarioN.usuario);
                    command.Parameters.AddWithValue("@password", usuarioN.password);
                    command.Parameters.AddWithValue("@id_persona", id_persona);
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = insertUser;
                    command.Connection = conexion;
                    command.ExecuteNonQuery();
                    resultado = true;

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
            return resultado;
        }


        public static bool verificacionUsuario(string usuario, string password)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string selectUsuarioVerificado = "SELECT usuario, password" +
                    " FROM Usuarios " +
                    " WHERE " +
                    " usuario = @usuario AND password = @password;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@usuario", usuario);
                command.Parameters.AddWithValue("@password", password);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = selectUsuarioVerificado;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    resultado = true;
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
            return resultado;
        }

        public static List<ProvinciaItemVM> obtenerListaProvincias()
        {
            List<ProvinciaItemVM> listaProvincias = new List<ProvinciaItemVM>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string selectProvincias = "SELECT *" +
                    " FROM Provincias;";
                command.Parameters.Clear();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = selectProvincias;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        ProvinciaItemVM prov = new ProvinciaItemVM();
                        prov.id_provincia = int.Parse(dataReader["id_provincia"].ToString());
                        prov.provincia = dataReader["provincia"].ToString();
                        listaProvincias.Add(prov);
                    }
                }
            }

            catch (Exception)
            {

            }
            finally
            {
                conexion.Close();
            }

            return listaProvincias;
        }
        public static List<PersonaVM> obtenerListaPersonas()
        {
            List<PersonaVM> listaPersonas = new List<PersonaVM>();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string selectPersonas = "SELECT p.id_persona, p.nombre, p.apellido, p.email, pr.provincia, u.usuario" +
                    " FROM Personas p " +
                    " JOIN Usuarios u " +
                    " ON p.id_persona = u.id_persona " +
                    " JOIN Provincias pr " +
                    " ON pr.id_provincia = p.id_provincia;";
                command.Parameters.Clear();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = selectPersonas;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        PersonaVM aux = new PersonaVM();
                        aux.id_persona = int.Parse(dataReader["id_persona"].ToString());
                        aux.nombre = dataReader["nombre"].ToString();
                        aux.apellido = dataReader["apellido"].ToString();
                        aux.email = dataReader["email"].ToString();
                        aux.provincia = dataReader["provincia"].ToString();
                        aux.usuario = dataReader["usuario"].ToString();

                        listaPersonas.Add(aux);
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

            return listaPersonas;
        }



        public static PersonaVM obtenerPersona(int id_persona)
        {
            PersonaVM personaVM = new PersonaVM();
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string selectPersona = "SELECT p.id_persona, p.nombre, p.apellido, p.email, pr.provincia, u.usuario" +
                    " FROM Personas p " +
                    " JOIN Usuarios u " +
                    " ON p.id_persona = u.id_persona " +
                    " JOIN Provincias pr " +
                    " ON pr.id_provincia = p.id_provincia " +
                    " WHERE p.id_persona = @id_persona;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_persona", id_persona);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = selectPersona;
                conexion.Open();
                command.Connection = conexion;
                SqlDataReader dataReader = command.ExecuteReader();
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {

                        personaVM.id_persona = int.Parse(dataReader["id_persona"].ToString());
                        personaVM.nombre = dataReader["nombre"].ToString();
                        personaVM.apellido = dataReader["apellido"].ToString();
                        personaVM.email = dataReader["email"].ToString();
                        personaVM.provincia = dataReader["provincia"].ToString();
                        personaVM.usuario = dataReader["usuario"].ToString();

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

            return personaVM;
        }


        public static bool modificarDatos(PersonaVM personaM)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string updatePersona = "UPDATE Personas " +
                    " SET nombre = @nombre, apellido = @apellido, email = @email " +
                    " WHERE id_persona = @id_persona;" +
                    " UPDATE Usuarios " +
                    " SET usuario = @usuario " +
                    " WHERE id_persona = @id_persona;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@nombre", personaM.nombre);
                command.Parameters.AddWithValue("@apellido", personaM.apellido);
                command.Parameters.AddWithValue("@email", personaM.email);
                command.Parameters.AddWithValue("@usuario", personaM.usuario);
                command.Parameters.AddWithValue("@id_persona", personaM.id_persona);

                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = updatePersona;
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

        public static bool eliminarUsuario(PersonaVM persona)
        {
            bool resultado = false;
            string cadenaConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaBD"].ToString();
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            try
            {
                SqlCommand command = new SqlCommand();
                string eliminarUsuario = " DELETE " +
                    " FROM Usuarios " +
                    " WHERE " +
                    " id_persona = @id_persona;" +
                    " DELETE " +
                    " FROM Personas " +
                    " WHERE " +
                    " id_persona = @id_persona;";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id_persona", persona.id_persona);
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = eliminarUsuario;
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

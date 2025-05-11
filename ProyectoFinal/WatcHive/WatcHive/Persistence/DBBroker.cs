using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcHive.persistence
{
    internal class DBBroker
    {
        /// <summary>
        /// The instancia
        /// </summary>
        private static DBBroker _instancia;
        /// <summary>
        /// The conexion
        /// </summary>
        private static MySql.Data.MySqlClient.MySqlConnection conexion;
        /// <summary>
        /// The cadena conexion
        /// </summary>
        private const String cadenaConexion = "server=localhost;database=watchive;uid=root;pwd=root";
        /// <summary>
        /// Initializes a new instance of the <see cref="DBBroker"/> class.
        /// </summary>
        public DBBroker()
        {
            DBBroker.conexion = new MySql.Data.MySqlClient.MySqlConnection(cadenaConexion);
        }

        /// <summary>
        /// Obteners the agente.
        /// </summary>
        /// <returns></returns>
        public static DBBroker obtenerAgente()
        {
            if (DBBroker._instancia == null)
            {
                DBBroker._instancia = new DBBroker();
            }
            return DBBroker._instancia;
        }

        /// <summary>
        /// Creates a connection with th DB and execute the String SQL parameter sentence
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        public List<Object> leer(String sql)
        {
            List<Object> resultado = new List<object>();
            List<Object> fila;
            int i;
            MySql.Data.MySqlClient.MySqlDataReader reader;
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, DBBroker.conexion);
            conectar();
            reader = com.ExecuteReader();
            while (reader.Read())
            {
                fila = new List<Object>();
                for (i = 0; i <= reader.FieldCount - 1; i++)
                {
                    fila.Add(reader[i].ToString());
                }
                resultado.Add(fila);
            }
            desconectar();
            return resultado;
        }
        /// <summary>
        /// Leers the identifier.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        //public int leerID(String sql)
        //{
        //    int id = 0;
        //    MySqlCommand cmd = new MySqlCommand(sql, DBBroker.conexion);

        //    conectar();
        //    object resultado = cmd.ExecuteScalar();  // Devuelve el primer valor de la primera fila

        //    if (resultado != null)
        //    {
        //        // Convertimos el resultado a int
        //        id = Convert.ToInt32(resultado);
        //    }

        //    desconectar();
        //    return id;
        //}

        //public string leerUser(String sql)
        //{
        //    string user = "";
        //    MySqlCommand cmd = new MySqlCommand(sql, DBBroker.conexion);

        //    conectar();
        //    object resultado = cmd.ExecuteScalar();  // Devuelve el primer valor de la primera fila

        //    if (resultado != null)
        //    {
        //        // Convertimos el resultado a int
        //        user = resultado.ToString();
        //    }

        //    desconectar();
        //    return user;
        //}

        /// <summary>
        /// Modifiers the specified SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <returns></returns>
        public int modifier(String sql)
        {
            MySql.Data.MySqlClient.MySqlCommand com = new MySql.Data.MySqlClient.MySqlCommand(sql, DBBroker.conexion);
            int resultado;
            conectar();
            resultado = com.ExecuteNonQuery();
            desconectar();
            return resultado;
        }

        /// <summary>
        /// Conectars this instance.
        /// </summary>
        public void conectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Closed)
            {
                DBBroker.conexion.Open();
            }
        }

        /// <summary>
        /// Desconectars this instance.
        /// </summary>
        public void desconectar()
        {
            if (DBBroker.conexion.State == System.Data.ConnectionState.Open)
            {
                DBBroker.conexion.Close();
            }
        }
    }
}

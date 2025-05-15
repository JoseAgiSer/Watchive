using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    internal class UsuarioPersistence
    {
        internal List<Usuario> usuarioList { get; set; }

        public UsuarioPersistence() {
            usuarioList = new List<Usuario>();    
        }

        internal void deleteUsuario(Usuario u)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Delete from ContenidoVisto WHERE NombreUsuario = '"+ u.username + "'");
            broker.modifier("Delete from ListaPendientes WHERE NombreUsuario = '" + u.username + "'");
            broker.modifier("Delete from Usuarios where NombreUsuario = '" + u.username + "'");
        }

        internal void insertUsuario(Usuario u)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Insert into Usuarios values " +
                "('" + u.username + "','" + u.password + "','"+u.nombre+"','"+u.apellidos+"',"
                +u.numHijos.ToString()+",'"+u.fechaNacimiento.ToString("yyyy-MM-dd") + "','"+u.email+"')");
        }

        //No modificado
        internal void modifyUsuario(Usuario u)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Update Usuarios set " +
                "Contrasena = '" + u.password + "', " +
                "Nombre = '" + u.nombre + "', " +
                "Apellidos = '" + u.apellidos + "', " +
                "NumeroHijos = " + u.numHijos + ", " +
                "FechaNacimiento = '" + u.fechaNacimiento.ToString("yyyy-MM-dd") + "', " +
                "Email = '" + u.email + "' " +
                "WHERE NombreUsuario = '" + u.username + "'");
        }

        internal void modifyUsuarioPass(Usuario u)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Update Usuarios set Contrasena = '" + u.password + "' where NombreUsuario = '" + u.username + "'");
        }


        internal bool checkExistingUsuario(Usuario u)
        {
            bool existe=false;
            List<Object> coincidencias = DBBroker.obtenerAgente().leer("select count(*) from Usuarios where NombreUsuario= '"+u.username+"'");

            int resultado = 0;

            foreach (List<Object> aux in coincidencias) { 
                resultado = Convert.ToInt32(aux[0]);
            }
            if (resultado > 0)
            {
                existe = true;
            }
            return existe;
        }

        internal void readUsuarios()
        {
            Usuario u = null;
            List<Object> lusuario;
            lusuario = DBBroker.obtenerAgente().leer("select * from Usuarios");
            foreach (List<Object> aux in lusuario)
            {
                u = new Usuario();
                u.username = aux[0].ToString();
                u.password = aux[1].ToString();
                u.nombre = aux[2].ToString();
                u.apellidos = aux[3].ToString();
                u.numHijos = Convert.ToInt32(aux[4]);
                if (aux[2] != DBNull.Value)
                {
                    u.fechaNacimiento = DateTime.Parse(aux[5].ToString());
                }
                u.email = aux[6].ToString();
                this.usuarioList.Add(u);
            }
        }

        internal void insertAdmin()
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Insert into Usuarios values " +
                "('admin','admin','admin','admin',0,'2000-02-19','admin@admin.com')");
        }

        internal bool contenidoEnListas(string username, int id)
        {
            bool existe = false;
            List<Object> coincidencias = DBBroker.obtenerAgente().leer("select count(*) from ListaPendientes where NombreUsuario= '" + username + "' AND idContenido = "+id);

            int resultado = 0;

            foreach (List<Object> aux in coincidencias)
            {
                resultado = Convert.ToInt32(aux[0]);
            }
            if (resultado > 0)
            {
                existe = true;
            }
            return existe;
        }

        internal bool contenidoYaVisto(string username, int id)
        {
            bool existe = false;
            List<Object> coincidencias = DBBroker.obtenerAgente().leer("select count(*) from ContenidoVisto where NombreUsuario= '" + username + "' AND idContenido = " + id);

            int resultado = 0;

            foreach (List<Object> aux in coincidencias)
            {
                resultado = Convert.ToInt32(aux[0]);
            }
            if (resultado > 0)
            {
                existe = true;
            }
            return existe;
        }
    }
}

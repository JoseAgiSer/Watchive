using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    public class Usuario
    {
        private string _username;
        private string _password;
        private string _email;
        private string _nombre;
        private string _apellidos;
        private int _numHijos;
        private DateTime _fechaNacimiento;
        private UsuarioPersistence persistence {  get; set; }

        public Usuario() { persistence = new UsuarioPersistence(); }
        public Usuario(string username, string password, string email, string nombre, string apellidos,
                        int numHijos, DateTime fechaNacimiento)
        {
            this._username = username;
            this._password = password;
            this._email = email;
            this._nombre = nombre;
            this._apellidos = apellidos;
            this._numHijos = numHijos;
            this._fechaNacimiento = fechaNacimiento.Date;
            this.persistence = new UsuarioPersistence();
        }

        public string username { get => _username; set =>  _username = value; }
        public string password { get => _password; set => _password = value; }
        public string email { get => _email; set => _email = value; }
        public string nombre { get => _nombre; set => _nombre = value; }
        public string apellidos { get => _apellidos; set => _apellidos = value; }
        public int numHijos { get => _numHijos; set => _numHijos = value;}
        public DateTime fechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento= value.Date; }

        public void readUsuario()
        {
            persistence.readUsuarios();
        }

        public List<Usuario> getListUsuarios()
        {
            return persistence.usuarioList;
        }

        public void insert()
        {
            persistence.insertUsuario(this);
        }

        public void delete()
        {
            persistence.deleteUsuario(this);
        }

        public void update()
        {
            persistence.modifyUsuario(this);
        }

        public bool existeUsername() { 
            return persistence.checkExistingUsuario(this);
        }

        internal void crearAdmin()
        {
            persistence.insertAdmin();
        }

        internal bool contenidoEnListas(int id)
        {
            return persistence.contenidoEnListas(this.username,id);
        }
    }
}

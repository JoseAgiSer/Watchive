using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    public abstract class Contenido
    {
        public int _id;
        public string _nombre;
        public DateTime _fechaEstreno;
        public string _descripcion;
        public string _imagen;
        public List<string> _listaGeneros;
        public ContenidoPersistence persistenceContenido;

        public abstract string Tipo {  get; }
        public Contenido() { persistenceContenido = new ContenidoPersistence(); }

        public int id { get => _id; set => _id = value; }
        public string nombre { get => _nombre; set => _nombre = value; }
        public string imagen { get => _imagen; set => _imagen = value; }
        public string descripcion { get => _descripcion; set => _descripcion = value; }
        public DateTime fechaEstreno { get => _fechaEstreno; set => _fechaEstreno = value.Date; }
        public List<string> listaGeneros { get => _listaGeneros; set => _listaGeneros = value; }

        internal bool exists()
        {
            return persistenceContenido.exists(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    public class ContenidoGenero
    {
        public int _idContenido;
        public List<int> _idGeneros;
        public ContenidoGeneroPersistence persistenceContenidoGenero { get; set; }
        public ContenidoGenero() { persistenceContenidoGenero = new ContenidoGeneroPersistence(); }

        public ContenidoGenero(int idContenido, List<int> idGenero)
        {
            _idContenido = idContenido;
            _idGeneros = idGenero;
            persistenceContenidoGenero = new ContenidoGeneroPersistence();
        }

        public int idContenido { get => _idContenido; set => _idContenido = value; }
        public List<int> idGeneros { get => _idGeneros; set => _idGeneros = value; }
        public void readContenidoGenero()
        {
            persistenceContenidoGenero.readContenidoGenero();
        }
        public List<ContenidoGenero> getListContenidoGenero()
        {
            return persistenceContenidoGenero.contenidoGeneroList;
        }
        public void insert()
        {
            foreach (int id in idGeneros)
            {
                persistenceContenidoGenero.insertContenidoGenero(this._idContenido,id);
            }
        }
        public void delete()
        {
            persistenceContenidoGenero.deleteContenidoGenero(this);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    public class ContenidoGeneroPersistence
    {
        public List<ContenidoGenero> contenidoGeneroList { get; set; }
        public ContenidoGeneroPersistence() { contenidoGeneroList = new List<ContenidoGenero>(); }

        internal void deleteContenidoGenero(ContenidoGenero contenidogenero)
        {
            throw new NotImplementedException();
        }
        internal void insertContenidoGenero(int idContenido, int idGenero)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Insert into ContenidoGenero values ("+ idContenido + ","+idGenero+")");
        }

        internal void readContenidoGenero()
        {
            throw new NotImplementedException();
        }

    }
}

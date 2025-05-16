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
            // Diccionario temporal para agrupar por idContenido
            Dictionary<int, ContenidoGenero> mapaContenido = new Dictionary<int, ContenidoGenero>();

            List<Object> lcontenidosgeneros = DBBroker.obtenerAgente().leer("select * from ContenidoGenero");

            foreach (List<Object> aux in lcontenidosgeneros)
            {
                int idContenido = Convert.ToInt32(aux[0]);
                int idGenero = Convert.ToInt32(aux[1]);

                // Si ya tenemos ese idContenido, añadimos el género a la lista existente
                if (mapaContenido.ContainsKey(idContenido))
                {
                    mapaContenido[idContenido].idGeneros.Add(idGenero);
                }
                else
                {
                    // Si no existe, lo creamos y lo añadimos al diccionario
                    ContenidoGenero nuevo = new ContenidoGenero
                    {
                        idContenido = idContenido,
                        idGeneros = new List<int> { idGenero }
                    };
                    mapaContenido.Add(idContenido, nuevo);
                }
            }

            // Finalmente, añadimos los objetos a la lista principal
            this.contenidoGeneroList = mapaContenido.Values.ToList();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    internal class GeneroPersistence
    {
        internal List<Genero> generoList { get; set; }

        public GeneroPersistence() { generoList = new List<Genero>(); }

        internal void deleteGenero(Genero genero)
        {
            throw new NotImplementedException();
        }

        internal void insertGenero(Genero genero)
        {
            throw new NotImplementedException();
        }

        internal void modifyGenero(Genero genero)
        {
            throw new NotImplementedException();
        }

        internal void readGeneros()
        {
            Genero g = null;
            List<Object> lgeneros;
            lgeneros = DBBroker.obtenerAgente().leer("select * from Genero");
            foreach (List<Object> aux in lgeneros)
            {
                g = new Genero();
                g.id = Convert.ToInt32(aux[0].ToString());
                g.nombreGenero = aux[1].ToString();
                this.generoList.Add(g);
            }
        }

        internal void insertInicio()
        {
            DBBroker broker = DBBroker.obtenerAgente();
            string query = "INSERT INTO Genero VALUES" +
                   "(28, 'Acción')," +
                   "(12, 'Aventura')," +
                   "(16, 'Animación')," +
                   "(35, 'Comedia')," +
                   "(80, 'Crimen')," +
                   "(99, 'Documental')," +
                   "(18, 'Drama')," +
                   "(10751, 'Familia')," +
                   "(14, 'Fantasía')," +
                   "(36, 'Historia')," +
                   "(27, 'Terror')," +
                   "(10402, 'Música')," +
                   "(9648, 'Misterio')," +
                   "(10749, 'Romance')," +
                   "(878, 'Ciencia Ficción')," +
                   "(10770, 'Película de TV')," +
                   "(53, 'Suspense')," +
                   "(10752, 'Guerra')," +
                   "(10759, 'Acción y Aventura')," +
                   "(10762, 'Niños')," +
                   "(10763, 'Noticias')," +
                   "(10764, 'Reality')," +
                   "(10765, 'Ciencia Ficción y Fantasía')," +
                   "(10766, 'Telenovela')," +
                   "(10767, 'Debate')," +
                   "(10768, 'Guerra y Política')," +
                   "(37, 'Western');";
            broker.modifier(query);
        }

        internal string readGeneroById(int id)
        {
            List<Object> lgeneros;
            List<Genero> auxlist = new List<Genero>();
            lgeneros = DBBroker.obtenerAgente().leer("select * from Genero where idGenero = " + id);
            foreach (List<Object> aux in lgeneros)
            {
                Genero g = new Genero();
                g.id = Convert.ToInt32(aux[0].ToString());
                g.nombreGenero = aux[1].ToString();
                auxlist.Add(g);
            }

            return auxlist.First().nombreGenero;
            
        }
    }
}

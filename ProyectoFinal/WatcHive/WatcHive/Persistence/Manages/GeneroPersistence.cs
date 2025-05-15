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
                g.tipo = aux[2].ToString();
                this.generoList.Add(g);
            }
        }

        internal void insertInicio()
        {
            DBBroker broker = DBBroker.obtenerAgente();
            string query = "INSERT INTO Genero VALUES" +
                    "(28, 'Acción', 'pelicula'),"+
                    "(12, 'Aventura', 'pelicula'),"+
                    "(16, 'Animación', 'both'),"+
                    "(35, 'Comedia', 'both'),"+
                    "(80, 'Crimen', 'both'),"+
                    "(99, 'Documental', 'both'),"+
                    "(18, 'Drama', 'both'),"+
                    "(10751, 'Familia', 'both'),"+
                    "(14, 'Fantasía', 'pelicula'),"+
                    "(36, 'Historia', 'pelicula'),"+
                    "(27, 'Terror', 'pelicula'),"+
                    "(10402, 'Música', 'pelicula'),"+
                    "(9648, 'Misterio', 'both'),"+
                    "(10749, 'Romance', 'pelicula'),"+
                    "(878, 'Ciencia Ficción', 'pelicula'),"+
                    "(10770, 'Película de TV', 'pelicula'),"+
                    "(53, 'Suspense', 'pelicula'),"+
                    "(10752, 'Guerra', 'pelicula'),"+
                    "(10759, 'Acción y Aventura', 'serie'),"+
                    "(10762, 'Niños', 'serie'),"+
                    "(10763, 'Noticias', 'serie'),"+
                    "(10764, 'Reality', 'serie'),"+
                    "(10765, 'Ciencia Ficción y Fantasía', 'serie'),"+
                    "(10766, 'Telenovela', 'serie'),"+
                    "(10767, 'Debate', 'serie'),"+
                    "(10768, 'Guerra y Política', 'serie'),"+
                    "(37, 'Western', 'both')";

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

        internal int getidByName(string nombre)
        {
            List<Object> lgeneros;
            List<Genero> auxlist = new List<Genero>();
            lgeneros = DBBroker.obtenerAgente().leer("select * from Genero where NombreGenero = '" + nombre+"'");
            foreach (List<Object> aux in lgeneros)
            {
                Genero g = new Genero();
                g.id = Convert.ToInt32(aux[0].ToString());
                g.nombreGenero = aux[1].ToString();
                auxlist.Add(g);
            }

            return auxlist.First().id;
        }
    }
}

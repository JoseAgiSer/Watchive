using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    public class PeliculaPersistence
    {
        internal List<Pelicula> peliculaList { get; set; }

        public PeliculaPersistence() { peliculaList = new List<Pelicula>(); }

        internal void deletePelicula(Pelicula pelicula)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Delete from Pelicula where idContenido = " + pelicula.id);
        }

        internal void insertPelicula(Pelicula p)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            string notaFormateada = p.nota.ToString(CultureInfo.InvariantCulture);
            broker.modifier("Insert into Pelicula values (" + p.id + "," + notaFormateada+")");
        }

        internal void modifyPelicula(Pelicula pelicula)
        {
            //No implementado
        }

        internal void readPeliculas()
        {
            List<object> lpeliculas = DBBroker.obtenerAgente().leer("select c.Id, c.Nombre, c.FechaEstreno, c.Descripcion, c.Imagen, p.NumTemporadas FROM Contenido c JOIN Pelicula p ON c.Id = p.IdContenido");

            foreach (List<object> fila in lpeliculas)
            {
                Pelicula p = new Pelicula
                {
                    id = Convert.ToInt32(fila[0]),
                    nombre = fila[1].ToString(),
                    fechaEstreno = fila[2] != DBNull.Value ? DateTime.Parse(fila[2].ToString()) : default,
                    descripcion = fila[3].ToString(),
                    imagen = fila[4].ToString(),
                    nota = Convert.ToInt32(fila[5])
                };

                this.peliculaList.Add(p);
            }
        }

        internal bool existsID(int id)
        {
            bool existe = false;
            List<Object> coincidencias = DBBroker.obtenerAgente().leer("select count(*) from Pelicula where idContenido=" + id);

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    public class ContenidoPersistence
    {
        internal List<Contenido> contenidoList { get; set; }

        public ContenidoPersistence() { contenidoList = new List<Contenido>(); }

        internal void deletePelicula(Contenido contenido)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Delete from Contenido where idContenido = " + contenido.id);
        }

        internal void insertContenido(Contenido contenido)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Insert into Contenido values " +"(" + contenido.id + ",'" + contenido.nombre + "','" +
                contenido.fechaEstreno.ToString("yyyy-MM-dd") + "','" + contenido.descripcion + "','" + contenido.imagen + "')");
        }

        internal void modifyContenido(Contenido contenido)
        {
            throw new NotImplementedException();
        }

        internal void readContenidos()
        {
            Contenido c = null;
            List<Object> lcontenido;
            lcontenido = DBBroker.obtenerAgente().leer("select * from Contenido");
            foreach (List<Object> aux in lcontenido)
            {
               // c = new Contenido();
                c.id = Convert.ToInt32(aux[0]);
                c.nombre = aux[1].ToString();
                if (aux[2] != DBNull.Value)
                {
                    c.fechaEstreno = DateTime.Parse(aux[2].ToString());
                }
                c.descripcion = aux[3].ToString();
                c.imagen = aux[4].ToString();
                this.contenidoList.Add(c);
            }
        }

        internal Pelicula readPeliById(int id)
        {
            List<object> lpelis = DBBroker.obtenerAgente().leer("SELECT c.idContenido, c.NombreContenido, c.FechaEstreno, c.Descripcion, c.Imagen, p.NotaMedia FROM Contenido c JOIN Pelicula p ON c.idContenido = p.idContenido AND c.idContenido = "+id);
            Pelicula p = null;
            foreach (List<object> fila in lpelis)
            {
                p = new Pelicula
                {
                    id = Convert.ToInt32(fila[0]),
                    nombre = fila[1].ToString(),
                    fechaEstreno = fila[2] != DBNull.Value ? DateTime.Parse(fila[2].ToString()) : default,
                    descripcion = fila[3].ToString(),
                    imagen = fila[4].ToString(),
                    nota = (float)Convert.ToDecimal(fila[5])
                };

            }
            return p;
        }

        internal Serie readSerieById(int id)
        {
            List<object> lseries = DBBroker.obtenerAgente().leer("SELECT c.idContenido, c.NombreContenido, c.FechaEstreno, c.Descripcion, c.Imagen, s.NumTemporadas FROM Contenido c JOIN Serie s ON c.idContenido = s.idContenido AND c.idContenido = "+id);
            Serie s = null;
            foreach (List<object> fila in lseries)
            {
                s = new Serie
                {
                    id = Convert.ToInt32(fila[0]),
                    nombre = fila[1].ToString(),
                    fechaEstreno = fila[2] != DBNull.Value ? DateTime.Parse(fila[2].ToString()) : default,
                    descripcion = fila[3].ToString(),
                    imagen = fila[4].ToString(),
                    numTemporadas = Convert.ToInt32(fila[5])
                };
            }

            return s;
        }

        internal bool exists(int id)
        {
            bool existe = false;
            List<Object> coincidencias = DBBroker.obtenerAgente().leer("select count(*) from Contenido where idContenido= " + id);

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

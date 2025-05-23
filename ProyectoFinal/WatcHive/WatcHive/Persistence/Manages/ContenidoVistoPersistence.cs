using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    internal class ContenidoVistoPersistence
    {
        internal List<ContenidoVisto> contenidoVistoList { get; set; }

        public ContenidoVistoPersistence() { contenidoVistoList = new List<ContenidoVisto>(); }

        internal void deleteContenidoVisto(ContenidoVisto contenidoVisto)
        {
            throw new NotImplementedException();
        }

        internal void insertContenidoVisto(ContenidoVisto cv)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Insert into ContenidoVisto values ('" + cv.nombreUsuario + "'," + cv.idContenido + "," + cv.idEmocion + ",'" +
                "" + cv.fechaVisto.ToString("yyyy-MM-dd") + "'," + cv.puntuacion + ")");
        }

        internal void modifyContenidoVisto(ContenidoVisto contenidoVisto)
        {
            throw new NotImplementedException();
        }

        internal void readContenidoVisto(string username)
        {
            List<object> lvistos = DBBroker.obtenerAgente().leer("select * from ContenidoVisto where NombreUsuario = '" + username + "'");

            foreach (List<object> fila in lvistos)
            {
                ContenidoVisto cv = new ContenidoVisto
                {
                    idContenido = Convert.ToInt32(fila[1]),
                    nombreUsuario = fila[0].ToString(),
                    idEmocion = Convert.ToInt32(fila[2]),
                    fechaVisto = fila[3] != DBNull.Value ? DateTime.Parse(fila[3].ToString()) : default,
                    puntuacion = Convert.ToInt32(fila[4])
                };

                this.contenidoVistoList.Add(cv);
            }
        }

        internal List<int> recomendacionUsuario(string useranme, string emocion, string tipo)
        {
            List<int> listaGenerosRecomendaciones = new List<int>();
            string query = "select g.idGenero, AVG(cv.Puntuacion) AS promedio_puntuacion," +
                " COUNT(*) AS veces_visto," +
                " AVG(cv.Puntuacion) * LOG(COUNT(*) + 1) AS relevancia " +
                "FROM ContenidoVisto cv " +
                "JOIN Emocion e ON cv.Emocion_idEmocion = e.idEmocion " +
                "JOIN ContenidoGenero cg ON cv.idContenido = cg.idContenido " +
                "JOIN Genero g ON cg.idGenero = g.idGenero WHERE cv.NombreUsuario = '"
                +useranme+"' AND e.NombreEmocion = '" + emocion + "'" +
                "AND (g.Tipo = '" + tipo + "' OR g.Tipo ='both')" +
                " GROUP BY g.idGenero " +
                "ORDER BY relevancia DESC " +
                "LIMIT 3";
            List<object> lvistos = DBBroker.obtenerAgente().leer(query);
            foreach (List<object> fila in lvistos)
            {
                int idGenero = Convert.ToInt32(fila[0]);
                listaGenerosRecomendaciones.Add(idGenero);
            }
            return listaGenerosRecomendaciones;
        }

        internal string readContenidoVistoByUser(string username, int id)
        {
            List<object> lvistos = DBBroker.obtenerAgente().leer("select * from ContenidoVisto where NombreUsuario = '" + username + "' and idContenido = "+id);

            foreach (List<object> fila in lvistos)
            {
                ContenidoVisto cv = new ContenidoVisto
                {
                    idContenido = Convert.ToInt32(fila[1]),
                    nombreUsuario = fila[0].ToString(),
                    idEmocion = Convert.ToInt32(fila[2]),
                    fechaVisto = fila[3] != DBNull.Value ? DateTime.Parse(fila[3].ToString()) : default,
                    puntuacion = Convert.ToInt32(fila[4])
                };

                return cv.fechaVisto.ToString();
            }
            return null;
        }

        internal string readEmocionByUserandID(string username, int id)
        {
            List<object> lvistos = DBBroker.obtenerAgente().leer(
                "SELECT cv.*, e.NombreEmocion " +
                "FROM ContenidoVisto cv " +
                "JOIN Emocion e ON cv.Emocion_idEmocion = e.idEmocion " +
                "WHERE cv.NombreUsuario = '" + username + "' AND cv.idContenido = " + id);


            foreach (List<object> fila in lvistos)
            {
                string emocion = fila[5].ToString();
                return emocion;
            }
            return null;
        }
    }
}

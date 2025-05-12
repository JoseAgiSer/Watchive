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
    }
}

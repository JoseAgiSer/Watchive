using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    internal class EmocionPersistence
    {
        internal List<Emocion> emocionList { get; set; }

        public EmocionPersistence() { emocionList = new List<Emocion>(); }

        internal void deleteEmocion(Emocion emocion)
        {
            throw new NotImplementedException();
        }

        internal void insertEmocion(Emocion emocion)
        {
            throw new NotImplementedException();
        }

        internal void modifyEmocion(Emocion emocion)
        {
            throw new NotImplementedException();
        }

        internal void readEmocion()
        {
            Emocion e = null;
            List<Object> lemociones;
            lemociones = DBBroker.obtenerAgente().leer("select * from Emocion");
            foreach (List<Object> aux in lemociones)
            {
                e = new Emocion();
                e.id = Convert.ToInt32(aux[0].ToString());
                e.nombre = aux[1].ToString();
                this.emocionList.Add(e);
            }
        }

        internal void insertInicio()
        {
            DBBroker broker = DBBroker.obtenerAgente();
            string query = "INSERT INTO Emocion VALUES" +
                   "(1, 'Feliz')," +
                   "(2, 'Triste')," +
                   "(3, 'Enfadado/a')," +
                   "(4, 'Ansioso/a')," +
                   "(5, 'Relajado/a')," +
                   "(6, 'Motivado/a')," +
                   "(7, 'Aburrido/a')," +
                   "(8, 'Indefinida');";
            broker.modifier(query);
        }

        internal int getIdEmocion(string emocion)
        {
            List<Object> lemocion;
            List<Emocion> auxlist = new List<Emocion>();
            lemocion = DBBroker.obtenerAgente().leer("select * from Emocion where NombreEmocion = '" +emocion+"'");
            foreach (List<Object> aux in lemocion)
            {
                Emocion e = new Emocion();
                e.id = Convert.ToInt32(aux[0].ToString());
                e.nombre = aux[1].ToString();
                auxlist.Add(e);
            }

            return auxlist.First().id;
        }
    }
}

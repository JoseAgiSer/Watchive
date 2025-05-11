using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    public class SeriePersistence
    {
        internal List<Serie> serieList { get; set; }

        public SeriePersistence() {  serieList = new List<Serie>(); }

        internal void deleteSerie(Serie s)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Delete from Serie where idContenido = " + s.id);
        }

        internal void insertSerie(Serie s)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Insert into Serie values (" + s.id + "," + s.numTemporadas+")"); 
        }

        internal void modifySerie(Serie usuario)
        {
            //sin implementar
        }


        //Revisar esto
        internal void readSerie()
        {
            List<object> lseries = DBBroker.obtenerAgente().leer("SELECT c.Id, c.Nombre, c.FechaEstreno, c.Descripcion, c.Imagen, s.NumTemporadas FROM Contenido c JOIN Serie s ON c.Id = s.IdContenido");

            foreach (List<object> fila in lseries)
            {
                Serie s = new Serie
                {
                    id = Convert.ToInt32(fila[0]),
                    nombre = fila[1].ToString(),
                    fechaEstreno = fila[2] != DBNull.Value ? DateTime.Parse(fila[2].ToString()) : default,
                    descripcion = fila[3].ToString(),
                    imagen = fila[4].ToString(),
                    numTemporadas = Convert.ToInt32(fila[5])
                };

                this.serieList.Add(s);
            }
        }
    }
    
}

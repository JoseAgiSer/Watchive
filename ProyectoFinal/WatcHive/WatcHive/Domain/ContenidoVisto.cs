using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    internal class ContenidoVisto
    {

        private string _nombreUsuario;
        private int _idContendio;
        private int _idEmocion;
        private DateTime _fechaVisto;
        private int _puntuacion;
        private ContenidoVistoPersistence persistence;

        public ContenidoVisto() { persistence = new ContenidoVistoPersistence(); }
        public ContenidoVisto (string nombreUsuario, int idContenido, int idEmocion, DateTime fechaVisto, int puntuacion)
        {
            this._nombreUsuario = nombreUsuario;
            this._idContendio = idContenido;
            this._idEmocion = idEmocion;
            this._fechaVisto = fechaVisto.Date;
            this._puntuacion = puntuacion;
            this.persistence = new ContenidoVistoPersistence();
        }

        public string nombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public int idContenido { get => _idContendio; set => _idContendio = value; }
        public int idEmocion { get => _idEmocion; set => _idEmocion = value; }
        public DateTime fechaVisto { get => _fechaVisto; set => _fechaVisto = value.Date; }
        public int puntuacion { get => _puntuacion; set => _puntuacion = value; }


        public void readContenidoVisto(string username)
        {
            persistence.readContenidoVisto(username);
        }

        public List<ContenidoVisto> getListContenidoVisto()
        {
            return persistence.contenidoVistoList;
        }

        public void insert()
        {
            persistence.insertContenidoVisto(this);
        }

        public void delete()
        {
            persistence.deleteContenidoVisto(this);
        }

        public void update()
        {
            persistence.modifyContenidoVisto(this);
        }

        internal List<int> getRecomendacionUsuario(string useranme, string emocion, bool pelicula)
        {
            if (!pelicula)
            {
                return persistence.recomendacionUsuario(useranme, emocion, "serie");
            }
            else
            {
                return persistence.recomendacionUsuario(useranme, emocion, "pelicula");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    public class Serie : Contenido
    {

        private int _numTemporadas;
        private SeriePersistence persistence { get; set; }

        public Serie() { persistence = new SeriePersistence(); persistenceContenido = new ContenidoPersistence();}
        public Serie(int id, string nombre, DateTime fechaEstreno, string imagen, string descripcion, int numTemporadas, List<int> generos)
        {
            this._id = id;
            this._nombre = nombre;
            this._fechaEstreno = fechaEstreno.Date;
            this._imagen = imagen;
            this._descripcion = descripcion;
            this.persistence = new SeriePersistence();
            this.persistenceContenido = new ContenidoPersistence();
            this._numTemporadas = numTemporadas;
            this._listaGeneros = obtenerNombresGeneros(generos);
        }

        private List<string> obtenerNombresGeneros(List<int> generos)
        {
            return Genero.obtenerNombresGeneros(generos);
        }

        public int numTemporadas {get => _numTemporadas; set => _numTemporadas = value; }
        public override string Tipo => "Serie";

        public void readSerie()
        {
            persistence.readSerie();
        }

        public List<Serie> getListPeliculas()
        {
            return persistence.serieList;
        }

        public void insert()
        {
            persistenceContenido.insertContenido(this);
            persistence.insertSerie(this);
        }

        public void delete()
        {
            persistence.deleteSerie(this);
        }

        public void update()
        {
            persistence.modifySerie(this);
        }

        internal Serie readById(int id)
        {
            return persistenceContenido.readSerieById(id);
        }

    }
}

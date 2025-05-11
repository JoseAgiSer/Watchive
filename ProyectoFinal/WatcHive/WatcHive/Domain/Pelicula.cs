using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    public class Pelicula : Contenido
    {
        private int _duracion;

        private PeliculaPersistence persistence { get; set; }
        private float _notaMedia {  get; set; }

        public Pelicula() { persistence = new PeliculaPersistence(); persistenceContenido = new ContenidoPersistence(); }
        public Pelicula(int id, string nombre, DateTime fechaEstreno, string imagen, string descripcion, float nota, List<int> generos)
        {
            this._id = id;
            this._nombre = nombre;
            this._fechaEstreno = fechaEstreno.Date;
            this._imagen = imagen;
            this._descripcion = descripcion;
            this.persistence = new PeliculaPersistence();
            this.persistenceContenido = new ContenidoPersistence();
            this._notaMedia = nota;
            this.listaGeneros = obtenerNombresGeneros(generos);
        }


        public float nota { get => _notaMedia; set => _notaMedia = value; }
        public override string Tipo => "Pelicula";
        public void readPelicula()
        {
            persistence.readPeliculas();
        }

        public List<Pelicula> getListPeliculas()
        {
            return persistence.peliculaList;
        }
        private List<string> obtenerNombresGeneros(List<int> generos)
        {
            return Genero.obtenerNombresGeneros(generos);
        }

        public void insert()
        {
            persistenceContenido.insertContenido(this);
            persistence.insertPelicula(this);
        }

        public void delete()
        {
            persistence.deletePelicula(this);
        }

        public void update()
        {
            persistence.modifyPelicula(this);
        }

        internal bool isPelicula(int id)
        {
            return persistence.existsID(id);
        }

        internal Pelicula readById(int id)
        {
            return persistenceContenido.readPeliById(id);
        }
    }
}

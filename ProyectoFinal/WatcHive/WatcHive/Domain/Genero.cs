using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    internal class Genero
    {
        private int _id;
        private string _nombreGenero;
        private string _tipo;

        private GeneroPersistence persistence { get; set; }

        public Genero() { persistence = new GeneroPersistence(); }
        public Genero(int id, string nombreGenero )
        {
            _id = id;
            _nombreGenero = nombreGenero;
            persistence = new GeneroPersistence();
            _tipo = "both";
        }

        public int id { get => _id; set => _id = value; }
        public string nombreGenero { get => _nombreGenero; set => _nombreGenero = value; }
        public string tipo { get => _tipo; set => _tipo = value; }

        public void readGenero()
        {
            persistence.readGeneros();
        }

        public string readGeneroById(int id)
        {
            return persistence.readGeneroById(id);
        }

        public List<Genero> getListGenero()
        {
            return persistence.generoList;
        }

        public void insert()
        {
            persistence.insertGenero(this);
        }

        public void delete()
        {
            persistence.deleteGenero(this);
        }

        public void update()
        {
            persistence.modifyGenero(this);
        }

        public static List<string> obtenerNombresGeneros(List<int> generos)
        {
            List<string> listaNombres = new List<string>();
            Genero genero = new Genero();
            genero.readGenero();
            foreach (Genero g in genero.getListGenero())
            {
                if (generos.Contains(g.id))
                {
                    listaNombres.Add(g.nombreGenero);
                }
            }
            return listaNombres;
        }

        internal void insertGenerosInicio()
        {
            persistence.insertInicio();
        }

        internal int getIdByName(string genero)
        {
            return persistence.getidByName(genero);
        }
    }
}

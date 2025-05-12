using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    internal class Emocion
    {
        public EmocionPersistence persistence { get; set; }
        public Emocion() { persistence = new EmocionPersistence(); }
        private int _id;
        private string _nombre;

        public Emocion (int id, string nombre)
        {
            this._id = id;
            this._nombre = nombre;
            this.persistence = new EmocionPersistence();
        }
        public int id { get => _id; set => _id = value; }
        public string nombre { get => _nombre; set => _nombre = value; }

        public void readEmocion()
        {
            persistence.readEmocion();
        }

        public List<Emocion> getListEmociones()
        {
            return persistence.emocionList;
        }
        public void insert()
        {
            persistence.insertEmocion(this);
        }
        public void delete()
        {
            persistence.deleteEmocion(this);
        }
        public void update()
        {
            persistence.modifyEmocion(this);
        }
        internal void insertEmocionInicio()
        {
            persistence.insertInicio();
        }

        internal int getIdEmocion(string emocion)
        {
            return persistence.getIdEmocion(emocion);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    internal class Plataforma
    {
            private int _id;
            private string _nombrePlataforma;

            private PlataformaPersistence persistence { get; set; }

            public Plataforma() { persistence = new PlataformaPersistence(); }
            public Plataforma(int id, string nombrePlataforma)
            {
                this._id = id;
                this._nombrePlataforma = nombrePlataforma;
                persistence = new PlataformaPersistence();
            }

            public int id { get => _id; set => _id = value; }
            public string nombrePlataforma { get => _nombrePlataforma; set => _nombrePlataforma = value; }

            public void readPlataforma()
            {
                persistence.readPlataformas();
            }

            public List<Plataforma> getListPlataforma()
            {
                return persistence.plataformaList;
            }

            public void insert()
            {
                persistence.insertPlataforma(this);
            }

            public void delete()
            {
                persistence.deletePlataforma(this);
            }

            public void update()
            {
                persistence.modifyPlataforma(this);
            }
        }
}

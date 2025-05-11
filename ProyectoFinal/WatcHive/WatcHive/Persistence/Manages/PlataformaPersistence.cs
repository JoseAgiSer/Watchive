using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;

namespace WatcHive.Persistence.Manages
{
    internal class PlataformaPersistence
    {
        internal List<Plataforma> plataformaList { get; set; }

        public PlataformaPersistence() { plataformaList = new List<Plataforma>(); }

        internal void deletePlataforma(Plataforma genero)
        {
            throw new NotImplementedException();
        }

        internal void insertPlataforma(Plataforma genero)
        {
            throw new NotImplementedException();
        }

        internal void modifyPlataforma(Plataforma genero)
        {
            throw new NotImplementedException();
        }

        internal void readPlataformas()
        {
            throw new NotImplementedException();
        }
    }
}

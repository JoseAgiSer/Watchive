using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;

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

        internal void insertContenidoVisto(ContenidoVisto contenidoVisto)
        {
            throw new NotImplementedException();
        }

        internal void modifyContenidoVisto(ContenidoVisto contenidoVisto)
        {
            throw new NotImplementedException();
        }

        internal void readContenidoVisto()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;

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
            throw new NotImplementedException();
        }
    }
}

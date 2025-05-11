using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Domain;
using WatcHive.persistence;

namespace WatcHive.Persistence.Manages
{
    internal class PendientesPersistence
    {
        internal List<Pendientes> pendientesList { get; set; }

        public PendientesPersistence() { pendientesList = new List<Pendientes>(); }

        internal void deletePendientes(Pendientes genero)
        {
            throw new NotImplementedException();
        }

        internal void insertPendientes(Pendientes g)
        {
            DBBroker broker = DBBroker.obtenerAgente();
            broker.modifier("Insert into ListaPendientes values ('" + g.nombreUsuario + "'," + g.id+","+g.visto+",'" +
                ""+g.fechaAdicion.ToString("yyyy-MM-dd")+"',"+g.eliminado+")");
        }

        internal void modifyPendientes(Pendientes genero)
        {
            throw new NotImplementedException();
        }

        internal void readPendientes(string username)
        {
            List<object> lpendientes = DBBroker.obtenerAgente().leer("select * from ListaPendientes where NombreUsuario = '"+username+"'");

            foreach (List<object> fila in lpendientes)
            {
                Pendientes p = new Pendientes
                {
                    id = Convert.ToInt32(fila[1]),
                    nombreUsuario = fila[0].ToString(),
                    visto = Convert.ToInt32(fila[2]) == 1,
                    fechaAdicion = fila[3] != DBNull.Value ? DateTime.Parse(fila[3].ToString()) : default,
                    eliminado = Convert.ToInt32(fila[4]) == 1
                };

                this.pendientesList.Add(p);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatcHive.Persistence.Manages;

namespace WatcHive.Domain
{
    internal class Pendientes
    {
        private string _nombreUsuario;
        public int _id;
        public bool _visto;
        public DateTime _fechaAdicion;
        public bool _eliminado;
        private PendientesPersistence persistence { get; set; }

        public Pendientes() { persistence = new PendientesPersistence(); }
        public Pendientes(string nombreUsuario, int id, bool visto, DateTime fechaAdicion, bool eliminado)
        {
            _nombreUsuario = nombreUsuario;
            _id = id;
            _visto = visto;
            _fechaAdicion = fechaAdicion.Date;
            _eliminado = eliminado;
            persistence = new PendientesPersistence();
        }

        public int id { get => _id; set => _id = value; }
        public string nombreUsuario { get => _nombreUsuario; set => _nombreUsuario = value; }
        public bool visto { get => _visto; set => _visto = value; }
        public DateTime fechaAdicion { get => _fechaAdicion; set => _fechaAdicion = value.Date; }
        public bool eliminado { get => _eliminado; set => _eliminado = value; }

        public void readPendientes(string username)
        {
            persistence.readPendientes(username);
        }

        public List<Pendientes> getListPendientes()
        {
            return persistence.pendientesList;
        }

        public void insert()
        {
            persistence.insertPendientes(this);
        }

        public void delete()
        {
            persistence.deletePendientes(this);
        }

        public void update()
        {
            persistence.modifyPendientes(this);
        }
    }
}

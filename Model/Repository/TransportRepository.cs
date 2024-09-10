using RegionSyd.Model.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Repository
{
    internal class TransportRepository : Repository<Transport>
    {
        private static TransportRepository? _instance;

        // Fix to not have to Repository<Type> all places
        public static new TransportRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TransportRepository();
            }
            return _instance;
        }

        // private constructor to enforce correct usage
        private TransportRepository() : base()
        {
        }
    }
}

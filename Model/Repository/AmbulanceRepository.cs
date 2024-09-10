using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Repository.Repository
{
    public class AmbulanceRepository : Repository<Ambulance>
    {
        private static AmbulanceRepository? _instance;

        // Fix to not have to Repository<Type> all places
        public static new AmbulanceRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new AmbulanceRepository();
            }
            return _instance;
        }

        // private constructor to enforce correct usage
        private AmbulanceRepository() : base()
        {
        }
    }
}

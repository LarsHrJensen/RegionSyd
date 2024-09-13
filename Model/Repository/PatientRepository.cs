using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Repository
{
    internal class PatientRepository : Repository<Patient>
    {
        private static PatientRepository? _instance;

        // Fix to not have to Repository<Type> all places
        public static new PatientRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PatientRepository();
            }
            return _instance;
        }

        // private constructor to enforce correct usage
        private PatientRepository() : base()
        {
        }
    }
}

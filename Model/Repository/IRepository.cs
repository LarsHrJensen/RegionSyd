using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Repository
{
    /**
     * Repository interface benevolently given to us by our overlords.
     */
    public interface IRepository<T> where T : class
    {

        // Implement in such a way, that if the list is unchanged since last get all,
        // then simply return the current state of the list instead of querying the DB.
        public abstract IEnumerable<T> GetAll();

        public abstract T GetById(int id);
        public abstract void DeleteById(int id);
        public abstract void Insert(T entity);
        public abstract void Update(T entity);
    }
}

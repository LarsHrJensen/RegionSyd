using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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
        // It would be nice if GetAll() doesn't have to query everytime we need a list
        // However, multiple SQL clients could be manipulating data in the meanwhile and we don't 
        // have a way ( ? ) to know when DB has been changed since last GetAll().
        public abstract IEnumerable<T> GetAll();
        public abstract T GetById(int id);
        public abstract void DeleteById(int id);
        public abstract void Insert(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
    }
}

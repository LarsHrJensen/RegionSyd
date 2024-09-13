using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Repository
{
    public class Repository<T>
    {
        public Action RepositoryChange;
        public Action RepositoryInitialized;

        protected List<T> repo;

        protected Repository(List<T> repo)
        {
            this.repo = repo;
        }

        protected Repository()
        {
            repo = new List<T>();
        }

        // TODO : return error code instead perhaps
        public bool Add(T t)
        {
            foreach (T item in repo)
            {
                // If item already exists in repo
                if (item.Equals(t))
                {
                    return false;
                }
            }
            repo.Add(t);

            RepositoryHasChanged();
            return true;
        }


        // TODO : can error even occur here?
        public void Clear()
        {
            RepositoryHasChanged();
            repo.Clear();
        }

        // TODO : return error code instead perhaps
        public bool Remove(T t)
        {
            RepositoryHasChanged();
            return repo.Remove(t);
        }

        // TODO : Should return immutable list
        // And fix all instances of usage of this, you should not be able
        // to add or remove elements outside of the repository methods
        public List<T> GetAll()
        {
            return repo;
        }

        // TODO : method is disgusting, rewrite if scope creep allows it
        public T? Find<D>(string propname, Type datatype, D value)
        {
            foreach (T item in repo)
            {
                D comparison = (D)typeof(T).GetProperty(propname).GetValue(item, null);
                if (comparison.Equals(value))
                {
                    return item;
                }
            }
            return default;
        }

        public void RepositoryHasChanged()
        {
            RepositoryChange?.Invoke();
        }
    
    }
}

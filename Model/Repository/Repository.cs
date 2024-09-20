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

        public bool Add(T t)
        {
            foreach (T item in repo)
            {

                if (item.Equals(t))
                {
                    return false;
                }
            }
            repo.Add(t);
            RepositoryHasChanged();
            return true;
        }

        public void Clear()
        {
            RepositoryHasChanged();
            repo.Clear();
        }

        public bool Remove(T t)
        {
            RepositoryHasChanged();
            return repo.Remove(t);
        }


        public List<T> GetAll()
        {
            return repo;
        }


        public void RepositoryHasChanged()
        {
            RepositoryChange?.Invoke();
        }
    
    }
}

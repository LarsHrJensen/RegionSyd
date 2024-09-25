 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model.Store;

namespace RegionSyd.Model
{
    public class Ambulance
    {
        public int Id { get; private set; }
        public string Name { get; }
        public Hospital Hospital  {  get; }
        public string Status { get; }
        public List<Transport> Tasks {  get; }

        public Ambulance(int id, Hospital hospital, string status)
        {
            Id = id;
            Hospital= hospital; 
            Status = status;
            Tasks = new List<Transport>();
        }

        public Ambulance(string name, Hospital hospital, string status, int id=-1)
        {
            Name = name;
            Hospital = hospital;
            Status = status;
            Tasks = new List<Transport>();
            if (id > 0) Id = id;
        }

        // Idea is, we don't know Id before added to DB, Insert query results in identity 
        // So we can save the int as result, and put it here on the object itself.
        public void SetId(int id)
        {
            Id = id;
        }

        public void AssignTask(Transport assign)
        {
            Tasks.Add(assign);
            MessageStore.Message = "Task successfully assigned.";
        }

        public void CompleteTask(Transport complete)
        {
            Tasks.Remove(complete);
            MessageStore.Message = "Task completed.";
        }

        public void RemoveTask(Transport remove)
        {
            Tasks.Remove(remove);
            MessageStore.Message = "Task removed successfully.";
        }

        public List<Transport> GetTasks()
        {
            return Tasks;
        }
    } 
}

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
        public string Name { get; }
        public string Station {  get; }
        public string Status { get; }
        public List<Transport> Tasks {  get; }

        public Ambulance(string name, string station, string status)
        {
            Name = name;
            Station = station; 
            Status = status;
            Tasks = new List<Transport>();
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

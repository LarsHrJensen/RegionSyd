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
        public string ID { get; set; }
        public string Station {  get; set; }
        public string Status { get; set; }
        public List<Transport> Tasks {  get; set; }

        public Ambulance(string id, string station, string status)
        {
            ID = id;
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
    } 
}

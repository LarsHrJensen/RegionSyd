using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class Ambulance
    {
        public int Id { get; set; }
        public string Station {  get; set; }
        public string Status { get; set; }
        public List<Transport> Tasks {  get; set; }

        public Ambulance(int id, string station, string status)
        {
            Id = id; 
            Station = station; 
            Status = status;
            Tasks = new List<Transport>();
        }

        public void AssignTask(Transport assign)
        {
            Tasks.Add(assign);
            Console.WriteLine("Opgaven er oprettet");
        }

        public void CompleteTask(Transport complete)
        {
            Tasks.Remove(complete);

            // Update status of task 
            //complete.Status = "Done";

            Console.WriteLine("Opgaven er gennemført"); 
        }

        public void RemoveTask(Transport remove)
        {
            Tasks.Remove(remove);
            Console.WriteLine("Opgaven er slettet");
        }
    } 
}

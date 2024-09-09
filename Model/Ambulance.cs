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
        public List<string> Transport {  get; set; }

        public Ambulance(int id, string station, string status)
        {
            Id = id; 
            Station = station; 
            Status = status; 
            Transport = new List<string>();
        }

        public void AssignTask(Transport assign)
        {
            Transport.Add(assign);
            Console.WriteLine("Opgaven er oprettet");
        }

        public void CompleteTask(Transport complete)
        {

        }

        public void RemoveTask(Transport remove)
        {
            Transport.Remove(remove);
            Console.WriteLine("Opgaven er slettet");
        }
    } 
}

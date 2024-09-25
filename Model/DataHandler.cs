using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class DataHandler
    {
        private string filePath;
        public DataHandler(string path) 
        { 
            filePath = path;
        }
        public void LoadFromFile()
        {
            
        }

        public void SaveToFile<T>(List<T> items) 
        { 
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in items)
                {
                    if (item is Ambulance ambulance)
                    {
                        writer.WriteLine($"{ambulance.Hospital},{ambulance.Status}");
                    }

                    else if (item is Transport transport)
                    {
                        writer.WriteLine("");
                    }
                }
            }

            Console.WriteLine("Data er gemt");
        }
    }
}

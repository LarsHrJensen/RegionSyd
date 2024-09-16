using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    internal class Patient
    {
    public string CPR { get; set; }
    public string FullName { get; set; }
    public string Status { get; set; }


        public Patient(string cpr, string fullName, string status) 
        {
            CPR = cpr;
            FullName = fullName;
            Status = status;
        }


    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model
{
    public class Patient
    {
        public string CPR { get; set; }
        public string FullName { get; set; }
        public string Status { get; set; }
        public int Id { get; private set; }

        public Patient(string cpr, string fullName, string status, int id=-1) 
        {
            CPR = cpr;
            FullName = fullName;
            Status = status;
            if (id > 0) Id = id;
        }

    }
}

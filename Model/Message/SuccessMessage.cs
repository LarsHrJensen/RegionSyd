using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Message
{
    public class SuccessMessage : EventArgs
    {
        public string Message { get; set; }
    }
}

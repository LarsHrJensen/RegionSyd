using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Store
{
    public static class MessageStore
    {
        public static EventHandler Updated { get; set; }

        private static string message;
        public static string Message {
            get { return message; }
            set 
            { 
                message = value;
                Updated?.Invoke(null, EventArgs.Empty);
            } 
        }
        
    }
}

using RegionSyd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModel
{
    public class ChangeTransportViewMoldel : ViewModelBase
    {
        private Transport _transport;
        public Transport Transport { get { return _transport; } }

        public ChangeTransportViewMoldel(Transport transport) 
        {
            _transport = transport;
        }
    }
}

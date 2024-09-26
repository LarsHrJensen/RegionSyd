using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.Model.Helper
{
    public class TravelManager
    {
        IConfiguration _configuration;

        public TravelManager(IConfiguration config)
        {
            _configuration = config;
        }

        public void Setup()
        {
            // check if travelnodes are setup

            // if not setup travelnodes
        }
    }
}

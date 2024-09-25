﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegionSyd.Model.Repository;
using RegionSyd.Model;
using System.Windows.Controls;
using RegionSyd.ViewModel;
using Microsoft.Extensions.Configuration;

namespace RegionSyd.VIewModel
{
    public class ShowHospitalViewModel : ViewModelBase
    {
        public ObservableCollection<Transport> TransportList { get; set; }

        public ShowHospitalViewModel(IConfiguration config)
        {

            TransportRepository repo = new TransportRepository(config);

            TransportList = new ObservableCollection<Transport>(repo.GetAll());
        }
    }
}

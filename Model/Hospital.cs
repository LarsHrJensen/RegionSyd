﻿using System;
using System.Windows.Media.Converters;

public class Hospital
{

    public int Id { get; private set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }

    public Hospital(string name, string address, string city, string region, int id=-1)
    {   
        Name = name;
        Address = address;
        City = city;          
        Region = region;
        if (id > 0) Id = id;
    }

    public void SetId(int id)
    {
        Id = id;
    }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Name)) return false;
        if (string.IsNullOrEmpty(Address)) return false;
        if (string.IsNullOrEmpty(City)) return false;
        if (string.IsNullOrEmpty(Region)) return false;

        return true;
    }

}

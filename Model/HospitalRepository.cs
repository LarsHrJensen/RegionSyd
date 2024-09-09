using RegionSyd.Model;
using System;
using System.ComponentModel;

public class HospitalRepo
{
    public class HospitalRepo
    {
        public void Add(Hospital hospital)
        {
            hospitals.Add(hospital);
            Console.WriteLine($"Ny hospital (id: {hospital.Id} er oprettet");
        }

        public void Remove(Hospital hospital)
        {
            hospital.Remove(hospital);
            Console.WriteLine($"Hospitalet (id: {hospital.Id} er slettet");
        }

    }

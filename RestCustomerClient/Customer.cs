using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestCustomerClient
{
    public class Customer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Year { get; set; }

        public Customer(int id, string firstName, string lastName, int year)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Year = year;
        }

        public Customer()
        {

        }

        public override string ToString()
        {
            return $"ID: {ID}, FirstName: {FirstName}, LastName: {LastName}, Year: {Year}";
        }
    }
}

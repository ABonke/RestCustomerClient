using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RestCustomerClient
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Print alle Customers");
            var customers = GetCustomersAsync().Result;
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }

            Console.WriteLine("Print en bestemt Customer med ID: 101");
            var singleCustomer = GetCustomer(101).Result;
            Console.WriteLine(singleCustomer);

            Console.WriteLine("Test af Delete funktion hvor Customer med ID: 101 slettes");

            HttpResponseMessage deleteResult = DeleteCustomer(101).Result;

            Console.WriteLine(deleteResult);

            var customersDeleteTest = GetCustomersAsync().Result;
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }

            Customer csToPost = new Customer(103, "Soren", "Kongsgaard", 1989);
            HttpResponseMessage InsertCustomer = Program.InsertCustomer(csToPost).Result;

            Console.WriteLine(InsertCustomer);
            var customersDeleteTest2 = GetCustomersAsync().Result;
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);    
            }

            csToPost.FirstName = "Peter";
            var customerUpdate = UpdateCustomer(csToPost).Result;
            Console.WriteLine(customerUpdate);

            var customers3 = GetCustomersAsync().Result;
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        #region Get all Customers


        private static async Task<IList<Customer>> GetCustomersAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(CustomersUri);
                IList<Customer> cList = JsonConvert.DeserializeObject<IList<Customer>>(content);
                return cList;
            }
        }

        #endregion


        #region Get a single Customer


        private static async Task<Customer> GetCustomer(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(CustomersUri + id);
                Customer singleCustomer = JsonConvert.DeserializeObject<Customer>(content);
                return singleCustomer;
            }
        }

        #endregion

        private static async Task<HttpResponseMessage> DeleteCustomer(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.DeleteAsync(CustomersUri + id);
                return response;
            }
        }

        private static async Task<HttpResponseMessage> InsertCustomer(Customer cs)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(CustomersUri, cs);
                return response;
            }
        }

        private static async Task<HttpResponseMessage> UpdateCustomer(Customer cs)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.PutAsJsonAsync(CustomersUri, cs);
                return response;
            }
        } 
        public static string CustomersUri { get; set; } = "http://localhost:2776/CustomerService.svc/Customers/";
    }
}

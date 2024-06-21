using CustomerMaintenanceApp.Contexts;
using CustomerMaintenanceApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMaintenanceApp.Classes
{
    public class CustomerList : IEnumerable<Customer>
    {
        private List<Customer> customers = new List<Customer>();

        public int Count => customers.Count;

        public void Fill()
        {
            using (var context = new CustomerContext())
            {
                customers = context.Customers.ToList();
            }
        }

        public void Save()
        {
            using (var context = new CustomerContext())
            {
                context.Customers.RemoveRange(context.Customers);
                context.Customers.AddRange(customers);
                context.SaveChanges();
            }
        }

        public static CustomerList operator +(CustomerList list, Customer customer)
        {
            list.customers.Add(customer);
            list.OnChanged();
            return list;
        }

        public static CustomerList operator -(CustomerList list, Customer customer)
        {
            list.customers.Remove(customer);
            list.OnChanged();
            return list;
        }

        public delegate void ChangeHandler(CustomerList customers);
        public event ChangeHandler Changed;

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this);
        }

        public Customer this[int index]
        {
            get { return customers[index]; }
            set { customers[index] = value; }
        }

        public IEnumerator<Customer> GetEnumerator()
        {
            return customers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return customers.GetEnumerator();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace Servicios
{
    public class ConsultasBD
    {
        DataBaseEntityDataContext db = new DataBaseEntityDataContext();

        public IList<Customers> getCustomers()
        {
            var customers = (from cust in db.Customers
                             select cust).ToList();
            return customers;
        }

        public IList<Persons> getPersons()
        {
            var persons = (from psr in db.Persons
                             select psr).ToList();
            return persons;
        }
    }
}

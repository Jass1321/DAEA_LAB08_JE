using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAEA_LAB08_JE
{
    class Program
    {
        public static DataClasses01DataContext context = new DataClasses01DataContext();
        static void Main(string[] args)
        {
            //IntroToLINQ();
            DataSource();
            //Filtering();
            //Ordering();
            //Grouping();
            //Grouping2();
            //Joinning();

            //IntroToLambda();
            //DataSourceLambda();
            //FilteringLambda();
            //OrderingLambda();
            //GroupingLambda();
            //Grouping2Lambda();
            //JoinningLambda();
            Console.Read();
        }
        //LINQ
        static void IntroToLINQ()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        static void DataSource()
        {
            var queryAllCustomers = from cust in context.clientes
                                    select cust;

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Filtering()
        {
            var queryLondonCustomers = from cust in context.clientes
                                       where cust.Ciudad == "Londres"
                                       select cust;

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void Ordering()
        {
            var queryLondonCustomers3 =
                                from cust in context.clientes
                                where cust.Ciudad == "Londres"
                                orderby cust.NombreCompañia ascending
                                select cust;

            foreach (var item in queryLondonCustomers3)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void Grouping()
        {
            var queryCustomersByCity =
                            from cust in context.clientes
                            group cust by cust.Ciudad;

            foreach (var customersGroup in queryCustomersByCity)
            {
                Console.WriteLine(customersGroup.Key);
                foreach (clientes customer in customersGroup)
                {
                    Console.WriteLine("    {0}", customer.NombreCompañia);
                }
            }
        }
        static void Grouping2()
        {
            var custQuery =
                         from cust in context.clientes
                         group cust by cust.Ciudad into custGroup
                         where custGroup.Count() > 2
                         orderby custGroup.Key
                         select custGroup;

            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
            }
        }
        static void Joinning()
        {
            var innerJoinQuery =
                            from cust in context.clientes
                            join dist in context.Pedidos on cust.idCliente equals dist.IdCliente
                            select new { CustomerName = cust.NombreCompañia, DistributorName = dist.PaisDestinatario };

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
        //Lambda
        static void IntroToLambda()
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                numbers.Where(x => (x % 2 == 0)).Select(x => x).ToList();
            foreach (int num in numQuery)
            {
                Console.Write("{0,1} ", num);
            }
        }
        static void DataSourceLambda()
        {
            var queryAllCustomers = context.clientes.Select(c => c);

            foreach (var item in queryAllCustomers)
            {
                Console.WriteLine(item.NombreCompañia);
            }
        }
        static void FilteringLambda()
        {
            var queryLondonCustomers = context.clientes.Where(c => c.Ciudad == "Londres").Select(c => c);

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void OrderingLambda()
        {
            var queryLondonCustomers = context.clientes.Where(c => c.Ciudad == "Londres").OrderBy(c => c.NombreCompañia).Select(c => c);

            foreach (var item in queryLondonCustomers)
            {
                Console.WriteLine(item.Ciudad);
            }
        }
        static void GroupingLambda()
        {
            IQueryable<IGrouping<string, clientes>> queryCustomersByCity = context.clientes.GroupBy(c => c.Ciudad).Select(c => c);

            foreach (var customerGroup in queryCustomersByCity)
            {
                Console.WriteLine(customerGroup.Key);
                foreach (clientes customer in customerGroup)
                {
                    Console.WriteLine($"{customer.NombreCompañia}");
                }
            }
        }
        static void Grouping2Lambda()
        {
            var custQuery = context.clientes.GroupBy(c => c.Ciudad).Where(c => c.Count() > 2).OrderBy(c => c.Key).Select(c => c);
            foreach (var item in custQuery)
            {
                Console.WriteLine(item.Key);
                foreach (clientes customer in item)
                {
                    Console.WriteLine($"{customer.NombreCompañia}");
                }
            }
        }
        static void JoinningLambda()
        {
            var innerJoinQuery = context.clientes.Join(context.Pedidos, c => c.idCliente,
                                                        p => p.IdCliente, (c, p) => new {
                                                            CustomerName = c.NombreCompañia,
                                                            DistributorName = p.PaisDestinatario
                                                        });

            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine(item.CustomerName);
            }
        }
    }
}
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibox.Models;

namespace Tibox.Repository.Northwind
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public Customer CustomerWithOrders(int id)
        {
            using (var connection = new SqlConnection(_connectionString)) {
                var parameters = new DynamicParameters();
                parameters.Add("@customerId", id);

                using (var multiple = connection.QueryMultiple("CustomerWithOrders",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure)) {

                    var customer = multiple.Read<Customer>().Single(); //single indica cual es el padre relacion uno a muchos 
                    customer.Orders = multiple.Read<Order>();
                    return customer;
                }
            }
        }

        public Customer SearchByNames(string firstName, string lastName)
        {
            using (var connection = new SqlConnection(_connectionString)) {
                var parameters = new DynamicParameters();
                parameters.Add("@firstName", firstName);
                parameters.Add("@lastName", lastName);
                 
                return connection
                    .QueryFirst<Customer>("CustomerSearchByNames",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}

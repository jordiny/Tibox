using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibox.Models;

namespace Tibox.Repository.Northwind
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public Order OrderByOrderNumber(string OrderNumber)
        {
           using(var connection=new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@OrderNumber", OrderNumber);
                return connection
                    .QueryFirst<Order>(
                    "OrderByOrderNumber", parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Order OrderWithOrderItems(int Id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@OrderId", Id);

                using (var multiple = connection.QueryMultiple(
                    "OrderWithOrderItems", parameters, commandType: System.Data.CommandType.StoredProcedure)) {
                    var order = multiple.Read<Order>().Single();
                    order.OrderItems = multiple.Read<OrderItem>();
                    return order;
                }
            }
        }
    }
}

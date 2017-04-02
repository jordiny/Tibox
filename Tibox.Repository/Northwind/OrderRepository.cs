using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Tibox.Models;

namespace Tibox.Repository.Northwind
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public Order OrderByOrderNumber(string orderNumber)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@orderNumber", orderNumber);                

                return connection
                    .QueryFirst<Order>("dbo.OrderByOrderNumber",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public Order OrderWithOrderItems(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@orderId", id);

                using (var multiple =
                    connection.QueryMultiple("dbo.OrderWithOrderItems",
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure))
                {
                    var order = multiple.Read<Order>().Single();
                    order.OrderItems = multiple.Read<OrderItem>();
                    return order;
                }
            }
        }
        
    }
}

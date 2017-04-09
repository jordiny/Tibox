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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User ValidateUser(string email, string password)
        {
            using (var connection = new SqlConnection(_connectionString)) {
                var parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@password", password);

               /* QueryFirst diferencia entre anterior y este QueryFirstOrDefault
                * 
                * el primero asi no encuentre ningun valor o registro te devuelve el primero
                * el default si no lo encuentra retorna null
                * */
                return connection.QueryFirstOrDefault<User>("ValidateUser",
                    parameters, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}

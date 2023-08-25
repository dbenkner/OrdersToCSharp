using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;

namespace OrderControllerProject
{
    public class OrderController
    {
        public SqlConnection SqlConnection { get; set; }

        public OrderController(SqlConnection sqlConnection)
        {
            this.SqlConnection = sqlConnection;
        }
        public List<Order> GetAllOrders()
        {
            var sql = "Select * from Orders";
            var cmd = new SqlCommand(sql, SqlConnection);
            var reader = cmd.ExecuteReader();
            var orders = new List<Order>();
            while (reader.Read())
            {
                var odr = new Order();
                odr.Id = Convert.ToInt32(reader["Id"]);
                odr.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                odr.Date = Convert.ToDateTime(reader["Date"]);
                odr.Description = Convert.ToString(reader["Description"]);
                orders.Add(odr);    
            }
            reader.Close();
            return orders;
        }
        public void AddNewOrder(Order order)
        {
            var sql = "INSERT Into Orders (CustomerId, Date, Description)  " +
                " VALUES (@CustomerId, @Date, @Description)";
            var cmd = new SqlCommand (sql, SqlConnection);
            cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            cmd.Parameters.AddWithValue("@Date", order.Date);
            cmd.Parameters.AddWithValue("@Description", order.Description);
            var rowsaffected = cmd.ExecuteNonQuery();
            if(rowsaffected != 1)
            {
                throw new Exception($"Insert Failed! RA{rowsaffected}");
            }
        }
        public void UpDateOrder(Order order, int Id)
        {
            var sql = "Update Orders Set CustomerId = @CustomerId, Date= @Date, Description = @Description where Id = @Id;";
            var cmd = new SqlCommand(sql, SqlConnection);
            cmd.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            cmd.Parameters.AddWithValue("@Date", order.Date);
            cmd.Parameters.AddWithValue("@Description", order.Description);
            cmd.Parameters.AddWithValue("Id", Id);
            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected != 1)
            {
                throw new Exception($"Update Failed! Ra = {rowsAffected}");
            }
        }
        public void DeleteOrder(int Id)
        {
            var sql = "Delete From Orders Where Id= @Id";
            var cmd = new SqlCommand(sql, SqlConnection);
            cmd.Parameters.AddWithValue("@Id", Id);
            var RowsAffected = cmd.ExecuteNonQuery();
            if (RowsAffected != 1)
            {
                throw new Exception($"Delete Failed! Rows Affected: {RowsAffected}");
            }
        }
        public List<Order>? OrdersByCustId(int CustId)
        {
            var sql = "SELECT * FROM ORDERS WHERE CustomerID = @CustId";
            var cmd = new SqlCommand(sql, SqlConnection);
            cmd.Parameters.AddWithValue("@CustId", CustId);
            var reader = cmd.ExecuteReader();
            if(!reader.HasRows)
            {
                return null;
            }
            var Orders = new List<Order>();
            while (reader.Read())
            {
                var ord = new Order();
                ord.Id = Convert.ToInt32(reader["Id"]);
                ord.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                ord.Date = Convert.ToDateTime(reader["Date"]);
                ord.Description = Convert.ToString(reader["Description"]);
                Orders.Add(ord);
            }
            return Orders;
        }
        public Order? GetOrderById(int Id)
        {
            var sql = "Select * from Orders where Id= @Id ";
            var cmd = new SqlCommand(sql, SqlConnection);
            cmd.Parameters.AddWithValue("@Id", Id);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                return null;
            }
            reader.Read();
            var order = new Order();
            order.Id = Convert.ToInt32(reader["Id"]);
            order.CustomerId = Convert.ToInt32(reader["CustomerId"]);
            order.Date = Convert.ToDateTime(reader["Date"]);
            order.Description = Convert.ToString(reader["Description"]);
            return order;
        }
    }
}
using Microsoft.Data.SqlClient;
using OrderControllerProject;
using System.Data;

var connStr = "server=localhost\\sqlexpress;" +
    "database=SalesDb;" +
    "trusted_connection=true;" +
    "trustServerCertificate=true;";
var conn = new SqlConnection(connStr);
conn.Open();

if(conn.State != System.Data.ConnectionState.Open)
{
    throw new Exception("Connection To Sql server failed");
}
Console.WriteLine("Connection open with SQL Serer!");

var OrderCtrlr = new OrderController(conn);
List<Order>? Orders = OrderCtrlr.GetAllOrders();
foreach (var order in Orders)
{
    Console.WriteLine(order.ToString());
}

Orders.Clear();

var newOrder = new Order();
newOrder = OrderCtrlr.GetOrderById(2);
if(newOrder == null)
{
    Console.WriteLine("No orders found!");
}
else
{
    Console.WriteLine(newOrder.ToString());
}

conn.Close();
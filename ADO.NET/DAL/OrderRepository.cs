namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public class OrderRepository : IOrderRepository
    {
        private readonly DbProviderFactory factory;

        private readonly string connectionString;


        public OrderRepository(string connectionString, string provider = "System.Data.SqlClient")
        {
            this.connectionString = connectionString;
            this.factory = DbProviderFactories.GetFactory(provider);
        }

        public IEnumerable<Order> GetOrders()
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    var commandString = "select o.OrderId as id, o.OrderDate as orderDate, o.ShippedDate as shippedDate from Orders o";
                    this.CreateCommandString(command, commandString);

                    using (var reader = command.ExecuteReader())
                    {
                        var result = new List<Order>();
                        while (reader.Read())
                        {
                            var order = this.ReadOrder(reader);
                            result.Add(order);
                        }

                        return result;
                    }
                }
            }
        }

        public Order GetOrderWithDetails(int id)
        {
            Order order;
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    this.SetParameter("@id", id, command);

                    var commandStringForOrderByID =
                        "select o.OrderId as id, o.OrderDate as orderDate, o.ShippedDate as shippedDate, " +
                        "o.CustomerID as customerId, o.EmployeeID as employeeID" +
                        "from Orders o" +
                        "where o.OrderId = @id;" +
                        "select od.ProductId as productId, od.UnitPrice as unitPrice, od.Quantity as quantity " +
                        "from [Order Details] where od.OrderID = @id;";
                    this.CreateCommandString(command, commandStringForOrderByID);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return null;
                        }

                        order = this.ReadOrder(reader);
                        order.OrderDetails = this.ReadOrderDetails(reader);
                    }
                }
            }
            return order;
        }

        public void AddOrder(Order order)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    this.SetParameter("@shippedDate", order.ShippedDate, command);
                    this.SetParameter("@orderDate", order.OrderDate, command);
                    this.SetParameter("@customerId", order.OrderDetails.CustomerId, command);
                    this.SetParameter("@employeeId", order.OrderDetails.EmployeeId, command);
                    var commandString =
                        "insert into Orders(CustomerID, EmployeeID, OrderDate, ShippedDate) " +
                        "values(@customerId, @employeeId, @orderDate, @shippedDate)";
                    this.CreateCommandString(command, commandString);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool DeleteOrder(int id)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    this.SetParameter("@id", id, command);
                    var commandString = "select o.OrderId as id from Orders o where o.OrderId = @id";
                    this.CreateCommandString(command, commandString);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return false;
                        }
                    }

                    var orderStatus = this.GetOrderWithDetails(id).Status;
                    if (orderStatus == OrderStatus.InProcess || orderStatus == OrderStatus.New)
                    {
                        var deleteString = "delete from Orders o where o.OrderID = @id;";
                        this.CreateCommandString(command, deleteString);
                        command.ExecuteNonQuery();
                        return true;
                    }
                    return false;
                }
            }
        }

        public bool MarkAsDone(int id)
        {
            return this.SetDate(OrderStatus.Completed, "ShippedDate", id);
        }

        public bool SendToProcess(int id)
        {
            return this.SetDate(OrderStatus.InProcess, "OrderDate", id);
        }

        private bool SetDate(OrderStatus status, string columnName, int id)
        {
            using (var connection = this.factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    this.SetParameter("@id", id, command);
                    var commandString = "select o.OrderId as id from Orders o where o.OrderId = @id";
                    this.CreateCommandString(command, commandString);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return false;
                        }
                    }

                    var orderStatus = this.GetOrderWithDetails(id).Status;
                    if (orderStatus != status)
                    {
                        this.SetParameter("@date", DateTime.Now, command);
                        this.SetParameter("@column", columnName, command);
                        var updateString = "update Orders set @column = @date where o.OrderID = @id;";
                        this.CreateCommandString(command, updateString);
                        command.ExecuteNonQuery();
                        return true;
                    }

                    return false;
                }
            }
        }

        private void SetParameter(string key, object value, DbCommand command)
        {
            var param = command.CreateParameter();
            param.ParameterName = key;
            param.Value = value;
        }

        private void CreateCommandString(DbCommand command, string commandString)
        {
            command.CommandType = CommandType.Text;
            command.CommandText = commandString;
        }

        private OrderDetails ReadOrderDetails(DbDataReader reader)
        {
            var details = new OrderDetails()
            {
                CustomerId = (string)reader["customerId"],
                EmployeeId = (int)reader["employeeID"]
            };
            reader.NextResult();
            details.ProductId = (int)reader["productId"];
            details.Quantity = (int)reader["quantity"];
            details.UnitPrice = (double)reader["unitPrice"];
            return details;
        }

        private Order ReadOrder(DbDataReader reader)
        {
            var order = new Order()
            {
                Id = (int)reader["id"],
                OrderDate = (reader["orderDate"] as DateTime?) ?? null,
                ShippedDate = (reader["shippedDate"] as DateTime?) ?? null
            };
            if (reader.IsDBNull(1))
            {
                order.Status = OrderStatus.New;
            }
            else if (reader.IsDBNull(2))
            {
                order.Status = OrderStatus.InProcess;
            }
            else
            {
                order.Status = OrderStatus.Completed;
            }

            return order;
        }
    }
}

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DemoExam.Models;
using DemoExam.AttributeT;
using System.Deployment.Internal;

namespace DemoExam
{
    public static class DBModel
    {

        public static List<Product> SelectAllProducts()
        {
            List<Product> products = new List<Product>();
            string query = "select * from `товары`";
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        products.Add(new Product
                        {
                            ID = dr.GetInt32("ID"),
                            ProductName = dr.GetString("ProductName"),
                            Description = dr.GetString("Description"),
                            Category = dr.GetString("Category"),

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return products;
        }

        public static List<Shipment> SelectAllShipments()
        {
            List<Shipment> shipments = new List<Shipment>();
            string query = "select * from `отгрузки`";
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        shipments.Add(new Shipment
                        {
                            ID = dr.GetInt32("ID"),
                            ShipmentDate = dr.GetDateTime("ShipmentDate"),
                            IDManager = dr.GetInt32("IDManager"),

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return shipments;
        }

        public static List<ShipmentContent> SelectShipmentContentByShipmentID(int IDShipment)
        {
            List<ShipmentContent> shipmentContent = new List<ShipmentContent>();
            string query = "select * from `состав отгрузок` WHERE IDShipment=" + IDShipment;
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        shipmentContent.Add(new ShipmentContent
                        {
                            ID = dr.GetInt32("ID"),
                            IDShipment = dr.GetInt32("IDShipment"),
                            IDProduct = dr.GetInt32("IDProduct"),
                            Quantity = dr.GetInt32("Quantity"),
                            Volume = dr.GetDouble("Volume"),
                            Weight = dr.GetDouble("Weight"),
                            Sum = dr.GetDouble("Sum"),

                        });
                    }
                }
                mySqlDB.CloseConnection();
            }
            return shipmentContent;
        }

        public static Employee SelectEmployeeByID(int ID)
        {
            Employee employee = new Employee();
            string query = "select * from `сотрудники` where ID=" + ID;
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        employee.ID = dr.GetInt32("ID");
                        employee.IDContact = dr.GetInt32("IDContact");
                    }
                }
                mySqlDB.CloseConnection();
            }
            return employee;
        }

        public static ContactPerson SelectContactPersonByID(int ID)
        {
            ContactPerson contactPerson = new ContactPerson();
            string query = "select * from `контактные лица` where ID=" + ID;
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        contactPerson.ID = dr.GetInt32("ID");
                        contactPerson.FirstName = dr.GetString("FirstName");
                        contactPerson.LastName = dr.GetString("LastName");
                        contactPerson.Patronymic = dr.GetString("Patronymic");
                        contactPerson.Sex = dr.GetString("Sex");
                    }
                }
                mySqlDB.CloseConnection();
            }
            return contactPerson;
        }

        public static Product SelectProductByID(int ID)
        {
            Product Product = new Product();
            string query = "select * from `товары` where ID=" + ID;
            var mySqlDB = MySqlDB.GetDB();
            if (mySqlDB.OpenConnection())
            {
                using (MySqlCommand mc = new MySqlCommand(query, mySqlDB.sqlConnection))
                using (MySqlDataReader dr = mc.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Product.ID = dr.GetInt32("ID");
                        Product.ProductName = dr.GetString("ProductName");
                        Product.Description = dr.GetString("Description");
                        Product.Category = dr.GetString("Category");
                    }
                }
                mySqlDB.CloseConnection();
            }
            return Product;
        }

        public static void Update<T>(T value) where T : Base
        {
            string table;
            List<RowData> values;
            GetMetaData(value, out table, out values);
            var query = CreateUpdateQuery(table, values, value.ID);
            var db = MySqlDB.GetDB();
            db.ExecuteNonQuery(query.Item1, query.Item2);

        }

        private static (string, MySqlParameter[]) CreateUpdateQuery(string table, List<RowData> values, int id)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            stringBuilder.Append($" WHERE ID = {id}");
            return (stringBuilder.ToString(), parameters.ToArray());
        }

        private static void GetMetaData<T>(T value, out string table, out List<RowData> values)
        {
            var type = value.GetType();
            var tableAtrributes = type.GetCustomAttributes(typeof(TableAttribute), false);
            table = ((TableAttribute)tableAtrributes.First()).Table;
            values = new List<RowData>();
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var columnAttributes = prop.GetCustomAttributes(typeof(ColumnAttribute), false);
                if (columnAttributes.Length > 0)
                {
                    string column = ((ColumnAttribute)columnAttributes.First()).Column;
                    values.Add(new RowData { StrValue = column, ObjValue = prop.GetValue(value) });
                }
            }
        }

        private static List<MySqlParameter> InitParameters(List<RowData> values, StringBuilder stringBuilder)
        {
            var parameters = new List<MySqlParameter>();
            int count = 1;
            var rows = values.Select(s =>
            {
                parameters.Add(new MySqlParameter($"p{count}", s.ObjValue));
                return $"{s.StrValue} = @p{count++}";
            });
            stringBuilder.Append(string.Join(",", rows));
            return parameters;
        }

        public static int Insert<T>(T value)
        {
            string table;
            List<RowData> values;
            GetMetaData(value, out table, out values);
            var query = CreateInsertQuery(table, values);
            var db = MySqlDB.GetDB();
            int id = db.GetNextID(table);
            db.ExecuteNonQuery(query.Item1, query.Item2);
            return id;
        }

        private static (string, MySqlParameter[]) CreateInsertQuery(string table, List<RowData> values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"INSERT INTO `{table}` set ");
            List<MySqlParameter> parameters = InitParameters(values, stringBuilder);
            return (stringBuilder.ToString(), parameters.ToArray());
        }

    }

    struct RowData
    {
        public string StrValue;
        public object ObjValue;
    }
}

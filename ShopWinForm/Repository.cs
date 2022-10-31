using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopWinForm
{
    internal class Repository
    {
        /// <summary>
        /// Method to get all customers
        /// </summary>
        /// <returns></returns>
        public static DataSet getCustomers()
        {

            Connection connection = new Connection();
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            try
            {
                command.CommandText = "SELECT * FROM Customers";
                command.Connection = connection.cnx;
                adapter.SelectCommand = command;
                connection.cnx.Open();
                adapter.Fill(ds);
                connection.cnx.Close();

                return ds;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return ds;
        }

        /// <summary>
        /// Method to create a new Customer
        /// </summary>
        /// <returns></returns>
        public static bool setCustomer(Customer c)
        {
            bool result = false;
            Connection connection = new Connection();
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = "set dateformat dmy; INSERT INTO Customers VALUES ('" + c.name + "', '" + c.registeredAt + "')";

                command.Connection = connection.cnx;
                connection.cnx.Open();
                command.ExecuteNonQuery();
                connection.cnx.Close();

                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Method to delete customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool deleteCustomer(Customer c)
        {
            bool result = false;
            Connection connection= new Connection();
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = "DELETE FROM Customers WHERE ID = '" + c.id + "'";

                command.Connection = connection.cnx;
                connection.cnx.Open();
                command.ExecuteNonQuery();
                connection.cnx.Close();

                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Method to edit customer
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static bool editCustomer(Customer c)
        {
            bool result = false;
            Connection Connection = new Connection();
            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = "set dateformat dmy; UPDATE Customers SET name = '" + c.name + "', registeredAt = '" + c.registeredAt + "'"
                    + " WHERE ID = '" + c.id + "'";

                command.Connection = Connection.cnx;
                Connection.cnx.Open();
                command.ExecuteNonQuery();
                Connection.cnx.Close();

                result = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                result = false;
            }

            return result;
        }
    }
}
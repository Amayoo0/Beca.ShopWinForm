using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopWinForm
{
    public class Connection
    {
        public SqlConnection cnx;

        public Connection()
        {
            try
            {
                cnx = new SqlConnection("Data Source=WINAPVSPDQCMK16\\SQLEXPRESS;Initial Catalog=Sales;Integrated Security=True");
                Console.WriteLine("Successful connection.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in connection. " + ex.Message);
            }
        }
    }
}
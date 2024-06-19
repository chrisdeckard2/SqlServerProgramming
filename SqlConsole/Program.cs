using Microsoft.Data.SqlClient;
using System.Net.Http.Headers;

namespace SqlConsole;

internal class Program {


    static void Main(string[] args) {

        var ConnStr = "server=localhost\\sqlexpress;" +
                      "database=SalesDb;" +
                      "trusted_connection=true;" +
                      "trustServerCertificate=true;";

        var Conn = new SqlConnection(ConnStr);

        Conn.Open();

        if(Conn.State != System.Data.ConnectionState.Open) {

            throw new Exception("The connection didn't open!");
        }

        Console.WriteLine("The matrix has you ...");


        var sql = "SELECT * from Customers;";
        var sqlcmd = new SqlCommand(sql, Conn);
        var reader = sqlcmd.ExecuteReader();
        if (!reader.HasRows) {
            Console.WriteLine("The customer returned no rows...");

        }
        while (reader.Read()) {
            var id = Convert.ToInt32(reader["Id"]);
            var name = Convert.ToString(reader["Name"]);
            var Sales = Convert.ToDecimal(reader["Sales"]);
            Console.WriteLine($"Id: {id} | Name: {name} | Sales: {Sales:C}");
        }

        reader.Close();

        Conn.Close();


    }
}

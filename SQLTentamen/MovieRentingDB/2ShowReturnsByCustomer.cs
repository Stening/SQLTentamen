using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void _2ShowReturnsByCustomer ()
    {
        using (SqlConnection conn = new SqlConnection("context connection = true"))
        {
            SqlCommand cmd = new SqlCommand("SELECT First_Name, Last_Name , Movies.Title, Rent_Date , Return_Date, DATEDIFF(day,Rent_Date,Return_Date)*Cost_Per_Day as total "+
"from Members "+
"join Rents "+
"ON Member_ID = Member_Rent_ID_FK "+
"join Movies "+
"on Movie_ID = Movie_ID_FK "+
"and First_Name = 'John' "+
"Group by First_Name, Last_Name, Title, Rent_Date, Return_Date, Cost_Per_Day");

            cmd.Connection = conn;
            conn.Open();
            SqlDataReader sqldr = cmd.ExecuteReader();
            SqlContext.Pipe.Send(sqldr);
            sqldr.Close();
            conn.Close();

        }
    }
}

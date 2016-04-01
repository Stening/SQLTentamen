using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ViewAmmountOfSciFi ()
    {
        using (SqlConnection conn = new SqlConnection("context connection = true"))
        {

            //SqlCommand cmd = new SqlCommand("SELECT * from Movies");
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) as 'Horror/sci-fi movies' " +
"from Movies, Categories " +
"where Category_ID = Movies.Category_ID_FK " +
"and Categories.Category_Title = 'Horror/sci-fi'");
            cmd.Connection = conn;
            conn.Open();
            SqlDataReader sqldr = cmd.ExecuteReader();
            SqlContext.Pipe.Send(sqldr);
            sqldr.Close();
            conn.Close();

        }
        

    }
}

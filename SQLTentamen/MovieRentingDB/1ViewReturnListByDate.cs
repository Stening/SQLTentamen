using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void _1ViewReturnListByDate (SqlDateTime Date)
    {
        using (SqlConnection conn = new SqlConnection("context connection = true"))
        {
            SqlCommand cmd = new SqlCommand("SELECT Title, Movie_ID, First_Name, Last_Name,Rent_Date , Return_Date FROM Members"+
"left Join Rents ON Member_Rent_ID_FK = Member_ID "+
"join Movies ON Movie_ID = Movie_ID_FK "+
" AND Return_Date = @date");
            SqlParameter DatePAram = new SqlParameter();
            DatePAram.DbType = DbType.DateTime;
            DatePAram.Value = Date;
            DatePAram.ParameterName = "@date";
            cmd.Parameters.Add(Date);
            conn.Open();
            SqlDataReader sqldr = cmd.ExecuteReader();
            SqlContext.Pipe.Send(sqldr);
            conn.Close();

        }
    }
}

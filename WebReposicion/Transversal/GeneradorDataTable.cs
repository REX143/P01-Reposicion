using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebReposicion.Transversal
{
    public class GeneradorDataTable
    {

        public static DataTable dtProcedimientoAlmacenado(string sp)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sp;
                cmd.Connection = conn;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();

            }
            return dt;
        }

        public static string dtreporteGenerado()
        {
            return "OK";
        }

    }
}
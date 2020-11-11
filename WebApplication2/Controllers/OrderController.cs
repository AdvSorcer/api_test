using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApplication2.Controllers
{
    public class OrderController : ApiController
    {
        [HttpGet]
        public string test()
        {
           
            string connectionString = "Server=192.168.180.215;Database=plm;User Id=innovator;Password=Plmsa@2020*!;";
            string JSONresult;
            DataTable dataTable = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = " SELECT TOP (10) KEYED_NAME,TS_CUSTOMER_ITEM FROM [plm].[innovator].[PART] ";

                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandTimeout = 0;
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(command);

                da.Fill(dataTable);
                da.Dispose();
               
                JSONresult = JsonConvert.SerializeObject(dataTable);
            }
           
            return JSONresult;
        }
    }

}

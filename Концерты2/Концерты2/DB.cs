using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Концерты2
{
    class DB
    {
        public string connectionstring = "Data Source=WIN-C89V64B1CKK;Initial Catalog=Yavlados;Integrated Security=True";
        bool openconnection;
        SqlConnection con = new SqlConnection("Data Source = (local)");
    }
}
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class KeyDAO
    {
        public KeyDTO getKey(string keyId)
        {
            SqlConnection con = DatabaseHelperCryptography.getConnectionKeyDB();
            con.Open();
            SqlCommand cmd = new SqlCommand($"select * from key_repo where cryptographic_id = '{keyId}'");
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            KeyDTO key = null;
            while (reader.Read())
            {
                key = new KeyDTO(reader.GetString(0), reader.GetString(1), reader.GetString(2));
            }
            con.Close();
            return key;
        }
    }
}

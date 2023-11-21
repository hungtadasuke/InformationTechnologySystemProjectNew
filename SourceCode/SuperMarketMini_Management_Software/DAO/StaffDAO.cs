using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Information;
using DTO;

namespace DAO
{
    public class StaffDAO
    {
        public DataTable getAllStaff()
        {
            SqlConnection con = DatabaseHelperCryptography.getConnectionQLSTDB();
            con.Open();
            SqlCommand cmd = new SqlCommand("select StaffId, StaffName, Gender, Birthday, NumberPhone, AddressNow, Position, StatusItem from Staff");
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Substitution substitution = KeyProcessing.ActSubstitution();
                RSA rsa = KeyProcessing.ActRSA();
                foreach (DataRow dr in dt.Rows)
                {
                    dr["StaffName"] = substitution.decode(dr["StaffName"].ToString());
                    dr["Birthday"] = rsa.Decode(dr["Birthday"].ToString());
                    dr["NumberPhone"] = rsa.Decode(dr["NumberPhone"].ToString());
                    dr["AddressNow"] = rsa.Decode(dr["AddressNow"].ToString());
                }
            }
            con.Close();
            return dt;
        }

        public DataTable getAllPosition()
        {
            SqlConnection con = DatabaseHelperCryptography.getConnectionQLSTDB();
            con.Open();
            SqlCommand cmd = new SqlCommand("select Position from Staff");
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            con.Close();
            return dt;
        }

        public DataTable getAllID()
        {
            SqlConnection con = DatabaseHelperCryptography.getConnectionQLSTDB();
            con.Open();
            SqlCommand cmd = new SqlCommand("select StaffId from Staff");
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            con.Close();
            return dt;
        }

        public bool addStaff(string StaffName, string Gender, string Birthday, string NumberPhone, string AddressNow, string Position, bool StatusItem)
        {
            try
            {
                SqlConnection con = DatabaseHelperCryptography.getConnectionQLSTDB();
                con.Open();
                Substitution substitution = KeyProcessing.ActSubstitution();
                RSA rsa = KeyProcessing.ActRSA();
                int status = StatusItem ? 1 : 0;
                SqlCommand cmd = new SqlCommand($"insert into Staff(StaffName, Gender, Birthday, NumberPhone, AddressNow, Position, StatusItem) Values (N'{substitution.encode(StaffName)}', N'{Gender}', '{rsa.Encode(Birthday)}', '{rsa.Encode(NumberPhone)}',N'{rsa.Encode(AddressNow)}',N'{Position}', {status})");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                con.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public bool editStaff(string StaffName, string Gender, string Birthday, string NumberPhone, string AddressNow, string Position, bool StatusItem, string StaffID)
        {
            try
            {
                SqlConnection con = DatabaseHelperCryptography.getConnectionQLSTDB();
                con.Open();
                Substitution substitution = KeyProcessing.ActSubstitution();
                RSA rsa = KeyProcessing.ActRSA();
                int status = StatusItem ? 1 : 0;
                SqlCommand cmd = new SqlCommand($"update Staff set StaffName = N'{substitution.encode(StaffName)}', Gender = N'{Gender}', Birthday = '{rsa.Encode(Birthday)}', NumberPhone = '{rsa.Encode(NumberPhone)}', AddressNow = N'{rsa.Encode(AddressNow)}', Position = N'{Position}', StatusItem = {status} where StaffId = '{StaffID}'");
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                con.Close();
                return true;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public static StaffDTO getStaffById(string id)
        {
            StaffDTO staff = new StaffDTO();
            SqlConnection con = DatabaseHelperCryptography.getConnectionQLSTDB();
            con.Open();
            SqlCommand cmd = new SqlCommand("select StaffId, StaffName, Gender, Birthday, NumberPhone, AddressNow, Position, StatusItem from Staff where StaffId like '" + id + "'");
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Substitution substitution = KeyProcessing.ActSubstitution();
            staff.StaffID = reader[0].ToString();
            staff.StaffName = substitution.decode(reader[1].ToString());
            reader.Close();
            return staff;
        }
    }
}
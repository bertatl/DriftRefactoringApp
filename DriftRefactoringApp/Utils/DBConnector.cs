using System;
using System.Configuration;
using System.Data.SqlClient;
using DriftRefactoringApp.Models;

namespace DriftRefactoringApp.Utils
{
    public class DBConnector
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["PersonDBConnectionString"].ConnectionString;
        private Person createPerson(int personId, string personName, string personHandle)
        {
            Person myPerson = new Person();
            myPerson.PersonId = personId;
            myPerson.Name = personName;
            myPerson.UserHandle = personHandle;
            return myPerson;
        }

        public int GetPersonCount()
        {
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string checkuser = "select count(*) from Users";
                SqlCommand cmd = new SqlCommand(checkuser, conn);
                int personCount = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                conn.Close();
                return personCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }

            return -1;
        }

        public Person GetPerson(int personId)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string checkUser = "select Id, Name, UserHandle from Users where Id=" + personId;
                SqlCommand cmd = new SqlCommand(checkUser, conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return createPerson(Convert.ToInt32(reader[0].ToString()), reader[1].ToString(), reader[2].ToString());
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }

            return null;
        }

        public bool AddPerson(Person person)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connStr);
                conn.Open();
                string insertQuery = "insert into Users(Id,Name,UserHandle) values (@pId,@pName,@pHandle)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@pId", person.PersonId);
                cmd.Parameters.AddWithValue("@pName", person.Name);
                cmd.Parameters.AddWithValue("@pHandle", person.UserHandle);
                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
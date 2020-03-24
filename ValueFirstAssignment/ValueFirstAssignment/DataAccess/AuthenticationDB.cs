using ValueFirstAssignment.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ValueFirstAssignment.DataAccess
{
    public class AuthenticationDB 
    {

        #region public methods

        public static string ConnectionString = AppConfiguration.ConnectionString;


        public static User GetUserById(int Id)
        {
            User thisUser = null;
            using (SqlConnection thisConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("User_Get", thisConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Id", Id);
                    thisConn.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            thisUser = FillDataRecord(myReader);
                        }
                        myReader.Close();
                    }
                }
                thisConn.Close();
            }
            return thisUser;
        }

        public static User GetUserByEmail(string Email)
        {
            User thisUser = null;
            using (SqlConnection thisConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("User_Get", thisConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Email", Email);
                    thisConn.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            thisUser = FillDataRecord(myReader);
                        }
                        myReader.Close();
                    }
                }
                thisConn.Close();
            }
            return thisUser;
        }

        public static User GetUserValidate(string Email, string Password)
        {
            User thisUser = null;
            using (SqlConnection thisConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("UserLogin_Get", thisConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Email", Email);
                    myCommand.Parameters.AddWithValue("@Password", Password);
                    thisConn.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.Read())
                        {
                            thisUser = FillDataRecord(myReader);
                        }
                        myReader.Close();
                    }
                }
                thisConn.Close();
            }
            return thisUser;
        }

        public static List<User> GetUsers()
        {
            List<User> thisUser = new List<User>();
            using (SqlConnection thisConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("User_Get", thisConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    thisConn.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                thisUser.Add(FillDataRecord(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
                thisConn.Close();
            }
            return thisUser;

        }



        public static int Save(User user)
        {
            int UserId = 0;
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("User_Set", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Id", user.UserId);
                    myCommand.Parameters.AddWithValue("@Email", user.Email);
                    myCommand.Parameters.AddWithValue("@Password", user.Password);
                    myCommand.Parameters.AddWithValue("@FullName", user.FullName);
                    myCommand.Parameters.AddWithValue("@Phone", user.Phone);
                    myCommand.Parameters.AddWithValue("@CommunicationAddress", user.CommunicationAddress);
                    myCommand.Parameters.AddWithValue("@IsActive", user.IsActive);
                    myCommand.Parameters.AddWithValue("@RolesId", user.RolesId);
                    myConnection.Open();
                    UserId = Convert.ToInt32(myCommand.ExecuteScalar());
                }
                myConnection.Close();
            }
            return UserId;
        }
        

        public static int UpdateUserStatus(int Id, bool status)
        {
            int UserId = 0;

            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("UserStatus_Update", myConnection))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Id", Id);
                    myCommand.Parameters.AddWithValue("@IsActive", status);
                    myConnection.Open();
                    UserId = Convert.ToInt32(myCommand.ExecuteScalar());
                }
                myConnection.Close();
            }
            return UserId;
        }


      
        public static List<Role> GetUserRoles(int UserId)
        {
            List<Role> thisRole = new List<Role>();
            using (SqlConnection thisConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("UserRoles_Get", thisConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@UserId", UserId);
                    thisConn.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                thisRole.Add(FillDataRecordRole(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
                thisConn.Close();
            }
            return thisRole;

        }


        public static List<Role> GetUserRolesByEmail(string Email)
        {
            List<Role> thisRole = new List<Role>();
            using (SqlConnection thisConn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCommand = new SqlCommand("UserRoles_Get", thisConn))
                {
                    myCommand.CommandType = CommandType.StoredProcedure;
                    myCommand.Parameters.AddWithValue("@Email", Email);
                    thisConn.Open();
                    using (SqlDataReader myReader = myCommand.ExecuteReader())
                    {
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                thisRole.Add(FillDataRecordRole(myReader));
                            }
                        }
                        myReader.Close();
                    }
                }
                thisConn.Close();
            }
            return thisRole;

        }

        #endregion

        #region Private Methods

        private static User FillDataRecord(IDataRecord myRecord)
        {
            User user = new User();
            user.UserId = myRecord.GetInt32(myRecord.GetOrdinal("UserId"));

            if (!myRecord.IsDBNull(myRecord.GetOrdinal("Email")))
            {
                user.Email = myRecord.GetString(myRecord.GetOrdinal("Email"));
            }

            if (!myRecord.IsDBNull(myRecord.GetOrdinal("Password")))
            {
                user.Password = myRecord.GetString(myRecord.GetOrdinal("Password"));
            }

            if (!myRecord.IsDBNull(myRecord.GetOrdinal("FullName")))
            {
                user.FullName = myRecord.GetString(myRecord.GetOrdinal("FullName"));
            }


            if (!myRecord.IsDBNull(myRecord.GetOrdinal("Phone")))
            {
                user.Phone = myRecord.GetString(myRecord.GetOrdinal("Phone"));
            }

            if (!myRecord.IsDBNull(myRecord.GetOrdinal("CommunicationAddress")))
            {
                user.CommunicationAddress = myRecord.GetString(myRecord.GetOrdinal("CommunicationAddress"));
            }

            if (!myRecord.IsDBNull(myRecord.GetOrdinal("IsActive")))
            {
                user.IsActive = myRecord.GetBoolean(myRecord.GetOrdinal("IsActive"));
            }


            if (!myRecord.IsDBNull(myRecord.GetOrdinal("RolesId")))
            {
                user.RolesId = myRecord.GetString(myRecord.GetOrdinal("RolesId"));
            }

            if (!string.IsNullOrEmpty(user.RolesId))
            {
                string[] roleIds = user.RolesId.Split(',');
                user.Roles = new List<Role>();
                foreach (var item in roleIds)
                {
                    Role role = new Role()
                    {
                        RoleId = int.Parse(item),
                        RoleName = Enum.GetName(typeof(RoleEnum), int.Parse(item))
                    };
                    user.Roles.Add(role);
                }

                user.RolesName =string.Join(",", user.Roles.Select(x => x.RoleName));
            }
            return user;
        }


        private static Role FillDataRecordRole(IDataRecord myRecord)
        {
            Role role = new Role();
            role.RoleId = myRecord.GetInt32(myRecord.GetOrdinal("RoleId"));

            if (!myRecord.IsDBNull(myRecord.GetOrdinal("RoleName")))
            {
                role.RoleName = myRecord.GetString(myRecord.GetOrdinal("RoleName"));
            }
            

            return role;
        }



        #endregion
    }
}
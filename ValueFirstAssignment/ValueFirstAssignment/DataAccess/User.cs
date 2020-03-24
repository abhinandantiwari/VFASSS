using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ValueFirstAssignment.DataAccess
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string CommunicationAddress { get; set; }
        public bool IsActive { get; set; }
        public List<Role> Roles { get; set; }

        //View Model Uses
        public string RolesId { get; set; }
        public string RolesName { get; set; }
    }
}
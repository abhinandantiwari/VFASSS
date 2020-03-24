using System;
using ValueFirstAssignment.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ValueFirstAssignment.Authentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties

        public int UserId { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public ICollection<Role> Roles { get; set; }

        #endregion

        public CustomMembershipUser(User user):base("CustomMembership", user.Email, user.UserId, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserId = user.UserId;
            Email = user.Email;
            Roles = user.Roles;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace webinar.Helpers
{
    public class CustomAuthorizationAttribute: AuthorizeAttribute
    {
        private readonly string[] Accessroles;

        public CustomAuthorizationAttribute(params string[] roles)
        {
            this.Accessroles = roles;

        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            bool authorize = false;
            var id = Convert.ToString(httpContext.Session["UserId"]);
            if(!string.IsNullOrEmpty(id))
            {
                string query = "select RoleName from RapidDataUsers where UserId=@UserId";
                string constr = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                using(SqlConnection sqlcon = new SqlConnection(constr))
                {
                    SqlCommand cmd = new SqlCommand(query, sqlcon);
                    sqlcon.Open();
                    cmd.Parameters.AddWithValue("UserId", id);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while(sdr.Read())
                    {
                        var dbrole = sdr["RoleName"]==DBNull.Value?string.Empty:sdr["RoleName"].ToString();

                        foreach(var role in Accessroles)
                        {
                            if (role == dbrole) return true;
                        }
                    }
                    return authorize;
                }
            }
            return authorize;
        }



        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
               new RouteValueDictionary
               {
                    { "controller", "Admin" },
                    { "action", "UnAuthorized" }
               });
        }

    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;
using System.Security.Principal;
using System.Collections.Generic;
using System.Web.Security;
using System.Web;

namespace HIMS.Models
{
    public class SiteIdentity
    {
        public static int UserId = 0;
        public static string Name = string.Empty;
        public static string Email = string.Empty;
        public static string Roles = string.Empty;
        
        public static void Load()
        {
            FormsIdentity ident = HttpContext.Current.User.Identity as FormsIdentity;
            bool chkUser = false;
            if (ident != null)
            {
                FormsAuthenticationTicket ticket = ident.Ticket;
                if (ticket != null)
                {
                    string userDataString = ticket.UserData;
                    string[] userDataPieces = userDataString.Split("|".ToCharArray());
                    UserId = Int32.Parse(userDataPieces[0]);
                    Name = userDataPieces[1];
                    Email = userDataPieces[2];
                    Roles = userDataPieces[3];
                    chkUser = true;
                }
            }
            if(!chkUser)
            {
                UserId = 0;
                Name = string.Empty;
                Email = string.Empty;
                Roles = string.Empty;
            }

        }
    }
}


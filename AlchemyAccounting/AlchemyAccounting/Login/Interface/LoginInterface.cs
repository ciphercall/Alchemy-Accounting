using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.Login.Interface
{
    public class LoginInterface
    {
        string userID;

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        string securityQ;

        public string SecurityQ
        {
            get { return securityQ; }
            set { securityQ = value; }
        }
        string securityA;

        public string SecurityA
        {
            get { return securityA; }
            set { securityA = value; }
        }
    }
}
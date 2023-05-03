using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.cr_user.Interface
{
    public class crinterface
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string usid;
    
        public string Usid
        {
            get { return usid; }
            set { usid = value; }
        }
        private string branch;

        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }
        private string userType;

        public string UserType
        {
            get { return userType; }
            set { userType = value; }
        }
        private string pass;

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }
        private string pcname;

        public string Pcname
        {
            get { return pcname; }
            set { pcname = value; }
        }
        private string ipaddress;

        public string Ipaddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }
        private string openuser;

        public string Openuser
        {
            get { return openuser; }
            set { openuser = value; }
        }
    }
}
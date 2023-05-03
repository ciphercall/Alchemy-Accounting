using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.Stock.Interface
{
    public class StockInterface
    {
        private string pstp;

        public string Pstp
        {
            get { return pstp; }
            set { pstp = value; }
        }

        private string pscd;

        public string Pscd
        {
            get { return pscd; }
            set { pscd = value; }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string contactno;

        public string Contactno
        {
            get { return contactno; }
            set { contactno = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string webid;

        public string Webid
        {
            get { return webid; }
            set { webid = value; }
        }
        private string cpnm;

        public string Cpnm
        {
            get { return cpnm; }
            set { cpnm = value; }
        }
        private string cpno;

        public string Cpno
        {
            get { return cpno; }
            set { cpno = value; }
        }
        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        private string ps_ID;

        public string Ps_ID
        {
            get { return ps_ID; }
            set { ps_ID = value; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.CNF.Party
{
    public class PartyInformationModel
    {

        string partyId;
        public string PartyID
        {
            get { return partyId; }
            set { partyId = value; }
        }

        string partyname;
        public string PartyNanme
        {
            get { return partyname; }
            set { partyname = value; }
        }

        string address;
        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        string contact;
        public string Contact
        {
            get { return contact; }
            set { contact = value; }
        }

        string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        string web;
        public string Web
        {
            get { return web; }
            set { web = value; }
        }

        string apname;
        public string APName
        {
            get { return apname; }
            set { apname = value; }
        }

        string apcontact;
        public string APContact
        {
            get { return apcontact; }
            set { apcontact = value; }
        }

        string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        string userpc;
        public string UserPC
        {
            get { return userpc; }
            set { userpc = value; }
        }

        string authUserId;
        public string AUTHUSERID
        {
            get { return authUserId; }
            set { authUserId = value; }
        }

        DateTime inTime;
        public DateTime InTime
        {
            get { return inTime; }
            set { inTime = value; }
        }

        DateTime updateTime;
        public DateTime UpdateTime
        {
            get { return updateTime; }
            set { updateTime = value; }
        }

        string IpAddress;
        public string IPAddress
        {
            get { return IpAddress; }
            set { IpAddress = value; }
        }

        string logid;
        public string Logid
        {
            get { return logid; }
            set { logid = value; }
        }

        string logpw;
        public string Logpw
        {
            get { return logpw; }
            set { logpw = value; }
        }
    }
}
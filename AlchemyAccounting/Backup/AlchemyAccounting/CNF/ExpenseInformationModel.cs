using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.CNF
{
    public class ExpenseInformationModel
    {
        public string expcnm;
        public string EXPCNM
        {
            get { return expcnm; }
            set { expcnm = value; }
        }

        string expcid;
        public string EXPCID
        {
            get { return expcid; }
            set { expcid = value; }
        }


        string expid;
        public string EXPID
        {
            get { return expid; }
            set { expid = value; }
        }

        string expnm;
        public string EXPNM
        {
            get { return expnm; }
            set { expnm = value; }
        }

        string remarks;
        public string REMARKS
        {
            get { return remarks; }
            set { remarks = value; }
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

    }
}
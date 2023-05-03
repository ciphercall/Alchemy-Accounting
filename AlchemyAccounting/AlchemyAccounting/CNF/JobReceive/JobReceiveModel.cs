using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.CNF.JobReceive
{
    public class JobReceiveModel
    {
        DateTime transdt;
        public DateTime TRANSDT
        {
            get { return transdt; }
            set { transdt = value; }
        }

        string transmy;
        public string TRANSMY
        {
            get { return transmy; }
            set { transmy = value; }
        }

        Int64 transno;
        public Int64 TRANSNO
        {
            get { return transno; }
            set { transno = value; }
        }

        string transfor;
        public string TRANSFOR
        {
            get { return transfor; }
            set { transfor = value; }
        }

        string compID;
        public string COMPID
        {
            get { return compID; }
            set { compID = value; }
        }

        Int64 jobyy;
        public Int64 JOBYY
        {
            get { return jobyy; }
            set { jobyy = value; }
        }

        string jobtp;
        public string JOBTP
        {
            get { return jobtp; }
            set { jobtp = value; }
        }

        Int64 jobno;
        public Int64 JOBNO
        {
            get { return jobno; }
            set { jobno = value; }
        }

        string partyId;
        public string PARTYID
        {
            get { return partyId; }
            set { partyId = value; }
        }

        string debitcd;
        public string DEBITCD
        {
            get { return debitcd; }
            set { debitcd = value; }
        }

        string remarks;
        public string REMARKS
        {
            get { return remarks; }
            set { remarks = value; }
        }

        decimal amount;
        public decimal AMOUNT
        {
            get { return amount; }
            set { amount = value; }
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
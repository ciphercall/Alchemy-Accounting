using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.CNF.JobStatus
{
    public class JobStatusModel
    {
        DateTime transdt;
        public DateTime TRANSDT
        {
            get { return transdt; }
            set { transdt = value; }
        }

        Int64 transyy;
        public Int64 TRANSYY
        {
            get { return transyy; }
            set { transyy = value; }
        }

        Int64 transno;
        public Int64 TRANSNO
        {
            get { return transno; }
            set { transno = value; }
        }

        string compid;
        public string COMPID
        {
            get { return compid; }
            set { compid = value; }
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

        DateTime billfdt;
        public DateTime BILLFDT
        {
            get { return billfdt; }
            set { billfdt = value; }
        }

        char status;
        public char STATUS
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

        private string expID;

        public string ExpID
        {
            get { return expID; }
            set { expID = value; }
        }
        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        private string partyID;

        public string PartyID
        {
            get { return partyID; }
            set { partyID = value; }
        }
        private Int64 exSL;

        public Int64 ExSL
        {
            get { return exSL; }
            set { exSL = value; }
        }
        private string userNm;

        public string UserNm
        {
            get { return userNm; }
            set { userNm = value; }
        }
        private string updateuserNm;

        public string UpdateuserNm
        {
            get { return updateuserNm; }
            set { updateuserNm = value; }
        }

    }
}
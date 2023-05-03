using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.CNF.JobBillInformation
{
    public class JobBillInformationModel
    {
        DateTime processdt;
        public DateTime PROCESSDT
        {
            get { return processdt; }
            set { processdt = value; }
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

        string jobTp;
        public string JOBTP
        {
            get { return jobTp; }
            set { jobTp = value; }
        }

        Int64 jobno;
        public Int64 JOBNO
        {
            get { return jobno; }
            set { jobno = value; }
        }

        string partyID;
        public string PARTYID
        {
            get { return partyID; }
            set { partyID = value; }
        }

        DateTime billdt;
        public DateTime BILLDT
        {
            get { return billdt; }
            set { billdt = value; }
        }

        Int64 billno;
        public Int64 BILLNO
        {
            get { return billno; }
            set { billno = value; }
        }

        Int64 expsl;
        public Int64 EXPSL
        {
            get { return expsl; }
            set { expsl = value; }
        }

        string expID;
        public string EXPID
        {
            get { return expID; }
            set { expID = value; }
        }

        decimal expamt;
        public decimal EXPAMT
        {
            get { return expamt; }
            set { expamt = value; }
        }

        decimal billamt;
        public decimal BILLAMT
        {
            get { return billamt; }
            set { billamt = value; }
        }

        DateTime exppdt;
        public DateTime EXPPDT
        {
            get { return exppdt; }
            set { exppdt = value; }
        }

        Int64 billsl;
        public Int64 BILLSL
        {
            get { return billsl; }
            set { billsl = value; }
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
        private string userid;

        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }
        private string updateuserID;

        public string UpdateuserID
        {
            get { return updateuserID; }
            set { updateuserID = value; }
        }

    }
}
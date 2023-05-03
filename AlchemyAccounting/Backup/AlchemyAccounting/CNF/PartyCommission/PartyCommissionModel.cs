using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.CNF.PartyCommission
{
    public class PartyCommissionModel
    {
        public string partyID;
        public string PARTYID
        {
            get { return partyID; }
            set { partyID = value; }
        }

       public  Int64  commSL;
        public Int64 COMMSL
        {
            get { return commSL; }
            set { commSL = value; }
        }


        public string excTP;
        public string EXCTP
        {
            get { return excTP; }
            set { excTP = value; }
        }

        public decimal valuefr;
        public decimal VALUEFROM
        {
            get { return valuefr; }
            set { valuefr = value; }
        }

        public decimal valueto;
        public decimal VALUETO
        {
            get { return valueto; }
            set { valueto = value; }
        }

        public string valueTP;
        public string VALUETP
        {
            get { return valueTP; }
            set { valueTP = value; }
        }

        public decimal commat;
        public decimal COMMAMT
        {
            get { return commat; }
            set { commat = value; }
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
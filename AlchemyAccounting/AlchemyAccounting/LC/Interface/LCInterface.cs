using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.LC.Interface
{
    public class LCInterface
    {
        private string lcTp;

        public string LcTp
        {
            get { return lcTp; }
            set { lcTp = value; }
        }
        private string lcID;

        public string LcID
        {
            get { return lcID; }
            set { lcID = value; }
        }
        private string lcNo;

        public string LcNo
        {
            get { return lcNo; }
            set { lcNo = value; }
        }
        private DateTime lcDT;

        public DateTime LcDT
        {
            get { return lcDT; }
            set { lcDT = value; }
        }
        private string bnkCD;

        public string BnkCD
        {
            get { return bnkCD; }
            set { bnkCD = value; }
        }
        private string importerNM;

        public string ImporterNM
        {
            get { return importerNM; }
            set { importerNM = value; }
        }
        private string beneficiary;

        public string Beneficiary
        {
            get { return beneficiary; }
            set { beneficiary = value; }
        }
        private string scipNO;

        public string ScipNO
        {
            get { return scipNO; }
            set { scipNO = value; }
        }
        private DateTime scipDT;

        public DateTime ScipDT
        {
            get { return scipDT; }
            set { scipDT = value; }
        }
        private string mcNM;

        public string McNM
        {
            get { return mcNM; }
            set { mcNM = value; }
        }
        private string mcNO;

        public string McNO
        {
            get { return mcNO; }
            set { mcNO = value; }
        }
        private DateTime mcDT;

        public DateTime McDT
        {
            get { return mcDT; }
            set { mcDT = value; }
        }
        private string mpiNO;

        public string MpiNO
        {
            get { return mpiNO; }
            set { mpiNO = value; }
        }
        private DateTime mpiDT;

        public DateTime MpiDT
        {
            get { return mpiDT; }
            set { mpiDT = value; }
        }
        private decimal lcUSD;

        public decimal LcUSD
        {
            get { return lcUSD; }
            set { lcUSD = value; }
        }
        private decimal lcERT;

        public decimal LcERT
        {
            get { return lcERT; }
            set { lcERT = value; }
        }
        private decimal lcBDT;

        public decimal LcBDT
        {
            get { return lcBDT; }
            set { lcBDT = value; }
        }
        private string remarks;

        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        private string usernm;

        public string Usernm
        {
            get { return usernm; }
            set { usernm = value; }
        }

    }
}
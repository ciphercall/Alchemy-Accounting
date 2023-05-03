using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlchemyAccounting.CNF.model
{
    public class cnf_model
    {
        private DateTime jobCrDT;

        public DateTime JobCrDT
        {
            get { return jobCrDT; }
            set { jobCrDT = value; }
        }
        private string compID;

        public string CompID
        {
            get { return compID; }
            set { compID = value; }
        }
        private string regID;

        public string RegID
        {
            get { return regID; }
            set { regID = value; }
        }
        private int jobYear;

        public int JobYear
        {
            get { return jobYear; }
            set { jobYear = value; }
        }
        private string jobTP;

        public string JobTP
        {
            get { return jobTP; }
            set { jobTP = value; }
        }
        private Int64 jobNo;

        public Int64 JobNo
        {
            get { return jobNo; }
            set { jobNo = value; }
        }
        private string partyID;

        public string PartyID
        {
            get { return partyID; }
            set { partyID = value; }
        }
        private string consigneeName;

        public string ConsigneeName
        {
            get { return consigneeName; }
            set { consigneeName = value; }
        }
        private string consigneeAddress;

        public string ConsigneeAddress
        {
            get { return consigneeAddress; }
            set { consigneeAddress = value; }
        }
        private string goodsDesc;

        public string GoodsDesc
        {
            get { return goodsDesc; }
            set { goodsDesc = value; }
        }
        private string pkgDetails;

        public string PkgDetails
        {
            get { return pkgDetails; }
            set { pkgDetails = value; }
        }
        private string containerNo;

        public string ContainerNo
        {
            get { return containerNo; }
            set { containerNo = value; }
        }
        private string supplierNM;

        public string SupplierNM
        {
            get { return supplierNM; }
            set { supplierNM = value; }
        }
        private decimal cnfUSD;

        public decimal CnfUSD
        {
            get { return cnfUSD; }
            set { cnfUSD = value; }
        }
        private decimal crfUSD;

        public decimal CrfUSD
        {
            get { return crfUSD; }
            set { crfUSD = value; }
        }
        private decimal exchangeRT;

        public decimal ExchangeRT
        {
            get { return exchangeRT; }
            set { exchangeRT = value; }
        }
        private decimal cnfBDT;

        public decimal CnfBDT
        {
            get { return cnfBDT; }
            set { cnfBDT = value; }
        }
        private decimal grossWeight;

        public decimal GrossWeight
        {
            get { return grossWeight; }
            set { grossWeight = value; }
        }
        private decimal netWeight;

        public decimal NetWeight
        {
            get { return netWeight; }
            set { netWeight = value; }
        }
        private string beNO;

        public string BeNO
        {
            get { return beNO; }
            set { beNO = value; }
        }
        private DateTime beDT;

        public DateTime BeDT
        {
            get { return beDT; }
            set { beDT = value; }
        }
        private DateTime wharfentDT;

        public DateTime WharfentDT
        {
            get { return wharfentDT; }
            set { wharfentDT = value; }
        }
        private DateTime delDT;

        public DateTime DelDT
        {
            get { return delDT; }
            set { delDT = value; }
        }
        private string exTP;

        public string ExTP
        {
            get { return exTP; }
            set { exTP = value; }
        }
        private string crfNO;

        public string CrfNO
        {
            get { return crfNO; }
            set { crfNO = value; }
        }
        private DateTime crfDT;

        public DateTime CrfDT
        {
            get { return crfDT; }
            set { crfDT = value; }
        }
        private string inNO;

        public string InNO
        {
            get { return inNO; }
            set { inNO = value; }
        }
        private DateTime inDT;

        public DateTime InDT
        {
            get { return inDT; }
            set { inDT = value; }
        }
        private string blNO;

        public string BlNO
        {
            get { return blNO; }
            set { blNO = value; }
        }
        private DateTime blDT;

        public DateTime BlDT
        {
            get { return blDT; }
            set { blDT = value; }
        }
        private string lcNO;

        public string LcNO
        {
            get { return lcNO; }
            set { lcNO = value; }
        }
        private DateTime lcDT;

        public DateTime LcDT
        {
            get { return lcDT; }
            set { lcDT = value; }
        }
        private string permitNO;

        public string PermitNO
        {
            get { return permitNO; }
            set { permitNO = value; }
        }
        private DateTime permitDT;

        public DateTime PermitDT
        {
            get { return permitDT; }
            set { permitDT = value; }
        }
        private decimal assessableAMT;

        public decimal AssessableAMT
        {
            get { return assessableAMT; }
            set { assessableAMT = value; }
        }
        private decimal commission;

        public decimal Commission
        {
            get { return commission; }
            set { commission = value; }
        }
        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        private string userNM;

        public string UserNM
        {
            get { return userNM; }
            set { userNM = value; }
        }
        private string updateUser;

        public string UpdateUser
        {
            get { return updateUser; }
            set { updateUser = value; }
        }
        private string userpc;

        public string Userpc
        {
            get { return userpc; }
            set { userpc = value; }
        }

        private string ipaddress;

        public string Ipaddress
        {
            get { return ipaddress; }
            set { ipaddress = value; }
        }
        private DateTime inTM;

        public DateTime InTM
        {
            get { return inTM; }
            set { inTM = value; }
        }
        private DateTime upTM;

        public DateTime UpTM
        {
            get { return upTM; }
            set { upTM = value; }
        }
        private DateTime exDT;

        public DateTime ExDT
        {
            get { return exDT; }
            set { exDT = value; }
        }
        private string exMY;

        public string ExMY
        {
            get { return exMY; }
            set { exMY = value; }
        }
        private Int64 invoiceNO;

        public Int64 InvoiceNO
        {
            get { return invoiceNO; }
            set { invoiceNO = value; }
        }
        private string expenseCD;

        public string ExpenseCD
        {
            get { return expenseCD; }
            set { expenseCD = value; }
        }
        private Int64 sl;

        public Int64 Sl
        {
            get { return sl; }
            set { sl = value; }
        }
        private string particulars;

        public string Particulars
        {
            get { return particulars; }
            set { particulars = value; }
        }
        private string expensesID;

        public string ExpensesID
        {
            get { return expensesID; }
            set { expensesID = value; }
        }
        private decimal amount;

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        private string remarksTOP;

        public string RemarksTOP
        {
            get { return remarksTOP; }
            set { remarksTOP = value; }
        }
        private string remarksBOT;

        public string RemarksBOT
        {
            get { return remarksBOT; }
            set { remarksBOT = value; }
        }

        private string awbno;

        public string Awbno
        {
            get { return awbno; }
            set { awbno = value; }
        }
        private DateTime awbdt;

        public DateTime Awbdt
        {
            get { return awbdt; }
            set { awbdt = value; }
        }
        private string hbl;

        public string Hbl
        {
            get { return hbl; }
            set { hbl = value; }
        }
        private DateTime hbldt;

        public DateTime Hbldt
        {
            get { return hbldt; }
            set { hbldt = value; }
        }
        private string hawbno;

        public string Hawbno
        {
            get { return hawbno; }
            set { hawbno = value; }
        }
        private DateTime hawbdt;

        public DateTime Hawbdt
        {
            get { return hawbdt; }
            set { hawbdt = value; }
        }
        private string underTakeNo;

        public string UnderTakeNo
        {
            get { return underTakeNo; }
            set { underTakeNo = value; }
        }
        private DateTime underTakeDt;

        public DateTime UnderTakeDt
        {
            get { return underTakeDt; }
            set { underTakeDt = value; }
        }

        private string comRemarks;

        public string ComRemarks
        {
            get { return comRemarks; }
            set { comRemarks = value; }
        }

        public string FVessel { get; set; }
        public string RotNo { get; set; }
        public DateTime EtaDate { get; set; }
        public DateTime EtbDate { get; set; }
        public DateTime UdDate { get; set; }
        public DateTime ExpFDate { get; set; }
        public string UdNo { get; set; }
        public string ExpFNo { get; set; }
    }
}
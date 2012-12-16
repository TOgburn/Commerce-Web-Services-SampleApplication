﻿#region DISCLAIMER
/* Copyright (c) 2004-2010 IP Commerce, INC. - All Rights Reserved.
 *
 * This software and documentation is subject to and made
 * available only pursuant to the terms of an executed license
 * agreement, and may be used only in accordance with the terms
 * of said agreement. This software may not, in whole or in part,
 * be copied, photocopied, reproduced, translated, or reduced to
 * any electronic medium or machine-readable form without
 * prior consent, in writing, from IP Commerce, INC.
 *
 * Use, duplication or disclosure by the U.S. Government is subject
 * to restrictions set forth in an executed license agreement
 * and in subparagraph (c)(1) of the Commercial Computer
 * Software-Restricted Rights Clause at FAR 52.227-19; subparagraph
 * (c)(1)(ii) of the Rights in Technical Data and Computer Software
 * clause at DFARS 252.227-7013, subparagraph (d) of the Commercial
 * Computer Software--Licensing clause at NASA FAR supplement
 * 16-52.227-86; or their equivalent.
 *
 * Information in this software is subject to change without notice
 * and does not represent a commitment on the part of IP Commerce.
 * 
 * Sample Code is for reference Only and is intended to be used for educational purposes. It's the responsibility of 
 * the software company to properly integrate into thier solution code that best meets thier production needs. 
*/
#endregion DISCLAIMER

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn;
using EntryMode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.EntryMode;

namespace IPCommerce
{//Please note that the DataGenerator class will not likely be used for a software company's solution. Instead the information below would come from a database
    class DataGenerator
    {
        #region Variable Declarations
        public static IndustryTypeValues _ITV = getIndustryType(ConfigurationManager.AppSettings["IndustryType"]);
        private static Desktop target;
        #endregion Variable Declarations

        public DataGenerator(Desktop _myForm)
        {
            target = _myForm;
        }

        #region Service Information

        public static ApplicationData CreateApplicationData()
        {
            ApplicationData appData = new ApplicationData();
            appData.ApplicationAttended = _ITV._ApplicationAttended;
            appData.ApplicationLocation = _ITV._ApplicationLocation;
            appData.ApplicationName = "MyTestApp";//Should be the software company's application name
            appData.DeveloperId= "";//Only used int he case of a First Data TPPId
            appData.DeviceSerialNumber = "";
            appData.EncryptionType = _ITV._EncryptionType;
            appData.HardwareType = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.HardwareType.PC;
            appData.PINCapability = _ITV._PINCapability;
            appData.PTLSSocketId = "";
            appData.ReadCapability = _ITV._ReadCapability;
            appData.SerialNumber = "208093707";//This default values should always be used. 
            appData.SoftwareVersion = "1.0";
            appData.SoftwareVersionDate = new DateTime(2012, 10, 27);
            appData.VendorId = "";

            return appData;
        }

        public static MerchantProfile CreateMerchantProfile()
        {
            MerchantProfile merchData = new MerchantProfile();

            merchData.LastUpdated = DateTime.UtcNow;
            //MerchantData : https://my.ipcommerce.com/Docs/TransactionProcessing/CWS/API_Reference/2.0.18/ServiceInfoDataElements/MerchantProfileMerchantData.aspx
            merchData.MerchantData = new MerchantProfileMerchantData();
            //Address
            merchData.MerchantData.Address = new schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.AddressInfo();
            merchData.MerchantData.Address.City = "Pleasanton";
            merchData.MerchantData.Address.CountryCode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.TypeISOCountryCodeA3.USA;
            merchData.MerchantData.Address.PostalCode = "94566";
            merchData.MerchantData.Address.StateProvince = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.TypeStateProvince.CA;
            merchData.MerchantData.Address.Street1 = "123 Merch address";
            merchData.MerchantData.Address.Street2 = "";
            merchData.MerchantData.CustomerServiceInternet = "test@test.com";
            merchData.MerchantData.CustomerServicePhone = "303 5553232";
            merchData.MerchantData.Language = TypeISOLanguageCodeA3.ENG;
            merchData.MerchantData.MerchantId = "123456789012";
            merchData.MerchantData.Name = "Test Merch";
            merchData.MerchantData.Phone = "720 8881212";
            merchData.MerchantData.TaxId = "";
            
            //BankcardMerchantData : https://my.ipcommerce.com/Docs/TransactionProcessing/CWS/API_Reference/2.0.18/ServiceInfoDataElements/BankcardMerchantData.aspx
            merchData.MerchantData.BankcardMerchantData = new BankcardMerchantData();
            merchData.MerchantData.BankcardMerchantData.ABANumber = "987654321";
            merchData.MerchantData.BankcardMerchantData.AcquirerBIN = "654321";
            merchData.MerchantData.BankcardMerchantData.AgentBank = "123456";
            merchData.MerchantData.BankcardMerchantData.AgentChain = "645231";
            merchData.MerchantData.BankcardMerchantData.Aggregator = false;
            merchData.MerchantData.BankcardMerchantData.ClientNumber = "1234";
            merchData.MerchantData.BankcardMerchantData.IndustryType = _ITV._IndustryType;
            merchData.MerchantData.BankcardMerchantData.Location = "12345";
            merchData.MerchantData.BankcardMerchantData.MerchantType = "";
            merchData.MerchantData.BankcardMerchantData.PrintCustomerServicePhone = false;
            merchData.MerchantData.BankcardMerchantData.QualificationCodes = "";
            merchData.MerchantData.BankcardMerchantData.ReimbursementAttribute = "A";
            merchData.MerchantData.BankcardMerchantData.SIC = "5999";
            merchData.MerchantData.BankcardMerchantData.SecondaryTerminalId = "12345678";
            merchData.MerchantData.BankcardMerchantData.SettlementAgent = "12AB";
            merchData.MerchantData.BankcardMerchantData.SharingGroup = "123ABC";
            merchData.MerchantData.BankcardMerchantData.StoreId = "1234";
            merchData.MerchantData.BankcardMerchantData.TerminalId = "001";
            merchData.MerchantData.BankcardMerchantData.TimeZoneDifferential = "005";
            //ElectronicCheckingMerchantData : https://my.ipcommerce.com/Docs/TransactionProcessing/CWS/API_Reference/2.0.18/ServiceInfoDataElements/ElectronicCheckingMerchantData.aspx
            merchData.MerchantData.ElectronicCheckingMerchantData = new ElectronicCheckingMerchantData();
            merchData.MerchantData.ElectronicCheckingMerchantData.OrginatorId = "";
            merchData.MerchantData.ElectronicCheckingMerchantData.ProductId = "";
            merchData.MerchantData.ElectronicCheckingMerchantData.SiteId = "";
            //StoredValueMerchantData : https://my.ipcommerce.com/Docs/TransactionProcessing/CWS/API_Reference/2.0.18/ServiceInfoDataElements/StoredValueMerchantData.aspx
            merchData.MerchantData.StoredValueMerchantData = new StoredValueMerchantData();
            merchData.MerchantData.StoredValueMerchantData.AgentChain = "";
            merchData.MerchantData.StoredValueMerchantData.ClientNumber = "";
            merchData.MerchantData.StoredValueMerchantData.IndustryType = _ITV._IndustryType;
            merchData.MerchantData.StoredValueMerchantData.SIC = "";
            merchData.MerchantData.StoredValueMerchantData.StoreId = "";
            merchData.MerchantData.StoredValueMerchantData.TerminalId = "";
            
            //TransactionData
            merchData.TransactionData = new MerchantProfileTransactionData();
            merchData.TransactionData.BankcardTransactionDataDefaults = new BankcardTransactionDataDefaults();
            merchData.TransactionData.BankcardTransactionDataDefaults.CurrencyCode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.TypeISOCurrencyCodeA3.USD;
            merchData.TransactionData.BankcardTransactionDataDefaults.CustomerPresent = _ITV._CustomerPresent;
            merchData.TransactionData.BankcardTransactionDataDefaults.EntryMode = _ITV._EntryMode;
            merchData.TransactionData.BankcardTransactionDataDefaults.RequestACI = _ITV._RequestACI;
            merchData.TransactionData.BankcardTransactionDataDefaults.RequestAdvice = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.RequestAdvice.Capable;

            return merchData;
        }

        #endregion Service Information

        #region Transaction Processing

        #region Bankcard Processing

        public static BankcardTransaction SetBankCardTxnData()
        {//Contains information about the Bankcard transaction (Credit and PIN Debit). 
            return SetBankCardTxnData(null);
        }

        public static BankcardTransactionPro SetBankCardTxnData(BankCardProProcessingOptions _BCPPO)
        {
            //Note : the following values aplly to the BankcardTransaction
            BankcardTransactionPro BCtransaction = new BankcardTransactionPro();
            BankcardTransactionDataPro TxnData = new BankcardTransactionDataPro();//The following is necessary due to inheritance

            //The following are typical settings please ask your solution consultant if you have any questions
            TxnData.Amount = Convert.ToDecimal( (target.TxtAmount));
            #region Ways to Convert to a decimal with two decimals
            //Note : Decimal Example please remember that the amount must always be two decimals "0.00"
            //decimal dAmount = 10;
            //TxnData.Amount = Convert.ToDecimal(dAmount);
            //TxnData.Amount = Decimal.Parse(TxnData.Amount.ToString("0.00"));
            //Or Simply
            //TxnData.Amount = Decimal.Parse(TxnData.Amount.ToString("0.00"));

            //Note : String Example please remember that the amount must always be two decimals "0.00"
            //string strAmount = "10";
            //TxnData.Amount = Convert.ToDecimal(strAmount);
            //TxnData.Amount = Decimal.Parse(TxnData.Amount.ToString("0.00"));

            //Note : Int Example please remember that the amount must always be two decimals "0.00"
            //int intAmount = 10;
            //TxnData.Amount = Convert.ToDecimal(intAmount);
            //TxnData.Amount = Decimal.Parse(TxnData.Amount.ToString("0.00"));
            //Or
            //((int)(100 * amount)) * 0.01m;

            #endregion Ways to Convert to a decimal with two decimals

            if (target.TxtTip.Length > 0)
                if (Convert.ToDecimal(target.TxtTip) > 0)
                    TxnData.TipAmount = Convert.ToDecimal(target.TxtTip);

            if (target.chkProcessAsPINDebitTxn)//Cashback for PINDebit only
                if (target.TxtCashBack.Length > 0)
                    if (Convert.ToDecimal(target.TxtCashBack) > 0)
                        TxnData.CashBackAmount = Convert.ToDecimal(target.TxtCashBack);

            TxnData.CurrencyCode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.TypeISOCurrencyCodeA3.USD;
            //Set below 
            //try { TxnData.EntryMode = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.EntryMode)_ITV._EntryMode; }
            //catch { }
            try { TxnData.CustomerPresent = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CustomerPresent)_ITV._CustomerPresent; }
            catch { }
            try { TxnData.SignatureCaptured = _ITV._SignatureCaptured; }
            catch { }

            //Used for Vantiv
            TxnData.TransactionDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");

            //In the case of Retail or Restaurant
            //TxnData.TipAmount = 3.00M;

            // Used for QuasiCash transactions
            TxnData.IsQuasiCash = false;

            TxnData.LaneId = "1";//Used for Vantiv Tandem

            //Used for Retail/Restaurant/MOTO
            TxnData.EmployeeId = "123456";

            //Used for Ecommerce/MOTO
            TxnData.OrderNumber = "123543"; //This values must be unique for each transaction. OrderNum should never be zero
            //TxnData.GoodsType = GoodsType.PhysicalGoods;

            //Check to see if partial approval should be allowed. To test a value of *.59 can be used
            if (target.ChkAllowPartialApprovals)
                TxnData.PartialApprovalCapable = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.PartialApprovalSupportType.Capable;

            TxnData.TransactionDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");

            // The below value is to support partial authorizations as mandated by Visa and Mastercard
            // First check to see if the service supports partial approvals, if it does then set the flag
            // that this transaction will support that partial approval.  Remember to parse your Response.Amount to verify 
            // the amount that was approved.  Card Not Present applications do not need to abide by the partial approval mandate
            if (target._bcs.Tenders.PartialApprovalSupportType == schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PartialApprovalSupportType.Enabled)
                TxnData.PartialApprovalCapable = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.PartialApprovalSupportType.Capable;

            BCtransaction.CustomerData = new TransactionCustomerData();
            BCtransaction.CustomerData.CustomerId = TxnData.OrderNumber;

            BCtransaction.TenderData = new BankcardTenderData();
            BCtransaction.TenderData.CardData = new CardData();

            //Process as a Swipe or as a Keyed Transaction
            if (target.ChkCardNotPresent)
            {//Keyed Transaction
                TxnData.EntryMode = EntryMode.Keyed;
                BCtransaction.TenderData.CardData.CardType = (TypeCardType)target.CboCardTypes;
                BCtransaction.TenderData.CardData.Expire = target.TxtExpirationDate; // Note : that in a swipe track data the format is "YYMM" however here it's "MMYY"
                BCtransaction.TenderData.CardData.PAN = target.TxtPAN;
                BCtransaction.TenderData.CardData.CardholderName = "Dirk Pit";
            }
            else if (target.ChkTokenization)
            {//Card tokenization
                try
                {
                    TxnData.EntryMode = EntryMode.Keyed;
                    TokenizedTransaction T = (TokenizedTransaction)target.CboTokenizedCard;
                    BCtransaction.TenderData.CardData = new CardData();
                    BCtransaction.TenderData.PaymentAccountDataToken = T.PaymentAccountDataToken;
                    BCtransaction.TenderData.CardData.CardType = T.CardType;
                    BCtransaction.TenderData.CardData.Expire = T.ExpireationDate; // Note : that in a swipe track data the format is "YYMM" however here it's "MMYY"
                    BCtransaction.TenderData.CardData.PAN = T.MaskedPAN; //It's recommended to set the masked PAN that was returned with the original token
                }
                catch { }
            }
            else if (target.ChkTrackDataFromMSR)
            {//Swiped Transaction
                #region Card Validation Samples (Optional however good reference for sending valid card swipes)
                //NOTE Example : In the case of processing a card swipe string use the following.
                //NOTE Example : The following will remove starting and ending sentenals
                BCtransaction.TenderData.CardData.CardType = (TypeCardType) target.CboCardTypes;
                string SwipeToSeperateTracks = target.TxtTrackDataFromMSR;
                TrackFromMSRSwipe TFMSRS = target.Helper.seperateTrackData(SwipeToSeperateTracks);

                string[] TrackProcessingOrder = ConfigurationManager.AppSettings["TxnData_OrderOfProcessingTracks"].Split('|');
                foreach (string track in TrackProcessingOrder)
                {
                    if (track == "Track1")
                        if (target.Helper.validateTrackData(ref BCtransaction, TFMSRS.Track1Data))
                        {
                            TxnData.EntryMode = EntryMode.TrackDataFromMSR;
                            break;
                        }
                    if (track == "Track2")
                        if (target.Helper.validateTrackData(ref BCtransaction, TFMSRS.Track2Data))
                        {
                            TxnData.EntryMode = EntryMode.Track2DataFromMSR;
                            break;
                        }
                    if (track == "Keyed")
                    {
                        DialogResult Result;
                        Result = MessageBox.Show("Neither Track 1 nor Track 2 match. Process as a Keyed transaction instead?",
                                    "Process as Keyed", MessageBoxButtons.OKCancel);
                        if (Result == DialogResult.Cancel) { Exception e = new Exception("Neither Track 1 or Track 2 is valid. Plesae swipe again"); throw e; }
                        TxnData.EntryMode = EntryMode.Keyed;
                        BCtransaction.TenderData.CardData.CardType = (TypeCardType)target.CboCardTypes;
                        BCtransaction.TenderData.CardData.Expire = target.TxtExpirationDate; // Note : that in a swipe track data the format is "YYMM" however here it's "MMYY"
                        BCtransaction.TenderData.CardData.PAN = target.TxtPAN;
                        break;
                    }
                }

                //Note : Other Track Examples (includes track 1 track 2 and an example of track 3)
                //MasterCard : %B5454545454545454^IPCOMMERCE/TESTCARD^1312101013490000000001000880000?;5454545454545454=13121010134988000010?
                //MasterCardPurchase : %B5480020605154711^IPCOMMERCE/TESTCARD^1312101100000001000000218000000?;5480020605154711=13121011000017026218?
                //MasterCardSigDebit : %B9999989900007723^IPCOMMERCE/TESTCARD^13121015432112345678?;9999989900007723=13121015432112345678?
                //Visa : %B4111111111111111^IPCOMMERCE/TESTCARD^13121010454500415000010?;4111111111111111=13121010454541500010?
                //Visa Purchasecard : %B4005765777003^IPCOMMERCE/TESTCARD^13121015432112345678?;4005765777003=13121015432112345678?
                //AmericanExpress : %B371449635398456^IPCOMMERCE/TESTCARD^1312060523319?;371449635398456=1312060523319?
                //Discover : %B6011000995504101^IPCOMMERCE/TESTCARD^13121011000627210201?;6011000995504101=13121011000627210201?
                //The following track includes a track 3 which is possible in Costco American Express cards %B371449635398456^IPCOMMERCE/TESTCARD^1312060523319?;371449635398456=1312060523319?+823156444000?

                #endregion Card Validation Samples (Optional however good reference for sending valid card swipes)
            }

            #region Simulating a flag used to set either AVS CV or PINDebit data
            //Simulating a flag used to set either AVS, CV data or PINDebit
            bool blnAVS = _ITV._IncludeAVS;
            bool blnCVV = _ITV._IncludeCV;

            if (blnAVS | blnCVV | target.chkProcessAsPINDebitTxn)
            {
                BCtransaction.TenderData.CardSecurityData = new CardSecurityData(); //Required if AVS or CV is used
                if (blnAVS)
                {
                    //AVSData
                    BCtransaction.TenderData.CardSecurityData.AVSData = new AVSData();
                    //Required AVS Elements
                    BCtransaction.TenderData.CardSecurityData.AVSData.PostalCode = "80023";
                    //Optional AVS Elements
                    BCtransaction.TenderData.CardSecurityData.AVSData.CardholderName = "John Smith";
                    BCtransaction.TenderData.CardSecurityData.AVSData.City = "Mason";
                    BCtransaction.TenderData.CardSecurityData.AVSData.Country = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.TypeISOCountryCodeA3.USA;
                    BCtransaction.TenderData.CardSecurityData.AVSData.Phone = "513 5456699"; //Must be of format "NNN NNNNNNN"
                    BCtransaction.TenderData.CardSecurityData.AVSData.StateProvince = "OH";
                    BCtransaction.TenderData.CardSecurityData.AVSData.Street = "1234 IrwinSimpaon";
                }
                if (blnCVV)
                {
                    //CVData
                    string strCVData = "111"; //Please note that this would typically be an input field in the application.
                    if ((TypeCardType)target.CboCardTypes == TypeCardType.AmericanExpress)
                        strCVData = "1111";//AVS has 4 numeric
                    if (strCVData.Length > 0)
                    {
                        BCtransaction.TenderData.CardSecurityData.CVDataProvided = CVDataProvided.Provided;
                        BCtransaction.TenderData.CardSecurityData.CVData = strCVData;
                    }
                    else
                    {
                        //In this case the card was present (Retail or Restaurant) however the CV code was not readable or available
                        BCtransaction.TenderData.CardSecurityData.CVDataProvided = CVDataProvided.DeliberatelyBypass;
                    }
                }
                //Check to see if the transaction is a PINDebit Transaction
                if (target.chkProcessAsPINDebitTxn)
                {
                    BCtransaction.TenderData.CardSecurityData.KeySerialNumber = "1234567890123456";
                    BCtransaction.TenderData.CardSecurityData.PIN = "1234567890";
                    TxnData.AccountType = AccountType.CheckingAccount;
                }
            }
            #endregion END Simulating a flag used to set either AVS CV or PINDebit data

            #region Check to see if PINLessDebit selected

            if (target.ChkProcessAsPINLessDebit)
            {
                TxnData.PINlessDebitData = new PINlessDebitData();
                TxnData.PINlessDebitData.PayeeData = new PayeeData();
                TxnData.PINlessDebitData.PayeeData.AccountNumber = "000056";
                TxnData.PINlessDebitData.PayeeData.CompanyName = "ABC Company";
                TxnData.PINlessDebitData.PayeeData.Phone = "555 1238888";
            }

            #endregion END Check to see if PINLessDebit selected

            //#region Simulating a flag used to set Magensa data
            ////Simulating a flag used to set either AVS, CV data or PINDebit
            //bool blnMagensa = false;

            //try { blnMagensa = Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_ProcessMagensaTxn"]); }
            //catch { };

            //if (blnMagensa)
            //{ //*** ToDo : the following are generic values which will only work against Sandbox.***
            //    //First set the CardData to null
            //    BCtransaction.TenderData.CardData = null;

            //    if (BCtransaction.TenderData.CardSecurityData == null) { BCtransaction.TenderData.CardSecurityData = new CardSecurityData(); }
            //    BCtransaction.TenderData.CardSecurityData.CVData = null;
            //    BCtransaction.TenderData.CardSecurityData.CVDataProvided = CVDataProvided.NotSet;
            //    BCtransaction.TenderData.CardSecurityData.IdentificationInformation = "A52AFB9FB5B283A6C8C38377A6CB1D2C63CC59D3B0B29D2A0DF1C9A54F123D37536756C77B4A9B75E51BF028B51971E81C8B221533A3AFF4";

            //    BCtransaction.TenderData.SecurePaymentAccountData = "13A7783BD91D0A05712606644778CF8F34397EAC2AB26676A52A380350CAA07E";

            //    BCtransaction.TenderData.EncryptionKeyId = "9011400B042692000398";
            //    BCtransaction.TenderData.SwipeStatus = "1065057";

            //    TxnData.ScoreThreshold = ".5";
            //    TxnData.EntryMode = EntryMode.Track2DataFromMSR;
            //    TxnData.Reference = "11";

            //}
            //#endregion Simulating a flag used to set Magensa data

            #region Pro specific functionality
            //Note : BankcardTransactionPro
            //The following sections are specific to the Pro object
            //Applications that will support Level 2, Level 3, and Managed Billing (recurring/installment payments) data may be
            //required to provide the following data elements in addition to the Base Transaction and Bankcard
            //Transaction data elements described above.
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["ProcessAsBankcardTransaction_Pro"]) | _BCPPO != null)//Determines if additional Pro objects should be used.
            {
                PurchaseCardLevel PCL;
                bool InterchangeData;
                bool L2L3;
                if (_BCPPO != null)
                {//In this case an override was passed in
                    PCL = _BCPPO.PurchaseCardLevel;
                    InterchangeData = _BCPPO.InterchangeData;
                    L2L3 = _BCPPO.IncludeLevel2OrLevel3Data;
                }
                else
                {//Use the default values in the app.config
                    PCL = (PurchaseCardLevel)Enum.Parse(typeof(PurchaseCardLevel), ConfigurationManager.AppSettings["Pro_PurchaseCardLevel"]);
                    InterchangeData = Convert.ToBoolean(ConfigurationManager.AppSettings["Pro_InterchangeData"]);
                    L2L3 = Convert.ToBoolean(ConfigurationManager.AppSettings["Pro_IncludeLevel2OrLevel3Data"]);
                }

                #region Purchase Card Level 2 or Level 3
                if (PCL == PurchaseCardLevel.Level2 | PCL == PurchaseCardLevel.Level3)
                {
                    /*
                        For AuthorizeAndCapture the level2/3 goes in immediately as there is no follow up transaction.
                        For Authorize followed by Capture the level 2/3 goes in the Capture, never in the Authorize.  
                        The schema requires BaseAmt and OrderNum for level 2.  Amt, Quantity, SeqNum, TaxIncludedInd, and UnitPrice are all required for level 3.
                        • Of course you must have level 2 in order to have level 3.
                        Set TxnData.CmrclCardReq to “Enable” since some providers get that value and will return the card type in the response in CmrclCardResp.  Not all providers do this but they should always set it.  
                        The value is returned in the response and can be used to determine whether or not to submit level 2.

                        Level 2 requirements for Ecomm/Moto:
                        • TaxExempt
                        • DestinationPostal
                        • ShipFromPostalCode
                        Level 2 requirements for Retail:
                        • DestinationPostal
                        • TaxExempt                   
                        Level 3 requirements for Ecomm/Moto:
                        • Desc
                        • ProductCode
                        • UnitOfMeasure
                        • only 98 items allowed
                    */

                    //Send with the original Authorize with the "RequestCommerciaCard" flag set. In the response you will reference
                    //"CommercialCardResponse" as the indicator that the card is a valid Level 2 or Level 3 card.
                    BCtransaction.InterchangeData = new BankcardInterchangeData();
                    BCtransaction.InterchangeData.RequestCommercialCard = RequestCommercialCard.Enable;

                    if (L2L3 & PCL == PurchaseCardLevel.Level2)
                    {//Level 2 data
                        TxnData.Level2Data = SetLevel2Data();

                        if (BCtransaction.TenderData.CardData.CardType == TypeCardType.AmericanExpress)
                        {
                            BCtransaction.CustomerData = new TransactionCustomerData();
                            BCtransaction.CustomerData.ShippingData = new CustomerInfo();
                            BCtransaction.CustomerData.ShippingData.Name = new NameInfo();
                            BCtransaction.CustomerData.ShippingData.Address = new schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.AddressInfo();

                            //For Amex only cards. Amex only supports Purchase Card Level 2
                            BCtransaction.CustomerData.ShippingData.Name.First = "Dan"; //Full name or parameterized name.
                            BCtransaction.CustomerData.ShippingData.Name.Middle = "Joe";
                            BCtransaction.CustomerData.ShippingData.Name.Last = "Billings";

                            BCtransaction.CustomerData.ShippingData.Address.Street1 = "123 HappyWay";
                            //BCtransaction.CustomerData.ShippingData.Address.Street2 = "";
                            BCtransaction.CustomerData.ShippingData.Address.City = "Mason";
                            BCtransaction.CustomerData.ShippingData.Address.StateProvince = "OH";
                            BCtransaction.CustomerData.ShippingData.Address.PostalCode = "45040";
                            BCtransaction.CustomerData.ShippingData.Address.CountryCode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.TypeISOCountryCodeA3.USA;
                        }
                    }
                    if (L2L3 & PCL == PurchaseCardLevel.Level3)
                    {//Level 3 data
                        TxnData.Level2Data = SetLevel2Data();

                        //Level 3 data includes Level 2 data plus line items details. in this case your application should handle calling the following
                        // for each line item
                        List<LineItemDetail> LIDs = new List<LineItemDetail>();
                        LineItemDetail LID = new LineItemDetail();
                        LID = SetLevel3Data();
                        LIDs.Add(LID);
                        TxnData.LineItemDetails = LIDs;
                    }
                }

                #endregion Purchase Card Level 2 or Level 3

                #region Recurring/Bill Payments

                if (InterchangeData)
                {
                    BCtransaction.InterchangeData = new BankcardInterchangeData();

                    //Single Payment
                    BCtransaction.InterchangeData.BillPayment = BillPayment.SinglePayment;
                    BCtransaction.InterchangeData.ExistingDebt = ExistingDebt.IsExistingDebt;

                    /*
                    //Deferred Billing
                    BCtransaction.InterchangeData.BillPayment = BillPayment.DeferredBilling;
                    BCtransaction.InterchangeData.ExistingDebt = ExistingDebt.IsExistingDebt;
        
                    //Installment
                    BCtransaction.InterchangeData.BillPayment = BillPayment.Installment;
                    BCtransaction.InterchangeData.ExistingDebt = ExistingDebt.IsExistingDebt;
                    BCtransaction.InterchangeData.CurrentInstallmentNumber = 1;
                    BCtransaction.InterchangeData.TotalNumberOfInstallments = 3;
                    */
                    //Recurring
                    //Note: Any time recurring payments are used for any BCP service MultiClearSeqNum should be 1 for the first payment and any number 
                    //greater than 1 for the additional recurring payments.  The software company doesn’t have to count payments.  
                    //They can always send 2 for the additional transactions if they want. Sending the 1 sends a code to the service provider that this 
                    //is the first payment and CVData and AVSData are expected.  Sending greater than 1 means that the lack of CVData and AVSData is OK 
                    //since they don’t have to store or submit that data with the additional payments.
                    BCtransaction.InterchangeData.BillPayment = BillPayment.Recurring;
                    BCtransaction.InterchangeData.ExistingDebt = ExistingDebt.IsExistingDebt;
                    BCtransaction.InterchangeData.CurrentInstallmentNumber = 1; //If this is a follow-up recuring payment the value needs to be greater than 1. 

                    //Any time BillPayInd is set to either “Deferred_Billing”, “Installment” or “Recurring”, CustPresentFlag should be set to “Bill_Payment"
                    if (BCtransaction.InterchangeData.BillPayment == BillPayment.DeferredBilling | BCtransaction.InterchangeData.BillPayment == BillPayment.Installment | BCtransaction.InterchangeData.BillPayment == BillPayment.Recurring)
                        TxnData.CustomerPresent = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CustomerPresent.BillPayment;
                }

                #endregion Recurring/Bill Payments

            }

            #endregion END-Pro specific functionality

            #region Verified By Visa (VbV/VPAS)
            bool blnVPAS = false;
            try { blnVPAS = Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_IncludeVPAS"]); }
            catch { };
            /* IF supported
                If a token is obtained:
                    TenderData/EcommerceSecurityData/TokenData is required and contains the token provided by the service.
                    TenderData/EcommerceSecurityData/TokenInd is required and must be set to VPAS_3D_Secure_V100 or VPAS_3D_Secure_V102.  Even if the merchant has a newer version, either setting will work for any version.
                    TxnData/EcommerceData/PayTypeInd is required and must be set to Secure_Ecom.
                    TenderData/EcommerceSecurityData/XID is optional and may contain the Visa XID value.
                If VbV is supported but the token could not be obtained:
                    TenderData/EcommerceSecurityData/TokenData will not be populated.
                    TenderData/EcommerceSecurityData/TokenInd is required and must be set to Attempted_Card_Unsupported or Attempted_Service_Unavailable.
                    TxnData/EcommerceData/PayTypeInd is required and must be set to Non_authenticated_Security_Without_SSL or Non_authenticated_Security_With_SSL.
             */
            if (blnVPAS && BCtransaction.TenderData.CardData.CardType == TypeCardType.Visa)
            {
                //Token Obtained
                BCtransaction.TenderData.EcommerceSecurityData.TokenData = "";
                BCtransaction.TenderData.EcommerceSecurityData.TokenIndicator = TokenIndicator.VPAS;
                //TODO :TxnData.
                BCtransaction.TenderData.EcommerceSecurityData.XID = "";

                //No Token Obtained
                BCtransaction.TenderData.EcommerceSecurityData.TokenIndicator = TokenIndicator.AttemptedCardUnsupported;
                //TODO :TxnData.
            }
            #endregion Verified By Visa (VbV/VPAS)

            #region MasterCard Secure Code (MCSC/UCAF)
            bool blnUCAF = false;
            try { blnUCAF = Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_IncludeUCAF"]); }
            catch { };
            /*
             * If a token is obtained:
                    TenderData/EcommerceSecurityData/TokenData is required and contains the token provided by the service.
                    TenderData/EcommerceSecurityData/TokenInd is required and must be set to UCAF or UCAF_With_Data.
                    TxnData/EcommerceData/PayTypeInd is required and must be set to Secure_Ecom.
               If MCSC is supported but the token could not be obtained or will not be sent with the transaction:
                    TenderData/EcommerceSecurityData/TokenData will not be populated.
                    TenderData/EcommerceSecurityData/TokenInd is required and must be set to Attempted_Card_Unsupported, Attempted_Service_Unavailable, or UCAF_Without_Data.
                    TxnData/EcommerceData/PayTypeInd is required and must be set to either Non_authenticated_Security_Without_SSL or Non_authenticated_Security_With_SSL.
             */
            if (blnUCAF && BCtransaction.TenderData.CardData.CardType == TypeCardType.MasterCard)
            {
                //Token Obtained
                BCtransaction.TenderData.EcommerceSecurityData.TokenData = "";
                BCtransaction.TenderData.EcommerceSecurityData.TokenIndicator = TokenIndicator.UCAFWithData;
                //TODO :TxnData.
                BCtransaction.TenderData.EcommerceSecurityData.XID = "";

                //No Token Obtained
                BCtransaction.TenderData.EcommerceSecurityData.TokenIndicator = TokenIndicator.AttemptedCardUnsupported;
                //TODO :TxnData.
            }
            #endregion MasterCard Secure Code (MCSC/UCAF)

            # region Convenience Fees

            //Simulating a flag used to include Convenience Fees
            bool cFees = false;

            try { cFees = Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_IncludeCFees"]); }
            catch { };

            if (cFees)
            {
                //TxnData.FeeAmount = 5.00M;
            }

            # endregion Convenience Fees

            #region Soft Descriptors

            //Simulating setting soft descriptors
            try
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_SoftDescriptors"]))
                {
                    TxnData.AlternativeMerchantData = target.Helper.SetSoftDescriptors();
                }
            }
            catch { };

            #endregion Soft Descriptors

            BCtransaction.TransactionData = TxnData;
            return BCtransaction;
        }

        public static Level2Data SetLevel2Data()
        {
            Level2Data L2 = new Level2Data();
            L2.BaseAmount = 10.00M; //Required : Base amount of the transaction, not including freight, handling, and tax amounts.
            //L2.CommodityCode = ""; //Optional : Commodity code for the entire purchase.
            //L2.CompanyName = ""; //Optional : Name of company making purchase. 
            //L2.CustomerCode = ""; //Optional : Provided by cardholder to appear on invoice. 
            //L2.Description = ""; //Optional : Description of the purchase. 
            //L2.DestinationCountryCode = TypeISOCountryCodeA3.USA; //Optional : Specifies the destination country code of the goods being shipped. 
            L2.DestinationPostal = "80040";//(Required) Specifies the destination postal code. 
            //L2.DiscountAmount = 1.00M;
            //L2.DutyAmount = 2.00M; //Optional : Specifies duty amount. 
            //L2.FreightAmount = 3.00M; //Optional : Specifies freight amount. 
            //L2.MiscHandlingAmount = 4.00M; //Optional : Miscellaneous handling charges. 
            //L2.OrderDate = DateTime.utcNow(); //Optional : Date the order was placed. 
            L2.OrderNumber = "1235";//Required : Indicates the Order Number. 
            //L2.RequesterName = "Don"; //Optional : Name of the person making the request. 
            L2.ShipFromPostalCode = "94566";//(Required) The zip/postal code of the location from which the goods are shipped. 
            L2.ShipmentId = "12DDER"; //Optional : Number of the shipment. 

            L2.TaxExempt = new TaxExempt(); //Optional : Tax exemption indicator and number. 
            L2.TaxExempt.IsTaxExempt = IsTaxExempt.NotExemptTaxInfoProvided;//Required : Indicates tax exempt status for the transaction. 
            L2.TaxExempt.TaxExemptNumber = "123jsd"; //Conditional : Indicates tax exempt number for the transaction. Conditional, required if TaxExemptInd = 'Exempt' or 'Not_Exempt_Tax_Info_Provided'.

            //The following is used if the "IsTaxExempt" is not "Exempt"
            L2.Tax = new Tax(); //Optional : Aggregate of tax information. 
            L2.Tax.Amount = 1.00M; //Required : Total amount of tax applied. 
            L2.Tax.Rate = 1.00M; //Optional : Total tax rate.
            L2.Tax.InvoiceNumber = ""; //Optional : Tax invoice number. 

            List<ItemizedTax> ITxs = new List<ItemizedTax>(); //Optional : Collection of itemized tax information. 
            //ItemizedTax ITX = new ItemizedTax();
            //ITX.Amount = 0.05M; //Required : Amount of specified tax applied.
            //ITX.Rate = 0.02M; //Optional : Rate for tax specified. 
            //ITX.Type = TypeTaxType.StateSalesTax; //Required : Type of tax specified. 
            //ITxs.Add(ITX);
            L2.Tax.ItemizedTaxes = ITxs;

            return L2;
        }

        public static LineItemDetail SetLevel3Data()
        {
            LineItemDetail LID = new LineItemDetail();
            LID.Amount = 2.00M; //Required : Line item total cost. See DiscountInd and TaxInd to determine whether this amount is inclusive of DiscountAmt and Tax. 
            LID.CommodityCode = "123456789012"; //Optional : Line item commodity code.
            LID.Description = "PartXYZ"; //Optional : Line item description. 
            LID.DiscountAmount = 1.00M; //Optional : Discount amount for this line item. 
            LID.DiscountIncluded = true; //Conditional : Indicates whether Amt is inclusive of discount. Conditional, required if DiscountAmt is specified.
            LID.ProductCode = "xyz123"; //Optional : Line item product code. 
            LID.Quantity = 1.00M; //Required : Quantity of item. 
            schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.Tax tx = new Tax();
            tx.Amount = 0.15M; //Required : Total amount of tax applied. 
            tx.InvoiceNumber = "IONum"; //Optional : Tax invoice number. 
            //List<ItemizedTax> ITxs = new List<ItemizedTax>(); //Optional : Collection of itemized tax information. 
            //ItemizedTax ITX = new ItemizedTax();
            //ITX.Amount = 0.05M; //Required : Amount of specified tax applied.
            //ITX.Rate = 0.02M; //Optional : Rate for tax specified. 
            //ITX.Type = TypeTaxType.StateSalesTax; //Required : Type of tax specified. 
            //ITxs.Add(ITX);
            LID.Tax = tx;
            LID.TaxIncluded = true;//Required: Specifies whether Amt is inclusive of tax. 
            LID.UnitOfMeasure = TypeUnitOfMeasure.Ounce; //Optional : Units used to measure quantity. 
            LID.UnitPrice = 0.80M; //Required : Price per unit of line item. 
            LID.UPC = "UPC123"; //Optional : Line item UPC code. 

            return LID;
        }

        #endregion Bankcard Processing

        #region Electronic Checking

        public static ElectronicCheckingTransaction SetElectronicCheckTxnData()
        {
            ElectronicCheckingTransaction ECKTransaction = new ElectronicCheckingTransaction();
            ECKTransaction.TransactionData = new ElectronicCheckingTransactionData();

            //Consumer Data
            //NOTE : Need one or the other
            ECKTransaction.CustomerData = new TransactionCustomerData();
            ECKTransaction.CustomerData.BillingData = new CustomerInfo();
            ECKTransaction.CustomerData.BillingData.BusinessName = "ACME Supplies";
            //OR
            ECKTransaction.CustomerData.BillingData.Name = new NameInfo();
            ECKTransaction.CustomerData.BillingData.Name.First = "Joe";
            ECKTransaction.CustomerData.BillingData.Name.Middle = "Johnson";
            ECKTransaction.CustomerData.BillingData.Name.Last = "Smith";

            //Transaction Data
            ECKTransaction.TransactionData.Amount = Convert.ToDecimal(target.TxtAmount);
            //ECKTransaction.TransactionData.CurrencyCode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.TypeISOCurrencyCodeA3.USD;
            ECKTransaction.TransactionData.EffectiveDate = DateTime.UtcNow; //Specifies the effective date of the transaction. Required.
            ECKTransaction.TransactionData.IsRecurring = false; //Indicates whether the transaction is recurring. Conditional, required if SECCode = 'WEB'.
            //Supported SEC Codes are PPD, CCD, TEL, WEB and BOC.  CCD and PPD transactions can be either credit or debit.  TEL, WEB and BOC are debit transactions only.
            ECKTransaction.TransactionData.SECCode = SECCode.WEB; //The three letter code that indicates what NACHA regulations the transaction must adhere to. Required.
            ECKTransaction.TransactionData.ServiceType = ServiceType.ACH; //Indicates the Electronic Checking service type: ACH, RDC or ECK. Required.
            //ECKTransaction.TransactionData.TransactionDateTime = DateTime.UtcNow;
            ECKTransaction.TransactionData.TransactionType = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.TransactionType.Debit; //Indicates the transaction type. Required.

            //Tender Data
            ECKTransaction.TenderData = new ElectronicCheckingTenderData();
            ECKTransaction.TenderData.CheckData = new CheckData();
            ECKTransaction.TenderData.CheckData.AccountNumber = "1234567890";//Account number on the check. Required.
            ECKTransaction.TenderData.CheckData.CheckCountryCode = CheckCountryCode.US;//Bank account country of origin for receiver deposit.
            ECKTransaction.TenderData.CheckData.CheckNumber = "191";//The check number as printed on the check. Optional.
            ECKTransaction.TenderData.CheckData.OwnerType = OwnerType.Personal;//Indicates the type of entity which owns the account.
            ECKTransaction.TenderData.CheckData.RoutingNumber = "987654321"; //9-digit bank routing number of the receiver deposit account. Required.
            ECKTransaction.TenderData.CheckData.UseType = UseType.Checking;//Indicates the type of account.

            return ECKTransaction;
        }

        #endregion Electronic Checking

        #region Stored Value

        public static StoredValueTransaction SetStoredValueTxnData()
        {
            StoredValueTransaction SVATransaction = new StoredValueTransaction();
            SVATransaction.TransactionData = new StoredValueTransactionData();

            //Transaction Data
            SVATransaction.TransactionData.Amount = Convert.ToDecimal(target.TxtAmount);
            SVATransaction.TransactionData.CurrencyCode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.TypeISOCurrencyCodeA3.USD;
            SVATransaction.TransactionData.EmployeeId = "122";

            SVATransaction.TransactionData.IndustryType = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.IndustryType.Retail;

            SVATransaction.TransactionData.TenderCurrencyCode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.TypeISOCurrencyCodeA3.USD;
            SVATransaction.TransactionData.TransactionDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffzzz");

            //Tender Data
            SVATransaction.TenderData = new StoredValueTenderData();
            SVATransaction.TenderData.CardData = new CardData1();
            SVATransaction.TenderData.CardSecurityData = new CardSecurityData1();
            SVATransaction.TenderData.CardSecurityData.CVDataProvided = CVDataProvided.Provided;
            SVATransaction.TenderData.CardSecurityData.CVData = "1111";

            if (Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_ProcessAsKeyed"]))
            {
                SVATransaction.TenderData.CardData.AccountNumber = "5858836401000004";
                SVATransaction.TenderData.CardData.Expire = "1012";
                SVATransaction.TransactionData.EntryMode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.EntryMode.Keyed;
            }
            else
            {
                SVATransaction.TenderData.CardData.Track2Data = "5858836401000004^ / ^4912101000005320000000532000000";
                SVATransaction.TenderData.CardData.AccountNumber = "5858836401000004";
                SVATransaction.TransactionData.EntryMode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.EntryMode.TrackDataFromMSR;
            }

            return SVATransaction;
        }

        #endregion Stored Value

        #endregion Transaction Processing

        public static IndustryTypeValues getIndustryType(string _industryType)
        {
            IndustryTypeValues i = new IndustryTypeValues();
            if (_industryType == "")
            {
                //ApplicationData schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.
                i._ApplicationAttended = Convert.ToBoolean(ConfigurationManager.AppSettings["ApplicationAttended"]);
                i._ApplicationLocation = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ApplicationLocation)Enum.Parse(typeof(schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ApplicationLocation), ConfigurationManager.AppSettings["ApplicationLocation"]);
                i._PINCapability = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PINCapability)Enum.Parse(typeof(schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PINCapability), ConfigurationManager.AppSettings["PINCapability"]);
                i._ReadCapability = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ReadCapability)Enum.Parse(typeof(schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ReadCapability), ConfigurationManager.AppSettings["ReadCapability"]);
                i._EncryptionType = (EncryptionType)Enum.Parse(typeof(EncryptionType), ConfigurationManager.AppSettings["EncryptionType"]);

                //MerchantData
                i._IndustryType = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.IndustryType)Enum.Parse(typeof(schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.IndustryType), ConfigurationManager.AppSettings["IndustryType"]);
                i._CustomerPresent = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CustomerPresent)Enum.Parse(typeof(schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CustomerPresent), ConfigurationManager.AppSettings["CustomerPresent"]);
                i._RequestACI = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.RequestACI)Enum.Parse(typeof(schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.RequestACI), ConfigurationManager.AppSettings["RequestACI"]);
                i._EntryMode = (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.EntryMode)Enum.Parse(typeof(schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.EntryMode), ConfigurationManager.AppSettings["EntryMode"]);

                //TransactionData
                i._ProcessAsKeyed = Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_ProcessAsKeyed"]);
                i._SignatureCaptured = Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_SignatureCaptured"]);
                i._IncludeAVS = Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_IncludeAVS"]);
                i._IncludeCV= Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_IncludeCV"]);
            }
            if (_industryType == "Ecommerce")
            {
                //ApplicationData
                i._ApplicationAttended = false;
                i._ApplicationLocation = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ApplicationLocation.OffPremises;
                i._PINCapability = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PINCapability.PINNotSupported;
                i._ReadCapability = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ReadCapability.KeyOnly;
                i._EncryptionType = EncryptionType.NotSet;

                //MerchantData
                i._IndustryType = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.IndustryType.Ecommerce;
                i._CustomerPresent = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CustomerPresent.Ecommerce;
                i._RequestACI = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.RequestACI.IsCPSMeritCapable;
                i._EntryMode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.EntryMode.Keyed;

                //TransactionData
                i._ProcessAsKeyed = true;
                i._SignatureCaptured = false;
                i._IncludeAVS = true;
                i._IncludeCV = true;
            }
            if (_industryType == "MOTO")
            {
                //ApplicationData
                i._ApplicationAttended = false;
                i._ApplicationLocation = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ApplicationLocation.OffPremises;
                i._PINCapability = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PINCapability.PINNotSupported;
                i._ReadCapability = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ReadCapability.KeyOnly;
                i._EncryptionType = EncryptionType.NotSet;

                //MerchantData
                i._IndustryType = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.IndustryType.MOTO;
                i._CustomerPresent = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CustomerPresent.MOTO;
                i._RequestACI = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.RequestACI.IsCPSMeritCapable;
                i._EntryMode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.EntryMode.Keyed;

                //TransactionData
                i._ProcessAsKeyed = true;
                i._SignatureCaptured = false;
                i._IncludeAVS = true;
                i._IncludeCV = true;
            }
            if (_industryType == "Retail")
            {
                //ApplicationData
                i._ApplicationAttended = true;
                i._ApplicationLocation = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ApplicationLocation.OnPremises;
                i._PINCapability = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PINCapability.PINNotSupported;
                i._ReadCapability = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ReadCapability.HasMSR;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_ProcessMagensaTxn"]))
                    i._EncryptionType = EncryptionType.MagneSafeV4V5Compatible;
                else
                    i._EncryptionType = EncryptionType.NotSet;
                //MerchantData
                i._IndustryType = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.IndustryType.Retail;
                i._CustomerPresent = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CustomerPresent.Present;
                i._RequestACI = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.RequestACI.IsCPSMeritCapable;
                i._EntryMode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.EntryMode.TrackDataFromMSR;

                //TransactionData
                i._ProcessAsKeyed = false;
                i._SignatureCaptured = true;
                i._IncludeAVS = false;
                i._IncludeCV = false;
            }
            if (_industryType == "Restaurant")
            {
                //ApplicationData
                i._ApplicationAttended = true;
                i._ApplicationLocation = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ApplicationLocation.OnPremises;
                i._PINCapability = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PINCapability.PINNotSupported;
                i._ReadCapability = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ReadCapability.HasMSR;
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["TxnData_ProcessMagensaTxn"]))
                    i._EncryptionType = EncryptionType.MagneSafeV4V5Compatible;
                else
                    i._EncryptionType = EncryptionType.NotSet;
                //MerchantData
                i._IndustryType = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.IndustryType.Restaurant;
                i._CustomerPresent = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CustomerPresent.Present;
                i._RequestACI = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.RequestACI.IsCPSMeritCapable;
                i._EntryMode = schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.EntryMode.TrackDataFromMSR;

                //TransactionData
                i._ProcessAsKeyed = false;
                i._SignatureCaptured = true;
                i._IncludeAVS = false;
                i._IncludeCV = false;
            }
            _ITV = i;
            return i;
        }

        public static bool LoadPersistedConfig()
        {
            //Check to see if previous settings exists. 
            /*NOTE
              * SECURITY CONSIDERATIONS
              * Stored on file system with read/write permission for only the application/service and IT Administration
              * Stored in DB with read/write permission for only the application/service and IT Administration
            */
            try
            {
                //Get Settings
                target._blnEncryptedIdentityToken = false;

                try
                {
                    if (File.Exists(target.Helper.ServiceKey + "_TransactionProcessing.config"))
                    {
                        XmlTextReader xtr = new XmlTextReader(target.Helper.ServiceKey + "_TransactionProcessing.config");
                        XmlDocument doc = new XmlDocument();
                        doc.Load(xtr);
                        xtr.Close();

                        XPathNavigator xnav = doc.CreateNavigator();
                        XPathNavigator node = xnav.SelectSingleNode(@"//TransactionProcessing/Configuration/ApplicationProfielId");
                        if (node != null && node.Value != null)
                            target.Helper.ApplicationProfileId = node.Value;

                        node = xnav.SelectSingleNode(@"//TransactionProcessing/Configuration/ServiceId");
                        if (node != null && node.Value != null)
                            target.Helper.ServiceID = node.Value;

                        node = xnav.SelectSingleNode(@"//TransactionProcessing/Configuration/WorkflowId");
                        if (node != null && node.Value != null)
                            target.Helper.WorkflowID = node.Value;

                        node = xnav.SelectSingleNode(@"//TransactionProcessing/Configuration/MerchantProfielId");
                        if (node != null && node.Value != null)
                            target.Helper.MerchantProfileId = node.Value;

                        node = xnav.SelectSingleNode(@"//TransactionProcessing/Configuration/IdentityToken");
                        if (node != null && node.Value != null)
                            target.Helper.IdentityToken = node.Value;
                        target._blnEncryptedIdentityToken = Convert.ToBoolean(node.GetAttribute("Encrypted", ""));

                        string strEncryptedIdentityToken = "\r\nIdentity Token [NOT ENCRYPTED]";

                        if (target._blnEncryptedIdentityToken)
                        {
                            strEncryptedIdentityToken = "\r\nIdentity Token [ENCRYPTED]";
                            target.Helper.IdentityToken = target.Helper.Decrypt(target.Helper.IdentityToken);
                        }

                        if (target.Helper.ApplicationProfileId.Length > 0) target.chkStep2 = true;
                        target.txtPersistedAndCached = "ApplicationProfileId : " + target.Helper.ApplicationProfileId + "\r\nServiceId : " + target.Helper.ServiceID + "\r\nWorkflowId : " + target.Helper.WorkflowID + "\r\nMerchantProfileId : " + target.Helper.MerchantProfileId + strEncryptedIdentityToken;
                    }
                    return true;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    target.txtPersistedAndCached = "ApplicationProfileId : " + target.Helper.ApplicationProfileId + "\r\nServiceId : " + target.Helper.ServiceID + "\r\nWorkflowId : " + target.Helper.WorkflowID + "\r\nMerchantProfileId : " + target.Helper.MerchantProfileId;
                    return false;
                }
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                MessageBox.Show("No persisted values found in '[SK]_TransactionProcessing.config'. Initializing application for the first time");
                target.txtPersistedAndCached = "ApplicationProfileId : " + target.Helper.ApplicationProfileId + "\r\nServiceId : " + target.Helper.ServiceID + "\r\nWorkflowId : " + target.Helper.WorkflowID + "\r\nMerchantProfileId : " + target.Helper.MerchantProfileId;
                return false;
            }
        }

        public static bool SavePersistedConfig(PersistAndCacheSettings pacs)
        {
            //Save to file
            /*NOTE
              * SECURITY CONSIDERATIONS
              * Stored on file system with read/write permission for only the application/service and IT Administration
              * Stored in DB with read/write permission for only the application/service and IT Administration
            */

            try
            {
                //Save Settings
                XmlDocument doc = new XmlDocument();
                XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                doc.AppendChild(docNode);
                XmlNode transactionProcessingValues = doc.CreateElement("TransactionProcessing");
                doc.AppendChild(transactionProcessingValues);
                XmlNode configuration = doc.CreateElement("Configuration");
                transactionProcessingValues.AppendChild(configuration);

                XmlNode applicationProfielId = doc.CreateElement("ApplicationProfielId");
                applicationProfielId.InnerText = pacs.ApplicationProfileId;
                configuration.AppendChild(applicationProfielId);

                XmlNode serviceId = doc.CreateElement("ServiceId");
                serviceId.InnerText = pacs.ServiceId;
                XmlAttribute multipleServiceId = doc.CreateAttribute("MultipleServiceId");
                multipleServiceId.Value = (target._si.BankcardServices.Count > 1 ? "True" : "False");
                serviceId.Attributes.Append(multipleServiceId);
                configuration.AppendChild(serviceId);

                XmlNode workflowId = doc.CreateElement("WorkflowId");
                workflowId.InnerText = pacs.WorkflowId;
                XmlAttribute multipleWorkflowId = doc.CreateAttribute("MultipleWorkflowId");
                multipleWorkflowId.Value = (target._si.BankcardServices.Count > 1 ? "True" : "False");
                workflowId.Attributes.Append(multipleWorkflowId);
                configuration.AppendChild(workflowId);

                XmlNode merchantProfielId = doc.CreateElement("MerchantProfielId");
                merchantProfielId.InnerText = pacs.MerchantProfileId;
                XmlAttribute multipleMerchants = doc.CreateAttribute("MultipleMerchants");
                multipleMerchants.Value = (target.MerchantProfileIds.Count > 2 ? "True" : "False");//Since by default empty comes back we need to validate at 2 and not 1
                merchantProfielId.Attributes.Append(multipleMerchants);
                configuration.AppendChild(merchantProfielId);

                XmlNode identityToken = doc.CreateElement("IdentityToken");
                identityToken.InnerText = pacs.IdentityToken;
                XmlAttribute encrypted = doc.CreateAttribute("Encrypted");
                encrypted.Value = pacs.EncryptedIdentityToken.ToString();//The following is dependant on the software company integration needs.
                identityToken.Attributes.Append(encrypted);
                configuration.AppendChild(identityToken);

                doc.Save(target.Helper.ServiceKey + "_TransactionProcessing.config");

                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return false;
        }

        public static void SandboxTestCardData()
        {
            if (target.CboCardTypes.ToString() == "Visa")
            {
                target.TxtPAN = "4111111111111111";
                target.TxtTrackDataFromMSR = "%B4111111111111111^IPCOMMERCE/TESTCARD^13121010454500415000010?;4111111111111111=13121010454541500010?";
            }
			else if (target.CboCardTypes.ToString() == "MasterCard")
            {
                target.TxtPAN = "5454545454545454";
                target.TxtTrackDataFromMSR = "%B5454545454545454^IPCOMMERCE/TESTCARD^1312101013490000000001000880000?;5454545454545454=13121010134988000010?";
            }
			else if (target.CboCardTypes.ToString() == "AmericanExpress")
            {
                target.TxtPAN = "371449635398456";
                target.TxtTrackDataFromMSR = "%B371449635398456^IPCOMMERCE/TESTCARD^1312060523319?;371449635398456=1312060523319?";
            }
			else if (target.CboCardTypes.ToString() == "Discover")
            {
                target.TxtPAN = "6011000995504101";
                target.TxtTrackDataFromMSR = "%B6011000995504101^IPCOMMERCE/TESTCARD^13121011000627210201?;6011000995504101=13121011000627210201?";
            }
			else if (target.CboCardTypes.ToString() == "PrivateLabel")
            {
                target.TxtPAN = "8818889876543211";
                target.TxtTrackDataFromMSR = ";8818889876543211=13121014764094900001?";
            }
            else
            {
                target.TxtPAN = "";
                target.TxtTrackDataFromMSR = "";
            }
        }
    }
    public class Item
    {
        public string Name;
        public string Value1;
        public string Value2;
        public Item(string name, string value1, string value2)
        {
            Name = name;
            Value1 = value1;
            Value2 = value2;
        }
        public override string ToString()
        {// Generates the text shown in the combo box
            return Name;
        }
    }

    public class BankCardProProcessingOptions
    {
        public PurchaseCardLevel PurchaseCardLevel;
        public bool InterchangeData;
        public bool IncludeLevel2OrLevel3Data;
        public BankCardProProcessingOptions(PurchaseCardLevel purchaseCardLevel, bool interchangeData, bool includeLevel2OrLevel3Data)
        {
            PurchaseCardLevel = purchaseCardLevel;
            InterchangeData = interchangeData;
            IncludeLevel2OrLevel3Data = includeLevel2OrLevel3Data;
        }
    }

    public class TokenizedTransaction
    {
        public string PaymentAccountDataToken;
        public string ExpireationDate;
        public string MaskedPAN;
        public TypeCardType CardType;
        public TokenizedTransaction(string paymentAccountDataToken, string expireationDate, string maskedPAN, TypeCardType cardType)
        {
            PaymentAccountDataToken = paymentAccountDataToken;
            ExpireationDate = expireationDate;
            MaskedPAN = maskedPAN;
            CardType = cardType;
        }
        public override string ToString()
        {// Generates the text shown in the combo box
            return CardType + " - [Exp:" + ExpireationDate + "] " + MaskedPAN;
        }
    }

    public class IndustryTypeValues
    {
        //ApplicationData
        public bool _ApplicationAttended;
        public schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ApplicationLocation _ApplicationLocation;
        public schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PINCapability _PINCapability;
        public schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ReadCapability _ReadCapability;
        public EncryptionType _EncryptionType;

        //MerchantData
        public schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.IndustryType _IndustryType;
        public schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CustomerPresent _CustomerPresent;
        public schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.RequestACI _RequestACI;
        public schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.EntryMode _EntryMode;

        //TransactionData
        public bool _ProcessAsKeyed;
        public bool _SignatureCaptured;
        public bool _IncludeAVS;
        public bool _IncludeCV;

        public IndustryTypeValues()
        {

        }


    }
}

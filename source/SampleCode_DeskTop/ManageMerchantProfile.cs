#region DISCLAIMER
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
using System.Windows.Forms;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo;

namespace SampleCode
{
    public partial class ManageMerchantProfile : Form
    {
        private bool _Add = true;
        private TypeISOLanguageCodeA3 _Language;
        private TypeISOCountryCodeA3 _CountryCode;
        private TypeISOCurrencyCodeA3 _CurrencyCode;
        private CustomerPresent _CustomerPresent;
        private RequestACI _RequestACI;
        private IndustryType _MerchantIndustryType;
        private EntryMode _EntryMode ;
        public bool _Dirty;
        public HelperMethods Helper = new HelperMethods();//The following class performs many of the different operations needs for service information and trasaction processing
        private BankcardService _bcs = new BankcardService();
        private ElectronicCheckingService _ecks = new ElectronicCheckingService();
        private StoredValueService _svas = new StoredValueService();
        private string _strServiceID;

        protected FaultHandler.FaultHandler _FaultHandler = new FaultHandler.FaultHandler();

        public ManageMerchantProfile()
        {
            InitializeComponent();
        }

        private void showECKFields()
        {
            txtProfileId.ReadOnly = false;
            //MerchantData
            txtCustomerServiceInternet.ReadOnly = false;
            txtCustomerServicePhone.ReadOnly = false;
            txtMerchantId.ReadOnly = false;
            lblMerchantId.Text = "OriginatorId";
            txtName.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtTaxId.ReadOnly = true;
            //MerchantData.Address
            txtCity.ReadOnly = false;
            txtPostalCode.ReadOnly = false;
            txtStateProvince.ReadOnly = false;
            txtStreetAddress1.ReadOnly = false;
            txtStreetAddress2.ReadOnly = false;
            //MerchantData.BankcardMerchantData
            txtABANumber.ReadOnly = true;
            txtAcquirerBIN.ReadOnly = true;
            txtAgentBank.ReadOnly = true;
            txtAgentChain.ReadOnly = true;
            txtClientNum.ReadOnly = true;
            txtLocation.ReadOnly = true;
            txtSecondaryTerminalId.ReadOnly = true;
            txtSettlementAgent.ReadOnly = true;
            txtSharingGroup.ReadOnly = true;
            txtSIC.ReadOnly = false;
            txtStoreId.ReadOnly = false;
            lblStoreId.Text = "SiteId";
            txtSocketNum.ReadOnly = false;
            lblTerminalId.Text = "ProductId";
            txtTimeZoneDifferential.ReadOnly = true;
            txtReimbursementAttribute.ReadOnly = true;
            txtClientNum.Text = "";

        }

        private void showSVAFields()
        {
            txtProfileId.ReadOnly = false;
            //MerchantData
            txtCustomerServiceInternet.ReadOnly = false;
            txtCustomerServicePhone.ReadOnly = false;
            txtMerchantId.ReadOnly = false;
            txtName.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtTaxId.ReadOnly = false;
            //MerchantData.Address
            txtCity.ReadOnly = false;
            txtPostalCode.ReadOnly = false;
            txtStateProvince.ReadOnly = false;
            txtStreetAddress1.ReadOnly = false;
            txtStreetAddress2.ReadOnly = false;
            //MerchantData.BankcardMerchantData
            txtABANumber.ReadOnly = true;
            txtAcquirerBIN.ReadOnly = true;
            txtAgentBank.ReadOnly = true;
            txtAgentChain.ReadOnly = false;
            txtAgentChain.Text = "124423";
            txtClientNum.ReadOnly = false;
            txtLocation.ReadOnly = true;
            txtSecondaryTerminalId.ReadOnly = true;
            txtSettlementAgent.ReadOnly = true;
            txtSharingGroup.ReadOnly = true;
            txtSIC.ReadOnly = false;
            txtStoreId.ReadOnly = false;
            txtSocketNum.ReadOnly = false;
            txtTimeZoneDifferential.ReadOnly = true;
            txtReimbursementAttribute.ReadOnly = true;
            
        }

        private void showBCPFields()
        {
            txtProfileId.ReadOnly = false;
            //MerchantData
            txtCustomerServiceInternet.ReadOnly = false;
            txtCustomerServicePhone.ReadOnly = false;
            txtMerchantId.ReadOnly = false;
            txtName.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtTaxId.ReadOnly = false;
            //MerchantData.Address
            txtCity.ReadOnly = false;
            txtPostalCode.ReadOnly = false;
            txtStateProvince.ReadOnly = false;
            txtStreetAddress1.ReadOnly = false;
            txtStreetAddress2.ReadOnly = false;
            //MerchantData.BankcardMerchantData
            txtABANumber.ReadOnly = true;
            txtAcquirerBIN.ReadOnly = true;
            txtAgentBank.ReadOnly = true;
            txtAgentChain.ReadOnly = true;
            txtClientNum.ReadOnly = false;
            txtLocation.ReadOnly = true;
            txtSecondaryTerminalId.ReadOnly = true;
            txtSettlementAgent.ReadOnly = true;
            txtSharingGroup.ReadOnly = true;
            txtSIC.ReadOnly = false;
            txtStoreId.ReadOnly = true;
            txtStoreId.Text = "";
            txtSocketNum.ReadOnly = false;
            txtTimeZoneDifferential.ReadOnly = true;
            txtReimbursementAttribute.ReadOnly = true;
            
        }

        private void showBCPExpandedFields()
        {
            txtProfileId.ReadOnly = false;
            //MerchantData
            txtCustomerServiceInternet.ReadOnly = false;
            txtCustomerServicePhone.ReadOnly = false;
            txtMerchantId.ReadOnly = false;
            txtName.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtTaxId.ReadOnly = false;
            //MerchantData.Address
            txtCity.ReadOnly = false;
            txtPostalCode.ReadOnly = false;
            txtStateProvince.ReadOnly = false;
            txtStreetAddress1.ReadOnly = false;
            txtStreetAddress2.ReadOnly = false;
            //MerchantData.BankcardMerchantData
            txtABANumber.ReadOnly = false;
            txtAcquirerBIN.ReadOnly = false;
            txtAgentBank.ReadOnly = false;
            txtAgentChain.ReadOnly = false;
            txtClientNum.ReadOnly = false;
            txtLocation.ReadOnly = false;
            txtSecondaryTerminalId.ReadOnly = false;
            txtSettlementAgent.ReadOnly = false;
            txtSharingGroup.ReadOnly = false;
            txtSIC.ReadOnly = false;
            txtStoreId.ReadOnly = false;
            txtSocketNum.ReadOnly = false;
            txtTimeZoneDifferential.ReadOnly = false;
            txtReimbursementAttribute.ReadOnly = false;
        }
        
        private void hideAllFields()
        {
            txtProfileId.ReadOnly = true;
            //MerchantData
            txtCustomerServiceInternet.ReadOnly = true;
            txtCustomerServicePhone.ReadOnly = true;
            txtMerchantId.ReadOnly = true;
            lblMerchantId.Text = "MerchantId";
            txtName.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtTaxId.ReadOnly = true;
            //MerchantData.Address
            txtCity.ReadOnly = true;
            txtPostalCode.ReadOnly = true;
            txtStateProvince.ReadOnly = true;
            txtStreetAddress1.ReadOnly = true;
            txtStreetAddress2.ReadOnly = true;
            //MerchantData.BankcardMerchantData
            txtABANumber.ReadOnly = true;
            txtAcquirerBIN.ReadOnly = true;
            txtAgentBank.ReadOnly = true;
            txtAgentChain.ReadOnly = true;
            txtClientNum.ReadOnly = true;
            txtLocation.ReadOnly = true;
            txtSecondaryTerminalId.ReadOnly = true;
            txtSettlementAgent.ReadOnly = true;
            txtSharingGroup.ReadOnly = true;
            txtSIC.ReadOnly = true;
            txtStoreId.ReadOnly = true;
            lblStoreId.Text = "StoreId";
            txtSocketNum.ReadOnly = true;
            lblTerminalId.Text = "TerminalId";
            txtTimeZoneDifferential.ReadOnly = true;
            txtReimbursementAttribute.ReadOnly = true;
        }

        private void showAllFields()
        {
            txtProfileId.ReadOnly = false;
            //MerchantData
            txtCustomerServiceInternet.ReadOnly = false;
            txtCustomerServicePhone.ReadOnly = false;
            txtMerchantId.ReadOnly = false;
            lblMerchantId.Text = "MerchantId";
            txtName.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtTaxId.ReadOnly = false;
            //MerchantData.Address
            txtCity.ReadOnly = false;
            txtPostalCode.ReadOnly = false;
            txtStateProvince.ReadOnly = false;
            txtStreetAddress1.ReadOnly = false;
            txtStreetAddress2.ReadOnly = false;
            //MerchantData.BankcardMerchantData
            txtABANumber.ReadOnly = false;
            txtAcquirerBIN.ReadOnly = false;
            txtAgentBank.ReadOnly = false;
            txtAgentChain.ReadOnly = false;
            txtClientNum.ReadOnly = false;
            txtLocation.ReadOnly = false;
            txtSecondaryTerminalId.ReadOnly = false;
            txtSettlementAgent.ReadOnly = false;
            txtSharingGroup.ReadOnly = false;
            txtSIC.ReadOnly = false;
            txtStoreId.ReadOnly = false;
            lblStoreId.Text = "StoreId";
            txtSocketNum.ReadOnly = false;
            lblTerminalId.Text = "TerminalId";
            txtTimeZoneDifferential.ReadOnly = false;
            txtReimbursementAttribute.ReadOnly = false;
        }

        public void CallingForm(MerchantProfile merchantProfile, bool blnNewProfile, BankcardService bcs, ElectronicCheckingService ecks, StoredValueService svas, string serviceId)
        {
            _bcs = bcs;
            _ecks = ecks;
            _svas = svas;
            _strServiceID = serviceId;

            hideAllFields();
            //Since MerchantProfile is saved at the serviceId level, display serviceId.
            txtMerchantProfileServiceId.Text = merchantProfile.ServiceId;
            
            if(blnNewProfile)
            {//New profile to add to CWS
                cmdAddUpdate.Text = "Add";

                //Populate combo boxes with the Enumeration
                cboCountryCode.Sorted = true;
                cboCountryCode.DataSource = Enum.GetValues(typeof(TypeISOCountryCodeA3));
                cboCountryCode.SelectedItem = TypeISOCountryCodeA3.NotSet;

                cboLanguage.Sorted = true;
                cboLanguage.DataSource = Enum.GetValues(typeof(TypeISOLanguageCodeA3));
                cboLanguage.SelectedItem = TypeISOLanguageCodeA3.NotSet;

                cboCurrencyCode.Sorted = true;
                cboCurrencyCode.DataSource = Enum.GetValues(typeof(TypeISOCurrencyCodeA3));
                cboCurrencyCode.SelectedItem = TypeISOCurrencyCodeA3.NotSet;


                cboCustomerPresent.Sorted = true;
                cboCustomerPresent.DataSource = Enum.GetValues(typeof(CustomerPresent));
                cboCustomerPresent.SelectedItem = CustomerPresent.NotSet;

                cboRequestACI.Sorted = true;
                cboRequestACI.DataSource = Enum.GetValues(typeof(RequestACI));
                cboRequestACI.SelectedItem = RequestACI.IsCPSMeritCapable;

                cboEntryMode.Sorted = true;
                cboEntryMode.DataSource = Enum.GetValues(typeof(EntryMode));
                cboEntryMode.SelectedItem = EntryMode.NotSet;

                cboMerchantIndustryType.Sorted = true;
                cboMerchantIndustryType.DataSource = Enum.GetValues(typeof(IndustryType));
                cboMerchantIndustryType.SelectedItem = IndustryType.NotSet;
            }
            else
            {//Existing Profile to Update;
                //Note : items commented out are not use so no need to wire up a text box as well as add to 'SaveMerchantInformation()'
                txtProfileId.Text = merchantProfile.ProfileId;
                txtProfileId.ReadOnly = true;
                lblLastUpdated.Text = "Last Updated : " + merchantProfile.LastUpdated;
                //MerchantData
                //MerchantData.Address
                txtCity.Text = merchantProfile.MerchantData.Address.City;
                txtPostalCode.Text = merchantProfile.MerchantData.Address.PostalCode;
                txtStateProvince.Text = merchantProfile.MerchantData.Address.StateProvince.ToString();
                txtStreetAddress1.Text = merchantProfile.MerchantData.Address.Street1;
                txtStreetAddress2.Text = merchantProfile.MerchantData.Address.Street2;

                txtCustomerServiceInternet.Text = merchantProfile.MerchantData.CustomerServiceInternet;
                txtCustomerServicePhone.Text = merchantProfile.MerchantData.CustomerServicePhone;
                txtMerchantId.Text = merchantProfile.MerchantData.MerchantId;
                txtName.Text = merchantProfile.MerchantData.Name;
                txtPhone.Text = merchantProfile.MerchantData.Phone;
                txtTaxId.Text = merchantProfile.MerchantData.TaxId;
                if (_bcs != null)
                {
		                //MerchantData.BankcardMerchantData
		                txtABANumber.Text = merchantProfile.MerchantData.BankcardMerchantData.ABANumber;
		                txtAcquirerBIN.Text = merchantProfile.MerchantData.BankcardMerchantData.AcquirerBIN;
		                txtAgentBank.Text = merchantProfile.MerchantData.BankcardMerchantData.AgentBank;
		                txtAgentChain.Text = merchantProfile.MerchantData.BankcardMerchantData.AgentChain;
		                txtClientNum.Text = merchantProfile.MerchantData.BankcardMerchantData.ClientNumber;
		                txtLocation.Text = merchantProfile.MerchantData.BankcardMerchantData.Location;
		                //txtTBD.Text = _MerchantProfile.MerchantData.BankcardMerchantData.PrintCustomerServicePhone == "";
		                txtSecondaryTerminalId.Text = merchantProfile.MerchantData.BankcardMerchantData.SecondaryTerminalId;
		                txtSettlementAgent.Text = merchantProfile.MerchantData.BankcardMerchantData.SettlementAgent;
		                txtSharingGroup.Text = merchantProfile.MerchantData.BankcardMerchantData.SharingGroup;
		                txtSIC.Text = merchantProfile.MerchantData.BankcardMerchantData.SIC;
		                txtStoreId.Text = merchantProfile.MerchantData.BankcardMerchantData.StoreId;
		                txtSocketNum.Text = merchantProfile.MerchantData.BankcardMerchantData.TerminalId;
		                txtTimeZoneDifferential.Text = merchantProfile.MerchantData.BankcardMerchantData.TimeZoneDifferential;
		                txtReimbursementAttribute.Text = merchantProfile.MerchantData.BankcardMerchantData.ReimbursementAttribute;
              	}
              	if (_svas != null)
              	{
              			//MerchantData.StoredValueMerchantData
		                txtAgentChain.Text = merchantProfile.MerchantData.StoredValueMerchantData.AgentChain;
		                txtClientNum.Text = merchantProfile.MerchantData.StoredValueMerchantData.ClientNumber;
		                txtSIC.Text = merchantProfile.MerchantData.StoredValueMerchantData.SIC;
		                txtStoreId.Text = merchantProfile.MerchantData.StoredValueMerchantData.StoreId;
		                txtSocketNum.Text = merchantProfile.MerchantData.StoredValueMerchantData.TerminalId;
		                _MerchantIndustryType = merchantProfile.MerchantData.StoredValueMerchantData.IndustryType;
		            }
                    if (_ecks != null)
                    {
                        //MerchantData.ElectronicCheckingMerchantData
                        txtMerchantId.Text = merchantProfile.MerchantData.ElectronicCheckingMerchantData.OrginatorId;
                        txtStoreId.Text = merchantProfile.MerchantData.ElectronicCheckingMerchantData.SiteId;
                        txtSocketNum.Text = merchantProfile.MerchantData.ElectronicCheckingMerchantData.ProductId;
                    }

                //First Populate with the Enumeration
                cboCountryCode.DataSource = Enum.GetValues(typeof(TypeISOCountryCodeA3));
                //Now select the index that matches
                if (merchantProfile.MerchantData.Address.CountryCode.ToString().Length > 0)
                {
                    cboCountryCode.SelectedItem = merchantProfile.MerchantData.Address.CountryCode;
                    _CountryCode = (TypeISOCountryCodeA3)cboCountryCode.SelectedItem;
                }
                //First Populate with the Enumeration
                cboLanguage.DataSource = Enum.GetValues(typeof(TypeISOLanguageCodeA3));
                //Now select the index that matches
                if (merchantProfile.MerchantData.Language.ToString().Length > 0)
                {
                    cboLanguage.SelectedItem = merchantProfile.MerchantData.Language;
                    _Language = (TypeISOLanguageCodeA3)cboLanguage.SelectedItem;
                }
                //First Populate with the Enumeration
                cboCurrencyCode.DataSource = Enum.GetValues(typeof(TypeISOCurrencyCodeA3));
                //Now select the index that matches
                if (merchantProfile.MerchantData.Language.ToString().Length > 0)
                {
                    cboCurrencyCode.SelectedItem = merchantProfile.TransactionData.BankcardTransactionDataDefaults.CurrencyCode;
                    _CurrencyCode = (TypeISOCurrencyCodeA3)cboCurrencyCode.SelectedItem;
                }

                //First Populate with the Enumeration
                cboCustomerPresent.DataSource = Enum.GetValues(typeof(CustomerPresent));
                //Now select the index that matches
                if (merchantProfile.TransactionData.BankcardTransactionDataDefaults.CustomerPresent.ToString().Length > 0)
                {
                    cboCustomerPresent.SelectedItem = merchantProfile.TransactionData.BankcardTransactionDataDefaults.CustomerPresent;
                    _CustomerPresent = (CustomerPresent)cboCustomerPresent.SelectedItem;
                }

                //First Populate with the Enumeration
                cboRequestACI.DataSource = Enum.GetValues(typeof(RequestACI));
                //Now select the index that matches
                if (merchantProfile.TransactionData.BankcardTransactionDataDefaults.RequestACI.ToString().Length > 0)
                {
                    cboRequestACI.SelectedItem = merchantProfile.TransactionData.BankcardTransactionDataDefaults.RequestACI;
                    _RequestACI = (RequestACI)cboRequestACI.SelectedItem;
                }

                //First Populate with the Enumeration
                cboMerchantIndustryType.DataSource = Enum.GetValues(typeof(IndustryType));
                if (merchantProfile.MerchantData.BankcardMerchantData.IndustryType.ToString().Length > 0)
                {
                    cboMerchantIndustryType.SelectedItem = merchantProfile.MerchantData.BankcardMerchantData.IndustryType;
                    _MerchantIndustryType = (IndustryType)cboMerchantIndustryType.SelectedItem;
                }

                //First Populate with the Enumeration
                cboEntryMode.DataSource = Enum.GetValues(typeof(EntryMode));
                if (merchantProfile.TransactionData.BankcardTransactionDataDefaults.EntryMode.ToString().Length > 0)
                {
                    cboEntryMode.SelectedItem = merchantProfile.TransactionData.BankcardTransactionDataDefaults.EntryMode;
                    _EntryMode = (EntryMode)cboEntryMode.SelectedItem;
                }

                _Add = false; //In this case it's an update and not an add
                cmdAddUpdate.Text = "Update";
            }
            

            if (_bcs != null)
            {
                if (_strServiceID == "C82ED00001" || _strServiceID == "71C8700001" ||
                    _strServiceID == "88D9300001" || _strServiceID == "B447F00001" ||
                    _strServiceID == "D806000001" || _strServiceID == "E88FD00001")
                    showBCPExpandedFields();
                else if (_strServiceID == "168511300C" || _strServiceID == "9999999999")
                    showBCPExpandedFields();
                else
                {
                    showBCPFields();
                }
            }

            if (_ecks != null)
            {
                showECKFields();
            }
            if (_svas != null)
            {
                showSVAFields();
            }
        }

        private void SaveMerchantInformation()
        {
            List<MerchantProfile> MPList = new List<MerchantProfile>();
            MerchantProfile MerP = new MerchantProfile();

            MerP.ProfileId = txtProfileId.Text;
            MerP.MerchantData = new MerchantProfileMerchantData();
            MerP.MerchantData.Address = new AddressInfo();
            //MerP.MerchantData.BankcardMerchantData = new BankcardMerchantData();
            
            MerP.TransactionData = new MerchantProfileTransactionData();
           
            
            MerP.MerchantData.Address.Street1 = txtStreetAddress1.Text;
            MerP.MerchantData.Address.Street2 = txtStreetAddress2.Text;
            MerP.MerchantData.Address.City = txtCity.Text;
            try{ MerP.MerchantData.Address.StateProvince = (TypeStateProvince) Enum.Parse(typeof (TypeStateProvince), txtStateProvince.Text);}
            catch{}
            MerP.MerchantData.Address.PostalCode = txtPostalCode.Text;
            MerP.MerchantData.CustomerServicePhone = txtCustomerServicePhone.Text;
            MerP.MerchantData.CustomerServiceInternet = txtCustomerServiceInternet.Text;
            MerP.MerchantData.MerchantId = txtMerchantId.Text;
            MerP.MerchantData.Name = txtName.Text;
            MerP.MerchantData.Phone = txtPhone.Text;
            MerP.MerchantData.TaxId = txtTaxId.Text;
            MerP.MerchantData.Language = _Language;
            MerP.MerchantData.Address.CountryCode = _CountryCode;

            MerP.MerchantData.BankcardMerchantData = new BankcardMerchantData();

            if (_bcs != null)
            {
                MerP.TransactionData.BankcardTransactionDataDefaults = new BankcardTransactionDataDefaults();
                MerP.MerchantData.BankcardMerchantData = new BankcardMerchantData();
                MerP.MerchantData.BankcardMerchantData.ClientNumber = txtClientNum.Text;
                MerP.MerchantData.BankcardMerchantData.SIC = txtSIC.Text;
                MerP.MerchantData.BankcardMerchantData.TerminalId = txtSocketNum.Text;
                MerP.MerchantData.BankcardMerchantData.StoreId = txtStoreId.Text;
                MerP.TransactionData.BankcardTransactionDataDefaults.CurrencyCode = _CurrencyCode;
                MerP.TransactionData.BankcardTransactionDataDefaults.CustomerPresent = _CustomerPresent;
                MerP.TransactionData.BankcardTransactionDataDefaults.RequestACI = _RequestACI;
                MerP.TransactionData.BankcardTransactionDataDefaults.RequestAdvice = RequestAdvice.Capable;
                MerP.TransactionData.BankcardTransactionDataDefaults.EntryMode = _EntryMode;

                // Tsys Terminal Capture Siera
                MerP.MerchantData.BankcardMerchantData.ABANumber = txtABANumber.Text;
                MerP.MerchantData.BankcardMerchantData.AcquirerBIN = txtAcquirerBIN.Text;
                MerP.MerchantData.BankcardMerchantData.AgentBank = txtAgentBank.Text;
                MerP.MerchantData.BankcardMerchantData.AgentChain = txtAgentChain.Text;
                MerP.MerchantData.BankcardMerchantData.Location = txtLocation.Text;
                MerP.MerchantData.BankcardMerchantData.SecondaryTerminalId = txtSecondaryTerminalId.Text;
                MerP.MerchantData.BankcardMerchantData.SettlementAgent = txtSettlementAgent.Text;
                MerP.MerchantData.BankcardMerchantData.SharingGroup = txtSharingGroup.Text;
                MerP.MerchantData.BankcardMerchantData.TimeZoneDifferential = txtTimeZoneDifferential.Text;
                MerP.MerchantData.BankcardMerchantData.ReimbursementAttribute = txtReimbursementAttribute.Text;
                MerP.MerchantData.BankcardMerchantData.IndustryType = _MerchantIndustryType;
            }

            if (_ecks != null)
            {
                MerP.MerchantData.ElectronicCheckingMerchantData = new ElectronicCheckingMerchantData();
                MerP.MerchantData.ElectronicCheckingMerchantData.OrginatorId = txtMerchantId.Text;
                MerP.MerchantData.ElectronicCheckingMerchantData.ProductId = txtSocketNum.Text;
                MerP.MerchantData.ElectronicCheckingMerchantData.SiteId = txtStoreId.Text;
            }
            if (_svas != null)
            {
                MerP.MerchantData.StoredValueMerchantData = new StoredValueMerchantData();
                MerP.MerchantData.StoredValueMerchantData.AgentChain = txtAgentChain.Text;
                MerP.MerchantData.StoredValueMerchantData.ClientNumber = txtClientNum.Text;
                MerP.MerchantData.StoredValueMerchantData.SIC = txtSIC.Text;
                MerP.MerchantData.StoredValueMerchantData.StoreId = txtStoreId.Text;
                MerP.MerchantData.StoredValueMerchantData.TerminalId = txtSocketNum.Text;
                MerP.MerchantData.StoredValueMerchantData.IndustryType = _MerchantIndustryType;

            }
            
            //Add the profile to a list of profiles. This is necessary to save the profile
            MPList.Add(MerP);
            //From the calling form
            ((SampleCode_DeskTop)(Owner)).Helper.CheckTokenExpire();
            string _strServiceID = ((SampleCode_DeskTop)(Owner)).Helper.ServiceID;
            string _strSessionToken = ((SampleCode_DeskTop)(Owner)).Helper.SessionToken;

            if (ChkUseWorkFlowId.Checked)
            {
                ((SampleCode_DeskTop)(Owner)).Helper.Cwssic.SaveMerchantProfiles(_strSessionToken, ((SampleCode_DeskTop)(Owner)).Helper.WorkflowID, TenderType.Credit, MPList);
            }
            else
            {
                ((SampleCode_DeskTop)(Owner)).Helper.Cwssic.SaveMerchantProfiles(_strSessionToken, _strServiceID, TenderType.Credit, MPList);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Form Events

        private void cmdAddUpdate_Click(object sender, EventArgs e)
        {
            _Dirty = true;
            try
            {
                //Unique MerchantProfileId should be used for all new MerchantProfiles added
                if (_Add && txtProfileId.Text.Length < 1) { MessageBox.Show("MerchantProfileId required and cannot be empty"); txtProfileId.Focus(); return; }
                if (!_Add && txtProfileId.Text.Length < 1) { MessageBox.Show("Detected Empty MerchantProfileId. The Sample will allow the update however an empty MerchantProfileId is not a recommended solution."); txtProfileId.Focus(); }

                SaveMerchantInformation();
                if (_Add) { MessageBox.Show("Successfully Added a new Profile"); }
                else { MessageBox.Show("Successfully Updated a Profile"); }
                Close();
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {
                //In this case the SvcEndpoint was not available. Try the same logic again with the alternate Endpoint
                try
                {
                    ((SampleCode_DeskTop)(Owner)).Helper.SetSvcEndpoint();//Change the endpoint to use the backup.
                    SaveMerchantInformation();
                    if (_Add) { MessageBox.Show("Successfully Added a new Profile"); }
                    else { MessageBox.Show("Successfully Updated a Profile"); }
                    Close();
                }
                catch (System.ServiceModel.EndpointNotFoundException)
                {
                    MessageBox.Show(
                        "Neither the primary or secondary endpoints are available. Unable to process.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                string strErrorId;
                string strErrorMessage;
                if (_FaultHandler.handleSvcInfoFault(ex, out strErrorId, out strErrorMessage))
                { MessageBox.Show(strErrorId + " : " + strErrorMessage); }
                else { MessageBox.Show(ex.Message); }
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            DialogResult Result;
            string profileId = txtProfileId.Text;
            if (profileId.Trim() == "") profileId = "<default>";
            Result = MessageBox.Show(
                "The action will attempt to delete the profile \r\n\r\n     '" + profileId + "' \r\n\r\nDo you want to continue?",
                "Overwrite", MessageBoxButtons.OKCancel);
            if (Result == DialogResult.Cancel) return;

            //From the calling form
            ((SampleCode_DeskTop)(Owner)).Helper.CheckTokenExpire();
            string _strServiceID = ((SampleCode_DeskTop)(Owner)).Helper.ServiceID;
            string _strSessionToken = ((SampleCode_DeskTop)(Owner)).Helper.SessionToken;

            try
            {
                ((SampleCode_DeskTop)(Owner)).Helper.Cwssic.DeleteMerchantProfile(_strSessionToken, txtProfileId.Text, _strServiceID, TenderType.Credit);
            }
            catch (Exception ex)
            {
                string strErrorId;
                string strErrorMessage;
                if (_FaultHandler.handleSvcInfoFault(ex, out strErrorId, out strErrorMessage))
                { MessageBox.Show(strErrorId + " : " + strErrorMessage); }
                else { MessageBox.Show(ex.Message); }
            }
            //Since the profile no longer exists close the dialogue. 
            _Dirty = true;
            Close();
        }

        private void cmdPopulateTestValues_Click(object sender, EventArgs e)
        {//Online Reference https://my.ipcommerce.com/Docs/TransactionProcessing/CWS/API_Reference/2.0.17/ServiceInfoDataElements/BankcardMerchantData.aspx
            MessageBox.Show(
                "Please note that the following values are generic. Depending on the scope of your integration the following values may change. Please contact your solution consultant with any questions.");

            //The following are typical settings please ask your solution consultant if you have any questions
            txtStreetAddress1.Text = "123 Merchant name";
            txtStreetAddress2.Text = "";
            txtCity.Text = "Pleasanton";
            txtStateProvince.Text = "CA";
            txtPostalCode.Text = "94566";
            txtCustomerServicePhone.Text = "303 5553232"; // Must be of format “NNN NNNNNNN”
            txtMerchantId.Text = "123456789012";
            txtName.Text = "Test Merch";
            txtPhone.Text = "720 8881212"; // Must be of format “NNN NNNNNNN”
            txtTaxId.Text = "";
            txtClientNum.Text = "1234";
            txtSIC.Text = "5999";
            txtStoreId.Text = "1234";
            txtSocketNum.Text = "001";
            txtCustomerServiceInternet.Text = "test@test.com";

            //The following fields are used by Tsys Terminal Capture Siera and are part of the additional items
            if (_strServiceID == "C82ED00001" || _strServiceID == "71C8700001" ||
                    _strServiceID == "88D9300001" || _strServiceID == "B447F00001" ||
                    _strServiceID == "D806000001" || _strServiceID == "E88FD00001")//Tsys/Vantiv Terminal Capture ServiceId
            {
                txtStoreId.Text = "1234"; //Used only for RBS and Tsys
                txtABANumber.Text = "987654321";
                txtAcquirerBIN.Text = "654321";
                txtAgentBank.Text = "123456";
                txtAgentChain.Text = "645231";
                txtLocation.Text = "12345";
                txtSecondaryTerminalId.Text = "12345678";
                txtSettlementAgent.Text = "12AB";
                txtSharingGroup.Text = "123ABC";
                txtTimeZoneDifferential.Text = "005";
                txtReimbursementAttribute.Text = "A";
                txtSocketNum.Text = "0001";
            }

            try
            {
                try { cboMerchantIndustryType.SelectedItem = (IndustryType)Enum.Parse(typeof(IndustryType), ConfigurationSettings.AppSettings["IndustryType"]); }
                catch { }
                cboLanguage.SelectedItem = TypeISOLanguageCodeA3.ENG;
                cboCountryCode.SelectedItem = TypeISOCountryCodeA3.USA;
                cboCurrencyCode.SelectedItem = TypeISOCurrencyCodeA3.USD;
                try { cboCustomerPresent.SelectedItem = (CustomerPresent)Enum.Parse(typeof(CustomerPresent), ConfigurationSettings.AppSettings["CustomerPresent"]); }// [Ecommerce : Ecommerce] [MOTO : MOTO] [Retail/Restaurant : Present]
                catch { }
                try { cboRequestACI.SelectedItem = (RequestACI)Enum.Parse(typeof(RequestACI), ConfigurationSettings.AppSettings["RequestACI"]); }//In general default to "IsCPSMeritCapable"
                catch { }
                try { cboEntryMode.SelectedItem = (EntryMode)Enum.Parse(typeof(EntryMode), ConfigurationSettings.AppSettings["EntryMode"]); }//[Keyed : TrackDataFromMSR ]
                catch { }
            }
            catch
            {
            }
            if (_bcs != null)
            {
                if (_strServiceID == "C82ED00001" || _strServiceID == "71C8700001" ||
                    _strServiceID == "88D9300001" || _strServiceID == "B447F00001" ||
                    _strServiceID == "D806000001" || _strServiceID == "E88FD00001")
                    showBCPExpandedFields();
                else if (_strServiceID == "168511300C" || _strServiceID == "9999999999")
                    showBCPExpandedFields();
                else
                {
                    showBCPFields();
                }
            }

            if (_ecks != null)
            {
                showECKFields();
            }
            if (_svas != null)
            {
                showSVAFields();
            }
        }

        private void ChkEnableAllMerchantFields_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkEnableAllMerchantFields.Checked)
            {
                showAllFields();
            }
            else
            {
                hideAllFields();
            }
        }

        #region ComboBox Events
        private void cboCountryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CountryCode = (TypeISOCountryCodeA3)cboCountryCode.SelectedItem;
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Language = (TypeISOLanguageCodeA3)cboLanguage.SelectedItem;
        }

        private void cboCurrencyCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CurrencyCode = (TypeISOCurrencyCodeA3)cboCurrencyCode.SelectedItem;
        }

        private void cboCustomerPresent_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CustomerPresent = (CustomerPresent)cboCustomerPresent.SelectedItem;
        }

        private void cboRequestACI_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RequestACI = (RequestACI)cboRequestACI.SelectedItem;
        }

        private void cboMerchantIndustryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _MerchantIndustryType = (IndustryType)cboMerchantIndustryType.SelectedItem;
        }

        private void cboEntryMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _EntryMode = (EntryMode)cboEntryMode.SelectedItem;
        }

        #endregion ComboBox Events

        #endregion END Form Events


    }
}
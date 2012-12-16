using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn;

namespace IPCommerce
{
	class Process
	{
		private HelperMethods Helper = new HelperMethods();
		private FaultHandler.FaultHandler _FaultHandler = new FaultHandler.FaultHandler();

		// Unique identifier used to obtain a applicationProfileId and is specific to the certified application
		// A new PTLS SocketId will be provided once your solution is approved for production.
			//NOTE : The following values changes from Sandbox to Production.  These values are provided by your Solution Consultant
			//Value provided by solution consultant. You'll have one idenityToken for Sandbox and a different one for production. The value needs 
			// to be configurable as the token expires every 3 years or if a security breach is detected.
		public string PtlsSocketId;

		private bool _blnPersistedConfigExists;
		private IndustryTypeValues IndustryValues;
		private MerchantProfile merchantProfile;

		public ServiceInformation _si;
		public List<string> MerchantProfileIds = new List<string>();
		private MerchantProfile _merP = new MerchantProfile();
		public BankcardService _bcs = new BankcardService();
		public ElectronicCheckingService _ecks = new ElectronicCheckingService();
		public StoredValueService _svas = new StoredValueService();


		public Process(string identityToken, string industryType)
		{
			if (identityToken.Length > 1)
			{
				Helper.ServiceKey = Helper.RetrieveServiceKeyFromIdentityToken(identityToken.Trim());
				Helper.IdentityToken = identityToken;
			}
			else
			{
				//TODO return or display an error
				return;
			}

			PtlsSocketId = ConfigurationManager.AppSettings["PtlsSocketId"];

			if (industryType.Length > 0)
			{
				if (industryType == "Ecommerce")
					IndustryValues = DataGenerator.getIndustryType("Ecommerce");
				if (industryType == "MOTO")
					IndustryValues = DataGenerator.getIndustryType("MOTO");
				if (industryType == "Retail")
					IndustryValues = DataGenerator.getIndustryType("Retail");
				if (industryType == "Restaurant")
					IndustryValues = DataGenerator.getIndustryType("Restaurant");
			}

		}


		private void cmdGo_Click(object sender, EventArgs e)
		{
			try
			{
				//Default setup values
				Helper.ApplicationProfileId = "";
				Helper.ServiceID = "";
				Helper.MerchantProfileId = "";

				//Set the Primary and Secondary Endpoints from the Config
				Helper.BaseSvcEndpointPrimary = ConfigurationManager.AppSettings["BaseSvcEndpointPrimary"];
				Helper.BaseSvcEndpointSecondary = ConfigurationManager.AppSettings["BaseSvcEndpointSecondary"];
				Helper.BaseTxnEndpointPrimary = ConfigurationManager.AppSettings["BaseTxnEndpointPrimary"];
				Helper.BaseTxnEndpointSecondary = ConfigurationManager.AppSettings["BaseTxnEndpointSecondary"];
				Helper.BaseTMSEndpointPrimary = ConfigurationManager.AppSettings["BaseDataServicesEndpointPrimary"];
				Helper.BaseTMSEndpointSecondary = ConfigurationManager.AppSettings["BaseDataServicesEndpointSecondary"];

				if (Helper.ServiceKey.Length > 0 && DataGenerator.LoadPersistedConfig())
				{
					_blnPersistedConfigExists = true;
					if (Helper.ApplicationProfileId.Length > 0 && Helper.ServiceID.Length > 0 && Helper.IdentityToken.Length > 0)
					{						
						// Since you've previously persisted ApplicationProfileId, MerchantProfileId and ServiceId go directly to 'Transaction Processing'
						//TODO Start Transaction Processing
						//tabControl1.SelectedTab = tbTransactionProcessing;
					}
					else
					{
						//TODO Prepare Application to Transact
						//tabControl1.SelectedTab = tbPreparingApplicationToTransact;
						//cmdSignOnWithToken.Enabled = true;
						return;
					}

					if (!Helper.SetSvcEndpoint()) MessageBox.Show("Unable to setup the service endpoint");
					if (!Helper.SetTxnEndpoint()) MessageBox.Show("Unable to setup the Transaction endpoint");
					if (!Helper.SetTMSEndpoint()) MessageBox.Show("Unable to setup the TMS endpoint");

					//Perform Step 1 as a previous configuration was already saved
					Helper.CheckTokenExpire();

					//Check to see if a previously saved ServiceID exists. 
					if (Helper.ServiceID.Length > 1)
					{
						//Perform Step 3 as a previous configuration was already saved
						GetServiceInformation();
					}
					if (Helper.MerchantProfileId.Length > 1 | Helper.MerchantProfileId == "")
					{
						//Perform Step 4 as a previous configuration was already saved
						GetMerchantProfileIds();
						GetMerchantProfile();
					}

				}
				else
				{
					//TODO Prepare Application to Transact
					//tabControl1.SelectedTab = tbPreparingApplicationToTransact;
					//cmdSignOnWithToken.Enabled = true;
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			finally
			{
			}
		}

		private void GetServiceInformation()
		{
			if (!_blnPersistedConfigExists)
				Helper.ServiceID = "";

			if (!_blnPersistedConfigExists)
				Helper.MerchantProfileId = "";

			//Reset previously selected services
			_bcs = null;
			_ecks = null;
			_svas = null;

			//The GetServiceInformation() operation provides information about the services that are available to a specific Service Key. 
			//This operation should be automatically invoked during initial application configuration, and manually by an application 
			//administrator if/when available services are updated.

			_si = Helper.Cwssic.GetServiceInformation(Helper.SessionToken);
		}

		public void GetMerchantProfileIds()
		{
			if (!_blnPersistedConfigExists)
				Helper.MerchantProfileId = "";

			/* A Merchant Profile is associated with a specific service id and tender type. For example, if there is a Bankcard 
			 * service available that supports both Credit and PIN Debit, a Merchant Profile is needed for both Credit and PIN Debit.
			 * The GetMerchantProfiles() operation retrieves all Merchant Profiles for a specific service and tender type.
			*/
			List<string> MerchantProfileIds = Helper.Cwssic.GetMerchantProfileIds(Helper.SessionToken, Helper.ServiceID, TenderType.Credit);
		}

		private void GetMerchantProfile()
		{
			if (Helper.MerchantProfileId.Length < 1)
			{
				MessageBox.Show("Please select a merchant profileId");
				return;
			}

			Helper.CheckTokenExpire();
			string _strServiceID = Helper.ServiceID;
			string _strSessionToken = Helper.SessionToken;


			merchantProfile =
				Helper.Cwssic.GetMerchantProfile(_strSessionToken, Helper.MerchantProfileId, _strServiceID, TenderType.Credit);
		}



		/*The second step in preparing the application to transact is to retrieve, update (if necessary), and save the appropriate 
		  * characteristics and configuration information associated with your payment-enabled application. This is a one-time event 
		  * that should be performed upon initial installation or launch of the application.
              
		  * SECURITY CONSIDERATIONS
		  * Stored on file system with read/write permission for only the application/service and IT Administration
		  * Stored in DB with read/write permission for only the application/service and IT Administration
		*/

		//TODO Add this to the Supervisor Admin payment set up tab

		public void GetApplicationData()
		{
			PtlsSocketId = ConfigurationManager.AppSettings["PtlsSocketId"];


			if (Helper.ApplicationProfileId.Length < 1) return;

			//Call GetApplicationData if a previous applicationProfileId exists
			ApplicationData AD = new ApplicationData();
			//From the calling form
			string _strSessionToken = Helper.SessionToken;
			try
			{
				Helper.CheckTokenExpire();
				AD = Helper.Cwssic.GetApplicationData(_strSessionToken, Helper.ApplicationProfileId);
			}
			catch
			{
				MessageBox.Show("Unable to pull application data for persisted ApplicationProfileId in the file '[SK]_applicationProfileId.config'");
				return;
			}

			SaveApplicationData();
		}

		private void SaveApplicationData()
		{
			if (Helper.ApplicationProfileId.Length > 0)
			{
				DialogResult Result;
				Result = MessageBox.Show(
					"The following will attempt to overwrite an existing ApplicationProfileId. Do you want to continue?",
					"Overwrite", MessageBoxButtons.OKCancel);
				if (Result == DialogResult.Cancel) return;
			}

			try
			{
				ApplicationData AD = new ApplicationData();

				string strApplicationProfileId = Helper.Cwssic.SaveApplicationData(Helper.SessionToken, AD);
				Helper.ApplicationProfileId = strApplicationProfileId.Trim();
				MessageBox.Show(
						"ApplicationData successfully saved. Your application should persist and cache the ApplicationProfileId returned. "
						+ "This ApplicationProfileID will be used for all subsequent transaction processing and does not require a re-saving of application data in the future. "
						+ "\r\n\r\nFor now, the values have been saved in a text file, which is located"
						+ " in the same folder as the executing application '[SK]_applicationProfileId.config'");
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

		private void DeleteApplicationData()
		{
			if (Helper.ApplicationProfileId.Length < 1) { MessageBox.Show("Please enter a valid ApplicationProfileId in order to delete the ApplicationData"); return; }

			//From the calling form
			Helper.CheckTokenExpire();
			string _strSessionToken = Helper.SessionToken;
			try
			{
				Helper.Cwssic.DeleteApplicationData(_strSessionToken,
																					   Helper.ApplicationProfileId);
				MessageBox.Show("Successfully deleted " + Helper.ApplicationProfileId);
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

		public void AuthorizeAndCapture(BankCardProProcessingOptions BCPPO)
		{
			//The AuthorizeAndCapture() operation is used to authorize and capture a transaction in a single invocation.

			//Check to see if this transaction type is supported
			if (!_bcs.Operations.AuthAndCapture) { MessageBox.Show("AuthAndCapture Not Supported"); return; }

			//if (_bcs != null)
			//{
			//    if (!chkProcessAsPINDebitTxn.Checked &&
			//        _bcs.Tenders.CreditAuthorizeSupport == CreditAuthorizeSupportType.Both)
			//    {
			//        if (_bcs.Tenders.CreditAuthorizeSupport == CreditAuthorizeSupportType.AuthorizeOnly)
			//        {
			//            MessageBox.Show("This service only support AuthorizeOnly for Credit transactions");
			//            Cursor = Cursors.Default;
			//            return;
			//        }
			//        if (_bcs.Tenders.CreditAuthorizeSupport == CreditAuthorizeSupportType.AuthorizeAndCaptureOnly)
			//        {
			//            MessageBox.Show("This service only support AuthorizeAndCaptureOnly for Credit transactions");
			//            Cursor = Cursors.Default;
			//            return;
			//        }
			//    }

				try
				{
					BankcardTransaction BCtransaction = DataGenerator.SetBankCardTxnData(BCPPO);
					processResponse(Helper.ProcessBCPTransaction(TransactionType.AuthorizeAndCapture, BCtransaction, null, null, null,
												 null, null, null, null, false,
												 false));
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
				}
//			}
			//else if (_ecks != null) //Process as a Check transaction
			//{
			//    try
			//    {
			//        MessageBox.Show(@"Placeholder for ECK code. Please ask your solution consultant for an example");
			//    }
			//    catch (Exception ex)
			//    {
			//        MessageBox.Show(ex.Message);
			//    }
			//    finally
			//    {
			//        Cursor = Cursors.Default;
			//    }
			//}
			//else if (_svas != null) //Process as a Check transaction
			//{
			//    try
			//    {
			//        StoredValueTransaction SVATransaction = DataGenerator.SetStoredValueTxnData();
			//        //Let's Query a transaction
			//        processResponse(Helper.ProcessSVATransaction(TransactionType.Authorize, SVATransaction, null, null, null, null, ChkAcknowledge.Checked));
			//    }
			//    catch (Exception ex)
			//    {
			//        MessageBox.Show(ex.Message);
			//    }
			//    finally
			//    {
			//        Cursor = Cursors.Default;
			//    }
			//}
		}

		private void processResponse(List<ResponseDetails> _RD)
		{
			if (_RD != null && _RD.Count > 0)
			{
				foreach (ResponseDetails rd in _RD)
				{
//					ChkLstTransactionsProcessed.Items.Add(rd);

					try
					{
						if (rd.Response.Status == Status.Successful && (rd.TransactionType == "Authorize" | rd.TransactionType == "AuthorizeAndCapture"))
						{
							BankcardTransactionResponse BTR = (BankcardTransactionResponse)rd.Response;
							if (BTR.PaymentAccountDataToken.Length > 0)
							{
								if (BTR.MaskedPAN.Length < 1)
									BTR.MaskedPAN = rd.MaskedPan;
								if (BTR.CardType == TypeCardType.NotSet)
									BTR.CardType = rd.CardType;

								//Your logic may be different
								//bool match = false;
								//foreach (TokenizedTransaction t in CboTokenizedCard.Items)
								//{
								//    if (t.MaskedPAN == BTR.MaskedPAN && t.CardType == BTR.CardType)
								//        match = true;
								//}
								////only add if this is a new card
								//if (!match)
								//    CboTokenizedCard.Items.Add(new TokenizedTransaction(BTR.PaymentAccountDataToken, TxtExpirationDate.Text, BTR.MaskedPAN, BTR.CardType));
							}

						}
//						CboTokenizedCard.SelectedIndex = -1;
					}
					catch { }
				}
				//Uncheck all boxes
//				for (int i = 0; i < ChkLstTransactionsProcessed.Items.Count; ++i)
//					ChkLstTransactionsProcessed.SetItemChecked(i, false);
			}
		}



	}
}

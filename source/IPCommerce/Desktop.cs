using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo;

namespace IPCommerce
{
	class Desktop
	{
		#region Variable Declarations
		public HelperMethods Helper = new HelperMethods();//The following class performs many of the different operations needs for service information and trasaction processing
//		private string _authorizeTxn = "";//Used for a Capture() transaction type.
//		private ElectronicCheckingTransactionResponse _queryAccountTxn;//Used for a check Authorize() transaction.
//		private StoredValueTransactionResponse _queryAccountSVATxn;//Used for a SVA Authorize() transaction.
//		private string _checkTxn = "";//Used as an approved check transaction for either Undo or CaptureAll
		// Unique identifier used to obtain a applicationProfileId and is specific to the certified application
		// A new PTLS SocketId will be provided once your solution is approved for production.
		public string PtlsSocketId = "MIIEwjCCA6qgAwIBAgIBEjANBgkqhkiG9w0BAQUFADCBsTE0MDIGA1UEAxMrSVAgUGF5bWVudHMgRnJhbWV3b3JrIENlcnRpZmljYXRlIEF1dGhvcml0eTELMAkGA1UEBhMCVVMxETAPBgNVBAgTCENvbG9yYWRvMQ8wDQYDVQQHEwZEZW52ZXIxGjAYBgNVBAoTEUlQIENvbW1lcmNlLCBJbmMuMSwwKgYJKoZIhvcNAQkBFh1hZG1pbkBpcHBheW1lbnRzZnJhbWV3b3JrLmNvbTAeFw0wNjEyMTUxNzQyNDVaFw0xNjEyMTIxNzQyNDVaMIHAMQswCQYDVQQGEwJVUzERMA8GA1UECBMIQ29sb3JhZG8xDzANBgNVBAcTBkRlbnZlcjEeMBwGA1UEChMVSVAgUGF5bWVudHMgRnJhbWV3b3JrMT0wOwYDVQQDEzRFcWJwR0crZi8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vLy8vL0E9MS4wLAYJKoZIhvcNAQkBFh9zdXBwb3J0QGlwcGF5bWVudHNmcmFtZXdvcmsuY29tMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQD7BTLqXah9t6g4W2pJUfFKxJj/R+c1Dt5MCMYGKeJCMvimAJOoFQx6Cg/OO12gSSipAy1eumAqClxxpR6QRqO3iv9HUoREq+xIvORxm5FMVLcOv/oV53JctN2fwU2xMLqnconD0+7LJYZ+JT4z3hY0mn+4SFQ3tB753nqc5ZRuqQIDAQABo4IBVjCCAVIwCQYDVR0TBAIwADAdBgNVHQ4EFgQUk7zYAajw24mLvtPv7KnMOzdsJuEwgeYGA1UdIwSB3jCB24AU3+ASnJQimuunAZqQDgNcnO2HuHShgbekgbQwgbExNDAyBgNVBAMTK0lQIFBheW1lbnRzIEZyYW1ld29yayBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkxCzAJBgNVBAYTAlVTMREwDwYDVQQIEwhDb2xvcmFkbzEPMA0GA1UEBxMGRGVudmVyMRowGAYDVQQKExFJUCBDb21tZXJjZSwgSW5jLjEsMCoGCSqGSIb3DQEJARYdYWRtaW5AaXBwYXltZW50c2ZyYW1ld29yay5jb22CCQD/yDY5hYVsVzA9BglghkgBhvhCAQQEMBYuaHR0cHM6Ly93d3cuaXBwYXltZW50c2ZyYW1ld29yay5jb20vY2EtY3JsLnBlbTANBgkqhkiG9w0BAQUFAAOCAQEAFk/WbEleeGurR+FE4p2TiSYHMau+e2Tgi+L/oNgIDyvAatgosk0TdSndvtf9YKjCZEaDdvWmWyEMfirb5mtlNnbZz6hNpYoha4Y4ThrEcCsVhfHLLhGZZ1YaBD+ZzCQA7vtb0v5aQb25jX262yPVshO+62DPxnMiJevSGFUTjnNisVniX23NVouUwR3n12GO8wvzXF8IYb5yogaUcVzsTIxEFQXEo1PhQF7JavEnDksVnLoRf897HwBqcdSs0o2Fpc/GN1dgANkfIBfm8E9xpy7k1O4MuaDRqq5XR/4EomD8BWQepfJY0fg8zkCfkuPeGjKkDCitVd3bhjfLSgTvDg==";//Used when saving Applicationdata. This field should be compiled into your code. You will receive one PTLSSocketId per application you certify.

		//Service Information variables
		public Operations SupportedTxnTypes = new Operations();
		public ServiceInformation _si;
		public List<string> MerchantProfileIds = new List<string>();
		private MerchantProfile _merP = new MerchantProfile();
		public BankcardService _bcs = new BankcardService();
		public ElectronicCheckingService _ecks = new ElectronicCheckingService();
		public StoredValueService _svas = new StoredValueService();

		private FaultHandler.FaultHandler _FaultHandler = new FaultHandler.FaultHandler();
		public bool _blnEncryptedIdentityToken;//Flag used to determine if the identity token is encrypted or not. Recommendation is to alway encrypt.
		private bool _blnPersistedConfigExists;//Flag used to determine if a previous config exists.

		#endregion Variable Declarations

		public string TxtAmount;
		public string TxtTip;
		public string TxtCashBack;
		public string TxtExpirationDate;
		public string TxtPAN;
		public string TxtTrackDataFromMSR;
		public string txtPersistedAndCached;

		public bool chkProcessAsPINDebitTxn;
		public bool ChkAllowPartialApprovals;
		public bool ChkCardNotPresent;
		public bool ChkTokenization;
		public bool ChkTrackDataFromMSR;
		public bool ChkProcessAsPINLessDebit;
		public bool chkStep2;

		public TokenizedTransaction CboTokenizedCard;
		public TypeCardType CboCardTypes;

	}
}

using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Windows.Forms;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices;

namespace SampleCode
{
    public partial class TransactionManagementServices : Form
    {
        private FaultHandler.FaultHandler _FaultHandler = new FaultHandler.FaultHandler();
        private int _intCurrentPage = 0; //Zero based so 0 is page 1
        private int _intResultsPerPage = 50; //The upperlimit is 50.

        private int _intCurrentPageResults = 0; //Zero based so 0 is page 1
        private int _intResultsPerPageResults = 50; //The upperlimit is 50.

        private LastSearchType _lastSearch;

        public TransactionManagementServices()
        {
            InitializeComponent();

            cboQTP_CaptureStates.Sorted = true;
            cboQTP_CaptureStates.DataSource = Enum.GetValues(typeof(CaptureState));
            cboQTP_CaptureStates.SelectedIndex = -1;

            cboQTP_CardTypes.Sorted = true;
            cboQTP_CardTypes.DataSource = Enum.GetValues(typeof(TypeCardType));
            cboQTP_CardTypes.SelectedIndex = -1;

            //Populate drop downs with enumerated values
            cboQTP_IsAcknowledged.Items.Add(true);
            cboQTP_IsAcknowledged.Items.Add(false);

            cboQTP_QueryType.Sorted = true;
            cboQTP_QueryType.DataSource = Enum.GetValues(typeof(QueryType));
            cboQTP_QueryType.SelectedIndex = -1;

            cboQTP_TransactionStates.Sorted = true;
            cboQTP_TransactionStates.DataSource = Enum.GetValues(typeof(TransactionState));
            cboQTP_TransactionStates.SelectedIndex = -1;

            //Format the dateTimePicker for TMS queries
            dtpStartTimeTMS.Format = DateTimePickerFormat.Custom;
            dtpStartTimeTMS.CustomFormat = "dddd MM'/'dd'/'yyyy hh':'mm tt";
            dtpStartTimeTMS.Value = DateTime.Now.AddHours(-2);

            dtpEndTimeTMS.Format = DateTimePickerFormat.Custom;
            dtpEndTimeTMS.CustomFormat = "dddd MM'/'dd'/'yyyy hh':'mm tt";
            dtpEndTimeTMS.Value = DateTime.Now;
        }

        private void TransactionManagementServices_Load(object sender, EventArgs e)
        {
            //Load Service and Merchant values from ServiceInformation
            if (((SampleCode_DeskTop)(Owner)).cboAvailableServices.Items.Count > 0)
            {
                foreach (var item in ((SampleCode_DeskTop)(Owner)).cboAvailableServices.Items)
                {
                    CboQTP_ServiceIds.Items.Add(((SampleCode.SampleCode_DeskTop.Item)(item)).Value1);
                }
                
            }
            if (((SampleCode_DeskTop)(Owner)).cboAvailableProfiles.Items.Count > 0)
            {
                foreach (var item in ((SampleCode_DeskTop)(Owner)).cboAvailableProfiles.Items)
                {
                    CboQTP_MerchantProfileIds.Items.Add(item);
                    CboQBP_MercProfileIds.Items.Add(item);
                }
            }
        }

        private void cmdQueryTransactionsSummary_Click(object sender, EventArgs e)
        {
            ResetPreviousNext();
            if (_lastSearch != LastSearchType.QueryTransactionsSummary) { ResetPreviousNext(); _lastSearch = LastSearchType.QueryTransactionsSummary; }
            QueryTransactionsSummary();
        }

        private void cmdQueryTransactionFamilies_Click(object sender, EventArgs e)
        {
            if (txtQTP_TransactionIds.Text.Length < 1 && !chkUseTransactionIdSelected.Checked) { MessageBox.Show("At Lease one TransactionId is necessary in Query Transaction Parameters"); Cursor = Cursors.Default; return; }
            ResetPreviousNext();
            if (_lastSearch != LastSearchType.QueryTransactionFamilies) { ResetPreviousNext(); _lastSearch = LastSearchType.QueryTransactionFamilies; }
            QueryTransactionFamilies();
        }

        private void cmdQueryTransactionsDetail_Click(object sender, EventArgs e)
        {
            if (txtQTP_TransactionIds.Text.Length < 1 && !chkUseTransactionIdSelected.Checked) { MessageBox.Show("At Lease one TransactionId is necessary in Query Transaction Parameters"); Cursor = Cursors.Default; return; }
            ResetPreviousNext();
            if (_lastSearch != LastSearchType.QueryTransactionsDetail) { ResetPreviousNext(); _lastSearch = LastSearchType.QueryTransactionsDetail; }
            QueryTransactionsDetail();
        }

        private void cmdQueryBatch_Click(object sender, EventArgs e)
        {
            ResetPreviousNext();
            if (_lastSearch != LastSearchType.QueryBatch) { ResetPreviousNext(); _lastSearch = LastSearchType.QueryBatch; }
            QueryBatch();
        }

        private void QueryTransactionsSummary()
        {//https://my.ipcommerce.com/Docs/DataServices/TMS_Developer_Guide/2.0.17/Implementation/SOAP/QueryTransactionsSummary.aspx

            bool BlnIncludeRelated = chkIncludeRelated.Checked;
            
            try
            {
                ((SampleCode_DeskTop)(Owner)).Helper.CheckTokenExpire();//Make sure the current token is valid
                txtTMSResults.Text = "";

                Cursor = Cursors.WaitCursor;
                ProcessQueryTransactionSummaryResponse(((SampleCode_DeskTop)(Owner)).Helper.Tmsoc.QueryTransactionsSummary(((SampleCode_DeskTop)(Owner)).Helper.SessionToken, QTP(), BlnIncludeRelated, PP())); ;
                
                txtTMSResults.Text = "Last Transaction Summary Search : " + DateTime.Now;

                Cursor = Cursors.Default;
            }
            catch (EndpointNotFoundException)
            {
                //In this case the SvcEndpoint was not available. Try the same logic again with the alternate Endpoint
                try
                {
                    ((SampleCode_DeskTop)(Owner)).Helper.SetTMSEndpoint();//Change the endpoint to use the backup.

                    ProcessQueryTransactionSummaryResponse(((SampleCode_DeskTop)(Owner)).Helper.Tmsoc.QueryTransactionsSummary(((SampleCode_DeskTop)(Owner)).Helper.SessionToken, QTP(), BlnIncludeRelated, PP())); ;
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(
                        "Neither the primary or secondary TMS endpoints are available. Unable to process.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Query Transactions Summary\r\nError Message : " + ex.Message, "Query Transactions Summary Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                string strErrorId;
                string strErrorMessage;
                if (_FaultHandler.handleTMSFault(ex, out strErrorId, out strErrorMessage))
                    MessageBox.Show(strErrorId + " : " + strErrorMessage);
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void QueryTransactionFamilies()
        {
            //https://my.ipcommerce.com/Docs/DataServices/TMS_Developer_Guide/2.0.17/Implementation/SOAP/QueryTransactionFamilies.aspx

            try
            {
                ((SampleCode_DeskTop)(Owner)).Helper.CheckTokenExpire();//Make sure the current token is valid
                txtTMSResults.Text = "";

                Cursor = Cursors.WaitCursor;
                ProcessQueryTransactionFamiliesResponse(((SampleCode_DeskTop)(Owner)).Helper.Tmsoc.QueryTransactionFamilies(((SampleCode_DeskTop)(Owner)).Helper.SessionToken, QTP(), PP()));

                Cursor = Cursors.Default;
            }
            catch (EndpointNotFoundException)
            {
                //In this case the SvcEndpoint was not available. Try the same logic again with the alternate Endpoint
                try
                {
                    ((SampleCode_DeskTop)(Owner)).Helper.SetTMSEndpoint();//Change the endpoint to use the backup.

                    ProcessQueryTransactionFamiliesResponse(((SampleCode_DeskTop)(Owner)).Helper.Tmsoc.QueryTransactionFamilies(((SampleCode_DeskTop)(Owner)).Helper.SessionToken, QTP(), PP()));
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(
                        "Neither the primary or secondary TMS endpoints are available. Unable to process.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Query Transaction Families\r\nError Message : " + ex.Message, "Query Transaction Families Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                string strErrorId;
                string strErrorMessage;
                if (_FaultHandler.handleTMSFault(ex, out strErrorId, out strErrorMessage))
                    MessageBox.Show(strErrorId + " : " + strErrorMessage);
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void QueryTransactionsDetail()
        {
            //https://my.ipcommerce.com/Docs/DataServices/TMS_Developer_Guide/2.0.17/Implementation/SOAP/QueryTransactionsDetail.aspx

            bool BlnIncludeRelated = chkIncludeRelated.Checked;
            try
            {
                ((SampleCode_DeskTop)(Owner)).Helper.CheckTokenExpire();//Make sure the current token is valid
                txtTMSResults.Text = "";

                Cursor = Cursors.WaitCursor;

                TransactionDetailFormat TDF = new TransactionDetailFormat();
                ProcessQueryTransactionsDetailResponse(((SampleCode_DeskTop)(Owner)).Helper.Tmsoc.QueryTransactionsDetail(((SampleCode_DeskTop)(Owner)).Helper.SessionToken, QTP(), TDF, BlnIncludeRelated, PP())); ;

                if (txtTMSResults.Text.Length < 1)
                    txtTMSResults.Text = "No Query Transaction Detail Results : " + DateTime.Now;
                Cursor = Cursors.Default;
            }
            catch (EndpointNotFoundException)
            {
                //In this case the SvcEndpoint was not available. Try the same logic again with the alternate Endpoint
                try
                {
                    ((SampleCode_DeskTop)(Owner)).Helper.SetTMSEndpoint();//Change the endpoint to use the backup.

                    TransactionDetailFormat TDF = new TransactionDetailFormat();
                    ProcessQueryTransactionsDetailResponse(((SampleCode_DeskTop)(Owner)).Helper.Tmsoc.QueryTransactionsDetail(((SampleCode_DeskTop)(Owner)).Helper.SessionToken, QTP(), TDF, BlnIncludeRelated, PP()));
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(
                        "Neither the primary or secondary TMS endpoints are available. Unable to process.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to Query Transaction Families\r\nError Message : " + ex.Message, "Query Transaction Families Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                string strErrorId;
                string strErrorMessage;
                if (_FaultHandler.handleTMSFault(ex, out strErrorId, out strErrorMessage))
                    MessageBox.Show(strErrorId + " : " + strErrorMessage);
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void QueryBatch()
        {
            //https://my.ipcommerce.com/Docs/DataServices/TMS_Developer_Guide/2.0.17/Implementation/SOAP/QueryBatch.aspx

            try
            {
                ((SampleCode_DeskTop)(Owner)).Helper.CheckTokenExpire();//Make sure the current token is valid
                txtTMSResults.Text = "";

                Cursor = Cursors.WaitCursor;
                ProcessQueryBatchResponse(((SampleCode_DeskTop)(Owner)).Helper.Tmsoc.QueryBatch(((SampleCode_DeskTop)(Owner)).Helper.SessionToken, QBP(), PP()));

                if (txtTMSResults.Text.Length < 1)
                    txtTMSResults.Text = "No Query Batch Results : " + DateTime.Now;
                Cursor = Cursors.Default;
            }
            catch (EndpointNotFoundException)
            {
                //In this case the SvcEndpoint was not available. Try the same logic again with the alternate Endpoint
                try
                {
                    ((SampleCode_DeskTop)(Owner)).Helper.SetTMSEndpoint();//Change the endpoint to use the backup.

                   ProcessQueryBatchResponse(((SampleCode_DeskTop)(Owner)).Helper.Tmsoc.QueryBatch(((SampleCode_DeskTop)(Owner)).Helper.SessionToken, QBP(), PP()));
                }
                catch (EndpointNotFoundException)
                {
                    MessageBox.Show(
                        "Neither the primary or secondary TMS endpoints are available. Unable to process.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to QueryBatch\r\nError Message : " + ex.Message, "QueryBatch Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (WebException eW)
            {
                MessageBox.Show(eW.Message);
                //e.Status;
                //((HttpWebResponse) e.Response).StatusCode;
            }
            catch (Exception ex)
            {
                string strErrorId;
                string strErrorMessage;
                if (_FaultHandler.handleTMSFault(ex, out strErrorId, out strErrorMessage))
                    MessageBox.Show(strErrorId + " : " + strErrorMessage);
                else
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
  
        private void ResetPreviousNext()
        {
            LnkPrevious.Visible = false;
            LnkNext.Visible = false;
            lblPageNumber.Visible = false;
            _intCurrentPage = 0; //Zero based so 0 is page 1
        }

        private void LnkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _intCurrentPage++;
            if(_lastSearch == LastSearchType.QueryTransactionsSummary)QueryTransactionsSummary();
            //Since data is saved to the results textbox the following do not apply
            //if (_lst == LastSearchType.QueryTransactionFamilies) QueryTransactionFamilies();
            //if (_lst == LastSearchType.QueryTransactionsDetail) QueryTransactionsDetail();
            //if (_lst == LastSearchType.QueryBatch) QueryBatch();
        }

        private void LnkPrevious_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _intCurrentPage--;
            if (_lastSearch == LastSearchType.QueryTransactionsSummary) QueryTransactionsSummary();
            //Since data is saved to the results textbox the following do not apply
            //if (_lst == LastSearchType.QueryTransactionFamilies) QueryTransactionFamilies();
            //if (_lst == LastSearchType.QueryTransactionsDetail) QueryTransactionsDetail();
            //if (_lst == LastSearchType.QueryBatch) QueryBatch();
        }

        private QueryBatchParameters QBP()
        {
            QueryBatchParameters QBP = new QueryBatchParameters();
            char[] splitter = { ',' };
            ////Specify batch paramaters
            QBP.BatchDateRange = new DateRange();
            QBP.BatchDateRange.StartDateTime = dtpStartTimeTMS.Value.ToUniversalTime();
            QBP.BatchDateRange.EndDateTime = dtpEndTimeTMS.Value.ToUniversalTime();

            if (txtQBP_BatchIds.Text.Length > 0)
                QBP.BatchIds = new List<string>(txtQBP_BatchIds.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));
            if (CboQBP_MercProfileIds.Text.Length > 0)
                QBP.MerchantProfileIds = new List<string>(CboQBP_MercProfileIds.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));
            if (txtQBP_ServiceKeys.Text.Length > 0)
                QBP.ServiceKeys = new List<string>(txtQBP_ServiceKeys.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));

            List<string> txnIds = new List<string>();
            if (txtQBP_TransactionIds.Text.Length > 0)
            {
                //First Process any transactionId's in the text box
                txnIds = new List<string>(txtQBP_TransactionIds.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));
            }
            //Now check to see if any are listed in the Check List.
            if (chkUseTransactionIdSelected.Checked && chklstTMSResults.CheckedItems.Count > 0)
            {
                //Iterate through the checked list box and add transactionId(s)
                foreach (object itemChecked in chklstTMSResults.CheckedItems)
                {
                    if (itemChecked.GetType().ToString() == "SampleCode.SummaryDetailVal")
                    {
                        txtQBP_TransactionIds.Text = txtQBP_TransactionIds.Text + ((SummaryDetailVal)(itemChecked)).SD.TransactionInformation.TransactionId + ", ";
                        txnIds.Add(((SummaryDetailVal)(itemChecked)).SD.TransactionInformation.TransactionId);
                    }

                    if (itemChecked.GetType().ToString() == "SampleCode.TransactionDetailVal")
                    {
                        txtQBP_TransactionIds.Text = txtQBP_TransactionIds.Text + ((TransactionDetailVal)(itemChecked)).TD.TransactionInformation.TransactionId + ", ";
                        txnIds.Add(((TransactionDetailVal)(itemChecked)).TD.TransactionInformation.TransactionId);
                    }                 
                }
            }
            //Note : Only set transactionId's if one or more actually exist. Otherwise the result set will be empty
            if(txnIds.Count >0)
                QBP.TransactionIds = txnIds;

            return QBP;
        }

        private QueryTransactionsParameters QTP()
        {
            QueryTransactionsParameters QTP = new QueryTransactionsParameters();
            char[] splitter = { ',' };

            if (txtQTP_Amounts.Text.Length > 0)
            {
                List<decimal> Amt = new List<decimal>();
                string[] values = txtQTP_Amounts.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in values)
                {
                    try
                    {
                        decimal d = Convert.ToDecimal(s);
                        d = decimal.Parse(d.ToString("0.00"));//Amounts must be in format N.NN
                        Amt.Add(d);
                    }
                    catch { }
                }
                QTP.Amounts = Amt;
            }

            if (txtQTP_ApprovalCodes.Text.Length > 0)
                QTP.ApprovalCodes = new List<string>(txtQTP_ApprovalCodes.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));
            if (txtQTP_BatchIds.Text.Length > 0)
                QTP.BatchIds = new List<string>(txtQTP_BatchIds.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));
            if (txtQTP_OrderNumber.Text.Length > 0)
                QTP.OrderNumbers = new List<string>(txtQTP_OrderNumber.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));

            //ToDo : add logic
            //QTP.CaptureDateRange = new DateRange();
            //QTP.CaptureDateRange.StartDateTime = dtpStartTimeTMS.Value.ToUniversalTime();
            //QTP.CaptureDateRange.EndDateTime = dtpEndTimeTMS.Value.ToUniversalTime();

            if (cboQTP_CaptureStates.Text.Length > 0)
            {
                List<CaptureState> TS = new List<CaptureState>();
                TS.Add((CaptureState)cboQTP_CaptureStates.SelectedItem);
                QTP.CaptureStates = TS;
            }

            if (cboQTP_CardTypes.Text.Length > 0)
            {
                List<TypeCardType> TS = new List<TypeCardType>();
                TS.Add((TypeCardType)cboQTP_CardTypes.SelectedItem);
                QTP.CardTypes = TS;
            }

            if (cboQTP_IsAcknowledged.Text.Length > 0)
            {
                if ((bool)cboQTP_IsAcknowledged.SelectedItem)
                    QTP.IsAcknowledged = BooleanParameter.True;
                else
                    QTP.IsAcknowledged = BooleanParameter.False;
            }

            if (CboQTP_MerchantProfileIds.Text.Length > 0)
                QTP.MerchantProfileIds = new List<string>(CboQTP_MerchantProfileIds.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));

            if (cboQTP_QueryType.Text.Length > 0)
                QTP.QueryType = (QueryType)cboQTP_QueryType.SelectedItem;

            if (CboQTP_ServiceIds.Text.Length > 0)
                QTP.ServiceIds = new List<string>(CboQTP_ServiceIds.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));

            if (txtQTP_ServiceKeys.Text.Length > 0)
                QTP.ServiceKeys = new List<string>(txtQTP_ServiceKeys.Text.Split(splitter, StringSplitOptions.RemoveEmptyEntries));

            //ToDo : add logic
            if (txtQTP_TransactionClassTypePairs.Text.Length > 0)
            {
                List<TransactionClassTypePair> TCTP = new List<TransactionClassTypePair>();
                TCTP.Add(new TransactionClassTypePair());
                QTP.TransactionClassTypePairs = TCTP;
            }

            ////Specify batch paramaters
            QTP.TransactionDateRange = new DateRange();
            QTP.TransactionDateRange.StartDateTime = dtpStartTimeTMS.Value.ToUniversalTime();
            QTP.TransactionDateRange.EndDateTime = dtpEndTimeTMS.Value.ToUniversalTime();


            List<string> txnIds = new List<string>();
            if (txtQTP_TransactionIds.Text.Length > 0)
            {
                //First Process any transactionId's in the text box
                txnIds = new List<string>(txtQTP_TransactionIds.Text.Replace(" ", "").Split(splitter, StringSplitOptions.RemoveEmptyEntries));
            }
            //Now check to see if any are listed in the Check List.
            if(chkUseTransactionIdSelected.Checked && chklstTMSResults.CheckedItems.Count > 0)
            {
                //Iterate through the checked list box and add transactionId(s)
                foreach (object itemChecked in chklstTMSResults.CheckedItems)
                {
                    if (itemChecked.GetType().ToString() == "SampleCode.SummaryDetailVal")
                    {
                        txtQTP_TransactionIds.Text = txtQTP_TransactionIds.Text + ((SummaryDetailVal)(itemChecked)).SD.TransactionInformation.TransactionId + ", ";
                        txnIds.Add(((SummaryDetailVal)(itemChecked)).SD.TransactionInformation.TransactionId);
                    }
                    
                    if (itemChecked.GetType().ToString() == "SampleCode.TransactionDetailVal")
                    {
                        txtQTP_TransactionIds.Text = txtQTP_TransactionIds.Text + ((TransactionDetailVal)(itemChecked)).TD.TransactionInformation.TransactionId + ", ";
                        txnIds.Add(((TransactionDetailVal)(itemChecked)).TD.TransactionInformation.TransactionId);
                    }

                    //if (itemChecked.GetType().ToString() == "SampleCode.FamilyDetailVal")
                    //    txnIds.Add(((FamilyDetailVal)(itemChecked)).FD.TransactionIds);
                    
                    //if (itemChecked.GetType().ToString() == "SampleCode.BatchDetailDataVal")
                    //    txnIds.Add(((BatchDetailDataVal)(itemChecked)).BDD.TransactionIds);                   
                }
            }
            //Note : Only set transactionId's if one or more actually exist. Otherwise the result set will be empty
            if (txnIds.Count > 0)
                QTP.TransactionIds = txnIds;
            
            if (cboQTP_TransactionStates.Text.Length > 0)
            {
                List<TransactionState> TS = new List<TransactionState>();
                TS.Add((TransactionState)cboQTP_TransactionStates.SelectedItem);
                QTP.TransactionStates = TS;
            }

            return QTP;
        }

        private PagingParameters PP()
        {
            //Specify Paging Parameters
            PagingParameters PP = new PagingParameters();

            if (_lastSearch == LastSearchType.QueryTransactionsSummary)
            {
                PP.Page = _intCurrentPage;
                PP.PageSize = _intResultsPerPage;
            }

            if (_lastSearch == LastSearchType.QueryTransactionFamilies 
                | _lastSearch == LastSearchType.QueryTransactionsDetail 
                | _lastSearch == LastSearchType.QueryBatch) 
            {
                PP.Page = _intCurrentPageResults;
                PP.PageSize = _intResultsPerPageResults;
            }

            return PP;
        }

        private void txtTMSResults_KeyDown(object sender, KeyEventArgs e)
        {
            // See if Ctrl-A is pressed... 
            if (e.Control && (e.KeyCode == Keys.A))
            {
                txtTMSResults.SelectAll();
                e.Handled = true;
            }
        }

        private void chklstTMSResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Process SummaryDetail response
            if (_lastSearch == LastSearchType.QueryTransactionsSummary)
            {
                if (chklstTMSResults.SelectedItem == null) return;
                SummaryDetail s = ((SummaryDetailVal)(chklstTMSResults.SelectedItem)).SD;
                txtTMSResults.Text = SummaryDetailString(s);
            }
            //Process FamilyDetail response
            if (_lastSearch == LastSearchType.QueryTransactionFamilies)
            {
                if (chklstTMSResults.SelectedItem == null) return;
                FamilyDetail f = ((FamilyDetailVal)(chklstTMSResults.SelectedItem)).FD;
                txtTMSResults.Text = FamilyDetailString(f);
            }
            //Process BatchDetailData response
            if (_lastSearch == LastSearchType.QueryBatch)
            {
                if (chklstTMSResults.SelectedItem == null) return;
                BatchDetailData b = ((BatchDetailDataVal)(chklstTMSResults.SelectedItem)).BDD;
                txtTMSResults.Text = BatchDetailDataString(b);
            }
            //Process TransactionDetail response
            if (_lastSearch == LastSearchType.QueryTransactionsDetail)
            {
                if (chklstTMSResults.SelectedItem == null) return;
                TransactionDetail t = ((TransactionDetailVal)(chklstTMSResults.SelectedItem)).TD;
                txtTMSResults.Text = TransactionDetailString(t);
            }
        }

        #region process TMS response

        private void ProcessQueryBatchResponse(List<BatchDetailData> _BDD)
        {
            chklstTMSResults.Items.Clear();

            //Check for the need of paging
            if (_BDD.Count > _intResultsPerPage - 1)
            {
                lblPageNumber.Text = @"Page : " + (_intCurrentPage + 1);
                lblPageNumber.Visible = true;
                LnkNext.Visible = true;
                LnkPrevious.Visible = (_intCurrentPage > 0 ? true : false);//Enable previous on page 2
            }
            else
            {
                lblPageNumber.Text = @"Page : " + (_intCurrentPage + 1);
                LnkNext.Visible = false;//End of the list so disable the Next link
                LnkPrevious.Visible = (_intCurrentPage > 0 ? true : false);//Enable previous on page 2
            }
            
            if (_BDD.Count > 0)
            {
                foreach (BatchDetailData b in _BDD)
                {
                    BatchDetailData BDD = new BatchDetailDataVal(b);
                    chklstTMSResults.Items.Add(BDD);
                }
            }
        }

        private void ProcessQueryTransactionFamiliesResponse(List<FamilyDetail> _FD)
        {
            chklstTMSResults.Items.Clear();

            //Check for the need of paging
            if (_FD.Count > _intResultsPerPage - 1)
            {
                lblPageNumber.Text = @"Page : " + (_intCurrentPage + 1);
                lblPageNumber.Visible = true;
                LnkNext.Visible = true;
                LnkPrevious.Visible = (_intCurrentPage > 0 ? true : false);//Enable previous on page 2
            }
            else
            {
                lblPageNumber.Text = @"Page : " + (_intCurrentPage + 1);
                LnkNext.Visible = false;//End of the list so disable the Next link
                LnkPrevious.Visible = (_intCurrentPage > 0 ? true : false);//Enable previous on page 2
            }

            if (_FD.Count > 0)
            {
                foreach (FamilyDetail f in _FD)
                {
                    FamilyDetailVal SDV = new FamilyDetailVal(f);
                    chklstTMSResults.Items.Add(SDV);
                }
            }
        }

        private void ProcessQueryTransactionSummaryResponse(List<SummaryDetail> _SD)
        {
            chklstTMSResults.Items.Clear();

            //Check for the need of paging
            if (_SD.Count > _intResultsPerPage-1)
            {
                lblPageNumber.Text = @"Page : " + (_intCurrentPage+1);
                lblPageNumber.Visible = true;
                LnkNext.Visible = true;
                LnkPrevious.Visible = (_intCurrentPage > 0 ? true : false);//Enable previous on page 2
            }
            else
            {
                lblPageNumber.Text = @"Page : " + (_intCurrentPage + 1);
                LnkNext.Visible = false;//End of the list so disable the Next link
                LnkPrevious.Visible = (_intCurrentPage > 0 ? true : false);//Enable previous on page 2
            }

            if (_SD.Count > 0)
            {
                foreach (SummaryDetail s in _SD)
                {
                    SummaryDetailVal SDV = new SummaryDetailVal(s);
                    chklstTMSResults.Items.Add(SDV);
                }
            }
        }

        private void ProcessQueryTransactionsDetailResponse(List<TransactionDetail> _TD)
        {
            chklstTMSResults.Items.Clear();

            //Check for the need of paging
            if (_TD.Count > _intResultsPerPage - 1)
            {
                lblPageNumber.Text = @"Page : " + (_intCurrentPage + 1);
                lblPageNumber.Visible = true;
                LnkNext.Visible = true;
                LnkPrevious.Visible = (_intCurrentPage > 0 ? true : false);//Enable previous on page 2
            }
            else
            {
                lblPageNumber.Text = @"Page : " + (_intCurrentPage + 1);
                LnkNext.Visible = false;//End of the list so disable the Next link
                LnkPrevious.Visible = (_intCurrentPage > 0 ? true : false);//Enable previous on page 2
            }

            if (_TD.Count > 0)
            {
                foreach (TransactionDetail t in _TD)
                {
                    TransactionDetailVal TDV = new TransactionDetailVal(t);
                    chklstTMSResults.Items.Add(TDV);
                }
            }
        }

        private string BatchDetailDataString(BatchDetailData b)
        {
            //int intPreviousIndex = -1;
            //foreach (int itemChecked in chklstTMSResults.CheckedIndices)
            //{
            //    intPreviousIndex = itemChecked;
            //    chklstTMSResults.SetItemChecked(itemChecked, false);
            //}

            //if (chklstTMSResults.SelectedIndex != intPreviousIndex)
            //    chklstTMSResults.SetItemChecked(chklstTMSResults.SelectedIndex, true);

            string strSummary = "";

            //Batch Summary
            strSummary = strSummary + "BatchCaptureDate : " + b.BatchCaptureDate + " (UTC)\r\n";
            strSummary = strSummary + "BatchId : " + b.BatchId + "\r\n";
            strSummary = strSummary + "Description : " + b.Description + "\r\n";
            //Batch Summary Data
            strSummary = strSummary + "Batch Summary Data";
            if (b.BatchSummaryData.CashBackTotals != null) strSummary = strSummary + "\r\nCash Back Totals \r\n  Count : " + b.BatchSummaryData.CashBackTotals.Count + "\r\n  Net Amount : " + b.BatchSummaryData.CashBackTotals.NetAmount;
            if (b.BatchSummaryData.NetTotals != null) strSummary = strSummary + "\r\nNet Totals \r\n  Count : " + b.BatchSummaryData.NetTotals.Count + "\r\n  Net Amount : " + b.BatchSummaryData.NetTotals.NetAmount;
            if (b.BatchSummaryData.PINDebitReturnTotals != null) strSummary = strSummary + "\r\nPINDebit Return Totals \r\n  Count : " + b.BatchSummaryData.PINDebitReturnTotals.Count + "\r\n  Net Amount : " + b.BatchSummaryData.PINDebitReturnTotals.NetAmount;
            if (b.BatchSummaryData.PINDebitSaleTotals != null) strSummary = strSummary + "\r\nPINDebit Sale Totals \r\n  Count : " + b.BatchSummaryData.PINDebitSaleTotals.Count + "\r\n  Net Amount : " + b.BatchSummaryData.PINDebitSaleTotals.NetAmount;
            if (b.BatchSummaryData.ReturnTotals != null) strSummary = strSummary + "\r\nReturn Totals \r\n  Count : " + b.BatchSummaryData.ReturnTotals.Count + "\r\n  Net Amount : " + b.BatchSummaryData.ReturnTotals.NetAmount;
            if (b.BatchSummaryData.SaleTotals != null) strSummary = strSummary + "\r\nSale Totals \r\n  Count : " + b.BatchSummaryData.SaleTotals.Count + "\r\n  Net Amount : " + b.BatchSummaryData.SaleTotals.NetAmount;
            if (b.BatchSummaryData.VoidTotals != null) strSummary = strSummary + "\r\nVoid Totals \r\n  Count : " + b.BatchSummaryData.VoidTotals.Count + "\r\n  Net Amount : " + b.BatchSummaryData.VoidTotals.NetAmount;
            //TransactionId
            strSummary = strSummary + "\r\nList of TransactionIds\r\n";
            if (b.TransactionIds != null)
            {
                foreach (string txnid in b.TransactionIds)
                {
                    strSummary = strSummary + txnid + "\r\n";
                }
            }
            strSummary = strSummary + "\r\n";
            return strSummary;
        }

        private string FamilyDetailString(FamilyDetail f)
        {
            //int intPreviousIndex = -1;
            //foreach (int itemChecked in chklstTMSResults.CheckedIndices)
            //{
            //    intPreviousIndex = itemChecked;
            //    chklstTMSResults.SetItemChecked(itemChecked, false);
            //}

            //if (chklstTMSResults.SelectedIndex != intPreviousIndex)
            //    chklstTMSResults.SetItemChecked(chklstTMSResults.SelectedIndex, true);

            string strSummary = "";

            //Family Summary
            strSummary = strSummary + "FamilyId : " + f.FamilyId + "\r\n";
            strSummary = strSummary + "FamilyState : " + f.FamilyState + "\r\n";
            strSummary = strSummary + "LastAuthorizedAmount : " + f.LastAuthorizedAmount + "\r\n";
            strSummary = strSummary + "MerchantProfileId : " + f.MerchantProfileId + "\r\n";
            strSummary = strSummary + "NetAmount : " + f.NetAmount + "\r\n";
            strSummary = strSummary + "ServiceKey : " + f.ServiceKey + "\r\n";

            //TransactionId
            strSummary = strSummary + "List of TransactionIds\r\n";
            foreach (string txnid in f.TransactionIds)
            {
                strSummary = strSummary + txnid + "\r\n";
            }
            strSummary = strSummary + "\r\n";
            return strSummary;
        }

        private string SummaryDetailString(SummaryDetail s)
        {
            s.TransactionInformation.BankcardData = new BankcardData();
            s.TransactionInformation.BankcardData.AVSResult = new AVSResult();

            //int intPreviousIndex = -1;
            //foreach (int itemChecked in chklstTMSResults.CheckedIndices)
            //{
            //    intPreviousIndex = itemChecked;
            //    chklstTMSResults.SetItemChecked(itemChecked, false);
            //}

            //if (chklstTMSResults.SelectedIndex != intPreviousIndex)
            //    chklstTMSResults.SetItemChecked(chklstTMSResults.SelectedIndex, true);

            string strSummary = "";
            //Family Information
            strSummary = strSummary + "Family Information\r\n";
            strSummary = strSummary + "FamilyId : " + s.FamilyInformation.FamilyId + "\r\n";
            strSummary = strSummary + "FamilySequenceCount : " + s.FamilyInformation.FamilySequenceCount + "\r\n";
            strSummary = strSummary + "FamilySequenceNumber : " + s.FamilyInformation.FamilySequenceNumber + "\r\n";
            strSummary = strSummary + "FamilyState : " + s.FamilyInformation.FamilyState + "\r\n";
            //Transaction Information
            strSummary = strSummary + "Transaction Information\r\n";
            strSummary = strSummary + "Amount : " + s.TransactionInformation.Amount + "\r\n";
            strSummary = strSummary + "ApprovalCode : " + s.TransactionInformation.ApprovalCode + "\r\n";
            strSummary = strSummary + "AVSResult Summary\r\n";
            strSummary = strSummary + " - ActualResult : " + s.TransactionInformation.BankcardData.AVSResult.ActualResult + "\r\n";
            strSummary = strSummary + " - AddressResult : " + s.TransactionInformation.BankcardData.AVSResult.AddressResult + "\r\n";
            strSummary = strSummary + " - PostalCodeResult : " + s.TransactionInformation.BankcardData.AVSResult.PostalCodeResult + "\r\n";
            strSummary = strSummary + "BatchId : " + s.TransactionInformation.BatchId + "\r\n";
            strSummary = strSummary + "CaptureDateTime : " + s.TransactionInformation.CaptureDateTime + "\r\n";
            strSummary = strSummary + "CaptureState : " + s.TransactionInformation.CaptureState + "\r\n";
            strSummary = strSummary + "CardType : " + s.TransactionInformation.BankcardData.CardType + "\r\n";
            strSummary = strSummary + "CustomerId : " + s.TransactionInformation.CustomerId + "\r\n";
            strSummary = strSummary + "CVResult : " + s.TransactionInformation.BankcardData.CVResult + "\r\n";
            strSummary = strSummary + "IsAcknowledged : " + s.TransactionInformation.IsAcknowledged + "\r\n";
            strSummary = strSummary + "MaskedPAN : " + s.TransactionInformation.MaskedPAN + "\r\n";
            strSummary = strSummary + "MerchantProfileId : " + s.TransactionInformation.MerchantProfileId + "\r\n";
            strSummary = strSummary + "OriginatorTransactionId : " + s.TransactionInformation.OriginatorTransactionId + "\r\n";
            strSummary = strSummary + "ServiceId : " + s.TransactionInformation.ServiceId + "\r\n";
            strSummary = strSummary + "ServiceKey : " + s.TransactionInformation.ServiceKey + "\r\n";
            strSummary = strSummary + "ServiceTransactionId : " + s.TransactionInformation.ServiceTransactionId + "\r\n";
            strSummary = strSummary + "Status : " + s.TransactionInformation.Status + "\r\n";
            strSummary = strSummary + "TransactionClass : " + s.TransactionInformation.TransactionClassTypePair.TransactionClass + "    ";
            strSummary = strSummary + "TransactionType : " + s.TransactionInformation.TransactionClassTypePair.TransactionType + "\r\n";
            strSummary = strSummary + "TransactionId : " + s.TransactionInformation.TransactionId + "\r\n";
            strSummary = strSummary + "TransactionState : " + s.TransactionInformation.TransactionState + "\r\n";
            strSummary = strSummary + "TransactionStatusCode : " + s.TransactionInformation.TransactionStatusCode + "\r\n";
            strSummary = strSummary + "TransactionTimestamp : " + s.TransactionInformation.TransactionTimestamp + "\r\n";
            strSummary = strSummary + "\r\n";
            return strSummary;
        }

        private string TransactionDetailString(TransactionDetail t)
        {
            //int intPreviousIndex = -1;
            //foreach (int itemChecked in chklstTMSResults.CheckedIndices)
            //{
            //    intPreviousIndex = itemChecked;
            //    chklstTMSResults.SetItemChecked(itemChecked, false);
            //}

            //if (chklstTMSResults.SelectedIndex != intPreviousIndex)
            //    chklstTMSResults.SetItemChecked(chklstTMSResults.SelectedIndex, true);

            string strSummary = "";

            t.TransactionInformation.BankcardData = new BankcardData();
            t.TransactionInformation.BankcardData.AVSResult = new AVSResult();
            //Batch Summary
            strSummary = strSummary + "CompleteTransaction CWS object : " + "OBJECT\r\n";
            strSummary = strSummary + (t.CompleteTransaction.SerializedTransaction == null ? "CompleteTransaction Serialized : NOT AVAILABLE\r\n" : "CompleteTransaction Serialized : " + t.CompleteTransaction.SerializedTransaction + "\r\n");
            //Family Information
            strSummary = strSummary + "Family Information \r\n";
            strSummary = strSummary + "FamilyId : " + t.FamilyInformation.FamilyId + "\r\n";
            strSummary = strSummary + "FamilySequenceCount : " + t.FamilyInformation.FamilySequenceCount + "\r\n";
            strSummary = strSummary + "FamilySequenceNumber : " + t.FamilyInformation.FamilySequenceNumber + "\r\n";
            strSummary = strSummary + "FamilyState : " + t.FamilyInformation.FamilyState + "\r\n";
            strSummary = strSummary + "NetAmount : " + t.FamilyInformation.NetAmount + "\r\n";
            //Transaction Information
            strSummary = strSummary + "Transaction Information\r\n";
            strSummary = strSummary + "Amount : " + t.TransactionInformation.Amount + "\r\n";
            strSummary = strSummary + "ApprovalCode : " + t.TransactionInformation.ApprovalCode + "\r\n";
            //TransactionInformation.BankcardData
            strSummary = strSummary + "AVSResult Summary";
            strSummary = strSummary + " - ActualResult : " + t.TransactionInformation.BankcardData.AVSResult.ActualResult + "\r\n";
            strSummary = strSummary + " - AddressResult : " + t.TransactionInformation.BankcardData.AVSResult.AddressResult + "\r\n";
            strSummary = strSummary + " - PostalCodeResult : " + t.TransactionInformation.BankcardData.AVSResult.PostalCodeResult + "\r\n";
            strSummary = strSummary + "CardType : " + t.TransactionInformation.BankcardData.CardType + "\r\n";
            strSummary = strSummary + "CVResult : " + t.TransactionInformation.BankcardData.CVResult + "\r\n";
            strSummary = strSummary + "BatchId : " + t.TransactionInformation.BatchId + "\r\n";
            strSummary = strSummary + "CaptureDateTime : " + t.TransactionInformation.CaptureDateTime + "\r\n";
            strSummary = strSummary + "CaptureState : " + t.TransactionInformation.CaptureState + "\r\n";
            strSummary = strSummary + "CaptureStatusMessage : " + t.TransactionInformation.CaptureStatusMessage + "\r\n";
            strSummary = strSummary + "CapturedAmount : " + t.TransactionInformation.CapturedAmount + "\r\n";
            strSummary = strSummary + "CustomerId : " + t.TransactionInformation.CustomerId + "\r\n";
            if (t.TransactionInformation.ElectronicCheckData != null)
            {
				//TransactionInformation.ElectronicCheckData
				strSummary = strSummary + "Electronic Check Data";
				strSummary = strSummary + " - CheckNumber : " + t.TransactionInformation.ElectronicCheckData.CheckNumber + "\r\n";
				strSummary = strSummary + " - MaskedAccountNumber : " + t.TransactionInformation.ElectronicCheckData.MaskedAccountNumber + "\r\n";
				strSummary = strSummary + " - TransactionType : " + t.TransactionInformation.ElectronicCheckData.TransactionType + "\r\n";
			}
            strSummary = strSummary + "IsAcknowledged : " + t.TransactionInformation.IsAcknowledged + "\r\n";
            strSummary = strSummary + "MaskedPAN : " + t.TransactionInformation.MaskedPAN + "\r\n";
            strSummary = strSummary + "MerchantProfileId : " + t.TransactionInformation.MerchantProfileId + "\r\n";
            strSummary = strSummary + "OriginatorTransactionId : " + t.TransactionInformation.OriginatorTransactionId + "\r\n";
            strSummary = strSummary + "ServiceId : " + t.TransactionInformation.ServiceId + "\r\n";
            strSummary = strSummary + "ServiceKey : " + t.TransactionInformation.ServiceKey + "\r\n";
            strSummary = strSummary + "ServiceTransactionId : " + t.TransactionInformation.ServiceTransactionId + "\r\n";
            strSummary = strSummary + "Status : " + t.TransactionInformation.Status + "\r\n";
            strSummary = strSummary + "TransactionClass : " + t.TransactionInformation.TransactionClassTypePair.TransactionClass + "    ";
            strSummary = strSummary + "TransactionType : " + t.TransactionInformation.TransactionClassTypePair.TransactionType + "\r\n";
            strSummary = strSummary + "TransactionId : " + t.TransactionInformation.TransactionId + "\r\n";
            strSummary = strSummary + "TransactionState : " + t.TransactionInformation.TransactionState + "\r\n";
            strSummary = strSummary + "TransactionStatusCode : " + t.TransactionInformation.TransactionStatusCode + "\r\n";
            strSummary = strSummary + "TransactionTimestamp : " + t.TransactionInformation.TransactionTimestamp + "\r\n";
            strSummary = strSummary + "\r\n";

            strSummary = strSummary + "\r\n";
            return strSummary;
        }

        #endregion END process TMS response

        #region Setup Help Links
        private void lnkQueryBatch_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://my.ipcommerce.com/Docs/DataServices/TMS_Developer_Guide/2.0.17/Implementation/SOAP/QueryBatch.aspx");
        }
        private void lnkQueryTransactions_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://my.ipcommerce.com/Docs/DataServices/TMS_Developer_Guide/2.0.17/Implementation/SOAP/QueryTransactionsSummary.aspx");
        }
        private void lnkQueryTransactionFamilies_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://my.ipcommerce.com/Docs/DataServices/TMS_Developer_Guide/2.0.17/Implementation/SOAP/QueryTransactionFamilies.aspx");
        }
        private void lnkQueryTransactionDetails_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://my.ipcommerce.com/Docs/DataServices/TMS_Developer_Guide/2.0.17/Implementation/SOAP/QueryTransactionsDetail.aspx");
        }
        #endregion END Setup Help Links

        private void TxtClearTransactionIds_Click(object sender, EventArgs e)
        {
            txtQTP_TransactionIds.Text = "";
        }

        private void TxtClearBatchTransactionIds_Click(object sender, EventArgs e)
        {
            txtQBP_TransactionIds.Text = "";
        }

    }

    #region Local Classes

    public class SummaryDetailVal : SummaryDetail
    {
        public SummaryDetail SD;
        public SummaryDetailVal(SummaryDetail sd)
        {
            SD = sd;
        }
        public override string ToString()
        {// Generates the text shown in the combo box
            return SD.TransactionInformation.Amount + " | " + SD.TransactionInformation.TransactionId + " (" + SD.TransactionInformation.TransactionTimestamp + ") UTC";
        }
    }

    public class FamilyDetailVal : FamilyDetail
    {
        public FamilyDetail FD;
        public FamilyDetailVal(FamilyDetail fd)
        {
            FD = fd;
        }
        public override string ToString()
        {// Generates the text shown in the combo box
            return "Net Amount : " + FD.NetAmount + " | Family State : " + FD.FamilyState;
        }
    }

    public class BatchDetailDataVal : BatchDetailData
    {
        public BatchDetailData BDD;
        public BatchDetailDataVal(BatchDetailData bdd)
        {
            BDD = bdd;
        }
        public override string ToString()
        {// Generates the text shown in the combo box
            return "BatchId : " + BDD.BatchId + " | BatchCaptureDate : " + BDD.BatchCaptureDate;
        }
    }

    public class TransactionDetailVal : TransactionDetail
    {
        public TransactionDetail TD;
        public TransactionDetailVal(TransactionDetail td)
        {
            TD = td;
        }
        public override string ToString()
        {// Generates the text shown in the combo box
            return TD.TransactionInformation.Amount + " | " + TD.TransactionInformation.TransactionId + " (" + TD.TransactionInformation.TransactionTimestamp + ") UTC";
        }
    }

    public enum LastSearchType : int
    {
        [System.Runtime.Serialization.EnumMemberAttribute()]
        QueryTransactionsSummary = 0,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        QueryTransactionFamilies = 1,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        QueryTransactionsDetail = 2,
        [System.Runtime.Serialization.EnumMemberAttribute()]
        QueryBatch = 3,

    }

    #endregion END Local Classes
}
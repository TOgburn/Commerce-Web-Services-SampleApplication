using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn;

namespace SampleCode
{
    public partial class Magensa : Form
    {
        public BankcardTransaction _bct;
        public TestTriggers TT;
        public bool ProcessTransaction;
        public Magensa()
        {
            InitializeComponent();

            CboTriggerTests.Items.Add(new TestTriggers("10.00", "E860", "Magensa returned OK."));
            CboTriggerTests.Items.Add(new TestTriggers("10.05", "E860", "Magensa error number: Y001. No PAN Found in Track2 Data."));
            CboTriggerTests.Items.Add(new TestTriggers("10.10", "E860", "Magensa error number: Y091. Invalid KSID."));
            CboTriggerTests.Items.Add(new TestTriggers("10.15", "E860", "Magensa error number: Y093. Invalid MagnePrint."));
            CboTriggerTests.Items.Add(new TestTriggers("10.20", "E860", "Magensa error number: Y094. Invalid MagnePrint. Invalid transaction CRC/PAN."));
            CboTriggerTests.Items.Add(new TestTriggers("10.25", "E860", "Magensa error number: Y095. Error Scoring Card."));
            CboTriggerTests.Items.Add(new TestTriggers("10.30", "E860", "Magensa error number: Y096. Inactive MagnePrint reference."));
            CboTriggerTests.Items.Add(new TestTriggers("10.35", "E860", "Magensa error number: Y097. DUKPT KSN and counter is replayed."));
            CboTriggerTests.Items.Add(new TestTriggers("10.40", "E860", "Magensa error number: Y098. Problem Decrypting Data."));
            CboTriggerTests.Items.Add(new TestTriggers("10.45", "E860", "Magensa error number: Y099. Error Validating Credentials."));
            CboTriggerTests.Items.Add(new TestTriggers("10.50", "E898", "Magensa returned an invalid data error: <Insert Magensa StatusCode>. <Insert Magensa input validation error "));
            CboTriggerTests.Items.Add(new TestTriggers("10.55", "E899", "Magensa returned an unknown error.  <Insert Magensa StatusCode>. <StatusMsg>."));
        }

        public void CallingForm(ref BankcardTransaction bct)
        {
            _bct = bct;
            TxtAmount.Text = _bct.TransactionData.Amount.ToString();
        }

        private void CmdGenerateFields_Click(object sender, EventArgs e)
        {
            //StringReader strReader = new StringReader(TxtTrackData.Text);
            string[] lines = TxtTrackInformation.Text.Replace("\r","").Split('\n');

            if (lines.Length < 21)
                MessageBox.Show("No Magensa Swipes to use");

            string encryptedTrack2 = lines[6].Substring(lines[6].IndexOf("=") + 1).Replace(" ", "");
            string encryptedMagnePrintData = lines[12].Substring(lines[12].IndexOf("=") + 1).Replace(" ", "");
            string DUKPTserialNo = lines[2].Substring(lines[2].IndexOf("=") + 1).Replace(" ", "");
            string magenPrintStatus = lines[10].Substring(lines[10].IndexOf("=") + 1).Replace(" ", "");

            TxtMagnePrintData.Text = encryptedMagnePrintData;
            TxtDukptKeySerialNumber.Text = DUKPTserialNo;
            TxtTrack2EncryptedData.Text = encryptedTrack2;
            TxtMagnePrintStatus.Text = magenPrintStatus;
        }

        private void CmdUseValues_Click(object sender, EventArgs e)
        {
            if (TxtMagnePrintData.Text.Length < 1 | TxtDukptKeySerialNumber.Text.Length < 1 | TxtTrack2EncryptedData.Text.Length < 1 | TxtMagnePrintStatus.Text.Length <1)
            {
                MessageBox.Show("Required information is missing");
                return;
            }

            _bct.TenderData.CardData = null;
            if (_bct.TenderData.CardSecurityData == null) { _bct.TenderData.CardSecurityData = new CardSecurityData(); }
            
            _bct.TenderData.CardSecurityData.CVData = null;
            _bct.TenderData.CardSecurityData.CVDataProvided = CVDataProvided.NotSet;
            _bct.TenderData.CardSecurityData.IdentificationInformation = TxtMagnePrintData.Text;
            _bct.TenderData.SecurePaymentAccountData = TxtTrack2EncryptedData.Text;
            _bct.TenderData.EncryptionKeyId = TxtDukptKeySerialNumber.Text;
            _bct.TenderData.SwipeStatus = TxtMagnePrintStatus.Text;
            //_bct.TransactionData.ScoreThreshold = ".5";
            _bct.TransactionData.EntryMode = EntryMode.Track2DataFromMSR;
            _bct.TransactionData.Reference = "11";

            _bct.TransactionData.Amount = Convert.ToDecimal(TxtAmount.Text);

            ProcessTransaction = true;
            this.Close();
        }

        private void CboTriggerTests_SelectedIndexChanged(object sender, EventArgs e)
        {
            TT = (TestTriggers) CboTriggerTests.SelectedItem;
            TxtAmount.Text = TT.Trigger;
        }

        private void CmdPopulateWithTestValues_Click(object sender, EventArgs e)
        {
            TxtTrackInformation.Text =
                "Card Encode Type = ISO\r\n\r\nDUKPT Key Serial Number = 9010010B0C247200001F\r\n\r\nTrack 1 Encrypted = CF BD 82 96 5A 66 FF C8 8D 51 2C 8D 1C 4B 1D B6 7C 45 5B 6C 1E 36 17 6A 20 26 BF 27 94 2F C5 71 7D 89 B1 BE A7 9B 88 14 26 47 9C 51 FA 30 1C 2D 99 F6 83 78 A0 66 4C 0D 79 6A 08 36 95 F6 1F 30 \r\n\r\nTrack 2 Encrypted = EA D1 C2 F9 6D DE 13 1B 35 6B 2C 6C 98 25 D3 68 C9 C6 F7 26 84 75 72 FC BC 78 81 18 C4 9F 46 F7 87 6E A8 7A 71 75 E4 7A \r\n\r\nTrack 3 Encrypted = \r\n\r\nMagnePrint Status (hex) = 00304061\r\n\r\nMagnePrint Data (hex) = A3 63 34 A6 4D C8 A5 35 67 EE 0F 28 AD B6 5E EB F1 5D 2B 22 AB 39 2F C2 DC 79 E3 4A A4 90 C3 6E 6E C1 90 A1 F3 E5 19 63 8F CC AA F0 37 23 CA CA 05 84 43 1C AE 3E 1B B1 \r\n\r\nDevice Serial Number = B0C2472071812AA\r\n\r\nTrack 1 Masked = %B4111000010001111^IPCOMMERCE/TESTCARD^13120000000000000000000?\r\n\r\nTrack 2 Masked = ;4111000010001111=13120000000000000000?\r\n\r\nEncrypted Session ID (Hex) = 14 1B 21 95 AB 89 D8 C9\r\n";
        }

        private void CmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkManageAccountById_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.magtek.com/support/software/demo_programs/encoder_reader.asp");
        }
    }

    public class TestTriggers
    {
        public string Trigger;
        public string StatusCode;
        public string StatusMessage;
        public TestTriggers(string trigger, string statusCode, string statusMessage)
        {
            Trigger = trigger;
            StatusCode = statusCode;
            StatusMessage = statusMessage;
        }
        public override string ToString()
        {// Generates the text shown in the combo box
            return Trigger + " - " + StatusCode + " - " + StatusMessage;
        }
    }
}
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
using System.ServiceModel;

namespace FaultHandler
{
    public class FaultHandler
    {
        public bool handleSvcInfoFault(Exception _ex, out string _strErrorID, out string _strErrorMessage)
        {//Note : that the boolean indicates if the fault was handled by this class

            _strErrorID = "";
            _strErrorMessage = "";

            #region AuthorizationFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.AuthorizationFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.AuthorizationFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.AuthorizationFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion AuthorizationFault

            #region ClaimNotFoundFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ClaimNotFoundFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ClaimNotFoundFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ClaimNotFoundFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion ClaimNotFoundFault

            #region CWSValidationResultFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSValidationResultFault>)(_ex)).Detail != null)
                {
                    foreach (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSValidationErrorFault error in ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSValidationResultFault>)(_ex)).Detail.Errors)
                    {
                        _strErrorMessage = _strErrorMessage + error.RuleKey + " : " + error.RuleMessage + "\r\n\r\n";
                    }
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSValidationResultFault

            #region AuthenticationFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.AuthenticationFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.AuthenticationFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.AuthenticationFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion AuthenticationFault

            #region STSUnavailableFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.STSUnavailableFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.STSUnavailableFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.STSUnavailableFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion STSUnavailableFault

            #region ExpiredTokenFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ExpiredTokenFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ExpiredTokenFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.ExpiredTokenFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion AuthenticationFault

            #region InvalidTokenFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.InvalidTokenFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.InvalidTokenFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.InvalidTokenFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion InvalidTokenFault

            #region CWSServiceInformationUnavailableFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSServiceInformationUnavailableFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSServiceInformationUnavailableFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSServiceInformationUnavailableFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSServiceInformationUnavailableFault

//Additional Faults called out in the data contract

            #region CWSValidationErrorFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSValidationErrorFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSValidationErrorFault>)(_ex)).Detail.RuleKey;
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.CWSValidationErrorFault>)(_ex)).Detail.RuleMessage;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSValidationErrorFault

            #region Presently Unsupported Faults
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.BadAttemptThresholdExceededFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "BadAttemptThresholdExceededFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.GeneratePasswordFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "GeneratePasswordFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.InvalidEmailFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "InvalidEmailFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.LockedByAdminFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "LockedByAdminFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.OneTimePasswordFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "OneTimePasswordFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PasswordExpiredFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "PasswordExpiredFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.PasswordInvalidFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "PasswordInvalidFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.SendEmailFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "SendEmailFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.STSUnavailableFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "STSUnavailableFault thrown however not handled by code"; 
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.SvcInfo.UserNotFoundFault>)(_ex)).Detail != null)  
                    _strErrorMessage = "UserNotFoundFault thrown however not handled by code"; 
            }
            catch { }
            #endregion Presently Unsupported Faults

            //As one last check look at the GeneralFaults
            if (GeneralFaults(_ex, out _strErrorID, out _strErrorMessage))
                return true;//Positive Match

            _strErrorMessage = "An unhandled fault was thrown \r\nMessage : " + _ex.Message;
            return false;//In this case unable to match fault so return false. 
        }

        public bool handleTxnFault(Exception _ex, out string _strErrorID, out string _strErrorMessage)
        {//Note : that the boolean indicates if the fault was handled by this class
            _strErrorID = "";
            _strErrorMessage = "";

            #region CWSConnectionFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSConnectionFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSConnectionFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSConnectionFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSConnectionFault

            #region CWSExtendedDataNotSupportedFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSExtendedDataNotSupportedFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSExtendedDataNotSupportedFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSExtendedDataNotSupportedFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSExtendedDataNotSupportedFault

            #region CWSInvalidOperationFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidOperationFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidOperationFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidOperationFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSInvalidOperationFault

            #region CWSOperationNotSupportedFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSOperationNotSupportedFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSOperationNotSupportedFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSOperationNotSupportedFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSOperationNotSupportedFault

            #region CWSValidationResultFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSValidationResultFault>)(_ex)).Detail != null)
                {
                    foreach (schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSValidationErrorFault error in ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSValidationResultFault>)(_ex)).Detail.Errors)
                    {
                        _strErrorMessage = _strErrorMessage + error.RuleKey + " : " + error.RuleMessage + "\r\n";
                    }
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSValidationResultFault

            #region AuthenticationFault

            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.AuthenticationFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.AuthenticationFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.AuthenticationFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion AuthenticationFault

            #region ExpiredTokenFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.ExpiredTokenFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.ExpiredTokenFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.ExpiredTokenFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion ExpiredTokenFault

            #region InvalidTokenFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.InvalidTokenFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.InvalidTokenFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.InvalidTokenFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion InvalidTokenFault

            #region CWSTransactionServiceUnavailableFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionServiceUnavailableFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionServiceUnavailableFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionServiceUnavailableFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSTransactionServiceUnavailableFault

            #region CWSInvalidMessageFormatFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidMessageFormatFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidMessageFormatFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidMessageFormatFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSInvalidMessageFormatFault

            #region CWSInvalidServiceInformationFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidServiceInformationFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidServiceInformationFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSInvalidServiceInformationFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSInvalidServiceInformationFault

            #region CWSTransactionAlreadySettledFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionAlreadySettledFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionAlreadySettledFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionAlreadySettledFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSTransactionAlreadySettledFault

            #region CWSTransactionFailedFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionFailedFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionFailedFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSTransactionFailedFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSTransactionFailedFault

//Additional Faults called out in the data contract
            #region CWSValidationErrorFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSValidationErrorFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSValidationErrorFault>)(_ex)).Detail.RuleKey;
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSValidationErrorFault>)(_ex)).Detail.RuleMessage;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion CWSValidationErrorFault

            #region CWSDeserializationFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSDeserializationFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSDeserializationFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSDeserializationFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }

            #endregion CWSDeserializationFault

            //As one last check look at the GeneralFaults
            if (GeneralFaults(_ex, out _strErrorID, out _strErrorMessage))
                return true;//Positive Match

            _strErrorMessage = "An unhandled fault was thrown \r\nMessage : " + _ex.Message;
            return false;//In this case unable to match fault so return false. 
        }

        public bool handleTMSFault(Exception _ex, out string _strErrorID, out string _strErrorMessage)
        {//Note : that the boolean indicates if the fault was handled by this class
            _strErrorID = "";
            _strErrorMessage = "";

            #region AuthenticationFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.AuthenticationFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.AuthenticationFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.AuthenticationFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion AuthenticationFault

            #region TMSOperationNotSupportedFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSOperationNotSupportedFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSOperationNotSupportedFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSOperationNotSupportedFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion TMSOperationNotSupportedFault

            #region TMSTransactionFailedFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSTransactionFailedFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSTransactionFailedFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSTransactionFailedFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion TMSTransactionFailedFault

            #region TMSUnknownServiceKeyFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSUnknownServiceKeyFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSUnknownServiceKeyFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSUnknownServiceKeyFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion TMSUnknownServiceKeyFault

            #region ExpiredTokenFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.ExpiredTokenFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.ExpiredTokenFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.ExpiredTokenFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion ExpiredTokenFault

            #region InvalidTokenFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.InvalidTokenFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.InvalidTokenFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.InvalidTokenFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion InvalidTokenFault

            #region TMSUnavailableFault
            try
            {
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSUnavailableFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSUnavailableFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.DataServices.TMSUnavailableFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }
            catch { }
            #endregion TMSUnavailableFault


            //As one last check look at the GeneralFaults
            if (GeneralFaults(_ex, out _strErrorID, out _strErrorMessage))
                return true;//Positive Match

            _strErrorMessage = "An unhandled fault was thrown \r\nMessage : " + _ex.Message;
            return false;//In this case unable to match fault so return false. 
        }

        public bool GeneralFaults(Exception _ex, out string _strErrorID, out string _strErrorMessage)
        {//Note : that the boolean indicates if the fault was handled by this class
            
            _strErrorID = "";
            _strErrorMessage = "";

            try{
                if (((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSFault>)(_ex)).Detail != null)
                {
                    _strErrorID = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSFault>)(_ex)).Detail.ErrorID.ToString();
                    _strErrorMessage = ((FaultException<schemas.ipcommerce.com.Ipc.General.WCF.Contracts.Common.External.Txn.CWSFault>)(_ex)).Detail.ProblemType;
                    return true;//Positive Match
                }
            }catch{}

            //No General faults found
            return false;
        }
    }
}

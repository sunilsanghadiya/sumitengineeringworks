using System.Net;
using System.Net.Mail;
using authmodule.Common.DTOs;
using Microsoft.Extensions.Options;
using static sew.Commons.Config;

namespace sew.Helpers
{
    // public interface IEmailSenderService
    // {
    //     // Result SendMails(MailAddress fromMailAddress, List<MailAddress> toMailAddresses, List<MailAddress> ccMailAddresses, List<MailAddress> bccMailAddresses, string subject, string body, List<Attachment>? attachments, int typeID);
    //     // Result SendAMail(MailAddress fromMailAddress, string toMailAddress, string subject, string body, int typeID);
    //     Result SendEmail(EmailDto emailDto, List<Attachment>? attachments = null);
    // }

    public class EmailSenderService 
    {
        public readonly ServiceSettings _serviceSettings;
        public EmailSenderService(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }

        public Result SendAMail(MailAddress fromMailAddress, string toMailAddress, string subject, string body, int typeID)
        {
            Result result = new();
            try
            {
                SmtpClient smtpClient = GetSmtpClient();
                MailMessage mailMessage = new();
                mailMessage.From = fromMailAddress;

                mailMessage.To.Add(toMailAddress);

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                if (typeID == (int)Enums.EmailType.Cancel)
                {
                    mailMessage.Headers.Add("Content-class", "urn:content-classes:calendarmessage");
                }
                smtpClient.Send(mailMessage);
                smtpClient.Dispose();
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while sending mail error : " + ex.Message, ex);
            }
            return result;
        }

        public Result SendMails(MailAddress fromMailAddress, List<MailAddress> toMailAddresses, List<MailAddress> ccMailAddresses, List<MailAddress> bccMailAddresses, string subject, string body, List<Attachment>? attachments, int typeID)
        {
            Result result = new();
            try
            {
                SmtpClient smtpClient = GetSmtpClient();
                MailMessage mailMessage = new();
                mailMessage.From = fromMailAddress;

                foreach (MailAddress mailAddress in toMailAddresses)
                {
                    mailMessage.To.Add(mailAddress);
                }

                if (ccMailAddresses != null)
                {
                    foreach (MailAddress mailAddress in ccMailAddresses)
                    {
                        mailMessage.CC.Add(mailAddress);
                    }
                }

                if (bccMailAddresses != null)
                {
                    foreach (MailAddress mailAddress in bccMailAddresses)
                    {
                        mailMessage.Bcc.Add(mailAddress);
                    }
                }

                if (attachments != null)
                {
                    foreach (Attachment attachment in attachments)
                    {
                        mailMessage.Attachments.Add(attachment);
                        if (typeID == (int)Enums.EmailType.Cancel)
                        {
                            attachment.ContentType = new System.Net.Mime.ContentType("text/calender; method=CANCEL");
                        }
                        else if (typeID == (int)Enums.EmailType.AddUpdate)
                        {
                            attachment.ContentType = new System.Net.Mime.ContentType("text/calender; method=REQUEST");
                        }
                    }
                }

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                if (typeID == (int)Enums.EmailType.Cancel)
                {
                    mailMessage.Headers.Add("Content-class", "urn:content-classes:calendarmessage");
                }
                smtpClient.Send(mailMessage);
                smtpClient.Dispose();
            }
            catch (Exception ex)
            {
                return new Result("An error occurred while sending mail error : " + ex.Message, ex);
            }
            return result;
        }

        public Result SendEmail(EmailDto emailDto, List<Attachment>? attachments = null)
        {
            try
            {
                MailAddress fromMail = new(_serviceSettings.MailSettings?.SmtpFromEmailAddress, _serviceSettings.MailSettings.SmtpFromName);

                string toEmailAddress = emailDto.ToEmail;

                // if (string.IsNullOrWhiteSpace(toEmailAddress))
                // {
                //     toEmailAddress = _serviceSettings.mailSettings.DefaultEmailAddress;
                // }

                // List<MailAddress> toMails = new();

                // if (toEmailAddress.Contains(_serviceSettings.mailSettings.EmailSeparator))
                // {
                //     List<string> toEmailAddressList = toEmailAddress.Split(_serviceSettings.mailSettings.EmailSeparator, StringSplitOptions.RemoveEmptyEntries).ToList();
                //     if (toEmailAddressList.Any())
                //     {
                //         foreach (string toEmail in toEmailAddressList)
                //         {
                //             toMails.Add(toEmail);
                //         }
                //     }
                // }
                // else
                // {
                //     toMails.Add(new MailAddress(toEmailAddress, toEmailAddress));
                // }

                List<MailAddress> ccMails = new();
                List<MailAddress> bccMails = new();

                #region  CC Mail Address
                // if (!string.IsNullOrWhiteSpace(emailDto.CcEmail))
                // {
                //     if (emailDto.CcEmail.Contains(_serviceSettings.mailSettings.EmailSeparator))
                //     {
                //         List<string> ccMailAddressList = emailDto.CcEmail.Split(_serviceSettings.mailSettings.EmailSeparator, StringSplitOptions.RemoveEmptyEntries).ToList();
                //         if (ccMailAddressList.Any())
                //         {
                //             foreach (string ccEmail in ccMailAddressList)
                //             {
                //                 ccMails.Add(new MailAddress(ccEmail));
                //             }
                //         }
                //     }
                //     else
                //     {
                //         ccMails.Add(new MailAddress(emailDto.CcEmail));
                //     }
                // }
                #endregion

                #region BCC Mail Addresses
                // if (!string.IsNullOrEmpty(emailDto.BccEmail))
                // {
                //     if (emailDto.BccEmail.Contains(_serviceSettings.mailSettings.EmailSeparator))
                //     {
                //         List<string> bccEmailAddressList = emailDto.BccEmail.Split(_serviceSettings.mailSettings.EmailSeparator, StringSplitOptions.RemoveEmptyEntries).ToList();
                //         if (bccEmailAddressList.Any())
                //         {
                //             foreach (string bccEmail in bccEmailAddressList)
                //             {
                //                 bccMails.Add(new MailAddress(bccEmail));
                //             }
                //         }
                //     }
                //     else
                //     {
                //         bccMails.Add(new MailAddress(emailDto.BccEmail));
                //     }
                // }
                #endregion

                Result result = SendAMail(fromMail, emailDto.ToEmail, emailDto.EmailSubject, emailDto.EmailBody, emailDto.typeID);
                if (result.HasError)
                {
                    return new Result("An error occurred while SendEmail()");
                }
                return result;

            }
            catch (Exception ex)
            {
                return new Result("An error occurred while SendEmail()", ex);
            }
        }

        #region  Private Methods
        private SmtpClient GetSmtpClient()
        {
            SmtpClient smtpClient = new();
            MailSettings smtpConfig = _serviceSettings.MailSettings;
            if (!String.IsNullOrEmpty(smtpConfig.SmtpUserName) && !String.IsNullOrEmpty(smtpConfig.SmtpPassword))
            {
                NetworkCredential networkCredential = new(smtpConfig.SmtpUserName, smtpConfig.SmtpPassword);
                smtpClient.Credentials = networkCredential;
            }
            else
            {
                smtpClient.UseDefaultCredentials = true;
            }
            smtpClient.Host = smtpConfig.SmtpServer;
            smtpClient.Port = smtpConfig.SmtpPort;
            smtpClient.EnableSsl = smtpConfig.IsEnableSsl;

            return smtpClient;
        }
        #endregion
    }
}
using Library.Mail;
using Library.Models;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using REST.Interfaces;
using MailKit.Net.Smtp;
using Library.Genderize;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace REST.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<InvitationService> _logger;
        private readonly IHttpClientFactory _factory;
        private HttpClient _clientGender;


        public InvitationService(IOptions<MailSettings> options, ILogger<InvitationService> logger, IHttpClientFactory factory)
        {
            _mailSettings = options.Value;
            _logger = logger;
            _factory = factory;
            _clientGender = _factory.CreateClient("Genderize");
           

        }

        public async Task<string> GetInvitation(string firstname, string lastname, string mail)
        {
            Invitation invitation = new Invitation();
            invitation.Location = "Prøvensvej 1";
            invitation.Date = DateOnly.FromDateTime(DateTime.Now.AddDays(4.3));
            invitation.FirstName = firstname;
            invitation.LastName = lastname;
            invitation.Mail = mail;
            GeoIP.Service1Client client = new GeoIP.Service1Client();
            string ip = "";
            if (client.InnerChannel.State != System.ServiceModel.CommunicationState.Faulted)
            {
                ip = client.MyCurrentIPAsync().Result;
            }
            else
            {
                return "System.ServiceModel.CommunicationState.Faulted";
            }

            HttpResponseMessage response = await _clientGender.GetAsync("?name[]=" + firstname + "&country_id=" + ip);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                if (!JsonConvert.DeserializeObject<List<GenderizeModel>>(content).First().Gender.Equals(null))
                {
                    GenderizeModel genderize = JsonConvert.DeserializeObject<List<GenderizeModel>>(content).First();

                    _logger.LogInformation($"The Gender of the {firstname} is {genderize}");

                    if (genderize.Gender == Gender.male)
                    {
                        if (genderize.Probability > 0.90)
                            invitation.Title = "Mr.";
                        else
                            invitation.Title = "";

                    }
                    else if (genderize.Gender == Gender.female)
                    {
                        if (genderize.Probability > 0.90)
                            invitation.Title = "Mrs.";
                        else
                            invitation.Title = "";
                    }
                    
                }
                else
                    invitation.Title = "";

            }


            return invitation.ToString();
        }

        public async Task<string> GetInvitationWithInputtedIp(string firstname, string lastname, string mail, string ip)
        {
            Invitation invitation = new Invitation();
            invitation.Location = "Prøvensvej 1";
            invitation.Date = DateOnly.FromDateTime(DateTime.Now.AddDays(4.3));
            invitation.FirstName = firstname;
            invitation.LastName = lastname;
            invitation.Mail = mail;
            GeoIP.Service1Client client = new GeoIP.Service1Client();
            string input = "";
            if (client.InnerChannel.State != System.ServiceModel.CommunicationState.Faulted)
            {
                if (String.IsNullOrWhiteSpace(ip))
                    return "IP not validate";
                String[] splitValues = ip.Split('.');
                if (splitValues.Length != 4)
                    return "IP not validate";

                byte tempForParsing;
                if (splitValues.All(x => byte.TryParse(x, out tempForParsing)) == false)
                    return "IP not validate";
                input = client.InputtedIPAsync(ip).Result;

            }
            else
            {
                return "System.ServiceModel.CommunicationState.Faulted";
            }

            HttpResponseMessage response = await _clientGender.GetAsync("?name[]=" + firstname + "&country_id=" + input);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                if (!JsonConvert.DeserializeObject<List<GenderizeModel>>(content).First().Gender.Equals(null))
                {

                    GenderizeModel genderize = JsonConvert.DeserializeObject<List<GenderizeModel>>(content).First();
                    _logger.LogInformation($"The Gender of the {firstname} is {genderize}");

                    if (genderize.Gender == Gender.male)
                    {
                        if (genderize.Probability > 0.90)
                            invitation.Title = "Mr.";
                        else
                            invitation.Title = "";

                    }
                    else if (genderize.Gender == Gender.female)
                    {
                        if (genderize.Probability > 0.90)
                            invitation.Title = "Mrs.";
                    }
                    
                }
                else { invitation.Title = ""; }
            }


            return invitation.ToString();
        }

        public async Task SendEmailAsync(string firstname,string lastname, string mail)
        {
            Invitation invitation = new Invitation();
            invitation.Location = "Prøvensvej 1";
            invitation.Date = DateOnly.FromDateTime(DateTime.Now.AddDays(4.3));
            invitation.FirstName= firstname;
            invitation.LastName = lastname;
            invitation.Mail = mail;

            _logger.LogInformation($"Sending mail to {mail}");

            var email = new MimeMessage();
            email.Sender= MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mail));
            email.Subject = "Invitation to the EVENT";

            GeoIP.Service1Client client = new GeoIP.Service1Client();

            var trimmedEmail = mail.Trim();
            if (trimmedEmail.EndsWith("."))
                throw new Exception("Mail is not valid");

            var address = new System.Net.Mail.MailAddress(mail);
            if (address.Address != trimmedEmail)
                throw new Exception("Mail is not valid");

            string ip = "";
            if (client.InnerChannel.State != System.ServiceModel.CommunicationState.Faulted)
            {
                ip = client.MyCurrentIPAsync().Result;

            }


            HttpResponseMessage response = await _clientGender.GetAsync("?name[]=" + firstname + "&country_id=" + ip);
            string content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                if (!JsonConvert.DeserializeObject<List<GenderizeModel>>(content).First().Gender.Equals(null))
                {
                    GenderizeModel genderize = JsonConvert.DeserializeObject<List<GenderizeModel>>(content).First();
                    _logger.LogInformation($"The Gender of the {firstname} is {genderize}");

                    if (genderize.Gender == Gender.male)
                    {
                        if (genderize.Probability > 0.90)
                            invitation.Title = "Mr.";
                        else
                            invitation.Title = "";

                    }
                    else if (genderize.Gender == Gender.female)
                    {
                        if (genderize.Probability > 0.90)
                            invitation.Title = "Mrs.";
                    }

                }
                else
                {
                    invitation.Title = "";
                }
            }
            var builder = new BodyBuilder();
            builder.HtmlBody = invitation.ToString();
            email.Body = builder.ToMessageBody();


            using var smtp = new SmtpClient();
            if(_mailSettings.DisplayName == "The Invitator")
            {
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.None);
            }
            else
            {
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            }
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            _logger.LogInformation($"Done sending mail to {mail}");
        }
        
    }
}

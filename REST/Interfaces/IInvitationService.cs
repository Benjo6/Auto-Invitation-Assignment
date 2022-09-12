using Library.Models;

namespace REST.Interfaces
{
    public interface IInvitationService
    {
        Task SendEmailAsync(string firstname,string lastname, string mail);
        Task<string> GetInvitation(string firstname, string lastname, string mail);
        Task<string> GetInvitationWithInputtedIp(string firstname, string lastname, string mail,string ip);

    }
}

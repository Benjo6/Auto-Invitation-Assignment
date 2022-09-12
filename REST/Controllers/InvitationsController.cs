using Library.Genderize;
using Library.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using REST.Interfaces;
using System;
using System.Net.Http.Headers;

namespace REST.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationService _invitationService;


        public InvitationsController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }
        [HttpGet("GetInvitation")]
        public async Task<String> GetInvitation(string firstname, string lastname, string mail)
        {
            try
            {
                return await _invitationService.GetInvitation(firstname,lastname, mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        [HttpGet("GetInvitationWithInputtedIp")]
        public async Task<String> GetInvitationWithInputtedIp(string firstname,string lastname, string mail, string ip)
        {
            try
            {
                return await _invitationService.GetInvitationWithInputtedIp(firstname,lastname,mail,ip);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
        [HttpGet("SendEmailOfInvitation")]
        public async Task<IActionResult> SendEmail(string firstname, string lastname, string mail)
        {
            try
            {
                await _invitationService.SendEmailAsync(firstname,lastname,mail);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}

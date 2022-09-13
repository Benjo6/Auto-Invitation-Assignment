### The Task: 

Your dev team is responsible for automatically generating the content of the invitation mail.

Your solution must involve at least one SOAP and one REST services.

The invitation starts with the text: “Dear <title> <name>,” and then continues with the facts about
the meeting.
The <title> is one of the following:
- “Mr.” – for male recipients
- “Ms.” – for female recipients
- blank – for receivers with unknown gender.


I enabled all names, mails, and IP addresses to be written down rather than maintaining a hardcoded list of them. Every name is put via the Genderize API, which determines the gender of the name depending on nationality. The name will be categorized as having an unknown gender if there is less than a 90% chance that it belongs to one of the genders. Using your own IP address (or inputted IP) determines your nationality. Either use the method that checks your current IP, or input your IP. SOAP is employed to determine the IP's nationality. There is a dummy invitation attached to the project, and you can choose to receive an email (to email tester) or a string response to your REST request.

### The Methods:

<strong>REST:</strong>

GetInvitation(string firstname, string lastname, string mail) = gets the text-based response with the body text of the email based on your own IP

GetInvitationWithInputtedIp(string firstname,string lastname,string mail, string ip) = gets the text-based based response with the body text of the email based on the inputted IP

SendEmail(string firstname, string lastname, string mail) = sends an email based on the current IP.

<strong>SOAP:</strong>

InputtedIP(string ip) = checks the nationality inputted IP into the WCF Service

MyCurrentIP() = checks the nationality based on your own IP into the WCF Service


### How to run the program.

1. Clone the project

2. Make the start-up projects into both REST and SOAP otherwise will the REST request not be successful. (You can't get the IPs to check nationality without running the SOAP project)

3. If you want to try the SendEmail() and want to see if the program works then you have to go to this site: https://www.wpoven.com/tools/free-smtp-server-for-testing

4. In the input space you enter TheInvitator@legit.mail and the program should be successful.

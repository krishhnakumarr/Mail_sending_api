using Microsoft.AspNetCore.Mvc;

using System.Net.Http;
using System.Threading.Tasks;

namespace Mail_sending_api.Controllers
{
    public class MailController : Controller
    {

        private  HttpClient _httpClient;




        public MailController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("send")]
        public async Task<IActionResult> SendMail(
        [FromQuery] string to,
        [FromQuery] string cc,
        [FromQuery] string content,
        [FromQuery] string subject,
        [FromQuery] string from)
        {
            string baseUrl = "http://KIPL1012:8080/Mail/JavaMailOutside";
            string fullUrl = $"{baseUrl}?to={to}&cc={cc}&Content={content}&Subject={subject}&From={from}";

            try
            {
                var response = await _httpClient.GetAsync(fullUrl);
                if (response.IsSuccessStatusCode)
                {
                    return Ok("Mail sent successfully.");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Failed to send mail.");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }




    }
}

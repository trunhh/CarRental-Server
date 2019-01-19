

using Microsoft.AspNetCore.Mvc;
using MimeKit;

using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
namespace TemplateWebApiPhucThinh.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {

        [HttpGet]
        [Route("sendMail")]
        public  IActionResult sendMail(string email ,string contentBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress( "Phuc Thinh", "nguyenphucthinhdh15dtb@gmail.com"));
            message.To.Add(new MailboxAddress("chao ban", email));
            message.Subject = "Cho thue Xe";
            message.Body = new TextPart("plain")
            {
                Text = contentBody
            };
            using (var client =new SmtpClient())
            {
                 client.Timeout = 1000 * 20;
                client.Connect("smtp.gmail.com",587,true);
                client.Authenticate("nguyenphucthinhdh15dtb@gmail.com", "thinh1997");
                client.Send(message);
                client.Disconnect(true);
            }
                return Ok("ok");
        }
        [HttpGet]
        [Route("sendMailCanhBaoHetHan/listEmail")]
        public IActionResult sendMailCanhBaoHetHan(string listEmail)
        {
            string[] words = listEmail.Split('*');
            var message = new MimeMessage();
           
            message.From.Add(new MailboxAddress("Phuc Thinh", "nguyenphucthinhdh15dtb@gmail.com"));
            foreach (var item in words)
            {
                message.To.Add(new MailboxAddress("canh bao gan het han goi cho thue xe", item));
            }
           
            message.Subject = "Cho thue Xe";
            message.Body = new TextPart("plain")
            {
                Text = "Chao cac ban .cong ty chung tao xin thong bao gan het han ngay gia hang goi thue xe"
            };
            using (var client = new SmtpClient())
            {
                 client.Timeout = 1000 * 20;
                client.Connect("smtp.gmail.com", 587, true);
                client.Authenticate("nguyenphucthinhdh15dtb@gmail.com", "thinh1997");
                client.Send(message);
                client.Disconnect(true);
            }
            return Ok("ok");
        }

    }
}

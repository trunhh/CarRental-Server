using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using TemplateWebApiPhucThinh.Data.ModelAppID;
using System.Linq;
using System.Net;
using TemplateWebApiPhucThinh.Repository.IRepository;
using TemplateWebApiPhucThinh.Data.Model;

namespace TemplateWebApiPhucThinh.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppIDController :ControllerBase
    {
     private readonly IHttpClientFactory _httpClientFactory;
     private const string tenantId ="d5b87848-1b6b-4b73-9f62-bd230025746c";
     private const string keySecret ="MTFlODVmOWQtODJlOS00NDZlLWI2YTEtMGRlMGE3NTA4YjA4";
     private const string clientId ="de62b59a-4f31-4580-b853-165a4ebf3a64";
    private readonly IPartnerRepository _repository;
    
    public AppIDController(IHttpClientFactory httpClientFactory,IPartnerRepository repository)
    {
        _httpClientFactory = httpClientFactory;
        _repository=repository;
    }
     
     private string getIAM()
     {
         var client = _httpClientFactory.CreateClient("appID");
  
        var content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("grant_type", "urn:ibm:params:oauth:grant-type:apikey"),
             new KeyValuePair<string, string>("apikey", "aIr9I1oFfXQTYk_6p4ddNVe8LB4wbO70YPCmADnfkAmx")
            
        });
        var result = client.PostAsync("identity/token", content).Result;
        string resultContent = result.Content.ReadAsStringAsync().Result;
        JObject json = JObject.Parse(resultContent);
        var a=JsonConvert.SerializeObject(json);
        var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<IAM>(a);
        return obj.access_token;
     }


    [Route("RegisterAccount")]
    [HttpPost]
    public async Task<IActionResult> RegisterAccount([FromBody] AccountRegister account)
    {
      
         var client = _httpClientFactory.CreateClient("appIDUser");
         client.DefaultRequestHeaders.Add("Authorization","Bearer "+getIAM());
         
         var myContent = JsonConvert.SerializeObject(account);

        var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        var byteContent = new ByteArrayContent(buffer);
        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        var content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("grant_type", "urn:ibm:params:oauth:grant-type:apikey"),
             new KeyValuePair<string, string>("apikey", "aIr9I1oFfXQTYk_6p4ddNVe8LB4wbO70YPCmADnfkAmx")
            
        });
        var result = client.PostAsync("management/v4/"+tenantId+"/cloud_directory/sign_up", byteContent).Result;
        string resultContent = result.Content.ReadAsStringAsync().Result;
        if(resultContent.Contains(account.emails[0].value)){
        Partner partnerRegister=new Partner();
        partnerRegister.Email=account.emails[0].value;
        partnerRegister.Id=Guid.NewGuid()+"";
        partnerRegister.IsDelete=false;
        partnerRegister.NameCompany=account.userName;
       _repository.Create(partnerRegister);
        return Ok("Dang ky thanh cong "+account.emails[0].value);
        }
       
        return Ok(resultContent);

        
    }
    [Route("LoginAccount")]
    [HttpPost]
    public async Task<IActionResult> LoginAccount([FromBody] UserLogin account)
    {
           
      
         var client = _httpClientFactory.CreateClient("appIDUserLogin");
         // client.DefaultRequestHeaders.Add("Authorization","Basic ZGU2MmI1OWEtNGYzMS00NTgwLWI4NTMtMTY1YTRlYmYzYTY0Ok1URmxPRFZtT1dRdE9ESmxPUzAwTkRabExXSTJZVEV0TUdSbE1HRTNOVEE0WWpBNA==");
         //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "ZGU2MmI1OWEtNGYzMS00NTgwLWI4NTMtMTY1YTRlYmYzYTY0Ok1URmxPRFZtT1dRdE9ESmxPUzAwTkRabExXSTJZVEV0TUdSbE1HRTNOVEE0WWpBNA==");
       
       client.DefaultRequestHeaders.Authorization = 
    new AuthenticationHeaderValue(
        "Basic", 
        Convert.ToBase64String(
            System.Text.ASCIIEncoding.ASCII.GetBytes(
                string.Format("{0}:{1}", clientId, keySecret))));
        var content = new FormUrlEncodedContent(new[]
        {
             new KeyValuePair<string, string>("grant_type", "password"),
             new KeyValuePair<string, string>("username", account.UserName),
            new KeyValuePair<string, string>("password", account.password)
        });
        var result = client.PostAsync("oauth/v3/"+tenantId+"/token", content).Result;
        string resultContent = result.Content.ReadAsStringAsync().Result;
       
       
       JObject json = JObject.Parse(resultContent);
        var a=JsonConvert.SerializeObject(json);
        var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenLogin>(a);



        return Ok(obj);
    
        
    }


    [Route("Test")]
    [HttpPost]
    public async Task<IActionResult> Test([FromBody] UserLogin account)
    {
        return Ok(account);
    }
    [Route("Test1")]
    [HttpGet]
    public IActionResult Test1()
    {
        string accessToken = User.FindFirst("access").Value;
         return Forbid();
    }

    [Authorize()]
    [Route("TestAuthor")]
    [HttpGet]
    public IActionResult TestAuthor()
    {
        
        var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
        if(claims.ContainsKey("email")){
        if(claims["email"].Equals("tmdt2019@gmail.com"))
            return Ok("ok");
        }else{
            return Forbid();
        }
      return Forbid();
    }




    }
}

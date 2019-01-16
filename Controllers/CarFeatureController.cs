using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReflectionIT.Mvc.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TemplateWebApiPhucThinh.Data.Model;

using TemplateWebApiPhucThinh.ModelValidation;
using TemplateWebApiPhucThinh.Repository.IRepository;

namespace TemplateWebApiPhucThinh.Controllers
{
    [Authorize()]
    [Route("[controller]")]
    [ApiController]
    public class CarFeatureController : ControllerBase
    {
        //ICustomerRepository CustomerRepository;
        //IMapper Mapper;
        //IConfiguration Configuration;
        private readonly ICarFeatureRepository _repository;

        public CarFeatureController(ICarFeatureRepository repository)
        {
            _repository = repository;
        }

        
        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] CarFeature CarFeature)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") )
                return Forbid();
            }
            CarFeature.Id = Guid.NewGuid() + "";
            CarFeature.IsDelete=false;
            return Ok(_repository.Create(CarFeature));
        }
     

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(string id)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") )
                return Forbid();
            }
            return Ok(_repository.GetById(id));
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") )
                return Forbid();
            }
            return Ok(_repository.Delete(id));
        }
        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult Update(string id, [FromBody] CarFeature CarFeature)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") )
                return Forbid();
            }
            return Ok(_repository.Update(id, CarFeature));
        }
        [HttpGet]
        [Route("Paging/pagesize/pageNow")]
        public IActionResult Paging(int pagesize, int pageNow)
        {
            
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") )
                return Forbid();
            }
            return Ok(_repository.Paging(pagesize, pageNow, "color"));

        }

         [HttpGet]
        [Route("CountOfPaging/pagesize/pageNow")]
        public IActionResult CountOfPaging(int pagesize, int pageNow)
        {
            
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") )
                return Forbid();
            }
            return Ok(_repository.CountOfPaging(pagesize, pageNow));

        }
         [HttpGet]
        [Route("CountAll/pagesize/pageNow")]
        public IActionResult CountAll(int pagesize, int pageNow)
        {
            
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") )
                return Forbid();
            }
            return Ok(_repository.CountAll());

        }
         
        [HttpDelete]
        [Route("DeleteEnable/{id}")]
        public IActionResult DeleteEnable(string id)
        {

            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") )
                return Forbid();
            }
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            return Ok(_repository.DeleteEnable(id));
        }
    }
}

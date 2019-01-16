using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReflectionIT.Mvc.Paging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TemplateWebApiPhucThinh.Data.Model;
using Microsoft.AspNetCore.Authorization;
using TemplateWebApiPhucThinh.ModelValidation;
using TemplateWebApiPhucThinh.Repository.IRepository;

namespace TemplateWebApiPhucThinh.Controllers
{
    [Authorize()]
    [Route("[controller]")]
    [ApiController]
    public class PartnerTenantPackageController : ControllerBase
    {
        //ICustomerRepository CustomerRepository;
        //IMapper Mapper;
        //IConfiguration Configuration;
        private readonly IPartnerTenantPackageRepository _repository;

        public PartnerTenantPackageController(IPartnerTenantPackageRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] PartnerTenantPackage PartnerTenantPackage)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    PartnerTenantPackage.Id = Guid.NewGuid() + "";
                    PartnerTenantPackage.IsDelete = false;
                    return Ok(_repository.Create(PartnerTenantPackage));
                }
            }else{
                return Forbid();
            }
                return Forbid();
            
        }


        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult GetById(string id)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    return Ok(_repository.GetById(id));
                }
            }else{
                return Forbid();
            }
                return Forbid();
            
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                     return Ok(_repository.Delete(id));
                }
            }else{
                return Forbid();
            }
                return Forbid();
           
        }

        [HttpPut]
        [Route("Update/{id}")]
        public IActionResult Update(string id, [FromBody] PartnerTenantPackage PartnerTenantPackage)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                     return Ok(_repository.Update(id, PartnerTenantPackage));
                }
            }else{
                return Forbid();
            }
                return Forbid();
            
        }


        [HttpGet]
        [Route("Paging/pagesize/pageNow")]
        public IActionResult Paging(int pagesize, int pageNow)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    return Ok(_repository.Paging(pagesize, pageNow, "color"));
                }
            }else{
                return Forbid();
            }
                return Forbid();

        }

        [HttpGet]
        [Route("CountOfPaging/pagesize/pageNow")]
        public IActionResult CountOfPaging(int pagesize, int pageNow)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    return Ok(_repository.CountOfPaging(pagesize, pageNow));
                }
            }else{
                return Forbid();
            }
                return Forbid();


        }
        [HttpGet]
        [Route("CountAll/pagesize/pageNow")]
        public IActionResult CountAll(int pagesize, int pageNow)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    return Ok(_repository.CountAll());
                }
            }else{
                return Forbid();
            }
                return Forbid();



        }
        [HttpDelete]
        [Route("DeleteEnable/{id}")]
        public IActionResult DeleteEnable(string id)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        return BadRequest();
                    }
                    return Ok(_repository.DeleteEnable(id));
                }
            }else{
                return Forbid();
            }
                return Forbid();
        }

        [HttpGet]
        [Route("PagingCondition/pagesize/pageNow/condition")]
        public IActionResult PagingCondition(int pagesize, int pageNow, string condition)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    return Ok(_repository.PagingCondition(pagesize, pageNow, condition));
                }
            }else{
                return Forbid();
            }
                return Forbid();

        }

        [HttpGet]
        [Route("CountCondition/condition")]
        public IActionResult CountCondition(string condition)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    return Ok(_repository.CountCondition(condition));
                }
            }else{
                return Forbid();
            }
                return Forbid();
        }


        [HttpGet]
        [Route("PagingConditionPrice/condition/pageIndex/pageSize/sortOrder/priceStart/priceEnd")]
        public IActionResult PagingConditionPrice(string condition, int pageIndex, int pageSize, string sortOrder, int priceStart, int priceEnd)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    return Ok(_repository.PagingConditionPrice(condition, pageIndex, pageSize, sortOrder, priceStart, priceEnd));
                }
            }else{
                return Forbid();
            }
                return Forbid();

        }
    }
}

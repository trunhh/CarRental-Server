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
    public class OrdersController : ControllerBase
    {
        //ICustomerRepository CustomerRepository;
        //IMapper Mapper;
        //IConfiguration Configuration;
        private readonly IOrdersRepository _repository;
        ChoThueXeContext context =new ChoThueXeContext();
        public OrdersController(IOrdersRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("Create")]
        public IActionResult Create([FromBody] Orders Orders)
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    Orders.Id = Guid.NewGuid() + "";
                    Orders.DateOrder=DateTime.Now.ToString("dd/MM/yyyy");
                    Orders.IsDelete=false;
                    return Ok(_repository.Create(Orders));
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
        public IActionResult Update(string id, [FromBody] Orders Orders)
        {
             var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                     return Ok(_repository.Update(id, Orders));
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
        public IActionResult PagingCondition(int pagesize, int pageNow,string condition)
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
                     return Ok(_repository.PagingConditionPrice( condition,  pageIndex,  pageSize,  sortOrder,  priceStart,  priceEnd));
                }
            }else{
                return Forbid();
            }
                return Forbid();
            
        }
         [HttpGet]
        [Route("PagingConditionGetByEmail/pagesize/pageNow/condition")]
        public IActionResult PagingConditionGetByEmail(int pagesize, int pageNow,string condition)
        {
            if(String.IsNullOrEmpty(condition)){
                condition="";
            }
             var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( !claims["name"].Equals("ADMIN") || !claims["name"].Equals("MANAGER") ){
                 var list = (from _car in context.Car
                 join _partnerCar in context.PartnerCar on _car.Id equals _partnerCar.IsCar
                 join _partner in context.Partner on _partnerCar.IdPartner equals _partner.Id
                 join _order in context.Orders on _car.Id equals _order.NameCar
                where _car.IsDelete==false
                where _partner.Email==claims["email"]
                where _car.Name.Contains(condition)
                 select _order).OrderByDescending(x => x.PriceOrder).Skip((pageNow - 1) * pagesize).Take(pagesize).ToList();
                   return Ok(list);
                    }
                }
             return Forbid();
    }
}
}

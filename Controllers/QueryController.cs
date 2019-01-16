using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TemplateWebApiPhucThinh.Data.Model;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
namespace TemplateWebApiPhucThinh.Controllers
{
    
    [Route("[controller]")]
    [ApiController]

    public class QueryController : ControllerBase
    {
        ChoThueXeContext context = new ChoThueXeContext();

        [Authorize()]
        [HttpGet]
        [Route("LayDanhSachGanHetHan")]
        public IActionResult LayDanhSachGanHetHan()
        {
            var claims = User.Claims.Select(claim => new { claim.Type, claim.Value }).ToDictionary( t => t.Type, t => t.Value);
            if(claims.ContainsKey("name")){
                if( claims["name"].Equals("ADMIN") || claims["name"].Equals("MANAGER") ){
                    DateTime today = System.DateTime.Now;
                    var List =
                        (from _partner in context.Partner
                        join _partnerTenantPackage in context.PartnerTenantPackage on _partner.Id equals _partnerTenantPackage.Id
                        join _package in context.Package on _partnerTenantPackage.IdPackage equals _package.Id
                        select new
                        {
                            idPackage= _partnerTenantPackage.IdPackage,
                            NameParnert = _partner.NameCompany,
                            Email = _partner.Email,
                            DayHetHan =(_partnerTenantPackage.DateTenant.AddDays(Int32.Parse(_package.Name))-today).Days,
                        }).Where(s=>s.DayHetHan<=10).ToList();
                    return Ok(List);
                }
            }else{
                return Forbid();
            }
                return Forbid();
        }

        [HttpGet]
        [Route("FilterLocationAndDateDontBusy")]
        public IActionResult LocThanhPhoVaNgayXeRanh(string location ,string dateStart, string dateEnd)
        {
            if(String.IsNullOrWhiteSpace(location)){
                DateTime dateRental = Convert.ToDateTime(dateStart);
                DateTime dateReturn = Convert.ToDateTime(dateEnd);

                var listCarAutoDontBusy = (
                    from _car in context.Car 
                    where !( 
                        from _orders in context.Orders select _orders.NameCar 
                    ).Contains(_car.Id) select _car
                ).Distinct().ToList();
                // SELECT * from Car c WHERE c.Id NOT IN (SELECT od.nameCar from Orders od )

                var ListCar =
                    (from _partner in context.Partner
                    join _partnerCar in context.PartnerCar on _partner.Id equals _partnerCar.IdPartner
                    join _car in context.Car on _partnerCar.IsCar equals _car.Id
                    // join _location in context.Location on _partner.Id equals _location.Country
                    join _order in context.Orders on _car.Id equals _order.NameCar
                    // where _location.City == location
                    where _order.CarReturnDay < dateRental || _order.DateOfhire > dateReturn
                    select _car
                    ).Distinct().ToList();
            
                // SELECT  od.dateOfHire, od.payDate from Partner p 	
                //     JOIN Partner_Car pc on pc.IdPartner = p.Id
                // 	JOIN Car c ON c.id = pc.IsCar
                // 	JOIN Orders od ON od.nameCar = c.Id
                // 	WHERE  od.payDate < '2019-01-06' or od.dateOfHire > '2019-01-08'

                List<Car> array = new List<Car>();

                foreach(var item in ListCar){
                    array.Add(item);
                }

                foreach(var item in listCarAutoDontBusy){
                    array.Add(item);
                }
                return Ok(array);
            }else{
                DateTime dateRental = Convert.ToDateTime(dateStart);
                DateTime dateReturn = Convert.ToDateTime(dateEnd);

                var listCarAutoDontBusy = (
                    from _partner in context.Partner
                    join _partnerCar in context.PartnerCar on _partner.Id equals _partnerCar.IdPartner
                    join _car in context.Car on _partnerCar.IsCar equals _car.Id
                    join _location in context.Location on _partner.Id equals _location.Id
                    where !( 
                        from _orders in context.Orders select _orders.NameCar 
                    ).Contains(_car.Id)
                    where _location.City == location
                    
                    select _car
                ).Distinct().ToList();
                // SELECT * from Car c WHERE c.Id NOT IN (SELECT od.nameCar from Orders od )

                var ListCar =
                    (from _partner in context.Partner
                    join _partnerCar in context.PartnerCar on _partner.Id equals _partnerCar.IdPartner
                    join _car in context.Car on _partnerCar.IsCar equals _car.Id
                    join _location in context.Location on _partner.Id equals _location.Id
                    join _order in context.Orders on _car.Id equals _order.NameCar
                    where _location.City == location
                    where _order.CarReturnDay < dateRental || _order.DateOfhire > dateReturn
                    select _car
                    ).Distinct().ToList();
            
                // SELECT  od.dateOfHire, od.payDate from Partner p 	
                //     JOIN Partner_Car pc on pc.IdPartner = p.Id
                // 	JOIN Car c ON c.id = pc.IsCar
                // 	JOIN Orders od ON od.nameCar = c.Id
                // 	WHERE  od.payDate < '2019-01-06' or od.dateOfHire > '2019-01-08'

                List<Car> array = new List<Car>();

                foreach(var item in ListCar){
                    array.Add(item);
                }

                foreach(var item in listCarAutoDontBusy){
                    array.Add(item);
                }
                return Ok(array );
            }
            
        }

        [HttpGet]
        [Route("CheckDatHang")]
         public IActionResult CheckDatHang(string dateStart, string dateEnd)
        {
                DateTime dateRental = Convert.ToDateTime(dateStart);
                DateTime dateReturn = Convert.ToDateTime(dateEnd);

                var listCarAutoDontBusy = (
                    from _car in context.Car 
                    where !( 
                        from _orders in context.Orders select _orders.NameCar 
                    ).Contains(_car.Id) select _car
                ).Distinct().ToList();
                // SELECT * from Car c WHERE c.Id NOT IN (SELECT od.nameCar from Orders od )

                var ListCar =
                    (from _partner in context.Partner
                    join _partnerCar in context.PartnerCar on _partner.Id equals _partnerCar.IdPartner
                    join _car in context.Car on _partnerCar.IsCar equals _car.Id
                    // join _location in context.Location on _partner.Id equals _location.Country
                    join _order in context.Orders on _car.Id equals _order.NameCar
                    // where _location.City == location
                    where _order.CarReturnDay < dateRental || _order.DateOfhire > dateReturn
                    select _car
                    ).Distinct().ToList();
            
                // SELECT  od.dateOfHire, od.payDate from Partner p 	
                //     JOIN Partner_Car pc on pc.IdPartner = p.Id
                // 	JOIN Car c ON c.id = pc.IsCar
                // 	JOIN Orders od ON od.nameCar = c.Id
                // 	WHERE  od.payDate < '2019-01-06' or od.dateOfHire > '2019-01-08'

               if(listCarAutoDontBusy.Count>0 || ListCar.Count>0){
                   return Ok(true);
               }
                return Ok(false);
        }
    }
}

     
   
        
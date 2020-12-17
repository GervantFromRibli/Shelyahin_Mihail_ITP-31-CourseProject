using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab6.Models;
using Lab6.ViewModels;

namespace Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaybillsController : ControllerBase
    {
        private readonly ApplicationContext db;

        public WaybillsController(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
        // GET: api/<WaybillsController>
        [HttpGet]
        public IEnumerable<WaybillViewModel> Get()
        {
            var waybills = db.Waybills.ToList();
            List<WaybillViewModel> waybillViewModels = new List<WaybillViewModel>();
            foreach (var waybill in waybills)
            {
                waybillViewModels.Add(new WaybillViewModel
                {
                    Id = waybill.Id,
                    Weight = waybill.Weight,
                    DateOfSupply = waybill.DateOfSupply,
                    EmployeeFIO = db.Employees.Where(item => item.Id == waybill.EmployeeId).First().FIO,
                    FurnitureName = db.Furniture.Where(item => item.Id == waybill.FurnitureId).First().Name,
                    Material = waybill.Material,
                    Price = waybill.Price,
                    ProviderId = waybill.ProviderId,
                    ProviderName = waybill.ProviderName
                });
            }
            return waybillViewModels;
        }

        // GET api/<WaybillsController>/5
        [HttpGet("{id}")]
        public Waybill Get(int id)
        {
            return db.Waybills.Where(item => item.Id == id).First();
        }

        // GET api/values
        [HttpGet("empl")]
        public IEnumerable<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        // GET api/values
        [HttpGet("furnit")]
        public IEnumerable<Furniture> GetFurniture()
        {
            return db.Furniture.ToList();
        }

        // POST api/<WaybillsController>
        [HttpPost]
        public IActionResult Post([FromBody] Waybill model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            model.Id = db.Waybills.Select(item => item.Id).Max() + 1;
            db.Waybills.Add(model);
            db.SaveChanges();
            return Ok(model);
        }

        // PUT api/<WaybillsController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Waybill waybill)
        {
            if (waybill == null)
            {
                return BadRequest();
            }
            Waybill changeBill = db.Waybills.Where(item => item.Id == waybill.Id).First();
            changeBill.DateOfSupply = waybill.DateOfSupply;
            changeBill.EmployeeId = waybill.EmployeeId;
            changeBill.FurnitureId = waybill.FurnitureId;
            changeBill.Material = waybill.Material;
            changeBill.Price = waybill.Price;
            changeBill.ProviderId = waybill.ProviderId;
            changeBill.ProviderName = waybill.ProviderName;
            changeBill.Weight = waybill.Weight;
            db.SaveChanges();
            return Ok(waybill);
        }

        // DELETE api/<WaybillsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id = -1)
        {
            if (id == -1)
            {
                return BadRequest();
            }
            Waybill waybill = db.Waybills.Where(item => item.Id == id).First();
            db.Waybills.Remove(waybill);
            db.SaveChanges();
            return Ok(id);
        }
    }
}

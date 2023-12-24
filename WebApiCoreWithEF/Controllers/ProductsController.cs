
namespace WebApiCoreWithEF.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using WebApiCoreWithEF.Context;
    using WebApiCoreWithEF.Models;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private NorthwindContext _companyContext;

        public ProductsController(NorthwindContext companyContext)
        {
            _companyContext = companyContext;
        }

        // GET: api/<EmployeeController>
        [HttpGet]
        public IEnumerable<Products> Get()
        {
            return _companyContext.Products;
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public Products Get(int id)
        {
            return _companyContext.Products.FirstOrDefault(s => s.ProductId == id);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] Products value)
        {
            _companyContext.Products.Add(value);
            _companyContext.SaveChanges();
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Products value)
        {
            var employee = _companyContext.Products.FirstOrDefault(s => s.ProductId == id);
            if (employee != null)
            {
                _companyContext.Entry<Products>(employee).CurrentValues.SetValues(value);
                _companyContext.SaveChanges();
            }
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var student = _companyContext.Products.FirstOrDefault(s => s.ProductId == id);
            if (student != null)
            {
                _companyContext.Products.Remove(student);
                _companyContext.SaveChanges();
            }
        }
    }
}

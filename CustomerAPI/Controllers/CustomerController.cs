using CustomerAPI.Data;
using CustomerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerAPIDbContext dbContext;

        public CustomerController(CustomerAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer()
        {
            return Ok(await dbContext.Customer.ToListAsync());
           
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetCustomer([FromRoute] Guid id)
        {
            var customer = await dbContext.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(AddCustomerRequest addCustomerRequest)
        {
            var customer = new Customer()
            {
                Id = Guid.NewGuid(),
                Name = addCustomerRequest.Name,
                Phone = addCustomerRequest.Phone,
                Address = addCustomerRequest.Address
            };

           await dbContext.Customer.AddAsync(customer);
            await dbContext.SaveChangesAsync();

            return Ok(customer);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, UpdateCustomerRequest updateCustomerRequest )
        {
            var customer = await dbContext.Customer.FindAsync(id);

            if (customer != null)
            {
                customer.Name = updateCustomerRequest.Name;
                customer.Phone = updateCustomerRequest.Phone;
                customer.Address = updateCustomerRequest.Address;

                await dbContext.SaveChangesAsync();

                return Ok(customer);
            }

            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var customer =  await dbContext.Customer.FindAsync(id);

            if (customer != null)
            {
                dbContext.Remove(customer);
                await dbContext.SaveChangesAsync();
                return Ok(customer);
            }

            return NotFound();
        }
    }
}

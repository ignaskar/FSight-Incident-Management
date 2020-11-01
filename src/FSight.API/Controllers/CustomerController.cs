using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FSight.Core.Entities;
using FSight.Core.Interfaces;
using FSight.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace FSight.API.Controllers
{
    public class CustomerController : BaseApiController
    {
        private readonly IGenericRepository<Customer> _customerRepo;

        public CustomerController(IGenericRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo ?? throw new ArgumentNullException(nameof(customerRepo));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Customer>>> GetAllCustomers()
        {
            var spec = new CustomersWithTicketsAndCommentsSpecification();
            
            return Ok(await _customerRepo.ListAsync(spec));
        }
        
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            return Ok(await _customerRepo.GetEntityById(id));
        }

        [HttpPost]
        public ActionResult<Customer> CreateCustomer([FromBody] Customer customer)
        {
            _customerRepo.Add(customer);

            return CreatedAtRoute("GetCustomer", new {id = customer.Id}, customer);
        }
    }
}
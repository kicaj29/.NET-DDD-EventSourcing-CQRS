using AppointmentScheduling.Core.Model.Events;
using AppointmentScheduling.Core.Services;
using FrontDesk.SharedKernel;
using FrontDesk.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace vet_clinic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {                        
            /*var appointmentConfirmedEvent = new AppointmentConfirmedEvent(null);
            DomainEvents.Raise(appointmentConfirmedEvent);
            Debug.WriteLine(DomainEvents.Container.WhatDidIScan());
            Debug.WriteLine(DomainEvents.Container.WhatDoIHave());

            var handlers = DomainEvents.Container.GetAllInstances<IHandle<AppointmentConfirmedEvent>>();
            var handlers1 = DomainEvents.Container.GetAllInstances<EmailConfirmationHandler>();
            var handlers2 = DomainEvents.Container.GetAllInstances(typeof(IHandle<AppointmentConfirmedEvent>));
            var handlers3 = DomainEvents.Container.GetAllInstances(typeof(EmailConfirmationHandler));
            var handlers4 = DomainEvents.Container.GetAllInstances(typeof(IHandle<>));
            */

            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

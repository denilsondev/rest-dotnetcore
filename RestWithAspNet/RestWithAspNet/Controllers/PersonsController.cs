﻿using Microsoft.AspNetCore.Mvc;
using RestWithAspNet.Model;
using RestWithAspNet.Services;

namespace RestWithAspNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;

        }
        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NoContent();
            return Ok(person);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personService.Create(person));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personService.Update(person));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}

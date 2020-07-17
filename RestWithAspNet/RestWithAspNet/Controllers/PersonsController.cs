using Microsoft.AspNetCore.Mvc;
using RestWithAspNet.Model;
using RestWithAspNet.Business;
using RestWithAspNet.Data.VO;
using Tapioca.HATEOAS;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RestWithAspNet.Controllers
{
    
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IPersonBusiness _personBusiness;

        public PersonsController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;

        }
        
        [HttpGet]
        [SwaggerResponse((200), Type = typeof(List<PersonVO>))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [Authorize("Bearer")]
        //[Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult Get()
        {
            return Ok(_personBusiness.FindAll());
        }

        
        [HttpGet("{id}")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult<string> Get(long id)
        {
            var person = _personBusiness.FindById(id);
            if (person == null) return NoContent();
            return Ok(person);
        }


        [HttpGet("get-by-name")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public ActionResult<string> GetByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var person = _personBusiness.FindByName(firstName, lastName);
            if (person == null) return NoContent();
            return Ok(person);
        }


        [HttpPost]
        [Authorize("Bearer")]
        [SwaggerResponse((202), Type = typeof(PersonVO))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusiness.Create(person));
        }

        
        [HttpPut("{id}")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put(int id, [FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            person.Id = id;
            return new ObjectResult(_personBusiness.Update(person));
        }


        [HttpPatch("{id}")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch(int id, [FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            person.Id = id;
            return new ObjectResult(_personBusiness.Update(person));
        }


        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [SwaggerResponse((200), Type = typeof(PersonVO))]
        [SwaggerResponse((204))]
        [SwaggerResponse((400))]
        [SwaggerResponse((401))]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _personBusiness.Delete(id);
            return NoContent();
        }
    }
}

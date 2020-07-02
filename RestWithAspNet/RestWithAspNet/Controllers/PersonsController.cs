using Microsoft.AspNetCore.Mvc;
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
        
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        
        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NoContent();
            return Ok(person);
        }

        
        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personService.Create(person));
        }

        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Person person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personService.Update(person));
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}

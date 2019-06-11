using DatingApp.API.Models;
using DatingApp.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ValueService _valueService;

        public ValuesController(ValueService valueService)
        {
            _valueService = valueService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<Value>> Get() => _valueService.Get();

        // GET api/values/5
        [HttpGet("{id:length(24)}", Name = "Getvalue")]
        public ActionResult<Value> Get(string id)
        {
            var value = _valueService.Get(id);

            if (value == null)
            {
                return NotFound();
            }

            return value;
        }

        // POST api/values
        [HttpPost]
        public ActionResult<Value> Post([FromBody] Value value)
        {
            _valueService.Create(value);

            return CreatedAtRoute("Getvalue", new { id = value.Id.ToString() }, value);
        }

        // PUT api/values/5
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody] Value valueIn)
        {
            var value = _valueService.Get(id);

            if (value == null)
            {
                return NotFound();
            }

            _valueService.Update(id, valueIn);

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var value = _valueService.Get(id);

            if (value == null)
            {
                return NotFound();
            }

            _valueService.Remove(value.Id);

            return NoContent();
        }
    }
}

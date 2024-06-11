using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaceRentalApp.API.Models;

namespace PlaceRentalApp.API.Controllers
{
    [Route("api/v1/places")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        // GET api/places
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/v1/places/1234
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        // POST api/v1/places
        [HttpPost]
        public IActionResult Post(CreatePlaceInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new {id = 1}, model);
        }

        // PUT api/v1/places/12234
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdatePlaceInputModel model)
        {
            return NoContent();
        }

        // POST api/v1/places/1234
        [HttpPut("{id}/amenities")]
        public IActionResult PostAmenity (int id, CreatePlaceAmenityInputModel model)
        {
            return NoContent();
        }

        // DELETE api/v1/places/1234
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
            return NoContent();
        }

        // POST api/v1/places/1234/books
        [HttpPost("{id}/books")]
        public  IActionResult PostBook(int id, CreateBookInputModel model)
        {
            return NoContent();
        }

        // POST api/v1/places/1234/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment (int id, CreateCommentInputModel model)
        {
            return NoContent();
        }
    }
}

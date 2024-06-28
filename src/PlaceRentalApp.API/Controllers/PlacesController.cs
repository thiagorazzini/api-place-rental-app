using Microsoft.AspNetCore.Mvc;
using PlaceRentalApp.API.Entities;
using PlaceRentalApp.API.Models;
using PlaceRentalApp.API.Persistence;
using PlaceRentalApp.API.ValueObjects;

namespace PlaceRentalApp.API.Controllers
{
    [Route("api/v1/places")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly PlaceRentalDbContext _context;
        public PlacesController(PlaceRentalDbContext context)
        {
            _context = context;
        }

        // GET api/places
        [HttpGet]
        public IActionResult Get(string search, DateTime startDate, DateTime endDate)
        {
            var availablePlaces = _context
                .Places
                .Where(p => p.Title.Contains(search) && !p.IsDeleted &&
                !p.Books.Any(b =>
                (startDate >= b.StartDate && startDate <= b.EndDate)// dentro do intervalo da reserva existente
                || (endDate >= b.StartDate && endDate <= b.EndDate) // A data de término da pesquisa está dentro de uma reserva existente
                || (startDate <= b.StartDate && endDate >= b.EndDate) // A reserva existente está totalmente dentro do intervalo de datas pesquisado.
                ));

            return Ok(availablePlaces);
        }

        // GET api/v1/places/1234
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var place = _context.Places.SingleOrDefault(p => p.Id == id);

            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }

        // POST api/v1/places
        [HttpPost]
        public IActionResult Post(CreatePlaceInputModel model)
        {
            var address = new Address(
                model.Address.Street,
                model.Address.Number,
                model.Address.ZipCode,
                model.Address.District,
                model.Address.City,
                model.Address.State,
                model.Address.Country
                );

            var place = new Place(
                model.Title,
                model.Description,
                model.DailyPrice,
                address,
                model.AllowedNumberPerson,
                model.AllowPets,
                model.CreatedBy
                );

            _context.Places.Add(place);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = place.Id }, model);
        }

        // PUT api/v1/places/12234
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdatePlaceInputModel model)
        {
            var place = _context.Places.SingleOrDefault(p => p.Id == id);

            if (place == null)
            {
                return NotFound();
            }

            place.Update(model.Title, model.Description, model.DalyPlice);

            _context.Places.Update(place);
            _context.SaveChanges();

            return NoContent();
        }

        // POST api/v1/places/1234
        [HttpPut("{id}/amenities")]
        public IActionResult PostAmenity(int id, CreatePlaceAmenityInputModel model)
        {
            var placeExists = _context.Places.Any(p => p.Id == id);

            if (!placeExists)
            {
                return NotFound();
            }

            var amenity = new PlaceAmenity(model.Description, id);

            _context.PlaceAmenities.Add(amenity);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/v1/places/1234
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var place = _context.Places.SingleOrDefault(p => p.Id == id);

            if (place == null)
            {
                return NotFound();
            }

            place.SetAsDeleted();

            _context.Places.Update(place);
            _context.SaveChanges();

            return NoContent();
        }

        // POST api/v1/places/1234/books
        [HttpPost("{id}/books")]
        public IActionResult PostBook(int id, CreateBookInputModel model)
        {
            var exists = _context.Places.Any(p => p.Id == id);

            if (!exists)
            {
                return NotFound();
            }

            var book = new PlaceBook(model.IdUser, model.IdPlace, model.StartDate, model.EndDate, model.Comment);


            _context.PlaceBooks.Add(book);
            _context.SaveChanges();

            return NoContent();
        }

        // POST api/v1/places/1234/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateCommentInputModel model)
        {
            var place = _context.Places.SingleOrDefault(p => p.Id == id);

            if (place == null)
            {
                return NotFound();
            }
            var comments = new Comment(model.IdUser, model.Comments);

            _context.Comments.Add(comments);
            _context.SaveChanges();

            return NoContent();
        }

        // GET api/v1/places/comments/1234
        [HttpGet("comments/id")]
        public IActionResult GetComment(int id)
        {
            var comment = _context.Comments.SingleOrDefault(p => p.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST api/places/1234/photos
        [HttpPost("{id}/photos")]
        public IActionResult PostPlacePhoto(int id, IFormFile file)
        {
            var description = $"File: {file.FileName}, Size: {file.Length}";

            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);

                var fileBytes = ms.ToArray();
                var base64 = Convert.ToBase64String(fileBytes);

                return Ok(new { description, base64 });
            }
        }
    }
}

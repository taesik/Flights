using Flights.Data;
using Flights.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Flights.Dtos;
using Flights.ReadModels;


namespace Flights.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly Entities _entities;


        public UserController(Entities entities)
        {
            _entities = entities;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Register(NewUserDto dto)
        {
            _entities.Users.Add(
                new User(
                    dto.Email,
                    dto.FirstName,
                    dto.LastName,
                    dto.Gender
                )
            );
            return CreatedAtAction(nameof(Find),
                new { email = dto.Email });
        }

        [HttpGet("{email}")]
        public ActionResult<PassengerRm> Find(string email)
        {
            var passenger = _entities.Users.FirstOrDefault(
                x => x.Email == email
            );

            if (passenger == null) return NotFound();
            var rm = new PassengerRm(
                passenger.Email,
                passenger.FirstName,
                passenger.LastName,
                passenger.Gender
            );

            return Ok(passenger);
        }
    }
}
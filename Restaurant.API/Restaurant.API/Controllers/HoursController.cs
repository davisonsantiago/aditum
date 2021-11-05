using Microsoft.AspNetCore.Mvc;
using Restaurant.API.Models.Filter;
using Restaurant.API.Repository.Interface;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoursController : ControllerBase
    {
        private readonly IRestaurantRepository _repository;

        public HoursController(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{hour}")]
        public IActionResult Get(string hour)
        {
            bool correctFormat = Regex.IsMatch(hour, @"\b([0-1][0-9]|2[0-3])\b:\b([0-5][0-9])\b$");

            if (!correctFormat)
            {
                return BadRequest("Formato da hora inválido. Favor informar hora no padrão HH:mm");
            }

            var result = _repository.Read(new RestaurantFilter { Hour = hour });

            if (result == null || result.Any())
            {
                return NotFound(); 
            }
            else
            {
                return Ok(result);
            }
        }
    }
}

using FruitApi.Models;
using FruitApi.Repository;
using FruitApi.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FruitApi.Controllers
{
    [ApiController]
    [Route("api/fruits")]
    public class FruitController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFruitRepository _fruitRepository;

        public FruitController(IHttpClientFactory httpClientFactory, IFruitRepository fruitRepository)
        {
            _httpClientFactory = httpClientFactory;
            _fruitRepository = fruitRepository;
           
        }
        [HttpPost("{fruitName}")]
        public async Task<IActionResult> CreateFruitFromApi(string fruitName)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var fruitFromDB = _fruitRepository.GetFruitByName(fruitName);

            if (fruitFromDB == null)
            {
                var requestUri = $"https://www.fruityvice.com/api/fruit/{fruitName}";

                HttpResponseMessage response;
                try
                {
                    response = await httpClient.GetAsync(requestUri);
                }
                catch (HttpRequestException ex)
                {
                    return StatusCode(503, $"Error contacting Fruityvice API: {ex.Message}"); //Issues with API connectivity
                }

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    throw new FruitNotFoundException(fruitName); //Throw exception if the fruit doesn't exist on the API
                }

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, $"Error: {response.ReasonPhrase}");
                }

                var jsonContent = await response.Content.ReadAsStringAsync();

                var fruit = JsonSerializer.Deserialize<Fruit>(jsonContent);

                if (fruit == null)
                {
                    return BadRequest("Error deserializing the fruit data.");
                }

                _fruitRepository.CreateFruit(fruit);

                return Ok(fruit);
            }
            else // If the fruit exists in the database, return it without making the API call
            {
                return Ok(fruitFromDB);
            }

        }

        [HttpGet("{name}")]
        public IActionResult GetFruitByName(string name)
        {
            try
            {
                var fruit = _fruitRepository.GetFruitByName(name);

                if (fruit == null)
                {
                    return NotFound($"Fruit with name '{name}' was not found in the database.");
                }

                return Ok(fruit);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/all")]
        public IActionResult GetAllFruits()
        {
            try
            {
                var fruits = _fruitRepository.GetAllFruits();

                if (fruits == null || !fruits.Any())
                {
                    return NotFound("No fruits found in the database.");
                }

                return Ok(fruits);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpDelete("{name}")]
        public IActionResult DeleteFruit(string name)
        {
            try
            {
                _fruitRepository.DeleteFruit(name);

                return NoContent(); 
            }
            catch (Exception ex)
            {
                
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{name}")]
        public IActionResult UpdateFruit(string name, string? family = null, string? order = null, string? genus = null, [FromBody] Nutritions? nutritions = null)
        {
            try
            {
                Fruit newFruit = new Fruit { family = family, order = order, genus = genus, nutritions = nutritions };
           

                Fruit fruit = _fruitRepository.UpdateFruit(name, newFruit);

                return Ok(fruit);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }



    }
}

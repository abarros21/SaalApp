using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseApp.Application.DTOs;
using WarehouseApp.Application.Interfaces;
using WarehouseApp.Domain;
using WarrehouseApp.Infrastructure.Data.DTOs;
using WarrehouseApp.Infrastructure.Data.Interfaces.SquarePrinter;
using WarrehouseApp.Infrastructure.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarehouseApp.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquareController(IApiSquarePrinter apiSquarePrinter, 
        ICalcShortestDistanceService calcShortestDistanceService,
        IDataService<List<Square>> dataService) : ControllerBase
    {
        private readonly IApiSquarePrinter _apiSquarePrinter = apiSquarePrinter;
        private readonly ICalcShortestDistanceService _calcShortestDistanceService = calcShortestDistanceService;
        private readonly IDataService<List<Square>> _dataService = dataService;

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return BadRequest("Key cannot be null or empty.");

            var squares = await _dataService.GetAsync(key);
            if (squares == null)
                return NotFound($"No found with key '{key}'");

            var result = _apiSquarePrinter.PrintAndReturnWarehouseDTO(key, squares);
            return Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var listsSquares = await _dataService.GetAllAsync();
            if (listsSquares == null || listsSquares.Count == 0)
                return NotFound("No squares found");

            var result = new List<WarehouseDto>();
            foreach (var squares in listsSquares)
            {
                result.Add(_apiSquarePrinter.PrintAndReturnWarehouseDTO(squares.Key, squares.Value));
            }
            return Ok(result);
        }

        [HttpPost("fromBody")]
        public async Task<IActionResult> PostFromBody([FromBody] WarehouseInputDto input)
        {          
            List<Square> squares = _calcShortestDistanceService.Execute(input);

            var key = Guid.NewGuid().ToString();

            await _dataService.SaveAsync(key, squares);

            var squareDtos = _apiSquarePrinter.PrintAndReturnWarehouseDTO(key, squares);
           
            return Ok(squareDtos);
        }

        [HttpPost("fromFile")]
        public async Task<IActionResult> PostFromFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "data.json");

            try
            {                
                var jsonContent = await System.IO.File.ReadAllTextAsync(filePath);
                var warehouseDto = JsonConvert.DeserializeObject<WarehouseInputDto>(jsonContent);
              
                List<Square> squares = _calcShortestDistanceService.Execute(warehouseDto);

                var key = Guid.NewGuid().ToString();

                await _dataService.SaveAsync(key, squares);

                var result = _apiSquarePrinter.PrintAndReturnWarehouseDTO(key, squares);

                return Ok(result);
            }
            catch (IOException ex)
            {
                return StatusCode(500, $"Error reading file: {ex.Message}");
            }
            catch (JsonException ex)
            {
                return BadRequest($"Deserialization error JSON: {ex.Message}");
            }
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteSquares(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return BadRequest("Key cannot be null or empty.");

            try
            {
                await _dataService.DeleteAsync(key);
                return Ok("Key deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting key: {ex.Message}");
            }
        }
    }
}

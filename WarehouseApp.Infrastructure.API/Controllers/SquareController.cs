using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WarehouseApp.Application.DTOs;
using WarehouseApp.Application.Interfaces;
using WarehouseApp.Domain;
using WarrehouseApp.Infrastructure.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarehouseApp.Infrastructure.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquareController(IApiSquarePrinter apiSquarePrinter, ICalcShortestDistanceService calcShortestDistanceService) : ControllerBase
    {
        private readonly IApiSquarePrinter _apiSquarePrinter = apiSquarePrinter;
        private readonly ICalcShortestDistanceService _calcShortestDistanceService = calcShortestDistanceService;

        [HttpPost("fromBody")]
        public IActionResult GetSquares([FromBody] WarehouseInputDto input)
        {          
            List<Square> squares = _calcShortestDistanceService.Execute(input);
            var squareDtos = _apiSquarePrinter.PrintAndReturnSquareDtos(squares);

            return Ok(squareDtos);
        }

        [HttpPost("fromFile")]
        public async Task<IActionResult> GetSquaresFromFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "data.json");

            try
            {                
                var jsonContent = await System.IO.File.ReadAllTextAsync(filePath);
                var warehouseDto = JsonConvert.DeserializeObject<WarehouseInputDto>(jsonContent);
              
                List<Square> squares = _calcShortestDistanceService.Execute(warehouseDto);
                var squareDtos = _apiSquarePrinter.PrintAndReturnSquareDtos(squares);

                return Ok(squareDtos);
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
    }
}

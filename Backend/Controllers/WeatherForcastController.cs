using Backend.Common;
using Backend.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("/api/get-weather")]
public class WeatherForcastController : ControllerBase
{
    public record WeatherData(string weather, float temp);

    [HttpGet("{id}")]
    public ActionResult<ApiResponse<WeatherData>> Get(int id)
    {
        try
        {
            var data = _generateWeather(id);
            return Ok(ApiResponse<WeatherData>.Ok(data));
        }
        catch (ApiException ex)
        {
            return StatusCode(ex.StatusCode, ApiResponse<WeatherData>.Fail(ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse<WeatherData>.Fail(ex.Message));
        }
    }

    private WeatherData _generateWeather(int id)
    {
        string[] weathers = { "Summer", "Winter", "Autum", "Spring" };
        float[] temps = { 40.00F, 20.00F, 25.00F, 30.00F };

        if (id < 0 || id >= weathers.Length)
        {
            throw new ApiException(400, "Invalid Id");
        }

        return new WeatherData(weathers[id], temps[id]);
    }
}

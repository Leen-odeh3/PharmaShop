using Microsoft.AspNetCore.Mvc;

namespace pharmacy.Api.Responses;
public interface IResponseHandler
{
    IActionResult Success<T>(T data, string message);
    IActionResult NotFound(string message);
    IActionResult BadRequest(string message);
    IActionResult Unauthorized(string message);
    IActionResult Created<T>(T data, string message);
    IActionResult NoContent(string message);
    IActionResult Conflict(string message);
    IActionResult UnprocessableEntity(string message);
    IActionResult Redirect(string url);
}
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MinervaFoods.Helpers;
using System.Security.Claims;

namespace MinervaFoods.Api.Common;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class BaseController : ControllerBase
{
    protected Guid GetCurrentUserId() =>
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException());

    protected string GetCurrentUserEmail() =>
        User.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();

    protected IActionResult OkResponse<T>(T data) =>
            base.Ok(new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse { Message = message, Success = false });

    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(new ApiResponse { Message = message, Success = false });

    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
            Ok(new PaginatedResponse<T>
            {
                Data = pagedList,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                TotalCount = pagedList.TotalCount,
                Success = true
            });

    protected IActionResult Return<T>(Func<T> func, string message = "Operation completed successfully")
    {
        try
        {
            var data = func();
            if (data == null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Resource not found"
                });

            return Ok(new ApiResponseWithData<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (MinervaFoods.Helpers.Http.HttpRequestException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
            {
                Success = false,
                Message = "An internal server error occurred.",
                Errors = new List<ValidationErrorDetail>
            {
                new ValidationErrorDetail
                {
                    Error = "InternalServerError",
                    Detail = ex.Message
                }
            }
            });
        }
    }


    protected async System.Threading.Tasks.Task<IActionResult> ReturnAsync<T>(Func<Task<T>> func, string message = "Operation completed successfully")
    {
        try
        {
            var data = await func();
            if (data == null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Resource not found"
                });

            return Ok(new ApiResponseWithData<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (MinervaFoods.Helpers.Http.HttpRequestException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
            {
                Success = false,
                Message = "An internal server error occurred.",
                Errors = new List<ValidationErrorDetail>
            {
                new ValidationErrorDetail
                {
                    Error = "InternalServerError",
                    Detail = ex.Message
                }
            }
            });
        }
    }

    protected async System.Threading.Tasks.Task<IActionResult> ReturnCreatedAsync<T>(Func<Task<T>> func, string message = "Resource created successfully")
    {
        try
        {
            var data = await func();
            if (data == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
                {
                    Success = false,
                    Message = "An unexpected error occurred during creation."
                });
            }

            return Created(string.Empty, new ApiResponseWithData<T>
            {
                Success = true,
                Message = message,
                Data = data
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }

        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (MinervaFoods.Helpers.Http.HttpRequestException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
            {
                Success = false,
                Message = "An internal server error occurred.",
                Errors = new List<ValidationErrorDetail>
            {
                new ValidationErrorDetail
                {
                    Error = "InternalServerError",
                    Detail = ex.Message
                }
            }
            });
        }
    }

    protected async System.Threading.Tasks.Task<IActionResult> ReturnVoidAsync(Func<Task> action, string successMessage = "Operation completed successfully")
    {
        try
        {
            await action();

            return Ok(new ApiResponse
            {
                Success = true,
                Message = successMessage
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }

        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (HttpRequestException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse
            {
                Success = false,
                Message = "An internal server error occurred.",
                Errors = new List<ValidationErrorDetail>
            {
                new ValidationErrorDetail
                {
                    Error = "InternalServerError",
                    Detail = ex.Message
                }
            }
            });
        }
    }




}

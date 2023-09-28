using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPI.Models;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Enum;
using SweatFlexData.Interface;
using System.Net;

namespace SweatFlexAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;
        private ApiResponse _response;

        public UserAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
            _response = new();
        }

        [HttpGet]        
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetUsers()
        {
            try
            {
                var userDtos = await _dataHandler.GetUsersAsync();

                if (userDtos == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No users found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = userDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting users: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("{id}", Name ="GetUserById")]
        [Authorize(Roles = "Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetUser(string id)
        {
            try
            {
                var userDto = await _dataHandler.GetUserByIdAsync(id);

                if (userDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No user with this Id found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = userDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting user: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Authorize(Roles = "Coach,Admin")]
        [Route("coach/{id}", Name = "GetUsersByCoach")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetUserByCoach(string coachId)
        {
            try
            {
                var userDtos = await _dataHandler.GetUsersByCoachIdAsync(coachId);

                if (userDtos == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No users for the coach found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = userDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting users: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> CreateUser(UserCreateDTO createDTO)
        {
            try
            {
                var userDto = await _dataHandler.CreateUserAsync(createDTO);

                if (userDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error creating user");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = userDto;
                return CreatedAtRoute("GetUserById", new { id = userDto.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error creating user: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> UpdateUser(string id, UserUpdateDTO updateDTO)
        {
            try
            {
                var userDto = await _dataHandler.GetUserByIdAsync(id);

                if (userDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("User for update operation not found");
                    return NotFound(_response);
                }

                userDto = await _dataHandler.UpdateUserAsync(id, updateDTO);

                if (userDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error updating user");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = userDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error updating user: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> DeleteUser(string id)
        {
            try
            {
                var userDto = await _dataHandler.GetUserByIdAsync(id);

                if (userDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("User for delete operation not found");
                    return NotFound(_response);
                }

                var result = await _dataHandler.DeleteUserAsync(id);

                if (!result)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error deleting user");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error deleting user: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        //TODO: Implement Berni

        [HttpDelete]
        [Route("setInactive/{id}")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> SetUserInactive(string id)
        {
            throw new NotImplementedException();
        }
    }    
}

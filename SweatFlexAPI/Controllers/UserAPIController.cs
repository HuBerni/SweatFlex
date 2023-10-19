using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPIClient.APIModels;
using SweatFlexData.DTOs;
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

        public UserAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }


        /// <summary>
        /// Getting all users, only for admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse<IList<UserDTO>>>> GetUsers()
        {
            ApiResponse<IList<UserDTO>> response = new();

            try
            {
                var userDtos = await _dataHandler.GetUsersAsync();

                if (userDtos == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No users found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting users: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Getting a user by id, available for admin and coach
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}", Name = "GetUserById")]
        [Authorize(Roles = "Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetUser(string id)
        {
            ApiResponse<UserDTO> response = new();

            try
            {
                var userDto = await _dataHandler.GetUserByIdAsync(id);

                if (userDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No user with this Id found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting user: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Getting a list of users, which got a coach assigned, available for admin and coach
        /// </summary>
        /// <param name="coachId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Coach,Admin")]
        [Route("coach/{id}", Name = "GetUsersByCoach")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<IList<UserDTO>>>> GetUserByCoach(string coachId)
        {
            ApiResponse<IList<UserDTO>> response = new();

            try
            {
                var userDtos = await _dataHandler.GetUsersByCoachIdAsync(coachId);

                if (userDtos == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No users for the coach found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting users: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Get's one User with the coresponding eMail in the Param, available for Customer, Coach, Admin
        /// </summary>
        /// <param name = "eMail" ></ param >
        /// < returns ></ returns >
        [HttpGet]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [Route("mail/{id}", Name = "GetUserByMail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> GetUserByMail(string eMail)
        {
            ApiResponse<UserDTO> response = new();

            try
            {
                var userDto = await _dataHandler.GetUserByMailAsync(eMail);

                if (userDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No users for the coach found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting users: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Creating a new user, only for admin
        /// </summary>
        /// <param name="createDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> CreateUser(UserCreateDTO createDTO)
        {
            ApiResponse<UserDTO> response = new();

            try
            {
                var userDto = await _dataHandler.CreateUserAsync(createDTO);

                if (userDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error creating user");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.Created;
                response.Result = userDto;
                return CreatedAtRoute("GetUserById", new { id = userDto.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error creating user: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }
        }


        /// <summary>
        /// Updating a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}", Name = "UpdateUser")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<UserDTO>>> UpdateUser(string id, UserUpdateDTO updateDTO)
        {
            ApiResponse<UserDTO> response = new();

            try
            {
                var userDto = await _dataHandler.GetUserByIdAsync(id);

                if (userDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("User for update operation not found");
                    return NotFound(response);
                }

                userDto = await _dataHandler.UpdateUserAsync(id, updateDTO);

                if (userDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error updating user");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = userDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error updating user: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deleting a user, only for admin, to insure data integrity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteUser(string id)
        {
            ApiResponse<bool> response = new();

            try
            {
                var userDto = await _dataHandler.GetUserByIdAsync(id);

                if (userDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("User for delete operation not found");
                    return NotFound(response);
                }

                var isDeleted = await _dataHandler.DeleteUserAsync(id);

                if (!isDeleted)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error deleting user");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = isDeleted;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error deleting user: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Setting a user to inactive to insure data integrity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("setInactive/{id}")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<bool>>> SetUserInactive(string id)
        {
            ApiResponse<bool> response = new();

            try
            {
                var userDto = await _dataHandler.GetUserByIdAsync(id);

                if (userDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("User for setting inactive operation not found");
                    return NotFound(response);
                }

                var isInactive = await _dataHandler.SetUserInactive(id);

                if (!isInactive)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error setting user inactive");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = isInactive;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error setting user inactive: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }
    }    
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPI.Models;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface;
using System.Data;
using System.Net;

namespace SweatFlexAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WorkoutAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;
        private ApiResponse _response;

        public WorkoutAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
            _response = new();
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> GetWorkouts(string? userId)
        {
            try
            {
                var workoutDtos = await _dataHandler.GetWorkoutsAsync(userId);

                if (workoutDtos == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No workouts found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = workoutDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting workouts: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetWorkoutById")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetWorkoutById(int id)
        {
            try
            {
                var workoutDto = await _dataHandler.GetWorkoutByIdAsync(id);

                if (workoutDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No workout found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = workoutDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting workout: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpPost]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> CreateWorkout(WorkoutCreateDTO createDTO)
        {
            try
            {
                var workoutDto = await _dataHandler.CreateWorkoutAsync(createDTO);

                if (workoutDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error creating workout");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = workoutDto;
                return CreatedAtRoute("GetWorkoutById", new { id = workoutDto.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error creating workout: {ex.Message}");
                _response.ErrorMessages.Add(ex.InnerException?.Message);
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateWorkout(int id, WorkoutUpdateDTO updateDTO)
        {
            try
            {
                var workoutDto = await _dataHandler.UpdateWorkoutAsync(id, updateDTO);

                if (workoutDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error updating workout");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = workoutDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error updating workout: {ex.Message}");
                _response.ErrorMessages.Add(ex.InnerException?.Message);
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpDelete]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DeleteWorkout(int id)
        {
            try
            {
                var isDeleted = await _dataHandler.DeleteWorkoutAsync(id);

                if (!isDeleted)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error deleting workout");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = isDeleted;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error deleting workout: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPI.Models;
using SweatFlexData.DTOs;
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

        public WorkoutAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        /// <summary>
        /// Getting all workouts for a user, optionally for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<IList<WorkoutDTO>>>> GetWorkouts(string? userId)
        {
            ApiResponse<IList<WorkoutDTO>> response = new();

            try
            {
                var workoutDtos = await _dataHandler.GetWorkoutsAsync(userId);

                if (workoutDtos == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No workouts found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = workoutDtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting workouts: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Getting a workout by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}", Name = "GetWorkoutById")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<WorkoutDTO>>> GetWorkoutById(int id)
        {
            ApiResponse<WorkoutDTO> response = new();

            try
            {
                var workoutDto = await _dataHandler.GetWorkoutByIdAsync(id);

                if (workoutDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No workout found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = workoutDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting workout: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }



        /// <summary>
        /// Creating a new workout
        /// </summary>
        /// <param name="createDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<WorkoutDTO>>> CreateWorkout(WorkoutCreateDTO createDTO)
        {
            ApiResponse<WorkoutDTO> response = new();

            try
            {
                var workoutDto = await _dataHandler.CreateWorkoutAsync(createDTO);

                if (workoutDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error creating workout");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.Created;
                response.Result = workoutDto;
                return CreatedAtRoute("GetWorkoutById", new { id = workoutDto.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error creating workout: {ex.Message}");
                response.ErrorMessages.Add(ex.InnerException?.Message);
                return StatusCode((int)response.StatusCode, response);
            }
        }



        /// <summary>
        /// Updating a workout, only affecting the name
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<WorkoutDTO>>> UpdateWorkout(int id, WorkoutUpdateDTO updateDTO)
        {
            ApiResponse<WorkoutDTO> _response = new();

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


        /// <summary>
        /// Deleting a workout
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteWorkout(int id)
        {
            ApiResponse<bool> response = new();

            try
            {
                var workoutDto = await _dataHandler.GetWorkoutByIdAsync(id);

                if (workoutDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Workout for delete operation not found");
                    return NotFound(response);
                }

                var isDeleted = await _dataHandler.DeleteWorkoutAsync(id);

                if (!isDeleted)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error deleting workout");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = isDeleted;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error deleting workout: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

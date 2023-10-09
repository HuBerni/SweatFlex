using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPIClient.APIModels;
using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.Interface;
using System.Net;

namespace SweatFlexAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class WorkoutExerciseAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;

        public WorkoutExerciseAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        /// <summary>
        /// Getting all workout exercises for a workout
        /// </summary>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{workoutId:int}", Name = "GetWorkoutExercises")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<IList<WorkoutExerciseDTO>>>> GetWorkoutExercises(int workoutId)
        {
            ApiResponse<IList<WorkoutExerciseDTO>> response = new();

            try
            {
                var workoutExerciseDtos = await _dataHandler.GetWorkoutExercisesAsync(workoutId);

                if (workoutExerciseDtos == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No workout exercises found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = workoutExerciseDtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting workout exercises: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Getting a workout exercise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetWorkoutExerciseById/{id:int}", Name = "GetWorkoutExerciseById")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<WorkoutExerciseDTO>>> GetWorkoutExerciseById(int id)
        {
            ApiResponse<WorkoutExerciseDTO> response = new();

            try
            {
                var workoutExerciseDto = await _dataHandler.GetWorkoutExerciseByIdAsync(id);

                if (workoutExerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No workout exercise found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = workoutExerciseDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting workout exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Adding a new workout exercise
        /// </summary>
        /// <param name="workoutExerciseDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<WorkoutExerciseDTO>>> CreateWorkoutExercise(WorkoutExerciseCreateDTO workoutExerciseDto)
        {
            ApiResponse<WorkoutExerciseDTO> response = new();

            try
            {
                if (workoutExerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Workout exercise object is null");
                    return BadRequest(response);
                }

                var createdWorkoutExerciseDto = await _dataHandler.CreateWorkoutExceriseAsync(workoutExerciseDto);

                if (createdWorkoutExerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error creating workout exercise");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.Created;
                response.Result = createdWorkoutExerciseDto;
                return CreatedAtRoute("GetWorkoutExerciseById", new { id = createdWorkoutExerciseDto.Id }, response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error creating workout exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }
        }


        /// <summary>
        /// Updating a workout exercise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}", Name = "UpdateWorkoutExercise")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<WorkoutExerciseDTO>>> UpdateWorkoutExercise(int id, WorkoutExerciseUpdateDTO updateDTO)
        {
            ApiResponse<WorkoutExerciseDTO> response = new();

            try
            {
                var workoutExerciseDto = await _dataHandler.UpdateWorkoutExerciseAsync(id, updateDTO);

                if (workoutExerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error updating workout exercise");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = workoutExerciseDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error updating workout exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deleting a workout exercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteWorkoutExercise")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteWorkoutExercise(int id)
        {
            ApiResponse<bool> response = new();

            try
            {
                var workoutExerciseDto = await _dataHandler.GetWorkoutExerciseByIdAsync(id);

                if (workoutExerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("WorkoutExercise for delete operation not found");
                    return NotFound(response);
                }

                var isDeleted = await _dataHandler.DeleteWorkoutExerciseAsync(id);

                if (!isDeleted)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error deleting workout exercise");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = isDeleted;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error deleting workout exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

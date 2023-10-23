using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPIClient.APIModels;
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
    public class TrainingExerciseAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;

        public TrainingExerciseAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        /// <summary>
        /// Getting all training exercises for a user, optionally for a specific workout
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workoutId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [Route("getExercises/{userId}/{workoutId:int?}", Name = "GetTrainingExercises")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<IList<TrainingExerciseDTO>>>> GetTrainingExercises(string userId, int? workoutId = null)
        {
            ApiResponse<IList<TrainingExerciseDTO>> response = new();

            try
            {
                var trainingExerciseDtos = await _dataHandler.GetTrainingExerciesAsync(userId, workoutId);

                if (trainingExerciseDtos == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No training exercises found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = trainingExerciseDtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting training exercises: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Getting a training exercise by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}", Name = "GetTrainingExercise")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<TrainingExerciseDTO>>> GetTrainingExerciseById(int id)
        {
            ApiResponse<TrainingExerciseDTO> response = new();

            try
            {
                var trainingExerciseDto = await _dataHandler.GetTrainingExerciseAsync(id);

                if (trainingExerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No training exercise found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = trainingExerciseDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting training exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Creating a new training exercise
        /// </summary>
        /// <param name="createDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<IList<TrainingExerciseDTO>>>> CreateTrainingExercise(IList<TrainingExerciseCreateDTO> createDTO)
        {
            ApiResponse<IList<TrainingExerciseDTO>> response = new();

            try
            {
                var trainingExerciseDTOs = await _dataHandler.CreateTrainingExerciseAsync(createDTO);

                if (trainingExerciseDTOs == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error creating training exercise");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.Created;
                response.Result = trainingExerciseDTOs;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error creating training exercise: {ex.Message}");
                response.ErrorMessages.Add(ex.InnerException?.Message);
                return StatusCode((int)response.StatusCode, response);
            }
        }


        /// <summary>
        /// Updating a training exercise
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [Route("{id:int}", Name = "UpdateTrainingExercise")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<TrainingExerciseDTO>>> UpdateTrainingExercise(int id, TrainingExerciseUpdateDTO updateDTO)
        {
            ApiResponse<TrainingExerciseDTO> response = new();

            try
            {
                var trainingExerciseDto = await _dataHandler.UpdateTrainingExerciseAsync(id, updateDTO);

                if (trainingExerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error updating training exercise");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = trainingExerciseDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error updating training exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Deleting a training exercise
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [Route("{id:int}", Name = "DeleteTrainingExercise")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteTrainingExercise(int id)
        {
            ApiResponse<bool> response = new();

            try
            {
                var trainingExerciseDto = await _dataHandler.GetTrainingExerciseAsync(id);

                if (trainingExerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("TrainingExercise for delete operation not found");
                    return NotFound(response);
                }

                var isDeleted = await _dataHandler.DeleteTrainingExerciseAsync(id);

                if (!isDeleted)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error deleting training exercise");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = isDeleted;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error deleting training exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }
    }
}

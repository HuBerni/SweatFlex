using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPIClient.APIModels;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface;
using System.Net;

namespace SweatFlexAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExerciseAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;        

        public ExerciseAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;            
        }


        /// <summary>
        /// Returning an ApiResponse with a list of all exercises from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<IList<ExerciseDTO>>>> GetExercises()
        {
            ApiResponse<IList<ExerciseDTO>> response = new();

            try
            {
                var exercisesDtos = await _dataHandler.GetExercisesAsync();

                if (exercisesDtos == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No exercises found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = exercisesDtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting exercises: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
       }


        /// <summary>
        /// Returning an ApiResponse with a list of all exercises for a specific user
        /// </summary>
        /// <param name="id">The user, from which the exercises should be returned</param>
        /// <returns></returns>
        [HttpGet]
        [Route("user/{id}", Name = "GetExercisesByUserId")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<IList<ExerciseDTO>>>> GetExercises(string id)
        {
            ApiResponse<IList<ExerciseDTO>> response = new();

            try
            {
                var exercisesDtos = await _dataHandler.GetExercisesAsync(id);

                if (exercisesDtos == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("No exercises found");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = exercisesDtos;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting exercises: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }


        /// <summary>
        /// Getting an exercise by id
        /// </summary>
        /// <param name="id">The id of the specific exercise</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [Route("{id:int}", Name = "GetExerciseById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<ExerciseDTO>>> GetExercise(int id)
        {
            ApiResponse<ExerciseDTO> response = new();

            try
            {
                var exerciseDto = await _dataHandler.GetExerciseByIdAsync(id);

                if (exerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add($"No exercise found with id: {id}");
                    return NotFound(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = exerciseDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error getting exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Adding a new exercise
        /// </summary>
        /// <param name="createDTO">The object for the object which is getting created</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<ExerciseDTO>>> CreateExercise(ExerciseCreateDTO createDTO)
        {
            ApiResponse<ExerciseDTO> response = new();

            try
            {
                var exerciseDto = await _dataHandler.CreateExerciseAsync(createDTO);

                if (exerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add("Error creating exercise");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.Created;
                response.Result = exerciseDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error creating exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            var result = response.Result as ExerciseDTO;

            return CreatedAtRoute("GetExerciseById", new { id = result?.Id }, response);
        }


        /// <summary>
        /// Updating an exercise
        /// </summary>
        /// <param name="id">the id of the exercise</param>
        /// <param name="updateDTO">the object with the new data</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [Route("{id:int}", Name = "UpdateExercise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse<ExerciseDTO>>> UpdateExercise(int id, ExerciseUpdateDTO updateDTO)
        {
            ApiResponse<ExerciseDTO> response = new();

            try
            {
                var exerciseDto = await _dataHandler.UpdateExerciseAsync(id, updateDTO);

                if (exerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add($"Error updating exercise with id: {id}");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = exerciseDto;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error updating exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Deleting an exercise
        /// </summary>
        /// <param name="id">The id of the exercise</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}", Name = "DeleteExercise")]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteExercise(int id)
        {
            ApiResponse<bool> response = new();

            try
            {
                var exerciseDto = await _dataHandler.GetExerciseByIdAsync(id);

                if (exerciseDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.NotFound;
                    response.ErrorMessages.Add("Exercise for delete operation not found");
                    return NotFound(response);
                }

                var isDeleted = await _dataHandler.DeleteExerciseAsync(id);

                if (!isDeleted)
                {
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.ErrorMessages.Add($"Exercise deleting exercise with id: {id}");
                    return BadRequest(response);
                }

                response.StatusCode = HttpStatusCode.OK;
                response.Result = isDeleted;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.ErrorMessages.Add($"Error deleting exercise: {ex.Message}");
                return StatusCode((int)response.StatusCode, response);
            }

            return Ok(response);
        }
    }
}


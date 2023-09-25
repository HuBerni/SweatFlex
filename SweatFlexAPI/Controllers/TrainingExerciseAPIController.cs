using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPI.Models;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexData.Interface;
using System.Net;

namespace SweatFlexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingExerciseAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;
        private ApiResponse _response;

        public TrainingExerciseAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetTrainingExercises(string userId, int? workoutId = null)
        {
            try
            {
                var trainingExerciseDtos = await _dataHandler.GetTrainingExerciesAsync(userId, workoutId);

                if (trainingExerciseDtos == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No training exercises found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = trainingExerciseDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting training exercises: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetTrainingExercise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetTrainingExerciseById(int id)
        {
            try
            {
                var trainingExerciseDto = await _dataHandler.GetTrainingExerciseAsync(id);

                if (trainingExerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No training exercise found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = trainingExerciseDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting training exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> CreateTrainingExercise(TrainingExerciseCreateDTO createDTO)
        {
            try
            {
                var trainingExerciseDto = await _dataHandler.CreateTrainingExerciseAsync(createDTO);

                if (trainingExerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error creating training exercise");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = trainingExerciseDto;
                return CreatedAtRoute("GetTrainingExercise", new { id = trainingExerciseDto.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error creating training exercise: {ex.Message}");
                _response.ErrorMessages.Add(ex.InnerException?.Message);
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateTrainingExercise(int id, TrainingExerciseUpdateDTO updateDTO)
        {
            try
            {
                var trainingExerciseDto = await _dataHandler.UpdateTrainingExerciseAsync(id, updateDTO);

                if (trainingExerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error updating training exercise");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = trainingExerciseDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error updating training exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DeleteTrainingExercise(int id)
        {
            try
            {
                var result = await _dataHandler.DeleteTrainingExerciseAsync(id);

                if (!result)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error deleting training exercise");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error deleting training exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }
    }
}

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
    public class ExerciseAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;
        private ApiResponse _response;

        public ExerciseAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
            _response = new();
        }

        [HttpGet]
        [Authorize(Roles = "Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetExercises()
        {
            try
            {
                var exercisesDtos = await _dataHandler.GetExercisesAsync();

                if (exercisesDtos == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No exercises found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = exercisesDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting exercises: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetExercises(string id)
        {
            try
            {
                var exercisesDtos = await _dataHandler.GetExercisesAsync(id);

                if (exercisesDtos == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No exercises found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = exercisesDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting exercises: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [Route("{id:int}", Name = "GetExerciseById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> GetExercise(int id)
        {
            try
            {
                var exerciseDto = await _dataHandler.GetExerciseByIdAsync(id);

                if (exerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add($"No exercise found with id: {id}");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = exerciseDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpPost]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> CreateExercise(ExerciseCreateDTO createDTO)
        {
            try
            {
                var exerciseDto = await _dataHandler.CreateExerciseAsync(createDTO);

                if (exerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error creating exercise");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = exerciseDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error creating exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            var result = _response.Result as ExerciseDTO;
         
            return CreatedAtRoute("GetExerciseById", new { id = result?.Id }, _response);
        }

        [HttpPut]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ApiResponse>> UpdateExercise(int id, ExerciseUpdateDTO updateDTO)
        {
            try
            {
                var exerciseDto = await _dataHandler.UpdateExerciseAsync(id, updateDTO);

                if (exerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add($"Error updating exercise with id: {id}");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = exerciseDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error updating exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpDelete]
        [Authorize(Roles = "Customer,Coach,Admin")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteExercise(int id)
        {
            try
            {
                var result = await _dataHandler.DeleteExerciseAsync(id);

                if (!result)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add($"Error deleting exercise with id: {id}");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error deleting exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }
    }
}

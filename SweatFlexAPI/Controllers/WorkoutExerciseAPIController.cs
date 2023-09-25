﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SweatFlexAPI.Models;
using SweatFlexData.Create.DTOs;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.Interface;
using System.Net;

namespace SweatFlexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutExerciseAPIController : ControllerBase
    {
        private readonly IDataHandler _dataHandler;
        private ApiResponse _response;

        public WorkoutExerciseAPIController(IDataHandler dataHandler)
        {
            _dataHandler = dataHandler;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> GetWorkoutExercises(int workoutId)
        {
            try
            {
                var workoutExerciseDtos = await _dataHandler.GetWorkoutExercisesAsync(workoutId);

                if (workoutExerciseDtos == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No workout exercises found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = workoutExerciseDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting workout exercises: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetWorkoutExerciseById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> GetWorkoutExerciseById(int id)
        {
            try
            {
                var workoutExerciseDto = await _dataHandler.GetWorkoutExerciseByIdAsnyc(id);

                if (workoutExerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages.Add("No workout exercise found");
                    return NotFound(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = workoutExerciseDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error getting workout exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> CreateWorkoutExercise(WorkoutExerciseCreateDTO workoutExerciseDto)
        {
            try
            {
                if (workoutExerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Workout exercise object is null");
                    return BadRequest(_response);
                }

                var createdWorkoutExerciseDto = await _dataHandler.CreateWorkoutExceriseAsync(workoutExerciseDto);

                if (createdWorkoutExerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error creating workout exercise");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.Created;
                _response.Result = createdWorkoutExerciseDto;
                return CreatedAtRoute("GetWorkoutExerciseById", new { id = createdWorkoutExerciseDto.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error creating workout exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateWorkoutExercise(int id, WorkoutExerciseUpdateDTO updateDTO)
        {
            try
            {
                var workoutExerciseDto = await _dataHandler.UpdateWorkoutExerciseAsync(id, updateDTO);

                if (workoutExerciseDto == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error updating workout exercise");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = workoutExerciseDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error updating workout exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> DeleteWorkoutExercise(int id)
        {
            try
            {
                var isDeleted = await _dataHandler.DeleteWorkoutExerciseAsync(id);

                if (!isDeleted)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Error deleting workout exercise");
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = isDeleted;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add($"Error deleting workout exercise: {ex.Message}");
                return StatusCode((int)_response.StatusCode, _response);
            }

            return Ok(_response);
        }
    }
}
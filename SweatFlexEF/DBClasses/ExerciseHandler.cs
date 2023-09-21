using NLog;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SweatFlexData.DTOs.Update;
using SweatFlexEF.Models;
using AutoMapper;

namespace SweatFlexEF.DBClasses
{
    public class ExerciseHandler
    {
        SweatFlexContext _context;
        public ExerciseHandler(SweatFlexContext context)
        {
            _context = context;
        }

        public async Task<IList<ExerciseDTO>> GetExercisesAsync(string? userId = null)
        {
            List<Exercise> exercises = new();

            if (userId == null)
            {
                var nonCustomerIds = _context.Users.Where(u => u.Role != 1).Select(s => s.Id).ToList();
                exercises = _context.Exercises.Where(e => e.Creator.Any(i => nonCustomerIds.Contains(e.Creator))).ToList();
                //TODO: needs to be tested
            }
            else
            {
                exercises = _context.Exercises.Where(e => e.Creator == userId).ToList();
            }

            return exercises.Select(Mapping.Mapper.Map<ExerciseDTO>).ToList();
            
        }

        public async Task<ExerciseDTO> GetExerciseByIdAsync(int id)
        {
            Exercise exercise;

            if(id != null)
            {
                exercise = _context.Exercises.Where(e => e.Id == id).FirstOrDefault();
                await _context.SaveChangesAsync();
                return Mapping.Mapper.Map<ExerciseDTO>(exercise);
            }
            else
            {
                return null;
            }
        }
        public async Task<ExerciseDTO> UpdateExerciseAsync(int id, ExerciseUpdateDTO updateDTO)
        {
            if(id != null && updateDTO != null)
            {
                var exercise = Mapping.Mapper.Map<Exercise>(updateDTO);

                _context.Exercises.Update(exercise);
                await _context.SaveChangesAsync();
                var exerciseNew = _context.Exercises.Where(e => e.Id == updateDTO.Id).FirstOrDefault();
                return Mapping.Mapper.Map<ExerciseDTO>(exerciseNew);
            }
            else
            {
                return null;
            }
        }
        public async Task<bool> DeleteExerciseAsync(int id)
        {

            if(id != null)
            {
                var exercise = _context.Exercises.Where(e => e.Id == id).FirstOrDefault();
                _context.Exercises.Remove(exercise);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<ExerciseDTO> CreateExerciseAsync(ExerciseCreateDTO createDTO)
        {
            if(createDTO != null)
            {
                var exercise = Mapping.Mapper.Map<Exercise>(createDTO);
                await _context.Exercises.AddAsync(exercise);
                await _context.SaveChangesAsync();

                return Mapping.Mapper.Map<ExerciseDTO>(exercise);
            }
            else
            {
                return null;
            }
        }

    }
}

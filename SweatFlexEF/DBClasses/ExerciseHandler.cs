using Microsoft.EntityFrameworkCore;
using SweatFlexData.DTOs;
using SweatFlexData.DTOs.Create;
using SweatFlexData.DTOs.Update;
using SweatFlexEF.Models;

namespace SweatFlexEF.DBClasses
{
    public class ExerciseHandler
    {
        SweatFlexContext _context;
        public ExerciseHandler(SweatFlexContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Reads Exercises from Database by the creater Id, if creater Id == null, it gets all Exercises that are not created by a customer
        /// </summary>
        /// <param name="userId">user that created an exercise</param>
        /// <returns></returns>
        public async Task<IList<ExerciseDTO>> GetExercisesAsync(string? userId = null)
        {
            List<Exercise> exercises = new();

            if (userId == null)
            {
                var nonCustomerIds = _context.Users.Where(u => u.Role != 1).Select(s => s.Id).ToList();
                exercises = await _context.Exercises.Where(e => nonCustomerIds.Any(i => i == e.Creator)).ToListAsync();
            }
            else
            {
                exercises = await _context.Exercises.Where(e => e.Creator == userId).ToListAsync();
            }

            var exerciseDTOs = new List<ExerciseDTO>();

            foreach(var exercise in exercises)
            {
                exerciseDTOs.Add(ExerciseDTOMapper(exercise));
            }

            return exerciseDTOs;
        }

        /// <summary>
        /// Reads 1 Exercise from Database, depending on the Exercise Id
        /// </summary>
        /// <param name="id">Exercise Id</param>
        /// <returns></returns>
        public async Task<ExerciseDTO> GetExerciseByIdAsync(int id)
        {
            Exercise exercise;

            if (id != null)
            {
                exercise = await _context.Exercises.Where(e => e.Id == id).FirstOrDefaultAsync();
                await _context.SaveChangesAsync();
                return ExerciseDTOMapper(exercise);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Updates 1 Exercise in the Database, depending on the Exercise Id
        /// </summary>
        /// <param name="id">Exercise Id</param>
        /// <param name="updateDTO">Exercise data for update</param>
        /// <returns></returns>
        public async Task<ExerciseDTO> UpdateExerciseAsync(int id, ExerciseUpdateDTO updateDTO)
        {
            if (id != null && updateDTO != null)
            {
                var exercise = Mapping.Mapper.Map<Exercise>(updateDTO);
                exercise.Id = id;

                _context.Exercises.Update(exercise);
                await _context.SaveChangesAsync();
                var exerciseNew = _context.Exercises.Where(e => e.Id == id).FirstOrDefault();
                return Mapping.Mapper.Map<ExerciseDTO>(exerciseNew);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Deletes 1 Exercise from the Database, depending on the Exercise Id
        /// </summary>
        /// <param name="id">Exercise Id</param>
        /// <returns></returns>
        public async Task<bool> DeleteExerciseAsync(int id)
        {

            if (id != null)
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

        /// <summary>
        /// Creates 1 new Exercise in the Database
        /// </summary>
        /// <param name="createDTO">Exercise data for creation</param>
        /// <returns></returns>
        public async Task<ExerciseDTO> CreateExerciseAsync(ExerciseCreateDTO createDTO)
        {
            if (createDTO != null)
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

        /// <summary>
        /// Manuelle Mapps an Exercise to an ExerciseDTO
        /// </summary>
        /// <param name="exercise">Exercise for Mapping</param>
        /// <returns></returns>
        private ExerciseDTO ExerciseDTOMapper(Exercise exercise)
        {
            return new ExerciseDTO()
            {
                Creator = exercise.Creator,
                Description = exercise.Description,
                Equipment = exercise.EquipmentNavigation.Name,
                Id = exercise.Id,
                Musclegroup = exercise.MusclegroupNavigation.Name,
                Name = exercise.Name,
                Type = exercise.TypeNavigation.Name
            };
        }

    }
}

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

namespace SweatFlexEF.DBClasses
{
    public class DBExercise
    {
        Logger _logger;
        public DBExercise(Logger logger, SweatFlexContext)
        {
            _logger = logger;
        }

        IList<ExerciseDTO> GetExercise(int? UserId = null)
        {

        }
        ExerciseDTO GetExerciseById(int id);
        ExerciseDTO UpdateExercise(int id, ExerciseUpdateDTO updateDTO);
        bool DeleteExercise(int id);
        ExerciseDTO CreateExercise(ExerciseCreateDTO createDTO);
    }
}

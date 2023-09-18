using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs
{
    public class ExerciseDTO
    {
        public int Id { get; set; }

        public int Creator { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Musclegroup { get; set; }

        public string? Equipment { get; set; }

        public string Description { get; set; }

    }
}

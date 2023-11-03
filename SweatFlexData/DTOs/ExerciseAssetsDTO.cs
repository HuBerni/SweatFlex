using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.DTOs
{
    public class ExerciseAssetsDTO
    {
        public List<MusclegroupDTO> MuscleGroups { get; set; }
        public List<TypeDTO> Types { get; set; }
        public List<EquipmentDTO> Equipments { get; set; }
    }
}

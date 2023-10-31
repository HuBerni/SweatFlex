using CommunityToolkit.Maui.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.Models
{
    public class Progress
    {
        public Workout Workout { get; set; }
        public int SessionId { get; set; }
        public decimal? TotalWeight { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }
}

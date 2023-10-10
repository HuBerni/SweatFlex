using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ConfirmationPopupViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;


        public ConfirmationPopupViewModel(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                Title = title;
            }
            else
            {
                Title = "Bist du dir sicher?";
            }
        }
    }
}

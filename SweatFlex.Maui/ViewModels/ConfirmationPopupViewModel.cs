using CommunityToolkit.Mvvm.ComponentModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ConfirmationPopupViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;


        public ConfirmationPopupViewModel(string title)
        {
            Title = title;
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;

namespace SweatFlex.Maui.ViewModels
{
    public partial class ConfirmationPopupViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;

        /// <summary>
        /// Creates a new instance of the <see cref="ConfirmationPopupViewModel"/> class
        /// </summary>
        /// <param name="title"></param>
        public ConfirmationPopupViewModel(string title)
        {
            Title = title;
        }
    }
}

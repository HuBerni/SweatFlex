using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlex.Maui.Services
{
    public static class ToastService
    {
        public static async Task ShowToast(string message, int duration = 0)
        {
            var toastDuration = duration switch
            {
                0 => ToastDuration.Short,
                1 => ToastDuration.Long,
                _ => ToastDuration.Short
            };

            var toast = Toast.Make(message, toastDuration);
            await toast.Show();
        }
    }
}

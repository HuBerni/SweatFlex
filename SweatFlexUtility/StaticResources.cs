using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexUtility
{
    public static class StaticResources
    {
        //public const string SweatFlexRestAPIURL = "https://sweatflexapi2.azurewebsites.net/api/";
        public const string SweatFlexRestAPIURL = "https://sweatflexapi2.azurewebsites.net/api/";

        public static List<List<string>> GetMonths()
        {
            List<List<string>> months = new()
            {
                new List<string>(){ "Jänner", "#E5B95B" },
                new List<string>(){ "Februar", "#E5975B" },
                new List<string>(){ "März", "#E5975B" },
                new List<string>(){ "April", "#E65683" },
                new List<string>(){ "Mai", "#D669E6" },
                new List<string>(){ "Juni", "#997FE5" },
                new List<string>(){ "Juli", "#7DB0E5" },
                new List<string>(){ "August", "#77E5B8" },
                new List<string>(){ "September", "#7CE672" },
                new List<string>(){ "Oktober", "#D8E66D" },
                new List<string>(){ "Novermber", "#C5556D" },
                new List<string>(){ "Dezember", "#E5CDB8" }
            };

            return months;

        }

    }
}

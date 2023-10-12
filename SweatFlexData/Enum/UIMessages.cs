using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexData.Enum
{
    public static class UIMessages
    {
        public static Dictionary<HttpStatusCode, string> UIMessagesForHTTPStatusCode { get; } = new Dictionary<HttpStatusCode, string>()
        {
            { HttpStatusCode.OK, "Kleinanzeigen" }
        };
    }
}

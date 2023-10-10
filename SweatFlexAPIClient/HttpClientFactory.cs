using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SweatFlexAPIClient
{
    public static class HttpClientFactory
    {
        public static IHttpClientFactory CreateFactory()
        {
            return new ServiceCollection().AddHttpClient().BuildServiceProvider().GetService<IHttpClientFactory>();
        }
    }
}

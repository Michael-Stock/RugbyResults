using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyResults.Configuration
{
    /// <summary>
    /// Represents the configuration for the external API
    /// </summary>
    public class ExternalApiOptions
    {
        public const string ExternalApi = "ExternalApi";

        public string Url { get; set; }

        public string Season { get; set; }

        public string AuthToken { get; set; }
    }
}

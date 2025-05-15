using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcHive.Domain.API
{
    public class TMDBProvider
    {
        public int provider_id { get; set; }
        public string provider_name { get; set; }
        public string logo_path { get; set; }
    }

    public class TMDBProviderResponse
    {
        public List<TMDBProvider> results { get; set; }
    }

}

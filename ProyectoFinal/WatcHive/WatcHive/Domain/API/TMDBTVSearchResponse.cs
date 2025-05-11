using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatcHive.Domain.API
{
    internal class TMDBTVSearchResponse
    {
        public List<TVShowDTO> results { get; set; }
    }
}

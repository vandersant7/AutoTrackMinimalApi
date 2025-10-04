using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrackMinimalApi.Domain.ModelViews
{
    public struct ErrosValidatorsMessage
    {
        public ErrosValidatorsMessage() => Messages = new List<string>();
        
        public List<string> Messages { get; set; }
    }
}
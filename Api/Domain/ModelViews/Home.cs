using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoTrackMinimalApi.Domain.ModelViews
{
    public struct Home
    {
        public string Message { get => "Bem vindo ao AutoTrack Minimal Api"; }
        public string Doc { get => "/swagger"; }
    }
}
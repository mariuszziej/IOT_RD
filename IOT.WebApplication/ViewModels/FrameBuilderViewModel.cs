using IOT.Domain.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOT.WebApplication.ViewModels
{
    public class FrameBuilderViewModel
    {
        public Frame Frame { get; set; }
        public FrameElement NewElement { get; set; }
    }
}
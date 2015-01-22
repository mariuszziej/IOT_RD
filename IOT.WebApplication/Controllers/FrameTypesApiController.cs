using IOT.Domain;
using IOT.Domain.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IOT.WebApplication.Controllers
{
    public class FrameTypesApiController : ApiController
    {
        List<FrameElement> types;

        public FrameTypesApiController()
        {

        }

        [Route("FrameTypes")]
        public IEnumerable<FrameElement> GetFrameTypes()
        {
            return types;
        }
    }
}

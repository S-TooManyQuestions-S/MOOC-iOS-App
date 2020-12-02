using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOOC_Server.MySettings;

namespace MOOC_Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class getDetailsController : ControllerBase
    {
        // GET: api/getDetails

        public IServerRepository ServerItem { get; }
        public getDetailsController(IServerRepository item)
       => ServerItem = item;

        [HttpGet("getDetails")]
        public CourseDetails GetDetailsByCourse(string link)
            => ServerItem.GetDetailsByCourse(link);
    }
}

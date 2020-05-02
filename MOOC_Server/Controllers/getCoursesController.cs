using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CourseLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOOC_Server.MySettings;
using NLog;

namespace MOOC_Server.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class getCoursesController : ControllerBase
    {
        //https://localhost:44317/getCourses?keyword=python
        // GET: api/getCourses
        public IServerRepository ServerItem {get;}

        public getCoursesController(IServerRepository item)
       => ServerItem = item;
        [HttpGet("getCourses")]

        public List<Course> GetByKeyWord(string keyword)
        => ServerItem.GetByKeyWord(HttpUtility.UrlEncode(keyword));

       
            
    }
}

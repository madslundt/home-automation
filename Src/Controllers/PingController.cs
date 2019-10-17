﻿using Microsoft.AspNetCore.Mvc;

namespace Src.Controllers
{
    [Route("/ping")]
    public class PingController : Controller
    {
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}

﻿using System.Threading;
using System.Threading.Tasks;
using Dinkle.Application.Profiles.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Dinkle.Controllers
{
    public class ProfileController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetProfile(GetProfileQuery cmd, CancellationToken ct = default)
        {
            return Accepted(cmd);
        }
        
        
        
    }
}
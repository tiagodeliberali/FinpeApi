using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinpeApi.Models.AppStates;
using Microsoft.AspNetCore.Mvc;

namespace FinpeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverviewController : ControllerBase
    {
        [HttpGet("{year}/{month}")]
        public OverviewState Get(int year, int month)
        {
            return new OverviewState();
        }

        [HttpPost]
        public void Post([FromBody] OverviewStatement statement)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id)
        {
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using lab1;
using Microsoft.AspNetCore.Mvc;

namespace lab6_elon.controllers
{
    [ApiController]
    public class ElonController : ControllerBase
    {
        [Route("elon/getcolor")]
        [HttpGet]
        public async Task<CardColor> GetColor()
        {
            return await Task.FromResult(Utils.Color);
        }
    }
}
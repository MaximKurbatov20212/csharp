using System.Threading.Tasks;
using lab1;
using Microsoft.AspNetCore.Mvc;

namespace lab6_mark.controllers
{
    [ApiController]
    public class MarkController : ControllerBase
    {
        [Route("mark/getcolor")]
        [HttpGet]
        public async Task<CardColor> GetColor()
        {
            return await Task.FromResult(Utils.Color);
        }
    }
}
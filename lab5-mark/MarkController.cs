using System;
using System.Collections.Generic;
using lab1;
using Microsoft.AspNetCore.Mvc;

namespace lab5_mark
{
    [Route("[controller]")]
    [ApiController]
    public class MarkController : ControllerBase
    {
        private readonly ICardPickStrategy _strategy;

        public MarkController(ICardPickStrategy strategy)
        {
            _strategy = strategy;
        }
        
        [HttpPost("/api/v1/cards")]
        public IActionResult GetChoice([FromBody] List<Card> cards) {
            Console.WriteLine(cards);
            
            var choice = _strategy.Pick(cards.ToArray());
            return Ok(choice);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using lab1;

namespace lab5Elon
{
    [Route("[controller]")]
    [ApiController]
    public class ElonController : ControllerBase
    {
        private readonly ICardPickStrategy _strategy;

        public ElonController(ICardPickStrategy strategy)
        {
            _strategy = strategy;
        }
        
        [HttpPost("/api/v1/cards")]
        public IActionResult GetChoice([FromBody] List<Card> cards) 
        {
            Console.WriteLine(cards);
            
            var choice = _strategy.Pick(cards.ToArray());
            return Ok(choice);
        }
    }
}

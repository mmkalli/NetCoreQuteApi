using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Data;
using QuotesApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuotesApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {

        private QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }

        // GET: api/<QuotesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_quotesDbContext.Quotes);
        }

        // GET api/<QuotesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _quotesDbContext.Quotes.FirstOrDefault(x => x.Id == id);
            if(entity == null)
            {
                return NotFound("No record found...");
            }
            return Ok(entity);
        }

        // POST api/<QuotesController>
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<QuotesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            var entity = _quotesDbContext.Quotes.FirstOrDefault(x=>x.Id == id);
            if(entity != null)
            {
                entity.Title = quote.Title;
                entity.Author = quote.Author;
                entity.Description = quote.Description;
                entity.Type = quote.Type;
                entity.CreatedAt = quote.CreatedAt;
                _quotesDbContext.SaveChanges();
            }
            else
            {
                return NotFound("No record found...");
            }
            return Ok("Record updated successfully...");
        }

        // DELETE api/<QuotesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _quotesDbContext.Quotes.FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                _quotesDbContext.Remove(entity);
                _quotesDbContext.SaveChanges();
                return Ok("Record deleted successfully...");
            }
            else
            {
                return NotFound("No record found...");
            }
        }
    }
}

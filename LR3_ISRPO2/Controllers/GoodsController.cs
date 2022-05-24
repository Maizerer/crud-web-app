using LR3_ISRPO2.Models;
 using Microsoft.AspNetCore.Mvc;
 using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LR7_PP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private GoodsContext? _db;
        public GoodsController(GoodsContext GoodsContext)

        {
            _db = GoodsContext;
        }
        // GET: api/<HotelsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goods>>> Get()
        {
            return await _db.Goods.ToListAsync();
        }
        // GET api/<HotelsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Goods>> Get(int id)
        {
            Goods animal = await _db.Goods.FirstOrDefaultAsync(x => x.Id
               == id);
            if (animal == null)
                return NotFound();
            return new ObjectResult(animal);
        }
        // POST api/<HotelsController>
        [HttpPost]
        public async Task<ActionResult<Goods>> Post(Goods animal)
        {
            if (animal == null)
            {
                return BadRequest();
            }
            _db.Goods.Add(animal);
            await _db.SaveChangesAsync();
            return Ok(animal);
        }
        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Goods>> Put(Goods animal)
        {
            if (animal == null)
            {
                return BadRequest();
            }
            if (!_db.Goods.Any(x => x.Id == animal.Id))
            {
                return NotFound();
            }

            _db.Update(animal);
            await _db.SaveChangesAsync();
            return Ok(animal);
        }
        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Goods>> Delete(int id)
        {
            Goods animal = _db.Goods.FirstOrDefault(x => x.Id == id);
            if (animal == null)
            {
                return NotFound();
            }
            _db.Goods.Remove(animal);
            await _db.SaveChangesAsync();
            return Ok(animal); ;
        }
    }
}
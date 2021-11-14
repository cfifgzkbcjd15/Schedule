using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Schedule.Data;
using Schedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleApiController: Controller
    {
        private readonly ApplicationContext db;
        public ScheduleApiController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedulee>>> Get()
        {
            return await db.Schedulee
                .Include(x => x.Lecture)
                .Include(x => x.Teachers)
                .Include(x => x.Group)
                .Include(x => x.Level)
                .Include(x => x.EducationalInstitutions)
                .Include(x => x.Time)
                .Include(x => x.Specialization)
                .Include(x => x.Group.subGroups)
                .ToListAsync();
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Schedulee>> Get(int id)
        {
            Schedulee schedulee = await db.Schedulee.FirstOrDefaultAsync(x => x.Id == id);
            if (schedulee == null)
                return NotFound();
            return new ObjectResult(schedulee);
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<Schedulee>> Post(Schedulee schedulee)
        {
            if (schedulee == null)
            {
                return BadRequest();
            }

            db.Schedulee.Add(schedulee);
            await db.SaveChangesAsync();
            return Ok(schedulee);
        }

        // PUT api/users/
        [HttpPut]
        public async Task<ActionResult<Schedulee>> Put(Schedulee schedulee)
        {
            if (schedulee == null)
            {
                return BadRequest();
            }
            if (!db.Schedulee.Any(x => x.Id == schedulee.Id))
            {
                return NotFound();
            }

            db.Update(schedulee);
            await db.SaveChangesAsync();
            return Ok(schedulee);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Schedulee>> Delete(int id)
        {
            Schedulee schedulee = db.Schedulee.FirstOrDefault(x => x.Id == id);
            if (schedulee == null)
            {
                return NotFound();
            }
            db.Schedulee.Remove(schedulee);
            await db.SaveChangesAsync();
            return Ok(schedulee);
        }
    }
}

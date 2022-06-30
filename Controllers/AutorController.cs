using APIBiblioteca.Context;
using APIBiblioteca.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIBiblioteca.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public AutorController(ApplicationDBContext context)
        {
            this.context = context;
        }
        // GET: api/<AutorController>
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get()
        {
            return context.Autor.Include(x => x.Libros).ToList();
        }

        // GET api/<AutorController>/5
        [HttpGet("{id}", Name ="ObtenerAutor")]
        public ActionResult<Autor> Get(int id)
        {
            var autor = context.Autor.Include(x => x.Libros).FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }

        // POST api/<AutorController>
        [HttpPost]
        public ActionResult Post([FromBody] Autor autor)
        {
            context.Autor.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.Id }, autor);

        }

        // PUT api/<AutorController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Autor autor, int id)
        {
            if (id != autor.Id)
            {
                return BadRequest();

            }

            context.Entry(autor).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        // DELETE api/<AutorController>/5
        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id)
        {
            var autor = context.Autor.FirstOrDefault(x => x.Id == id);
            if (autor == null)
            {
                return NotFound();

            }

            context.Entry(autor).State = EntityState.Deleted;
            context.SaveChanges();
            return autor;
        }
    }
}

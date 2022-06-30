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
    public class LibroController : ControllerBase
    {
        private readonly ApplicationDBContext context;
        public LibroController(ApplicationDBContext context)
        {
            this.context = context;
        }
        // GET: api/<LibroController>
        [HttpGet]
        public ActionResult<List<Libro>> Get()
        {
            var libros = context.Libro.Include(x => x.Autor).ToList();
            return libros;
        }

        // GET api/<LibroController>/5
        [HttpGet("/{id}", Name ="ObtenerLibro")]
        public ActionResult<Libro> Get(int id)
        {
            var libro = context.Libro.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();
            }

            return libro;
        }

        // POST api/<LibroController>
        [HttpPost]
        public ActionResult Post([FromBody] Libro libro)
        {
            context.Libro.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);

        }

        // PUT api/<LibroController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro)
        {
            if (id!= libro.Id)
            {
                return BadRequest();
            }

            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        // DELETE api/<LibroController>/5
        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id)
        {
            var libro = context.Libro.FirstOrDefault(x => x.Id == id);
            if (libro == null)
            {
                return NotFound();

            }

            context.Libro.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}

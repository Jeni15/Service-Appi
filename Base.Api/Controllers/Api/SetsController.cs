using Base.Model.Models;
using Base.Service.Infrastructure;
using Base.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Base.Api.Controllers.Api
{
    public class SetsController : ApiController
    {
        public EntityService<Set> _setService;

        public SetsController()
        {
            _setService = new EntityService<Set>();

        }

       

        // GET /api/sets
        
        public IHttpActionResult GetSets()
        {
            return Ok(_setService.GetAll());
        }

        //GET /api/sets/1

        /// <summary>
        /// Obtiene set por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Set consultado</returns>
        /// <remarks>Obtiene set por id</remarks>
        public IHttpActionResult GetSet(int id)
        {
            Set set = _setService.GetById(id);

            if (set == null)
                return NotFound();

            return Ok(set);
        }

        //POST /api/sets
        [HttpPost]
        public IHttpActionResult CreateSet(Set set)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _setService.Create(set);

            return Created(new Uri(Request.RequestUri + "/" + set.IDSet), set);
        }

        //PUT /api/sets/1
        [HttpPut]
        public IHttpActionResult UpdateSet(int id, Set set)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Set setInDb = _setService.GetById(id);

            if (setInDb == null)
                return NotFound();

            setInDb.Activa = set.Activa;
            setInDb.Descripcion = set.Descripcion;
            setInDb.AliasGAMS = set.AliasGAMS;
            setInDb.IDSet_Padre = set.IDSet_Padre;
            setInDb.IdVersion = set.IdVersion;
            setInDb.Nombre = set.Nombre;
            setInDb.Usuario_UltMod = set.Usuario_UltMod;
            setInDb.Fecha_UltMod = DateTime.Now;

            _setService.Update(setInDb);

            return Ok();
        }

        //DELETE /api/sets/1
        [HttpDelete]
        public IHttpActionResult DeleteSet(int id)
        {
            Set setInDb = _setService.GetById(id);

            if (setInDb == null)
                return NotFound();

            _setService.Delete(setInDb);

            return Ok();
        }

    }
}

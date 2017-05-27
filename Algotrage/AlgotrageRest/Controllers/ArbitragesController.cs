using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AlgotrageDAL.Context;
using AlgotrageDAL.Entities;
using AlgotrageDAL.EntityManagers;

namespace AlgotrageRest.Controllers
{
    public class ArbitragesController : ApiController
    {
        private AlgotrageContext db = new AlgotrageContext();
        private ArbitragesDbManager manager = new ArbitragesDbManager();

        

        // GET: api/Arbitrages
        public IEnumerable<Arbitrage> GetArbitrages()
        {
            try
            {
                return db.Arbitrages.Where(x => x.IsActive);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        //public IEnumerable<Arbitrage> GetActiveArbitrages()
        //{
        //    return manager.GetActiveArbitrages();
        //}

        // GET: api/Arbitrages/5
        [ResponseType(typeof(Arbitrage))]
        public async Task<IHttpActionResult> GetArbitrage(int id)
        {
            Arbitrage arbitrage = await db.Arbitrages.FindAsync(id);
            if (arbitrage == null)
            {
                return NotFound();
            }

            return Ok(arbitrage);
        }

        // PUT: api/Arbitrages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArbitrage(int id, Arbitrage arbitrage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != arbitrage.Id)
            {
                return BadRequest();
            }

            db.Entry(arbitrage).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArbitrageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Arbitrages
        [ResponseType(typeof(Arbitrage))]
        public async Task<IHttpActionResult> PostArbitrage(Arbitrage arbitrage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Arbitrages.Add(arbitrage);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = arbitrage.Id }, arbitrage);
        }

        // DELETE: api/Arbitrages/5
        [ResponseType(typeof(Arbitrage))]
        public async Task<IHttpActionResult> DeleteArbitrage(int id)
        {
            Arbitrage arbitrage = await db.Arbitrages.FindAsync(id);
            if (arbitrage == null)
            {
                return NotFound();
            }

            db.Arbitrages.Remove(arbitrage);
            await db.SaveChangesAsync();

            return Ok(arbitrage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArbitrageExists(int id)
        {
            return db.Arbitrages.Count(e => e.Id == id) > 0;
        }
    }
}
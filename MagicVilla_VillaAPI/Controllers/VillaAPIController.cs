using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{

    //[Route("api/[controller]")]
    [Route("api/villaAPI")]
    [ApiController]

    //set to make Swagger set content type to application/json for all endpoints
    //in this controller, and return 406 not acceptable if client requests a
    //format that is not supported
    [Produces("application/json")]

    public class VillaAPIController : ControllerBase
    {
        //inject logger specified (or use default console logger if not specified)
        //in Program.cs into controller for debugging and logging purposes

        //private readonly ILogger<VillaAPIController> _logger;
        //public VillaAPIController(ILogger<VillaAPIController> logger)
        //{
        //    _logger = logger;
        //}

        //use custom logger instead of the default console logger
        private readonly ILogging _logger;
        private readonly ApplicationDbContext _dbContext;

        //public VillaAPIController(ApplicationDbContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        public VillaAPIController(ApplicationDbContext dbContext, ILogging logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            //_logger.LogInformation("Getting all villas");
            _logger.Log("Getting all of the villas","information");
            //return Ok(VillaStore.villaList);
            return Ok(_dbContext.Villas.ToList());
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type=typeof(VillaDTO))]

        ///Get a villa by id, return 200 OK with the villa in the response body if found
        public ActionResult<VillaDTO> GetVilla(int id)
        {

            if (id == 0)
            {
                //_logger.LogError("GetVilla error with id: " + id);  
                _logger.Log("GetVilla error with id: " + id, "error");
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _dbContext.Villas.FirstOrDefault(v => v.Id == id);

            //did not find a villa with that id, return 404 not found
            if (villa == null)
            {
                //_logger.LogError("GetVilla an error, no villa found with id: " + id);
                _logger.Log("GetVilla error, no villa found with id: " + id, "error");
                return NotFound();
            }
            //_logger.LogInformation("Retrieving a villa found with id: " + id);
            _logger.Log("Retrieving villa found with id: " + id, "information");
            return Ok(villa);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //if (VillaStore.villaList.FirstOrDefault(v => v.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            if (_dbContext.Villas.FirstOrDefault(v => v.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Villa with that name already exists");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }

            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            //get maximum id in the list and add 1 to it, then assign it to villaDTO
            //villaDTO.Id = VillaStore.villaList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
            //VillaStore.villaList.Add(villaDTO);

            //must create a model to write to SQL table, since EF Core needs to use model to update db
            Villa villa = new()
            {
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancy = villaDTO.Occupancy,
                ImageUrl = villaDTO.ImageUrl,
                Amenity = villaDTO.Amenity,
            };

            _dbContext.Villas.Add(villa);
            _dbContext.SaveChanges();

            //return route to GetVilla with the id of the newly created villa,
            //and return the villaDTO in the response body
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {

            if (id == 0)
            {
                _logger.Log("id=0", "error");
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            var villa = _dbContext.Villas.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                _logger.Log("Can not delete villa with id: " + id, "error");
                return NotFound();
            }

            //VillaStore.villaList.Remove(villa);
            _dbContext.Villas.Remove(villa);
            _dbContext.SaveChanges();
            _logger.Log("Deleted villa with id: " + id, "information");
            return NoContent();

        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                _logger.Log("Can not update data for villa with id: " + id, "error");
                return BadRequest();
            }
            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
            //villa.Name = villaDTO.Name;
            //villa.Sqft = villaDTO.Sqft;
            //villa.Occupancy = villaDTO.Occupancy;

            //we need to create a model to write to SQL table, since EF Core needs to use model to update db
            Villa villa = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancy = villaDTO.Occupancy,
                ImageUrl = villaDTO.ImageUrl,
                Amenity = villaDTO.Amenity,
            };

            //check to see if this villa exists in the database, if not return 404 not found
            var result = this.GetVilla(villa.Id);
            //if the villa to be updated does not exist, return 404 not found
            if (result.Value == null)
            {
                _logger.Log("Can not update data for villa with id: " + id, "error");
                return NotFound();
            }

            _dbContext.Villas.Update(villa);
            _dbContext.SaveChanges();
            _logger.Log("Updated villa with id: " + id, "information");

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {

            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);

            //EF Core needs to track the entity to update it, so we need to get the villa
            //from the database without tracking it, then apply the patch to it,
            //then update it in the database
            var villa = _dbContext.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                _logger.Log("Can not update partial data for villa with id: " + id, "error");
                return BadRequest();
            }

            //patchDTO is used to apply the patch to the villa, but we need to create a
            //DTO to apply the patch to it, since the patch is for the DTO, not the model we 
            //just got from the database
            VillaDTO villaDTO = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Details = villa.Details,
                Rate = villa.Rate,
                Sqft = villa.Sqft,
                Occupancy = villa.Occupancy,
                ImageUrl = villa.ImageUrl,
                Amenity = villa.Amenity,
            };

            //patchDTO.ApplyTo(villa, ModelState);
            //we need to apply the patch to the DTO, since the patch is for the DTO,
            //not the model we just got from the database
            patchDTO.ApplyTo(villaDTO, ModelState);

            //we need to create a model to write to SQL table, since EF Core needs to use
            //model to update db
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Details = villaDTO.Details,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,
                Occupancy = villaDTO.Occupancy,
                ImageUrl = villaDTO.ImageUrl,
                Amenity = villaDTO.Amenity,
            };

            _dbContext.Villas.Update(model);
            _dbContext.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}

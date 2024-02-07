using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DifficultiesController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        public DifficultiesController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: https://localhost:port/Difficulties
        [HttpGet]
        public IActionResult GetAll()
        {
            var difficultyModel = dbContext.Difficulties.ToList();
            if (difficultyModel == null) return NotFound();

            return Ok(difficultyModel);
        }

        // GET: https://localhost:port/Difficulties/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var difficultyModel = dbContext.Difficulties.FirstOrDefault(x => x.Id == id);
            if(difficultyModel == null) return NotFound();

            return Ok(difficultyModel);
        }

        // POST: https://localhost:port/Difficulties
        [HttpPost]
        public IActionResult Create([FromBody] AddDifficultyDTO addDifficultyDTO)
        {
            var difficultyModel = new Difficulty
            {
                Name = addDifficultyDTO.Name,
            };

            // Map DTO to Domain Model
            dbContext.Difficulties.Add(difficultyModel);
            dbContext.SaveChanges();

            var difficultyDTO = new DifficultyDTO
            {
                Id = addDifficultyDTO.Id,
                Name = addDifficultyDTO.Name,
            };

            return Ok(difficultyDTO);
        }

        // PUT: https://localhost:port/Difficulties/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateDifficultyDTO updateDifficultyDTO)
        {
            var difficultyModel = dbContext.Difficulties.FirstOrDefault(x => x.Id == id);
            if (difficultyModel == null) return NotFound();

            difficultyModel.Name = updateDifficultyDTO.Name;

            dbContext.SaveChanges();

            var difficultyDTO = new DifficultyDTO
            {
                Id = difficultyModel.Id,
                Name = difficultyModel.Name,
            };

            return Ok(difficultyDTO);
        }

        // DELETE: https://localhost:port/Difficulties/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var difficultyModel = dbContext.Difficulties.FirstOrDefault(x => x.Id == id);
            if (difficultyModel == null) return NotFound();


            dbContext.Difficulties.Remove(difficultyModel);
            dbContext.SaveChanges();

            var difficultyDTO = new DifficultyDTO
            {
                Id = difficultyModel.Id,
                Name = difficultyModel.Name,
            };

            return Ok(difficultyDTO);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IWalksRepository walksRepository;
        private readonly IMapper mapper;

        public WalksController(NZWalksDbContext dbContext, IWalksRepository walksRepository, IMapper mapper)
        {

            this.dbContext = dbContext;
            this.walksRepository = walksRepository;
            this.mapper = mapper;
        }

        // GET: https://localhost:port/Walks
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber =1, [FromQuery] int pageSize=1000)
        {
            //var walksModel = dbContext.Walks.ToList();
            //if (walksModel == null)
            //    return NotFound();

            // Auto Mapper
            var walksModel = await walksRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            if (walksModel == null)
                return NotFound();
           
            return Ok(mapper.Map<List<WalkDTO>>(walksModel));
        }

        // GET: https://localhost:port/Walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var walksModel = dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if (walksModel == null) return NotFound();

            return Ok(walksModel);
        }

        // POST: https://localhost:port/Walks
        [HttpPost]
        public IActionResult Create([FromBody] AddWalkDTO addWalkDTO)
        {
            // Map or Covert DTO to Domain Model
            var walksModel = new Walk
            {
                Id = addWalkDTO.Id,
                Name = addWalkDTO.Name,
                Description = addWalkDTO.Description,
                LengthInKm = addWalkDTO.LengthInKm,
                WalkImageUrl = addWalkDTO.WalkImageUrl,
                DifficultyId = addWalkDTO.DifficultyId,
                RegionId = addWalkDTO.RegionId,
            };

            dbContext.Walks.Add(walksModel);
            dbContext.SaveChanges();

            // Map Domain Model to DTO
            var walkDTO = new WalkDTO
            {
                Id = walksModel.Id,
                Name = walksModel.Name,
                Description = walksModel.Description,
                WalkImageUrl = walksModel.WalkImageUrl,
                LengthInKm = walksModel.LengthInKm,
                DifficultyId = walksModel.DifficultyId,
                RegionId = walksModel.RegionId,
            };

            return Ok(walkDTO);
        }

        // PUT: https://localhost:port/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateWalkDTO updateWalkDTO)
        {
            var walkModel = dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if (walkModel == null) return NotFound();

            walkModel.Name = updateWalkDTO.Name;
            walkModel.Description = updateWalkDTO.Description;
            walkModel.LengthInKm = updateWalkDTO.LengthInKm;

            // Update Database
            dbContext.SaveChanges();


            // Map Domain Model to DTO
            var walkDTO = new WalkDTO
            {
                Id = walkModel.Id,
                Name = walkModel.Name,
                Description = walkModel.Description,
                WalkImageUrl = walkModel.WalkImageUrl,
                LengthInKm = walkModel.LengthInKm,
                DifficultyId = walkModel.DifficultyId,
                RegionId = walkModel.RegionId,
            };

            return Ok(walkDTO);
        }

        // DELETE: https://localhost:port/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var walkModel = dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if (walkModel == null) return NotFound();

            // Delete Record from Database
            dbContext.Walks.Remove(walkModel);
            dbContext.SaveChanges();

            var walkDTO = new WalkDTO
            {
                Id = walkModel.Id,
                Name = walkModel.Name,
                Description = walkModel.Description,
                WalkImageUrl = walkModel.WalkImageUrl,
                LengthInKm = walkModel.LengthInKm,
                DifficultyId = walkModel.DifficultyId,
                RegionId = walkModel.RegionId,
            };

            return Ok(walkDTO);
        }
    }
}

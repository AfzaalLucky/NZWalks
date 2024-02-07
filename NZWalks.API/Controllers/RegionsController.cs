using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Text.Json;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> _logger;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            _logger = logger;
        }

        //// GET: https://localhost:port/Regions
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var regions = dbContext.Regions.ToList();
        //    if (regions == null)
        //        return NotFound();
        //    return Ok(regions);
        //}

        // GET: https://localhost:port/Regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("GetAllRegions Action is Invoked!");
            //var regions = await dbContext.Regions.ToListAsync();
            var regions = await regionRepository.GetAllAsync();
            if (regions == null)
                return NotFound();

            //var regionDTO = new List<RegionDTO>();

            //foreach (var region in regions)
            //{
            //    regionDTO.Add(new RegionDTO()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}

            // Auto Mapper
            _logger.LogInformation($"Finished GetAllRegions request with data:{JsonSerializer.Serialize(regions)}");
            return Ok(mapper.Map<List<RegionDTO>>(regions));
        }

        // GET: https://localhost:port/Regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var regions = dbContext.Regions.Find(id);
            //var regions = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            var regions = await regionRepository.GetByIdAsync(id);
            if (regions == null)
                return NotFound();

            // Auto Mapper
            return Ok(mapper.Map<RegionDTO>(regions));
        }

        // POST: https://localhost:port/Regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionDTO addRegionDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            // Auto Mapper
            var regionModel = mapper.Map<Region>(addRegionDTO);

            // Map or Convert DTO to Domain Model
            //var regionModel = new Region
            //{
            //    Code = addRegionDTO.Code,
            //    Name = addRegionDTO.Name,
            //    RegionImageUrl = addRegionDTO.RegionImageUrl,
            //};

            // Use Domain Model to Create Region
            //dbContext.Regions.Add(regionModel);
            //dbContext.SaveChanges();

            regionModel = await regionRepository.CreateAsync(regionModel);

            // Map Domain Model to DTO
            //var region = new RegionDTO
            //{
            //    Id = regionModel.Id,
            //    Code = regionModel.Code,   
            //    Name = regionModel.Name,
            //    RegionImageUrl = regionModel.RegionImageUrl,
            //};

            // Auto Mapper
            return Ok(mapper.Map<RegionDTO>(regionModel));
        }

        // PUT: https://localhost:port/Regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        //[ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO)
        {
            //var regionModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            var regionModel = await regionRepository.GetByIdAsync(id);
            if (regionModel == null) return NotFound();

            // Map DTO To Domain Model
            //regionModel.Code = updateRegionDTO.Code;
            //regionModel.Name = updateRegionDTO.Name;
            //regionModel.RegionImageUrl = updateRegionDTO.RegionImageUrl;

            // Auto Mapper
            regionModel = mapper.Map<Region>(updateRegionDTO);

            //regionModel = new Region
            //{
            //    Code = updateRegionDTO.Code,
            //    Name = updateRegionDTO.Name,
            //    RegionImageUrl = updateRegionDTO.RegionImageUrl,
            //};

            regionModel = await regionRepository.UpdateAsync(id, regionModel);
            // Update Region
            //dbContext.SaveChanges();

            //var regionDTO = new RegionDTO
            //{
            //    Id = regionModel.Id,
            //    Code = regionModel.Code,
            //    Name = regionModel.Name,
            //    RegionImageUrl = regionModel.RegionImageUrl,
            //};

            // Auto Mapper
            return Ok(mapper.Map<RegionDTO>(regionModel));
        }

        // DELETE: https://localhost:port/Regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regionModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            var regionModel = await regionRepository.GetByIdAsync(id);
            if (regionModel == null) return NotFound();


            //// Delete Record
            //dbContext.Remove(regionModel);
            //dbContext.SaveChanges();

            regionModel = await regionRepository.DeleteAsync(id);

            // Map Domain Model to DTO
            //var region = new RegionDTO
            //{
            //    Id = regionModel.Id,
            //    Code = regionModel.Code,
            //    Name = regionModel.Name,
            //    RegionImageUrl = regionModel.RegionImageUrl,
            //};

            // Auto Mapper
            return Ok(mapper.Map<RegionDTO>(regionModel));
        }
    }
}

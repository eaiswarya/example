using example.API.Data;
using example.API.Models.Domain;
using example.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace example.API.Controllers
{
    //https://localhost:8080/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ExampleDbContext dbContext;

        public RegionsController(ExampleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        
        //GET ALL REGIONS

        [HttpGet]
        public IActionResult GetAll()
        {
            //get data from database-domain models
            var regionsDomain= dbContext.Regions.ToList();

            //map domain models to dtos
            var regionsDto=new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl

                });
            }
            
            return Ok(regionsDto);
        }
        //GET SINGLE REGION
        //https://localhost:8080/api/regions/{id}

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var regionDomain = dbContext.Regions.Find(id);

            //another method
            // var region=dbContext.Regions.FirstOrDefault(x=>x.id==id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //map/convert region domain or regiondto
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            //return dto back to client
            return Ok(regionDto);
        }
        //post to create new region
        //post:https://localhost:8080/api/regions
        [HttpPost]
        public IActionResult Create([FromBody]AddRegionRequestDto addRegionRequestDto)
        {
            //map dto to domain model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            //use domain to create
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //map domain model back to dto
            var regionDto = new RegionDto
            {
                Id=regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl

            };

            return CreatedAtAction(nameof(GetById),new {id=regionDomainModel.Id},regionDto);
        }
    }
}

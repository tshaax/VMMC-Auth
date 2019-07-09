using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VMMC.Auth.Web.API.ScaffoldDb;
using VMMC.Auth.Web.API.Services;

namespace VMMC.Auth.Web.API.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    /// 
    [ApiController]
    [Route("api/[controller]")]
    public class FunderController : ControllerBase
    {
        private readonly IGenericRepository<Funders> _FunderRepo;

        /// <summary>
        /// AccountController
        /// </summary>
        /// <param name="funderRepo"></param>
        public FunderController(IGenericRepository<Funders> funderRepo)
        {
            _FunderRepo = funderRepo;
    
        }

        /// <summary>
        /// Add Funder 
        /// </summary>
        /// <param name="funderModel"></param>
        /// <returns></returns>
        [HttpPost]
      
        public async Task<IActionResult> AddFunder([FromBody] Funders funder)
        {
          
            return this.Ok(await _FunderRepo.Create(funder));
        }

        /// <summary>
        /// Get All Funders
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetFunder([FromRoute] int id)
        {
            return id == 0 ? this.Ok(_FunderRepo.GetAll().Where(w => !w.IsDeleted))
                : this.Ok(_FunderRepo.GetAll().Where(w => w.FunderId == id && !w.IsDeleted));

        }

        /// <summary>
        /// Update Funder 
        /// </summary>
        /// <param name="funderModel"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateFunder([FromBody] Funders funder)
        {
           
            return this.Ok(await _FunderRepo.Update(funder));
        }

        /// <summary>
        /// Delete Funder 
        /// </summary>
        /// <param name="funderModel"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFunder([FromRoute] int id)
        {
            var entity = _FunderRepo.GetAll().Where(w => w.FunderId == id).FirstOrDefault();
            entity.IsDeleted = true;
            return this.Ok(await _FunderRepo.Update(entity));
        }

    }
}
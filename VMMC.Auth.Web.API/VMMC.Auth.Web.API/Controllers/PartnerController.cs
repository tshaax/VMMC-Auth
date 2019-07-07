using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VMMC.Auth.Web.API.Db;
using VMMC.Auth.Web.API.Services;

namespace VMMC.Auth.Web.API.Controllers
{
    /// <summary>
    /// PartnerController
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PartnerController : ControllerBase
    {
        private readonly IGenericRepository<Partners> _Repo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public PartnerController(IGenericRepository<Partners> repo)
        {
            _Repo = repo;
        }  
        
        /// <summary>
        /// Get All Partners
        /// </summary>
        /// <returns></returns>
        [HttpGet("Funder/{id}")]
        public IActionResult GetPartners([FromRoute] int id)
        {
            return id == 0 ? this.Ok(_Repo.GetAll().Where(w => !w.IsDeleted)) 
                : this.Ok(_Repo.GetAll().Where(w => w.FunderId == id && !w.IsDeleted));

        }

        /// <summary>
        /// Add Partner
        /// </summary>
        /// <param name="partners"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddPartner([FromBody] Partners partners)
        {
            return this.Ok(await _Repo.Create(partners));
        }

        /// <summary>
        /// Update Partner
        /// </summary>
        /// <param name="partners"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePartner([FromBody] Partners partners)
        {
            return this.Ok(await _Repo.Create(partners));
        }

        /// <summary>
        /// Delete Partner
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePartner([FromRoute] int id)
        {
            var entity = _Repo.GetAll().Where(w => w.FunderId == id).FirstOrDefault();
            entity.IsDeleted = true;
            return this.Ok(await _Repo.Update(entity));
        }
    }
}
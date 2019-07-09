using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VMMC.Auth.Web.API.ScaffoldDb;
using VMMC.Auth.Web.API.Services;

namespace VMMC.Auth.Web.API.Controllers
{
    /// <summary>
    /// ServiceProviderController
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class ServiceProviderController : ControllerBase
    {
        private readonly IGenericRepository<ServiceProviders> _Repo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        public ServiceProviderController(IGenericRepository<ServiceProviders> repo)
        {
            _Repo = repo;
        }

        /// <summary>
        /// Get All Partners
        /// </summary>
        /// <returns></returns>
        [HttpGet("Partner/{id}")]
        public IActionResult GetProviders([FromRoute] int id)
        {
            return id == 0 ? this.Ok(_Repo.GetAll().Where(w=> !w.IsDeleted.Value))
                : this.Ok(_Repo.GetAll().Where(w => w.PartnerId == id && !w.IsDeleted.Value));
        }

        /// <summary>
        /// Add Service Provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddProvider([FromBody] ServiceProviders provider)
        {
            return this.Ok(await _Repo.Create(provider));
        }

        /// <summary>
        /// Update Service Provider
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateProvider([FromBody] ServiceProviders provider)
        {
            return this.Ok(await _Repo.Update(provider));
        }
        
        /// <summary>
        /// Delete Service Provider
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProvider([FromRoute] int id)
        {
            var entity = _Repo.GetAll().Where(w => w.PartnerId == id).FirstOrDefault();
            entity.IsDeleted = true;
            return this.Ok(await _Repo.Update(entity));
        }
    }
}
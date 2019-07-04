using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VMMC.Auth.DataAccess.Models;
using VMMC.Auth.Web.API.Data;
using VMMC.Auth.Web.API.Db;
using VMMC.Auth.Web.API.Models.Metadata;
using VMMC.Auth.Web.API.Services;

namespace VMMC.Auth.Web.API.Controllers
{
    /// <summary>
    /// AccountController
    /// </summary>
    /// 
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IGenericRepository<Funders> _FunderRepo;
        private readonly IGenericRepository<Partners> _PartnerRepo;
        private readonly IGenericRepository<ServiceProviders> _ProviderRepo;

        /// <summary>
        /// AccountController
        /// </summary>
        /// <param name="funderRepo"></param>
        /// <param name="partnerRepo"></param>
        /// <param name="providerRepo"></param>
        public AccountController(IGenericRepository<Funders> funderRepo
            , IGenericRepository<Partners> partnerRepo
            , IGenericRepository<ServiceProviders> providerRepo)
        {
            _FunderRepo = funderRepo;
            _PartnerRepo = partnerRepo;
            _ProviderRepo = providerRepo;
        }
        /// <summary>
        /// Add Funder 
        /// </summary>
        /// <param name="funderModel"></param>
        /// <returns></returns>
        [HttpPost("Funder")]
        [Authorize]
        public async Task<IActionResult> AddFunder([FromBody] FunderModel funderModel)
        {
            var entity = Mapping.Mapper.Instance.Funder(funderModel);
            return this.Ok(await _FunderRepo.Create(entity));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VMMC.Auth.Web.API.ScaffoldDb;
using VMMC.Auth.Web.API.Services;

namespace VMMC.Auth.Web.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IGenericRepository<Users> _UserRepo;
        private readonly IGenericRepository<AccountType> _AccTypeRepo;
        public AccountController(IGenericRepository<Users> userRepo, IGenericRepository<AccountType> accRepo)
        {
            _UserRepo = userRepo;
            _AccTypeRepo = accRepo;
        }
        
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetUsers()
        {
            return this.Ok(_UserRepo.GetAll());
        }

        /// <summary>
        /// Get All Users by funder id
        /// </summary>
        /// <returns></returns>
        [HttpGet("Type/{typeId}/Actual/{id}")]
        public IActionResult GetUsersByTypeId(int typeId, int id)
        {
            switch (typeId)
            {
                case 1:
                    return this.Ok(_UserRepo.GetAll().Where(w => w.FunderId == id));
                case 2:
                    return this.Ok(_UserRepo.GetAll().Where(w => w.PartnerId == id));
                case 3:
                    return this.Ok(_UserRepo.GetAll().Where(w => w.ProviderId == id));
                default:
                    return this.Ok();

            };
        }
        
        /// <summary>
        /// Get All Account Types
        /// </summary>
        /// <returns></returns>
        [HttpGet("Types")]
        public IActionResult GetAccTypes()
        {
            var res = _AccTypeRepo.GetAll();

            return this.Ok(res);
        }

        /// <summary>
        /// Get All Account Types by Funder
        /// </summary>
        /// <returns></returns>
        [HttpGet("Types/{id}")]
        public IActionResult GetAccTypesById(int id)
        {
            switch (id)
            {
                case 1 :
                    return this.Ok(_AccTypeRepo.GetAll());
                case 2:
                    return this.Ok(_AccTypeRepo.GetAll().Where(w => w.TypeId == 2 || w.TypeId == 3));
                case 3:
                    return this.Ok(_AccTypeRepo.GetAll().Where(w => w.TypeId == 3));                   
                default:
                    return this.Ok();
               
            }

        }
    }
}
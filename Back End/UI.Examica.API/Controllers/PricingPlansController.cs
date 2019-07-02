using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http2.HPack;
using Microsoft.EntityFrameworkCore;
using UI.Examica.API.Dtos;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Helpers;
using UI.Examica.Model.Persistence;
using StatusCodes = Microsoft.AspNetCore.Http.StatusCodes;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingPlansController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public PricingPlansController(IUnitOfWork _unitOfWork, UserManager<AppUser> _userManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;

        }

        // GET: api/PricingPlans
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<PricingPlan>>> GetPricingPlans()
        {
            return Ok( await unitOfWork.PricingPlans.GetAll());
        }

        // GET: api/PricingPlans/5
        [Produces("application/json")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PricingPlan>> GetPricingPlan(int id)
        {
         
            var pricingPlan = await unitOfWork.PricingPlans.GetById(id);
         
            if (pricingPlan == null)
            {
                return NotFound();
            }

            return Ok(pricingPlan);
            
          

        }
        //GET :api/PricingPlan/GetPricingPlanByOrgnization/2
        [HttpGet("[action]/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]


       public async Task<ActionResult<PricingPlan>> GetPricingPlanByOrg(int id)
        {
            Organization organization = await unitOfWork.Organizations.SingleOrDefault(o => o.Id == id);
            if (organization == null) return NotFound();
            AppUser user = await userManager.GetUserAsync(User);
            user = unitOfWork.AppUsers.GetUserWithOrgs(user.Id);
            if (user.IsOwnerOfOrg(id))
            {
                PricingPlan pricingPlan = await unitOfWork.PricingPlans.SingleOrDefault(pPlan => pPlan.Id == organization.PricingPlanId);
                if (pricingPlan == null) return NotFound();
                PricingPlanDto pricingPlanDto = Mapper.Map<PricingPlanDto>(pricingPlan);
                return Ok(pricingPlanDto);
            }
            else
            {
                 return Unauthorized("You are not an owner!");
            }
        }




        //PUT: api/PricingPlans/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> PutPricingPlan(int id, [FromBody] PricingPlanDto pricingPlanVM)
        {
            PricingPlan pPlan = await  unitOfWork.PricingPlans.SingleOrDefault(p => p.Id == id);
            if (pPlan == null || pPlan.Id != id) return NotFound();
            pPlan = Mapper.Map(pricingPlanVM, pPlan);
            unitOfWork.PricingPlans.Update(pPlan);
            int result = await unitOfWork.SaveAsync();
            if (result < 1) return BadRequest();
            else return Ok(pPlan);
        }

        //// POST: api/PricingPlans

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<PricingPlan>> PostPricingPlan([FromBody]PricingPlan pricingPlan)
        {
            await unitOfWork.PricingPlans.Add( pricingPlan);
            int result = await unitOfWork.SaveAsync();
            if (result < 1) return BadRequest();
            else return Ok(pricingPlan);
        }

        // DELETE: api/PricingPlans/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> DeletePricingPlan(int id)
        {
            Organization org = await unitOfWork.Organizations.SingleOrDefault(o => o.PricingPlanId == id);
            if (org != null) return BadRequest("Delete Organizations on this pricing plan first");
            PricingPlan pricingPlan = await unitOfWork.PricingPlans.GetById(id);
            if (pricingPlan == null)
            {
                return NotFound();
            }

            unitOfWork.PricingPlans.Remove(pricingPlan);
            return await unitOfWork.SaveAsync();
        }

       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UI.Examica.Model.Core;
using UI.Examica.Model.Core.Domains;
using UI.Examica.Model.Persistence;
using AutoMapper;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using UI.Examica.Model.Helpers;

namespace UI.Examica.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase 
    {
        private readonly IUnitOfWork unitofwork;
        private readonly UserManager<AppUser> userManager;

        public ResultsController(IUnitOfWork _unitofwork, UserManager<AppUser> _userManager)
        {
            unitofwork = _unitofwork;
            userManager = _userManager;
        }

      
        // GET: api/Results
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Result>>> GetResults()
        {
            return Ok(await unitofwork.Results.GetAll());
        }


        // GET: api/Results/22
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetResult(string userid , int id)
        {
            AppUser user = await userManager.GetUserAsync(User);

            user = unitofwork.AppUsers.GetUserWithOrgs(user.Id);


            if (user.IsOwnerOfOrg(id) && user.IsExaminerOfOrg(id) && user.IsAdminOfOrg(id))
            {
                var result = await unitofwork.Results.Find(r => r.UserId == userid);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
                }
              else return Unauthorized("You are not an owner!");
        }


        // GET: api/Results/GetResultByExamId/16
        [HttpGet("[action]/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> GetResultByExamId(int id)
            {
                AppUser user = await userManager.GetUserAsync(User);
                user = unitofwork.AppUsers.GetUserWithOrgs(user.Id);

                var result = await unitofwork.Results.Find(r => r.ExamId == id);

                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
 
        }


        // POST: api/Results
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Result>> PostResult([FromBody] Result result )
        {
            AppUser user = await userManager.GetUserAsync(User);

            user = unitofwork.AppUsers.GetUserWithOrgs(user.Id);



            if (user.IsAdminOfOrg(result.Exam.OrganizationId) && user.IsExaminerOfOrg(result.Exam.OrganizationId))
            {

                Result resFromDB = await unitofwork.Results.SingleOrDefault(r => r.ExamId == result.ExamId && r.UserId == result.UserId);

                if (resFromDB != null) return BadRequest("ALready Exists!");

                await unitofwork.Results.Add(result);

                int res = await unitofwork.SaveAsync();

                if (res > 0) return Ok(result);

                else return BadRequest();
            }
            
              else return Unauthorized("You are not an owner!");
            
        }





        //DELETE: api/Results/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Result>> DeleteByExamId(int id)
        {
            var result = await unitofwork.Results.SingleOrDefault(r => r.ExamId == id);
            if (result == null)
            {
                return NotFound();
            }

            unitofwork.Results.Remove(result);
            await unitofwork.SaveAsync();

            return Ok(result);
        }



        //DELETE: api/Results/DeleteByUserId/5
        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Result>> DeleteByUserId(string id)
        {
            var result = await unitofwork.Results.SingleOrDefault(r => r.UserId == id);
            if (result == null)
            {
                return NotFound();
            }

            unitofwork.Results.Remove(result);
            await unitofwork.SaveAsync();

            return Ok(result);
        }




        // PUT: api/Results/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutResult(int id ,[FromBody] Result resultVM)
        {
            Result result = await unitofwork.Results.SingleOrDefault(r => r.ExamId == id);

            result = Mapper.Map(resultVM, result);

            unitofwork.Results.Update(result);

            await unitofwork.SaveAsync();

            return Ok(result);
        }
    }
}
    
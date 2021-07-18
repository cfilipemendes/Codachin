using Codachin.Services;
using Codachin.Services.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codachin.API.Controllers
{
    [ApiController]
    [Route("")]
    public class GitContoller : ControllerBase
    {

        private readonly ILogger<GitContoller> _logger;
        private IGitService gitService;


        public GitContoller(ILogger<GitContoller> logger, IGitService gitService)
        {
            _logger = logger;
            this.gitService = gitService;
        }


        [HttpGet("log")]
        public async Task<ActionResult> GetLog([FromQuery(Name = "Url")] string url, [FromQuery] PaginationFilter pager)
        {
            try
            {
                using var gitApi = gitService;
               
                Response<IEnumerable<Commit>> response = new Response<IEnumerable<Commit>>();
                try
                {
                    return Ok(new PagedResponse<IEnumerable<Commit>>(await gitApi.Init(url).GetLogAsync(pager),pager.Page,pager.PerPage));
                }
                catch(Exception e)
                {
                    _logger.LogError(e, typeof(GitContoller).Name);
                    response.Message = $"Unable to get git commits from API. Using CLI as backup. Error was: {e.Message}";
                }
                
                using var gitCli = new GitCliService(new GitUrlValidator()).Init(url);
                return Ok(new PagedResponse<IEnumerable<Commit>>(await gitCli.GetLogAsync(pager), pager.Page, pager.PerPage));

            }
            catch (Exception e)
            {
                _logger.LogError(e, typeof(GitContoller).Name);
                return BadRequest(new Response<string>()
                {
                    Succeeded = false,
                    Message = e.Message
                });
            }
        }
    }
}

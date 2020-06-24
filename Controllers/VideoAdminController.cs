using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WandaWebAdmin.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WandaWebAdmin.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VideoAdminController : ControllerBase
    {
        private readonly IVimeoService _vimeoService;
        public VideoAdminController(IVimeoService vimeoService)
        {
            _vimeoService = vimeoService;
        }
        // GET: api/<VideoAdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/<VideoAdminController>
        //[Route("api/VideoAdmin/Sync")]
        
        public IEnumerable<string> Sync()
        {
            try
            {
                _vimeoService.SyncDataFromVimeo();
                return new string[] { "result", "success" };
            }
            catch(Exception ex)
            {
                return new string[] { "result", $"Error occur: {ex.Message}" };
            }
            
        }

        // GET api/<VideoAdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VideoAdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VideoAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VideoAdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

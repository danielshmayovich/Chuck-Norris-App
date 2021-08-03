using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class JokesController : ApiController
    {
        [HttpGet]
        [Route("api/jokes/get")]
        public IHttpActionResult GetJokesEntry()
        {
            using (var bl = new BL.JokesLogic())
            {
                return Ok<List<DAL.Models.Joke>>(bl.GetJokes());
            };
        }

        [HttpPost]
        [Route("api/joke/votes/post")]
        public IHttpActionResult GetJokesEntry([FromBody]DAL.Models.Joke joke)
        {
            using (var bl = new BL.JokesLogic())
            {
                return Ok<bool>(bl.UpdateJokeLike(joke.Id));
            };
        }

        
    }
}

using Microsoft.AspNetCore.Mvc;
using RugbyResults.DAL.Matches;
using RugbyResults.Domain.Matches;
using System.Collections.Generic;

namespace RugbyResults.Matches
{
    /// <summary>
    /// Handles the API requests for the Match resource
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private IMatchDal _matchDal;

        /// <summary>
        /// Initializes a new instance of MatchController
        /// </summary>
        /// <param name="matchDal">The Match DAL</param>
        public MatchController(IMatchDal matchDal)
        {
            _matchDal = matchDal;
        }

        /// <summary>
        /// Gets all matches
        /// </summary>
        /// <returns>The list of all matches</returns>
        [HttpGet]
        public List<RugbyMatch> Get()
        {
            List<RugbyMatch> result = _matchDal.GetAll();

            return result;
        }

        /// <summary>
        /// Gets a match by the specific ID
        /// </summary>
        /// <param name="matchId">The match ID</param>
        /// <returns></returns>
        [HttpGet("{matchId}")]
        public IActionResult GetById(int matchId)
        {
            RugbyMatch result = _matchDal.GetById(matchId);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

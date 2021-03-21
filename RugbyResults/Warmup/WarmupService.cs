using RugbyResults.DAL.Matches;
using RugbyResults.Domain.Matches;
using RugbyResults.Matches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RugbyResults.Warmup
{
    /// <summary>
    /// Service responsible for warming up the application cache
    /// </summary>
    public class WarmupService : IWarmupService
    {
        private IMatchDal _matchDal;
        private IMatchService _matchService;

        public WarmupService(
            IMatchDal matchDal,
            IMatchService matchService)
        {
            _matchDal = matchDal;
            _matchService = matchService;
        }

        /// <summary>
        /// Executes the warmup
        /// </summary>
        /// <returns></returns>
        public async Task Execute()
        {
            List<RugbyMatch> matches = _matchDal.GetAll();

            if (matches.Count > 0)
            {
                return;
            }

            // Pull the data
            matches = await _matchService.GetByTeamId(103969);

            // Load into the DB
            _matchDal.Add(matches);
        }
    }

    /// <summary>
    /// Defines a service for warming up the application cache
    /// </summary>
    public interface IWarmupService
    {
        public Task Execute();
    }
}

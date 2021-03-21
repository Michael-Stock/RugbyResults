using RugbyResults.Domain.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RugbyResults.DAL.Matches
{
    /// <summary>
    /// Data layer for performing queries against Match data
    /// </summary>
    public class MatchDal : IMatchDal
    {
        /// <summary>
        /// Gets all stored matches
        /// </summary>
        /// <returns>The list of all stored matches</returns>
        public List<RugbyMatch> GetAll()
        {
            using (MatchContext context = new MatchContext())
            {
                List<RugbyMatch> result = context.Matches
                    .ToList();

                return result;
            }
        }

        /// <summary>
        /// Gets a match by a specific ID
        /// </summary>
        /// <param name="matchId">The match ID</param>
        /// <returns></returns>
        public RugbyMatch GetById(int matchId)
        {
            using (MatchContext context = new MatchContext())
            {
                RugbyMatch result = context.Matches
                    .FirstOrDefault(m => m.matchId == matchId);

                return result;
            }
        }

        /// <summary>
        /// Adds the specified matches to the data store
        /// </summary>
        /// <param name="matches">The matches to add</param>
        public void Add(List<RugbyMatch> matches)
        {
            using (MatchContext context = new MatchContext())
            {
                context.Matches.AddRange(matches);

                context.SaveChanges();
            }
        }
    }

    /// <summary>
    /// Defines a data layer for interacting with the match store
    /// </summary>
    public interface IMatchDal
    {
        List<RugbyMatch> GetAll();

        RugbyMatch GetById(int matchId);

        void Add(List<RugbyMatch> matches);
    }
}

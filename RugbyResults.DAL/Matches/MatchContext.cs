using RugbyResults.Domain.Matches;
using System.Data.Entity;

namespace RugbyResults.DAL.Matches
{
    /// <summary>
    /// The context for talking to the Match data layer
    /// </summary>
    public class MatchContext : DbContext
    {
        /// <summary>
        /// Initalizes a new instance of the MatchContext
        /// </summary>
        public MatchContext() : base("MatchContext")
        {
        }

        /// <summary>
        /// Represents the stored Matches
        /// </summary>
        public DbSet<RugbyMatch> Matches { get; set; }
    }
}

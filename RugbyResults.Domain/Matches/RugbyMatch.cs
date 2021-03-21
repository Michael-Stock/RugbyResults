using System;
using System.ComponentModel.DataAnnotations;

namespace RugbyResults.Domain.Matches
{
    /// <summary>
    /// Defines a Rugby Match
    /// </summary>
    public class RugbyMatch
    {
        [Key]
        public int id { get; set; }
        public int matchId { get; set; }
        public string result { get; set; }
        public string stadiumName { get; set; }
        public int sportradarMatchId { get; set; }
        public int matchStatusId { get; set; }
        public int matchTeamID { get; set; }
        public int opponentMatchTeamID { get; set; }
        public string matchStatus { get; set; }
        public DateTime kickOff { get; set; }
        public DateTime kickOffGmt { get; set; }
        public string displayOverDate { get; set; }
        public int opponentTeamId { get; set; }
        public string opponentName { get; set; }
        public string opponentNameShort { get; set; }
        public int stadiumId { get; set; }
        public int placeId { get; set; }
        public string placeName { get; set; }
        public bool isTeam1 { get; set; }
        public bool isAtHome { get; set; }
        public bool isTest { get; set; }
        public bool isResult { get; set; }
        public int pointsFor { get; set; }
        public int pointsAgainst { get; set; }
        public string teamLogoFilename { get; set; }
        public string oppenentTeamLogoFilename { get; set; }
        public string teamLogoFilenameDark { get; set; }
        public string oppenentTeamLogoFilenameDark { get; set; }
        public bool isInProgress { get; set; }
        public DateTime matchUpdated { get; set; }
    }
}

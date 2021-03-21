using RugbyResults.Domain.Matches;
using System;
using System.Collections.Generic;
using System.Text;

namespace RugbyResults.Tests.Mocks
{
    public class RugbyMatchMocks
    {
        public static List<RugbyMatch> GetData()
        {
            List<RugbyMatch> result = new List<RugbyMatch>
            {
                new RugbyMatch()
                {
                    matchId = 1,
                    result = "W",
                    stadiumName = "Old Trafford"
                },
                new RugbyMatch()
                {
                    matchId = 2,
                    result = "W",
                    stadiumName = "Signal Iduna Park"
                },
                new RugbyMatch()
                {
                    matchId = 3,
                    result = "L",
                    stadiumName = "Bernabeu"
                }
            };

            return result;
        }
    }
}

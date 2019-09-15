using System.Collections.Generic;
using System.Linq;
using reactApp.Models;
using reactApp.Models.Positions;

namespace reactApp.Services
{
    public class LineupBuilderService : ILineupBuilderService
    {
        public List<Lineup> Execute(List<Player> playerList){


            var quarterBacks = playerList
                .Where((Player p) => p.Position == Position.QB)
                .OrderByDescending((Player p) => p.Projection)
                .Take(8)
                .Cast<Quarterback>()
                .ToList();

            var runningBacks = playerList
                .Where((Player p) => p.Position == Position.RB)
                .OrderByDescending((Player p) => p.Projection)
                .Take(12)
                .Cast<RunningBack>()
                .ToList();

            var wideReceivers = playerList
                .Where((Player p) => p.Position == Position.WR)
                .OrderByDescending((Player p) => p.Projection)
                .Take(20)
                .Cast<WideReceiver>()
                .ToList();


            

            var tightEnds = playerList
                .Where((Player p) => p.Position == Position.TE)
                .OrderByDescending((Player p) => p.Projection)
                .Take(4)
                .Cast<TightEnd>()
                .ToList();
            
            // could also filter on rb, wr and te
            var flexPlays = playerList
                .Where((Player p) => p is IFlex)
                .Take(40)
                .Cast<IFlex>()
                .ToList();
            
            var defenses = playerList
                .Where((Player p) => p.Position == Position.DST)
                .OrderByDescending((Player p) => p.Projection)
                .Take(4)
                .Cast<Defense>()
                .ToList();

            // build lineups
            var optimizedLineups = new List<Lineup>();

            foreach(var qb in quarterBacks)
            {
                foreach(var rb in runningBacks)
                {
                    foreach(var wr in wideReceivers)
                    {
                        foreach(var te in tightEnds)
                        {
                            foreach(var flex in flexPlays)
                            {
                                foreach (var dst in defenses)
                                {
                                    var lineup = new Lineup(qb, rb, rb, wr, wr, wr, te, flex, dst);

                                }
                            }
                                
                        }
                    }
                }
            }

            // return lineups
            return optimizedLineups;
        }

    }
}

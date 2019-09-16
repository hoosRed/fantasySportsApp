using System;
using System.Collections.Generic;
using System.Linq;
using reactApp.Models;
using reactApp.Models.Positions;


namespace reactApp.Services
{
    public class LineupBuilderService : ILineupBuilderService
    {
        
        public IEnumerable<Lineup> Execute(List<Player> playerList){


            var quarterBacks = BuildQuarterbacks(playerList);

            var rbCombos = BuildRunningBacks(playerList);

            var wrCombos = BuildWideRecievers(playerList);

            var tightEnds = BuildTightEnds(playerList);
            
            // could also filter on rb, wr and te
            var flexPlays = playerList
                .Where((Player p) => p is IFlex)
                .Take(15)
                .Cast<IFlex>()
                .ToList();
            
            var defenses = playerList
                .Where((Player p) => p.Position == Position.DST)
                .OrderByDescending((Player p) => p.Projection)
                .Take(4)
                .Cast<Defense>()
                .ToList();

            var optimizedLineups = BuildLineups(quarterBacks, rbCombos, wrCombos, tightEnds, defenses, flexPlays);

            // return top 20 lineups
            var topLineups = optimizedLineups
                .Where(l => l.Salary <= 60000 && l.Salary >= 40000)
                .OrderByDescending(l => l.Projection)
                .Take(30);
            
            return topLineups;
        }

        public IEnumerable<Lineup> BuildLineups(IEnumerable<Quarterback> quarterBacks,
                                                HashSet<RunningBack[]> rbCombos,
                                                HashSet<WideReceiver[]> wrCombos,
                                                IEnumerable<TightEnd> tightEnds,
                                                IEnumerable<Defense> defenses,
                                                IEnumerable<IFlex> flexPlays)
        {
            // build lineups
            var optimizedLineups = new List<Lineup>();

            foreach (var qb in quarterBacks)
            {
                int salary = 60000;

                foreach (var rbs in rbCombos)
                {
                    foreach (var wrs in wrCombos)
                    {
                        salary -= wrs.Sum(x => x.Salary);
                        if (salary < 0) { continue; }

                        foreach (var te in tightEnds)
                        {
                            salary -= te.Salary;
                            if (salary < 0) { continue; }

                            foreach (var dst in defenses)
                            {
                                salary -= dst.Salary;
                                if (salary < 0) { continue; }

                                foreach (var flex in flexPlays)
                                {
                                    // can afford this flex
                                    if (salary < flex.Salary ) { continue; }

                                    // TODO - refactor
                                    if (rbs.Contains(flex) || wrs.Contains(flex) || te.Id == flex.Id) { continue; }

                                    var lineup = new Lineup(qb, rbs, wrs, te, flex, dst);
                                    optimizedLineups.Add(lineup);
                                }
                            }
                        }
                    }
                }
            }

            return optimizedLineups;
        }

        /// <summary>
        ///     Selects core QBs
        /// </summary>
        /// <returns>The quarterbacks.</returns>
        /// <param name="playerList">Player list.</param>
        private IEnumerable<Quarterback> BuildQuarterbacks(List<Player> playerList){
            return playerList
                .Where((Player p) => p.Position == Position.QB)
                .OrderByDescending((Player p) => p.Projection)
                .Take(5)
                .Cast<Quarterback>()
                .ToList();
        }

        /// <summary>
        ///     Selects core RBs
        /// </summary>
        /// <returns>The running backs.</returns>
        /// <param name="playerList">Player list.</param>
        private HashSet<RunningBack[]> BuildRunningBacks(List<Player> playerList){
            var runningBacks = playerList
                .Where((Player p) => p.Position == Position.RB)
                .OrderByDescending((Player p) => p.Projection)
                .Take(15)
                .Cast<RunningBack>()
                .ToList();

            // create RB HashSet
            HashSet<RunningBack[]> rbCombos = new HashSet<RunningBack[]>();


            // old
            for (int i = 0; i < runningBacks.Count - 1; i++){
                var rb1 = runningBacks[i];

                for (int j = i + 1; j < runningBacks.Count; j++){
                    var rb2 = runningBacks[j];

                    // populate with new combination
                    rbCombos.Add(new RunningBack[]{rb1, rb2});
                }
            }

            return rbCombos;
        }

        /// <summary>
        ///     Selects core WRs
        /// </summary>
        /// <returns>The wide recievers.</returns>
        /// <param name="playerList">Player list.</param>
        private HashSet<WideReceiver[]> BuildWideRecievers(List<Player> playerList)
        {
            var wideReceivers = playerList
                .Where((Player p) => p.Position == Position.WR)
                .OrderByDescending((Player p) => p.Projection)
                .Take(20)
                .Cast<WideReceiver>()
                .ToList();

            // create WR sets 
            HashSet<WideReceiver[]> wrCombos = new HashSet<WideReceiver[]>();


            for (int i = 0; i < wideReceivers.Count - 2; i++)
            {
                var wr1 = wideReceivers[i];

                for (int j = i + 1; j < wideReceivers.Count - 1; j++)
                {
                    var wr2 = wideReceivers[j];

                    for (int k = j + 1; k < wideReceivers.Count; k++)
                    {
                        var wr3 = wideReceivers[k];

                        wrCombos.Add(new WideReceiver[] { wr1, wr2, wr3 });
                    }
                }
            }

            // TODO - should be combinations not permutations
            return wrCombos;
        }

        /// <summary>
        ///     Selects core TEs
        /// </summary>
        /// <returns>The tight ends.</returns>
        /// <param name="playerList">Player list.</param>
        private IEnumerable<TightEnd> BuildTightEnds(List<Player> playerList){
            return playerList
                .Where((Player p) => p.Position == Position.TE)
                .OrderByDescending((Player p) => p.Projection)
                .Take(10)
                .Cast<TightEnd>()
                .ToList();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using reactApp.Models;
using reactApp.Models.Positions;


namespace reactApp.Services
{
    public class LineupBuilderService : ILineupBuilderService
    {
        private int SalaryCap = 50000;

        private int MinSalaryCap = 48000;

        public IEnumerable<Lineup> Execute(List<Player> playerList){
            
            var quarterBacks = BuildQuarterbacks(playerList);

            var rbCombos = BuildRunningBacks(playerList);

            var wrCombos = BuildWideRecievers(playerList);

            var tightEnds = BuildTightEnds(playerList);

            var flexPlays = playerList
                .Where((Player p) => p is IFlex)
                .OrderByDescending(p => p.Projection/p.Salary)
                .Take(20)
                .Cast<IFlex>()
                .ToList();
            
            var defenses = playerList
                .Where((Player p) => p.Position == Position.DST)
                .OrderByDescending((Player p) => p.Projection/p.Salary)
                .Take(4)
                .Cast<Defense>()
                .ToList();

            var optimizedLineups = BuildLineups(quarterBacks, rbCombos, wrCombos, tightEnds, defenses, flexPlays);

            // return top 20 lineups
            var topLineups = optimizedLineups
                .Where(l => l.Salary <= SalaryCap)
                .OrderByDescending(l => l.Projection)
                .Take(20);

            var priceLineups = optimizedLineups
                .Where(l => l.Salary <= SalaryCap)
                .OrderByDescending(l => l.Salary)
                .Take(20);
            
            return topLineups;
        }

        /// <summary>
        ///     Constructs Lineups
        /// </summary>
        /// <returns>The lineups.</returns>
        /// <param name="quarterBacks">Quarter backs.</param>
        /// <param name="rbCombos">Rb combos.</param>
        /// <param name="wrCombos">Wr combos.</param>
        /// <param name="tightEnds">Tight ends.</param>
        /// <param name="defenses">Defenses.</param>
        /// <param name="flexPlays">Flex plays.</param>
        public IEnumerable<Lineup> BuildLineups(IEnumerable<Quarterback> quarterBacks, List<RunningBack[]> rbCombos, List<WideReceiver[]> wrCombos,
                                                IEnumerable<TightEnd> tightEnds, IEnumerable<Defense> defenses, IEnumerable<IFlex> flexPlays)
        {
            // build lineups
            var optimizedLineups = new List<Lineup>();

            // TODO: need a better way to keep track of the salary 
            // have to reset the total salary each time you enter the next loop
            // could create a list and then use the lists sum to keep track of 
            // everything in the list at that point in time
            foreach (var qb in quarterBacks)
            {
                int salary = SalaryCap;
                salary = SalaryCap - qb.Salary;

                foreach (var rbs in rbCombos)
                {
                    int rbsSalary = rbs.Sum(rb => rb.Salary);
                    salary = SalaryCap - qb.Salary - rbsSalary;

                    foreach (var wrs in wrCombos)
                    {
                        var wrSal = wrs.Sum(wr => wr.Salary);
                        if (salary - wrSal < 0) { continue; }
                        salary = SalaryCap - qb.Salary - rbsSalary - wrSal;

                        foreach (var te in tightEnds)
                        {
                            if (salary - te.Salary < 0) { continue; }
                            salary = SalaryCap - qb.Salary - rbsSalary - wrSal - te.Salary;

                            foreach (var dst in defenses)
                            {
                                if (salary - dst.Salary < 0) { continue; }
                                salary = SalaryCap - qb.Salary - rbsSalary - wrSal - te.Salary - dst.Salary;

                                foreach (var flex in flexPlays)
                                {
                                    // can afford flex?
                                    if (salary < flex.Salary ) { continue; }

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
                .OrderByDescending((Player p) => p.Projection/p.Salary)
                .Take(5)
                .Cast<Quarterback>()
                .ToList();
        }

        /// <summary>
        ///     Selects core RBs
        /// </summary>
        /// <returns>The running backs.</returns>
        /// <param name="playerList">Player list.</param>
        private List<RunningBack[]> BuildRunningBacks(List<Player> playerList){
            var valueBacks = playerList
                .Where((Player p) => p.Position == Position.RB)
                .OrderByDescending((Player p) => p.Projection/p.Salary)
                .Take(5)
                .Cast<RunningBack>()
                .ToList();

            var topBacks = playerList
                .Where((Player p) => p.Position == Position.RB)
                .OrderByDescending((Player p) => p.Projection)
                .Take(5)
                .Cast<RunningBack>()
                .ToList();

            var runningBacks = valueBacks.Union(topBacks).ToList();

            // create RB List
            var rbCombos = new List<RunningBack[]>();

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
        private List<WideReceiver[]> BuildWideRecievers(List<Player> playerList)
        {
            var valueReceivers = playerList
                .Where((Player p) => p.Position == Position.WR)
                .OrderByDescending((Player p) => p.Projection/p.Salary)
                .Take(5)
                .Cast<WideReceiver>()
                .ToList();

            var topReceivers = playerList
                .Where((Player p) => p.Position == Position.WR)
                .OrderByDescending((Player p) => p.Projection)
                .Take(5)
                .Cast<WideReceiver>()
                .ToList();

            var wideReceivers = valueReceivers.Union(topReceivers).ToList();

            // create WR sets 
            var wrCombos = new List<WideReceiver[]>();


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
                .OrderByDescending((Player p) => p.Projection / p.Salary)
                .Take(5)
                .Cast<TightEnd>()
                .ToList();
        }
    }

}

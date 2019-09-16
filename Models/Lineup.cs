using System.Collections.Generic;
using System.Linq;
using reactApp.Models.Positions;

namespace reactApp.Models
{
    public class Lineup
    {
        public List<IPlayer> Players { get; set; }

        public Quarterback Quarterback { get; set; }

        public RunningBack RunningBack1 { get; set; }
        public RunningBack RunningBack2 { get; set; }

        public RunningBack[] RunningBacks { get; set; }
        public WideReceiver[] WideReceivers { get; set; }

        public WideReceiver WideReceiver1 { get; set; }
        public WideReceiver WideReceiver2 { get; set; }
        public WideReceiver WideReceiver3 { get; set; }

        public TightEnd TightEnd { get; set; }

        public IFlex Flex { get; set; }

        public Defense Defense { get; set; }

        public List<string> PlayerIDs { get; set; }

        public double Projection { get; set; }
        public double Salary { get; set; }

        public Lineup(Quarterback qb, RunningBack[] RBs,WideReceiver[] WRs,
                     TightEnd te1, IFlex flex, Defense defense)
        {
            this.Quarterback = qb;
            this.RunningBack1 = RBs[0];
            this.RunningBack2 = RBs[1];
            this.RunningBacks = RBs;
            this.WideReceivers = WRs;
            this.WideReceiver1 = WRs[0];
            this.WideReceiver2 = WRs[1];
            this.WideReceiver3 = WRs[2];
            this.TightEnd = te1;
            this.Flex = flex;
            this.Defense = defense;

            // populate list of players

            this.Players = new List<IPlayer>
            {
                qb,
                RBs[0],
                RBs[1],
                WRs[0],
                WRs[1],
                WRs[2],
                te1,
                flex,
                defense
            };

            this.PlayerIDs = this.Players.Select(p => p.Id).ToList();

            // generate total projection
            this.Projection = this.Players.Sum(x => x.Projection);
            // generate total salary
            this.Salary = this.Players.Sum(p => p.Salary);
        }

        public override string ToString()
        {
            return this.Projection
                + "," + this.Salary
                + "," + this.Quarterback.Name
                + "," + this.RunningBack1.Name
                + "," + this.WideReceiver1.Name;
        }
    }
}

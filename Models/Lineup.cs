using System.Collections.Generic;
using System.Linq;
using reactApp.Models.Positions;

namespace reactApp.Models
{
    public class Lineup
    {
        public List<IPlayer> Players { get; set; }

        public Player Quarterback { get; set; }
        public RunningBack RunningBack1 { get; set; }
        public RunningBack RunningBack2 { get; set; }

        public WideReceiver WideReceiver1 { get; set; }
        public WideReceiver WideReceiver2 { get; set; }
        public WideReceiver WideReceiver3 { get; set; }

        public TightEnd TightEnd { get; set; }

        public IFlex Flex { get; set; }
        public Defense Defense { get; set; }

        public double Projection { get; set; }
        public double Salary { get; set; }

        public Lineup(Quarterback qb, 
                      RunningBack rb1, RunningBack rb2, 
                      WideReceiver wr1, WideReceiver wr2, WideReceiver wr3,
                     TightEnd te1, IFlex flex, Defense defense)
        {
            this.Quarterback = qb;
            this.RunningBack1 = rb1;
            this.RunningBack1 = rb2;
            this.WideReceiver1 = wr1;
            this.WideReceiver2 = wr2;
            this.WideReceiver3 = wr3;
            this.TightEnd = te1;
            this.Flex = flex;
            this.Defense = defense;

            //
            this.Players.Add(this.Quarterback);
            this.Players.Add(this.RunningBack1);
            this.Players.Add(this.RunningBack2);
            this.Players.Add(this.WideReceiver1);
            this.Players.Add(this.WideReceiver2);
            this.Players.Add(this.WideReceiver3);
            this.Players.Add(this.TightEnd);
            this.Players.Add(this.Flex);
            this.Players.Add(this.Defense);

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

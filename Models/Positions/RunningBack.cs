namespace reactApp.Models.Positions
{
    public class RunningBack : Player, IFlex
    {
        public bool CanFlex { get; set; }

        public RunningBack(
            string name, Position position, string id, double proj, int salary) 
            : base(name, position, id, proj, salary)
        {
            this.CanFlex = true;
        }
    }
}

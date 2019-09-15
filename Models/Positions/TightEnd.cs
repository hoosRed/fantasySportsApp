namespace reactApp.Models.Positions
{
    public class TightEnd : Player, IFlex
    {
        public bool CanFlex { get; set; }

        public TightEnd(
            string name, Position position, string id, double proj, int salary)
            : base(name, position, id, proj, salary)
        {
            this.CanFlex = true;
        }
    }
}

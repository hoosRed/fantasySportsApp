namespace reactApp.Models.Positions
{
    public class WideReceiver : Player, IFlex
    {
        public bool CanFlex { get; set; }

        public WideReceiver(
            string name, Position position, string id, double proj, int salary)
            : base(name, position, id, proj, salary)
        {
            this.CanFlex = true;
        }
    }
}

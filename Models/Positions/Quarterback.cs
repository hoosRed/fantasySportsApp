namespace reactApp.Models.Positions
{
    public class Quarterback : Player
    {
        public Quarterback(string name, Position position, string id, double proj, int salary) 
            : base(name, position, id, proj, salary)
        {
        }
    }
}

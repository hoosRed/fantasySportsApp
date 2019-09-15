using reactApp.Models;

namespace reactApp.Models.Positions
{
    public class Defense : Player
    {
        public Defense(string name, Position position, string id, double proj, int salary)
            : base(name, position, id, proj, salary)
        {
        }
    }
}

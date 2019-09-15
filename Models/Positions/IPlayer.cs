using System.Collections;

namespace reactApp.Models.Positions
{
    public interface IPlayer
    {
        string Name { get; set; }
        Position Position { get; set; }
        string Id { get; set; }

        double Projection { get; set; }
        int Salary { get; set; }
    }
}

using reactApp.Models.Positions;

namespace reactApp.Models
{
    public abstract class Player : IPlayer
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public string Id { get; set; }

        public double Projection { get; set; }
        public int Salary { get; set; }

        public Player(string name, Position position, string id, double proj, int salary)
        {
            this.Name = name;
            this.Projection = proj;
            this.Salary = salary;
            this.Position = position;
            this.Id = id;
        }

        public override string ToString()
        {
            return this.Position + " :  " + this.Name + " : $" + this.Salary + " : " + this.Projection + "pts";
        }
    }
}

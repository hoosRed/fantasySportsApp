using System.Collections.Generic;
using reactApp.Models;

namespace reactApp.Services
{
    public interface ILineupBuilderService
    {
        IEnumerable<Lineup> Execute(List<Player> playerList);

    }
}

using System.Collections.Generic;
using reactApp.Models;

namespace reactApp.Services
{
    public interface ILineupBuilderService
    {
        List<Lineup> Execute(List<Player> playerList);

    }
}

using System.Collections.Generic;
using reactApp.Models;

namespace reactApp.Services
{
    public interface ICsvReaderService
    {
        List<Player> Execute(string fileInput);
    }
}

using System;
namespace reactApp.Models.Positions
{
    // Marker Interface for Flex players
    public interface IFlex : IPlayer
    {
        bool CanFlex { get; set; }
    }
}

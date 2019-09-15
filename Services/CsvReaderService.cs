using System;
using System.Collections.Generic;
using System.IO;
using reactApp.Models;
using reactApp.Models.Positions;

namespace reactApp.Services
{
    public class CsvReaderService : ICsvReaderService
    {
        public List<Player> Execute(string csvFile)
        {
            var reader = new StreamReader(csvFile);
            var playersList = new List<Player>();

            // build list of headers
            var headers = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');

                var name = values[2];
                var position = values[0];
                var projection = Convert.ToDouble(values[8]);
                var salary = Convert.ToInt32(values[5]);
                var id = values[3];

                // add player
                playersList.Add(BuildPlayer(name, position, id, projection, salary));
            }

            return playersList;
        }

        /// <summary>
        ///     Builds the player.
        /// </summary>
        /// <returns>Player</returns>
        /// <param name="name">Name</param>
        /// <param name="pos">Position</param>
        /// <param name="id">Identifier</param>
        /// <param name="proj">Proj</param>
        /// <param name="sal">Sal</param>
        private Player BuildPlayer(string name, string pos, string id, double proj, int sal){
            if(pos.ToLower() == "qb")
            {
                return new Quarterback(name, Position.QB, id, proj, sal);
            }
            else if(pos.ToLower() == "rb")
            {
                return new RunningBack(name, Position.RB, id, proj, sal);

            }
            else if(pos.ToLower() == "wr")
            {
                return new WideReceiver(name, Position.WR, id, proj, sal);

            }
            else if (pos.ToLower() == "te")
            {
                return new TightEnd(name, Position.TE, id, proj, sal);

            }
            else if(pos.ToLower() == "dst")
            {
                return new Defense(name, Position.DST, id, proj, sal);

            }

            // TODO: handle invalid position
            return null;
        }
    }
}
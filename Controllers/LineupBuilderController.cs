﻿using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using reactApp.Models;
using reactApp.Services;

namespace reactApp.Controllers
{
    [Route("api/[controller]")]
    public class LineupBuilderController : Controller
    {
        /// <summary>
        ///     The csv reader service.
        /// </summary>
        private readonly ICsvReaderService csvReaderService;

        /// <summary>
        ///     The csv reader service.
        /// </summary>
        private readonly ILineupBuilderService lineupBuilderService;

        public LineupBuilderController(ICsvReaderService csvReaderService, ILineupBuilderService lineupBuilderService)
        {
            this.csvReaderService = csvReaderService;
            this.lineupBuilderService = lineupBuilderService;
        }

        /// <summary>
        /// Projections Data
        /// </summary>
        /// <returns>The data.</returns>
        [HttpGet("[action]")]
        public IEnumerable<Lineup> BuildLineup()
        {
            // test csv Reader
            var allPlayers = csvReaderService.Execute(
                @"/Users/tylerredshaw/Documents/reactFolder/react2/reactApp/LineupTemplates/week4Players.csv");

            return lineupBuilderService.Execute(allPlayers);
        }

    }

};



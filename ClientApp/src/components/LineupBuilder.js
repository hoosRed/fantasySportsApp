import React, { Component } from 'react';
import "./LineupBuilder.css"; 
export class LineupBuilder extends Component {
  displayName = LineupBuilder.name

  constructor(props) {
    super(props);
    this.state = { players: [], loading: true };
   
    fetch('api/LineupBuilder/BuildLineup')
      .then(response => response.json())
      .then(data => {
        this.setState({ lineups: data, loading: false });
      });
  }

  static renderSingleLineup(lineup) {
    return (
      <table className='lineup'>
        <thead class ='card'>
          <tr>
            <th>Position</th>
            <th>Player</th>
            <th>Salary</th>
            <th>Proj</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td>QB</td><td>{lineup.quarterback.name}</td><td>${lineup.quarterback.salary}</td><td>{lineup.quarterback.projection.toFixed(1)}</td>
          </tr>
          <tr>
            <td>RB</td><td>{lineup.runningBack1.name}</td><td>${lineup.runningBack1.salary}</td><td>{lineup.runningBack1.projection.toFixed(1)}</td>
          </tr>
          <tr>
            <td>RB</td><td>{lineup.runningBack2.name}</td><td>${lineup.runningBack2.salary}</td><td>{lineup.runningBack2.projection.toFixed(1)}</td>
          </tr>
          <tr>
            <td>WR</td><td>{lineup.wideReceiver1.name}</td><td>${lineup.wideReceiver1.salary}</td><td>{lineup.wideReceiver1.projection.toFixed(1)}</td>
          </tr>
          <tr>
            <td>WR</td><td>{lineup.wideReceiver2.name}</td><td>${lineup.wideReceiver2.salary}</td><td>{lineup.wideReceiver2.projection.toFixed(1)}</td>
          </tr>
          <tr>
            <td>WR</td><td>{lineup.wideReceiver3.name}</td><td>${lineup.wideReceiver3.salary}</td><td>{lineup.wideReceiver3.projection.toFixed(1)}</td>
          </tr>
          <tr>
            <td>TE</td><td>{lineup.tightEnd.name}</td><td>${lineup.tightEnd.salary}</td><td>{lineup.tightEnd.projection.toFixed(1)}</td>
          </tr>
          <tr>
            <td>Flex</td><td>{lineup.flex.name}</td><td>${lineup.flex.salary}</td><td>{lineup.flex.projection.toFixed(1)}</td>
          </tr>
          <tr>
            <td>D/ST</td><td>{lineup.defense.name}</td><td>${lineup.defense.salary}</td><td>{lineup.defense.projection.toFixed(1)}</td>
          </tr>
        </tbody>
      </table>
    );
  }

  static renderLineups(lineups) {
    return (
      <div class = 'lineupContainer'>
        {lineups.map(lineup => this.renderSingleLineup(lineup))}
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : LineupBuilder.renderLineups(this.state.lineups);

    return (
      <div>
        <h1>NFL Lineup Optimizer</h1>
        {contents}
      </div>
    );
  }
}

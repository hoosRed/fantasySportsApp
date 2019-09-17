import React, { Component } from 'react';

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

  static renderUsersTable(lineups) {
    return (
      <table className='table'>
        <thead>
          <tr>
            <th>Projection</th>
            <th>Salary</th>
            <th>Quarterback</th>
            <th>RB1</th>
            <th>RB2</th>
            <th>WR1</th>
            <th>WR2</th>
            <th>WR3</th>
            <th>TE</th>
            <th>Flex</th>
            <th>DST</th>
          </tr>
        </thead>
        <tbody>
          {lineups.map(lineup =>
            <tr key={lineup.id}>
              <td>{lineup.projection.toFixed(1)}</td>
              <td>${lineup.salary.toFixed(0)}</td>
              <td>{lineup.quarterback.name}</td>
              <td>{lineup.runningBack1.name}</td>
              <td>{lineup.runningBack2.name}</td>
              <td>{lineup.wideReceiver1.name}</td>
              <td>{lineup.wideReceiver2.name}</td>
              <td>{lineup.wideReceiver3.name}</td>
              <td>{lineup.tightEnd.name}</td>
              <td>{lineup.flex.name}</td>
              <td>{lineup.defense.name}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : LineupBuilder.renderUsersTable(this.state.lineups);

    return (
      <div>
        <h1>Week 2 NFL Lineup Optimizer</h1>
        {contents}
      </div>
    );
  }
}

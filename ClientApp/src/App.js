import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { TestData } from './components/TestData';
import { Login } from './components/Login';
import { NflData } from './components/NflData';
import { LineupBuilder } from './components/LineupBuilder';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/Login' component={Login} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata' component={FetchData} />
        <Route path='/financialdata' component={TestData} />
        <Route path='/nfldata' component={NflData} />
        <Route path='/lineupbuilder' component={LineupBuilder} />
      </Layout>
    );
  }
}

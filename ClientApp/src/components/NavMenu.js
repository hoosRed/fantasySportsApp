import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

export class NavMenu extends Component {
  displayName = NavMenu.name

  render() {
    return (
      <Navbar inverse fixedTop fluid collapseOnSelect>
        <Navbar.Header>
          <Navbar.Brand>
            <Link to={'/'}>DFS Optimizer</Link>
          </Navbar.Brand>
          <Navbar.Toggle />
        </Navbar.Header>
        <Navbar.Collapse>
          <Nav>
            <LinkContainer to={'/home'} exact>
              <NavItem>
                <Glyphicon glyph='home' /> Home
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/login'} exact>
              <NavItem>
                <Glyphicon glyph='user' /> Login
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/nflData'}>
              <NavItem>
                <Glyphicon glyph='education' /> NFL Projections
              </NavItem>
            </LinkContainer>
            <LinkContainer to={'/lineupbuilder'}>
              <NavItem>
                <Glyphicon glyph='th-list' /> Lineup Optimizer
              </NavItem>
            </LinkContainer>
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    );
  }
}
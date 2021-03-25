import { Nav, Navbar } from 'react-bootstrap';
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link
} from 'react-router-dom';
import FAQ from '../FAQ/FAQ';
import HrefLink from '../HrefLink/HrefLink';
import Introduction from '../Introduction/Introduction';
import Main from '../Main/Main';
import NotFound from '../NotFound/NotFound';
import './App.scss';

function App() {
  return (
    <div id="app">
      <Router>
        <Navbar id="nav-bar">
          <Navbar.Brand><Link href="/" className="brand-text">Re;born</Link></Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="ml-auto">
              <HrefLink to="/">메인</HrefLink>
              <HrefLink to="/introduction">소개</HrefLink>
              <HrefLink to="/FAQ">FAQ</HrefLink>
            </Nav>
          </Navbar.Collapse>
        </Navbar>
        <Switch>
          <Route exact path="/">
            <Main />
          </Route>
          <Route path="/introduction">
            <Introduction />
          </Route>
          <Route path="/FAQ">
            <FAQ />
          </Route>
          <Route path="/">
            <NotFound />
          </Route>
        </Switch>
      </Router>

      <footer>
        ©2021 by Re;born.
      </footer>
    </div>
  );
}

export default App;

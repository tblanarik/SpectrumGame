import React from 'react';
import './App.css';
import Guess from './Guesser';
import GiveClue from './clueGiver';
import { Route, Switch } from 'react-router-dom';
import HomePage from './Game';

function App() {
  return (
    <Switch>
      <Route exact path="/" component={HomePage} />
      <Route path="/guess" component={Guess} />
      <Route path="/give" component={GiveClue} />
    </Switch>
  //<div className="App">
    //{Guess()}
    //{GiveClue("Total Rip-off", "Completely Original")}
  //</div>
   );
}
export default App;
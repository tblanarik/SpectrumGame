import React from 'react';
import './App.css';
import Guess from './Guesser';
import GiveClue from './clueGiver';

function App() {
  return (
  <div className="App">
    {Guess()}
    {GiveClue("Total Rip-off", "Completely Original")}
  </div>
   );
}
export default App;
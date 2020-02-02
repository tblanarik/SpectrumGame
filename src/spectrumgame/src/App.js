import React from 'react';
import './App.css';
import Guess from './guesser';
import GiveClue from './clueGiver';

function App() {
  return (
  <div className="App">
    {Guess()}
    {GiveClue()}
  </div>
   );
}
export default App;
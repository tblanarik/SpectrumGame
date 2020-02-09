import React from 'react';
import './App.css';
import Guess from './Guesser';
import GiveClue from './clueGiver';
import Backend from './Auth';
import { GoogleLogin } from 'react-google-login';

function App() {

  
  return (
  <div className="App">
    <Backend/>
  </div>
   );
}

function getGoogleAuth(response) {
  console.log("TREVOR: ", JSON.stringify(response)
  );
}

export default App;
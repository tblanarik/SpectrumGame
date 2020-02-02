import React from 'react';
import './App.css';
import ReactSpeedometer from "react-d3-speedometer"
import Backend from './Trevor';
import { GoogleLogin } from 'react-google-login';

function App() {
  return (
    <div className="App">
      <ReactSpeedometer/>
      <Backend/>
      <br></br>
    
    </div>
  );
}

export default App;

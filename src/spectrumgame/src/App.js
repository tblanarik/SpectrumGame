import React from 'react';
import './App.css';
import ReactSpeedometer from "react-d3-speedometer"
import Backend from './Trevor';
import { GoogleLogin } from 'react-google-login';

const responseGoogle = (response) => {
  console.log(response);
}

function App() {
  return (
    <div className="App">
      <ReactSpeedometer/>
      <Backend/>
      <br></br>
      <GoogleLogin
    clientId="782408472459-aaj4esol1tobks9rperfu9dolvj0q9nn.apps.googleusercontent.com"
    buttonText="Login"
    onSuccess={responseGoogle}
    onFailure={responseGoogle}
    cookiePolicy={'single_host_origin'}
    />
    </div>
  );
}

export default App;

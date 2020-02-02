import React, { Component } from 'react';
import { Button } from '@material-ui/core';
import { GoogleLogin } from 'react-google-login';

class Backend extends Component {
  constructor(props) {
    super(props);
    this.state = {
      data: null,
      auth: null,
    };
    this.getCategories = this.getCategories.bind(this);
    this.resetCategories = this.resetCategories.bind(this);
    this.getGoogleAuth = this.getGoogleAuth.bind(this);
  }

  render() {
    const button = <Button variant="contained" color="primary" onClick={this.getCategories}>
      Get Categories
    </Button>

    const resetButton = <Button variant="contained" color="primary" onClick={this.resetCategories}>
      Reset
</Button>

    const value = this.state.data ? this.state.data : "";//`LEFT: ${this.state.data.categoryLeft}, RIGHT: ${this.state.data.categoryRight}` : "";

    const goog = <GoogleLogin
      clientId="782408472459-aaj4esol1tobks9rperfu9dolvj0q9nn.apps.googleusercontent.com"
      buttonText="Login"
      onSuccess={this.getGoogleAuth}
      onFailure={this.getGoogleAuth}
      cookiePolicy={'single_host_origin'}
    />

    const token = this.state.auth ? this.state.auth.tokenId : "no token";

    return (
      <div>
        {button}
        <br></br>
        <br></br>
        {resetButton}
        <br></br>
        {value}
        <br></br>
        {goog}
        <br></br>
        {token}
      </div>)
  }

  resetCategories() {
    this.setState({ data: null })
  }

  getGoogleAuth(response) {
    console.log(response);
    this.setState({auth: response})
  }

  getCategories() {
    //fetch('http://localhost:7071/api/GetRandomCategory', {headers :{"Authorization": "Bearer " + this.state.auth.Zi.access_token}})
    fetch('https://spectrumgame-backend.azurewebsites.net/api/AuthTest', {headers :{"Authorization": "Bearer " + this.state.auth.tokenId}})
      .then(response => response)//.json())
      .then(data => this.setState({ data }));
  }
}
export default Backend;
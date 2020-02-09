import React, { Component } from 'react';
import { Button } from '@material-ui/core';
import { GoogleLogin } from 'react-google-login';

class SpectrumAuth extends Component {
    constructor(props) {
        super(props);
        this.state = {
            authToken: null
        };
        this.onGoogleAuth = this.onGoogleAuth.bind(this);
        this.getAuthToken = this.getAuthToken.bind(this);
    }

    render() {
        return (
            <GoogleLogin
                clientId="782408472459-aaj4esol1tobks9rperfu9dolvj0q9nn.apps.googleusercontent.com"
                buttonText="Login"
                onSuccess={this.onGoogleAuth}
                onFailure={this.onGoogleAuth}
                cookiePolicy={'single_host_origin'}
            />
        )
    }

    getAuthToken()
    {
        return this.state.authToken;
    }

    onGoogleAuth(response) {
        let authBody = {
            method: 'POST',
            body: JSON.stringify({
                id_token: response.tokenId
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        }
        fetch('https://spectrumgame-backend.azurewebsites.net/.auth/login/google', authBody).
        then(response => response.json())
        .then(authToken => this.setState({authToken}));
    }
}

export default SpectrumAuth;
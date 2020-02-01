import React, { Component } from 'react';
import { Button } from '@material-ui/core';

class Backend extends Component {
  constructor(props) {
    super(props);
    this.state = {
      data: null,
    };
    this.getCategories = this.getCategories.bind(this);
    this.resetCategories = this.resetCategories.bind(this);
  }

  render() {
    const button = <Button variant="contained" color="primary" onClick={this.getCategories}>
        Get Categories
    </Button>

const resetButton = <Button variant="contained" color="primary" onClick={this.resetCategories}>
Reset
</Button>

    const value = this.state.data ? `LEFT: ${this.state.data.categoryLeft}, RIGHT: ${this.state.data.categoryRight}` : "";

    return (
    <div>
    {button}
    <br></br>
    <br></br>
    {resetButton}
    <br></br>
    {value}
    </div>)
  }

  resetCategories()
  {
       this.setState({data:null})
  }

    getGoogleAuth()
    {

    }

  getCategories()
  {
       fetch('http://localhost:7071/api/GetRandomCategory')
      .then(response => response.json())
      .then(data => this.setState({ data }));
  }
}
export default Backend;
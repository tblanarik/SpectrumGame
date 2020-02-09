import React from 'react';
import ReactSpeedometer from "react-d3-speedometer"
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import Paper from '@material-ui/core/Paper';
import GenerateCategories from './Categories';
import GenerateTarget from './Target';
import Scoreboard from './Scoreboard';

function GiveClue() {
  const useStyles = makeStyles(theme => ({
    root: {
      flexGrow: 1,
    },
    paper: {
      padding: theme.spacing(2),
      textAlign: 'center',
      color: theme.palette.text.secondary,
    },
  }
  )
  );

  var categories = GenerateCategories();
  var leftCat = categories[0];
  var rightCat = categories[1];

  var target = GenerateTarget();

  const classes = useStyles();
  return (
    <div className="App">
      <div>
        <ReactSpeedometer
          value={target} segments={1000} needleColor="red" startColor="green" endColor="blue" minValue={0} maxValue={100} maxSegmentLabels={0} currentValueText={""} height="15" />
        <Grid justify="center" direction="row" alignItems="center" container spacing={3}>
          <Grid item sm={3} xs={4}>
            <Paper elevation={2} className="categories">{leftCat}</Paper>
          </Grid>
          <Grid item sm={3} xs={4}>
            <Paper elevation={2} className="categories">{rightCat}</Paper>
          </Grid>
          <Grid item sm={8} xs={6} >
            < form className={classes.root} noValidate autoComplete="off">
              <TextField fullWidth id="standard-basic" label="Enter your clue" />
            </form>
          </Grid>
          <Grid justify="center" item sm={8} xs={8} >
            <Button variant="contained" color="primary">
              Submit
          </Button>
          </Grid>
        </Grid>
      </div>
      <div>
        <Grid justify="center" align items="center" container spacing={1}>
          <Grid item xs={6} sm={5} md={3}> 
            <Scoreboard />
          </Grid>
        </Grid>
      </div>
    </div>
  );
}
export default GiveClue;
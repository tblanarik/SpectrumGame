import React from 'react';
import ReactSpeedometer from "react-d3-speedometer"
import Button from '@material-ui/core/Button';
import Slider from '@material-ui/core/Slider';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';
import Scoreboard from './Scoreboard'

function Guess() {
  const [value, setValue] = React.useState(50);
  const handleChange = (event, newValue) => {
    setValue(newValue);
  };
  const useStyles = makeStyles(theme => ({
    root: {
      flexGrow: 1,
    },
    paper: {
      padding: theme.spacing(2),
      textAlign: 'center',
      color: theme.palette.text.secondary,
    },
  }));
  const classes = useStyles();
  return (
    <div className="App">
      <div>
        <Grid container spacing={1}>
          <Grid item xs={12} s={6} m={6}>
            <Grid justify="center" container spacing={1}>
              <Grid item xs={12}>
                <ReactSpeedometer value={value} segments={1000} needleColor="red" startColor="green" endColor="blue" needleTransitionDuration={50} minValue={0} maxValue={100} maxSegmentLabels={0} currentValueText={""} />
              </Grid>
              <Grid item xs={6} sm={4}>
                <Paper className={classes.paper}>  <Slider value={value} onChange={handleChange} /></Paper>
              </Grid>
              <Grid item xs={10}>
                <Button variant="contained" color="primary">
                  Submit
                  </Button>
              </Grid>
            </Grid>
          </Grid>
        </Grid>
      </div>
      <div>
        <Grid justify="center" alignItems="center" container spacing={1}>
          <Grid item xs={6} sm={5} md={3}>
            <Scoreboard />
          </Grid>
        </Grid>
      </div>
    </div>
  );
}
export default Guess;
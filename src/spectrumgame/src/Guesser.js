import React from 'react';
import ReactSpeedometer from "react-d3-speedometer"
import Button from '@material-ui/core/Button';
import Slider from '@material-ui/core/Slider';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import Paper from '@material-ui/core/Paper';

function Guess()
{
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
          <ReactSpeedometer 
          value={value} needleTransitionDuration={50} minValue={0} maxValue={100} maxSegmentLabels={0} currentValueText={""} height="15"/>
          <Grid justify="center" container spacing={3}>
          <Grid item xs={6} sm={5}>
              <Paper className={classes.paper}>  <Slider value={value} onChange={handleChange} /></Paper>
          </Grid>
          </Grid> <br/>
          <Button variant="contained" color="primary">
          Submit
          </Button>
      </div>
  );
}
export default Guess;
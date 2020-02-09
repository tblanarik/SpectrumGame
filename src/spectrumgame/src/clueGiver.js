import React from 'react';
import ReactSpeedometer from "react-d3-speedometer"
import Button from '@material-ui/core/Button';
import { makeStyles } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import TextField from '@material-ui/core/TextField';
import Paper from '@material-ui/core/Paper';

function GiveClue(left, right)
{
  const useStyles = makeStyles(theme => ({
    root: {
      '& > *': {
        flexGrow: 1,
      },
      paper: {
        padding: theme.spacing(2),
        textAlign: 'center',
        color: theme.palette.text.secondary,
      },
    },
  }));
  
  const classes = useStyles();
  return (
      <div className="classes.root">
          <ReactSpeedometer 
          value={30} minValue={0} maxValue={100} maxSegmentLabels={0} currentValueText={""} height="15"/>
          <Grid justify="center" direction="row" alignItems="center" container spacing={3}>
            <br/>
            <Grid item sm={3} xs={4}>
              <Paper elevation={2} className="categories">{left}</Paper>
            </Grid>
            <Grid item sm={3} xs={4}>
  <Paper elevation={2} className="categories">{right}</Paper>
            </Grid>
            <Grid item sm={8} xs={6} >
              < form className={classes.root} noValidate autoComplete="off">
                  <TextField  fullWidth id="standard-basic" label="Enter your clue" />
              </form>
            </Grid>
          </Grid> <br/>
          <Button variant="contained" color="primary">
          Submit
          </Button>
      </div>
  );
}
export default GiveClue;
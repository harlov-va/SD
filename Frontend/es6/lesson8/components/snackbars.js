import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Snackbar from '@material-ui/core/Snackbar';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';
import cartStore from '../store/cart';
import {observer} from 'mobx-react';

export default @observer class extends React.Component{
  constructor(props){
    super(props);
    this.state ={
      open:true
    }

  }
  

  handleClick() {
    setOpen(true);
  }

  handleClose(event, reason) {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  }

  render () {
          return (
          <div>
            <Button onClick={this.handleClick}>Open simple snackbar</Button>
            <Snackbar
              anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'left',
              }}
              open={open}
              autoHideDuration={3000}
              onClose={this.handleClose}
              ContentProps={{
                'aria-describedby': 'message-id',
              }}
              message={<span id="message-id">Note archived</span>}
              action={[
                <IconButton
                  key="close"
                  aria-label="close"
                  color="inherit"
                  className={classes.close}
                  onClick={this.handleClose}
                >
                  <CloseIcon />
                </IconButton>,
              ]}
            />
          </div>
        );
      }
}
const useStyles = makeStyles(theme => ({
  close: {
    padding: theme.spacing(0.5),
  },
}));

const classes = useStyles();
const [open, setOpen] = React.useState(true);
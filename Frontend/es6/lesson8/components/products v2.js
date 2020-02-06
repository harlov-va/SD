import React from "react";
import PropTypes from "prop-types";
import { withStyles } from "@material-ui/core/styles";
import AppBar from "@material-ui/core/AppBar";
import Tabs from "@material-ui/core/Tabs";
import Tab from "@material-ui/core/Tab";
import { Paper } from "@material-ui/core";
import Typography from "@material-ui/core/Typography";

import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';

import {observer} from 'mobx-react';
import productsStore from '../store/products';
import cartStore from '../store/cart';

import { Link, Route, BrowserRouter, Switch } from "react-router-dom";


function TabContainer({ children, dir }) {
  return (
    <Typography component="div" dir={dir} style={{ padding: 8 * 3 }}>
      {children}
    </Typography>
  );
}

TabContainer.propTypes = {
  children: PropTypes.node.isRequired,
  dir: PropTypes.string.isRequired
};

const styles = theme => ({
  root: {
    backgroundColor: theme.palette.background.paper,
    //width: 500
  }
});

class FullWidthTabs extends React.Component {
  state = {
    value: 0
  };

  handleChange = (event, value) => {
    this.setState({ value });
  };

  handleChangeIndex = index => {
    this.setState({ value: index });
  };

  render() {
    const { classes, theme } = this.props;

    return (
      <BrowserRouter>
        <div className={classes.root}>
          <AppBar position="static" color="default">
            <Tabs
              value={this.state.value}
              onChange={this.handleChange}
              indicatorColor="primary"
              textColor="primary"
              fullWidth
            >
              <Tab label="Create a new transaction" component={Link} to="/one" />
              <Tab label="List transactions" component={Link} to="/two" />
            </Tabs>
          </AppBar>

          <Switch>
            <Route path="/one" component={ItemOne} />
            <Route path="/two" component={ItemTwo} />
          </Switch>
        </div>
      </BrowserRouter>
    );
  }
}

FullWidthTabs.propTypes = {
  classes: PropTypes.object.isRequired,
  theme: PropTypes.object.isRequired
};

function ItemOne(theme) {
  return (
    <Paper>
      <div>Item 1</div>
    </Paper>
  );
}

function ItemTwo(theme) {
  productsStore.listTransactions();
  return (
    <Paper>
      <Table >
                <TableHead>
                  <TableRow>
                    <TableCell>Date</TableCell>
                    <TableCell align="right">Correspondent Name</TableCell>
                    <TableCell align="right">Transaction amount</TableCell>
                    <TableCell align="right">Resulting balance</TableCell>
                    
                  </TableRow>
                </TableHead>
                <TableBody>
                  {productsStore.list.map(row => (
                    <TableRow key={row.id}>
                      <TableCell component="th" scope="row">
                        {row.date}
                      </TableCell>
                      <TableCell align="right">{row.username}</TableCell>
                      <TableCell align="right">{row.amount}</TableCell>
                      <TableCell align="right">{row.balance}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
    </Paper>
  );
}



export default withStyles(styles, { withTheme: true })(FullWidthTabs);
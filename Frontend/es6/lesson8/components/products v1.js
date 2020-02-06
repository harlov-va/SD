import React from 'react';

import Grid from '@material-ui/core/Grid';
import Card from '@material-ui/core/Card';
import CardHeader from '@material-ui/core/CardHeader';
import CardContent from '@material-ui/core/CardContent';
import CardActions from '@material-ui/core/CardActions';
import Button from '@material-ui/core/Button';

import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';

import {observer} from 'mobx-react';
import productsStore from '../store/products';
import cartStore from '../store/cart';

export default @observer class extends React.Component{
    render(){
        let products = productsStore.list.map((item) => {

            let cartBtn = !cartStore.inCart(item.id) ? 
                <Button variant="contained" color="primary" onClick={() => cartStore.add(item.id)}>
                    Add to cart
                </Button> :
                <Button variant="contained" color="secondary" onClick={() => cartStore.remove(item.id)}>
                    Remove from cart
                </Button>;

            return <Grid item xs={4} key={item.id}>
                <Card>
                    <CardHeader title={item.title}/>
                    <CardContent>
                        {item.price}
                    </CardContent>
                    <CardActions>
                        {cartBtn}
                    </CardActions>
                </Card>
            </Grid>
        });

        return (
            <Paper >
              <Table >
                <TableHead>
                  <TableRow>
                    <TableCell>Dessert (100g serving)</TableCell>
                    <TableCell align="right">Calories</TableCell>
                    <TableCell align="right">Fat&nbsp;(g)</TableCell>
                    
                  </TableRow>
                </TableHead>
                <TableBody>
                  {productsStore.list.map(row => (
                    <TableRow key={row.id}>
                      <TableCell component="th" scope="row">
                        {row.id}
                      </TableCell>
                      <TableCell align="right">{row.title}</TableCell>
                      <TableCell align="right">{row.price}</TableCell>
                      
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </Paper>
          );
    }
}
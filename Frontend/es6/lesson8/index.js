import 'babel-polyfill';
import React from 'react';
import ReactDOM from 'react-dom';
import { ThemeProvider } from '@material-ui/styles';
import theme from './components/theme';

import Site from './components/site';
ReactDOM.render(<ThemeProvider theme={theme}>
    <Site/>
  </ThemeProvider>, document.querySelector('.app'));

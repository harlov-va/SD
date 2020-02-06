import React from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Box from '@material-ui/core/Box';

//для таблицы
import { Paper } from "@material-ui/core";
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';

//для формы новой транзакции
import TextField from '@material-ui/core/TextField';
import deburr from 'lodash/deburr';
import Autosuggest from 'react-autosuggest';
import match from 'autosuggest-highlight/match';
import parse from 'autosuggest-highlight/parse';
import MenuItem from '@material-ui/core/MenuItem';
import Button from '@material-ui/core/Button';

//DAL
import {observer} from 'mobx-react';
import productsStore from '../store/products';
import cartStore from '../store/cart';



//#region 
//Autocomplete
let suggestions = [];
let inpAmount = React.createRef();
let inpRecipient = React.createRef();

function renderInputComponent(inputProps) {
  const { classes, inputRef = () => {}, ref, ...other } = inputProps;

  
  return (
    <TextField
      fullWidth
      inputRef={inpRecipient}
      // InputProps={{
      //   inputRef: node => {
      //     ref(node);
      //     inputRef(node);
      //   },
      //   classes: {
      //     input: classes.input,
      //   },
      // }}
      {...other}
    />
  );
}

function renderSuggestion(suggestion, { query, isHighlighted }) {
  const matches = match(suggestion.label, query);
  const parts = parse(suggestion.label, matches);

  return (
    <MenuItem selected={isHighlighted} component="div">
      <div>
        {parts.map(part => (
          <span key={part.text} style={{ fontWeight: part.highlight ? 500 : 400 }}>
            {part.text}
          </span>
        ))}
      </div>
    </MenuItem>
  );
}

async function getSuggestions(value) {
  const inputValue = deburr(value.trim()).toLowerCase();
  const inputLength = inputValue.length;
  let count = 0;
  await productsStore.filteredUserlist(inputValue);
  suggestions = productsStore.userList;
  return suggestions;
  // return inputLength === 0
  //   ? []
  //   : suggestions.filter(suggestion => {
  //       const keep =
  //         count < 5 && suggestion.label.slice(0, inputLength).toLowerCase() === inputValue;

  //       if (keep) {
  //         count += 1;
  //       }

  //       return keep;
  //     });
}

function getSuggestionValue(suggestion) {
  return suggestion.label;
}

const useStylesAutocompete = makeStyles(theme => ({
  root: {
 
    flexGrow: 1,
  },
  container: {
    position: 'relative',
  },
  suggestionsContainerOpen: {
    position: 'absolute',
    zIndex: 1,
    marginTop: theme.spacing(1),
    left: 0,
    right: 0,
  },
  suggestion: {
    display: 'block',
  },
  suggestionsList: {
    margin: 0,
    padding: 0,
    listStyleType: 'none',
  },
  divider: {
    height: theme.spacing(2),
  },
}));

function IntegrationAutosuggest() {
  const classes = useStylesAutocompete();
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [state, setState] = React.useState({
    single: '',
    popper: '',
  });

  const [stateSuggestions, setSuggestions] = React.useState([]);

  const handleSuggestionsFetchRequested = async ({ value }) => {
    setSuggestions(await getSuggestions(value));
  };

  const handleSuggestionsClearRequested = () => {
    setSuggestions([]);
  };

  const handleChange = name => (event, { newValue }) => {
    setState({
      ...state,
      [name]: newValue,
    });
  };

  const autosuggestProps = {
    renderInputComponent,
    suggestions: stateSuggestions,
    onSuggestionsFetchRequested: handleSuggestionsFetchRequested,
    onSuggestionsClearRequested: handleSuggestionsClearRequested,
    getSuggestionValue,
    renderSuggestion,
  };

  return (
    <div className={classes.root}>
      <Autosuggest
        {...autosuggestProps}
        inputProps={{
          classes,
          id: 'react-autosuggest-simple',
          label: 'Select recipient',
          placeholder: 'Start typing',
          value: state.single,
          onChange: handleChange('single'),
        }}
        theme={{
          container: classes.container,
          suggestionsContainerOpen: classes.suggestionsContainerOpen,
          suggestionsList: classes.suggestionsList,
          suggestion: classes.suggestion,
        }}
        renderSuggestionsContainer={options => (
          <Paper {...options.containerProps} square>
            {options.children}
          </Paper>
        )}
      />      
    </div>
  );
}
//#endregion

function TabPanel(props) {
  const { children, value, index, ...other } = props;

  return (
    <Typography
      component="div"
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      <Box p={3}>{children}</Box>
    </Typography>
  );
}

TabPanel.propTypes = {
  children: PropTypes.node,
  index: PropTypes.any.isRequired,
  value: PropTypes.any.isRequired,
};

function a11yProps(index) {
  return {
    id: `simple-tab-${index}`,
    'aria-controls': `simple-tabpanel-${index}`,
  };
}

const useStyles = makeStyles(theme => ({
  root: {
    flexGrow: 1,
    backgroundColor: theme.palette.background.paper,
  },
}));

function clickButton(){
  // if(inpAmount.current.value<= cartStore.userBalance){
  //   if(inpRecipient.current.value !==""){
      productsStore.createTransaction(inpRecipient.current.value,inpAmount.current.value);
      inpRecipient.current.value = '';
      inpAmount.current.value = '';
  //   }
  // }
}

export default function SimpleTabs() {
  const classes = useStyles();
  const [value, setValue] = React.useState(0);
  
  function handleChange(event, newValue) {
    setValue(newValue);
  }
  
  const autocomplete = IntegrationAutosuggest();

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Tabs value={value} onChange={handleChange} aria-label="simple tabs example">
          <Tab label="Create a new transaction" {...a11yProps(0)} />
          <Tab label="List transactions" {...a11yProps(1)} onFocus ={productsStore.listTransactions()}/>
        </Tabs>
      </AppBar>
      <TabPanel value={value} index={0}>
        <TextField
          id="outlined-number"
          label="Amount"
          type="number"
          InputLabelProps={{
            shrink: true,
          }}
          margin="normal"
          variant="outlined"
          inputRef={inpAmount}
        />
        {autocomplete}
        <br/>
        <Button variant="contained" color="primary" className={classes.button} onClick={() => clickButton()}>
          Create a new transaction
        </Button>
      </TabPanel>
      <TabPanel value={value} index={1}>
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
      </TabPanel>
    </div>
  );
}
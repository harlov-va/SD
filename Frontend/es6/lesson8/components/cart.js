import React from 'react';
import {observer} from 'mobx-react';
import cartStore from '../store/cart';
import Dialog from '@material-ui/core/Dialog';
import DialogContent from '@material-ui/core/DialogContent';
import DialogActions from '@material-ui/core/DialogActions';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Box from '@material-ui/core/Box';
//import Snackbar from './snackbars';

import Snackbar from '@material-ui/core/Snackbar';
import IconButton from '@material-ui/core/IconButton';
import CloseIcon from '@material-ui/icons/Close';

export default @observer class extends React.Component{
    constructor(props){
        super(props);
        this.inpLogin = React.createRef();
        this.inpPassword = React.createRef();
        this.inpUserName = React.createRef();
        this.inpConfirmPassword = React.createRef();
        this.inpButtonRegister = React.createRef();
    }
    onLogon = async () => {
        let login = this.inpLogin.current.value;
        let password = this.inpPassword.current.value;
        let response = await cartStore.auth(login, password);
    }
    registerUser = async () =>{
        cartStore.hideButtonLogon = "none";
        cartStore.hideField = "inline"
        cartStore.buttonRegisterText = "Sing up";
        if(this.inpPassword.current.value === this.inpConfirmPassword.current.value && this.inpLogin.current.value!==''){
            if(!this.checkEmail(this.inpLogin.current.value)){
                cartStore.errors = "Invalid mail";
                cartStore.openDialogErrors = true;
                return;
            }
            else{
                await cartStore.auth(this.inpLogin.current.value,this.inpPassword.current.value,this.inpUserName.current.value);
            }
        }
    }
    checkEmail(email){
        return /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email);
    }
    handleClose(){
        cartStore.openDialogErrors = false;
    }
    render(){
        // let errorsModal = cartStore.errors.map((item, i) => {
        //     return <p key={i}>{item}</p>
        // });
        return <div>
            <input type="button" value="Logout" onClick={() => cartStore.logout()} />
            <div>User name: {cartStore.userName}</div>
            <div>Balance: {cartStore.userBalance}</div>
            <Dialog open={cartStore.open} onClose={this.closeModal}>
                    <DialogContent>
                        <form>
                            <Box display={cartStore.hideField}>
                                <TextField
                                    label="User name"
                                    //required="true"
                                    margin="normal"
                                    variant="outlined"
                                    inputRef={this.inpUserName}
                                    hidden={true}
                                />
                            </Box>
                            <br/>
                            <TextField
                                label="Login"
                                autoFocus={true}
                                required={true}
                                margin="normal"
                                variant="outlined"
                                inputRef={this.inpLogin}
                                type="email"
                                autoComplete="email"
                            />
                            <br/>
                            <TextField
                                label="Password"
                                //type="password"
                                //required="true"
                                margin="normal"
                                variant="outlined"
                                inputRef={this.inpPassword}
                                onKeyPress={(e) => {(e.key === 'Enter' ? this.onLogon() : null)}} 
                            />
                            <br/>
                            <Box display={cartStore.hideField}>
                                <TextField
                                    label="Confirm password"
                                    //required="true"
                                    margin="normal"
                                    variant="outlined"
                                    inputRef={this.inpConfirmPassword}
                                    hidden={true}
                                />
                            </Box>
                        </form>
                        
                    </DialogContent>
                    <DialogActions>
                        <Box display={cartStore.hideButtonLogon}>
                            <Button variant="contained" color="primary" onClick={this.onLogon}>Logon</Button>
                        </Box>
                        <Button variant="contained" color="secondary" onClick={() =>this.registerUser()}>{cartStore.buttonRegisterText}</Button>
                    </DialogActions>
                </Dialog>
                <Snackbar
                    anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'left',
                    }}
                    open={cartStore.openDialogErrors}
                    autoHideDuration={4000}
                    onClose={this.handleClose}
                    ContentProps={{
                    'aria-describedby': 'message-id',
                    }}
                    message={<span id="message-id">{cartStore.errors}</span>}
                    action={[
                    <IconButton
                        key="close"
                        aria-label="close"
                        color="inherit"
                        onClick={this.handleClose}
                    >
                        <CloseIcon />
                    </IconButton>,
                    ]}
                />
        </div>;
    }
}
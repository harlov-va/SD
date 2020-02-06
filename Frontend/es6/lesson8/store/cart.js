import {observable, computed, action} from 'mobx';
import productsStore from './products';
import * as ArticlesModel from './articles';


class Cart{
    @observable idProducts = [];

    @observable errors = [];

    @observable openDialogErrors = false;

    @observable open = true;

    @observable userName = "";

    @observable userBalance = 0;

    @observable hideField = "none";//inline

    @observable hideButtonLogon = "inline";

    @observable buttonRegisterText = "Register a user";

    @computed get cnt(){
        return this.idProducts.length;
    }

    @computed get inCart(){
        return (id) => this.idProducts.indexOf(id) !== -1;
    }

    @computed get total(){
        return this.idProducts.reduce((total, id) => {
            return total + productsStore.item(id).price;
        }, 0);
    }

    @action add(id){
        if(this.idProducts.indexOf(id) === -1){
            this.idProducts.push(id);
        }
    }

    @action remove(id){
        let ind = this.idProducts.indexOf(id);

        if(this.idProducts.indexOf(id) !== -1){
            this.idProducts.splice(ind, 1);            
        }
    }

    @action clear(){
        this.idProducts = [];
    }

    @action auth(email,password,username){
        return ArticlesModel.auth(email,password,username).then((data)=>{
            let response = JSON.parse(data);
            if(("id_token" in response)){
                this.open = false;
                this.userInfo();
            }
        }).catch(error => {
            this.errors = error.response.data;
            this.openDialogErrors = true;
        });
    }

    @action userInfo(){
        return ArticlesModel.userInfo().then((info) =>{
        let user = JSON.parse(info);
        if(("userName" in user)){
            this.userName = user.userName;
        }
        if(("balance" in user)){
            this.userBalance = user.balance;
        }
        }).catch(error => {
            this.errors = error.response.data;
            this.openDialogErrors = true;
        });
    }

    @action logout(){
        this.hideButtonLogon = "inline";
        this.hideField = "none"
        this.buttonRegisterText = "Register a user";
        this.open = true;
        this.userName = "";
        this.userBalance = 0;
        productsStore.list = [];
        let token = localStorage.getItem('id_token');
        if(token !== null){
            localStorage.removeItem('id_token');
        }
    }
}

export default new Cart();
import {observable, computed, action} from 'mobx';
import * as ArticlesModel from './articles';
import cartStore from './cart';

class Products{
    @observable list = [];
    @observable userList = [];

    @computed get mapId(){
        let map = {};

        this.list.forEach((item, i) => {
            map[item.id] = i
        });

        return map;
    }

    @computed get item(){
        return (id) => this.list[this.mapId[id]];
    }

    @action listTransactions(){
        return ArticlesModel.listTransactions().then((data) =>{
        let res = JSON.parse(data);
        if(res !== null){
            this.list = [...res.trans_token] ;
        }
        }).catch(error => {
            this.errors = error.response.data;
            this.openDialogErrors = true;
        });
    }

    @action createTransaction(name ,amount){
        return ArticlesModel.createTransaction(name ,amount).then((data)=>{
            let response = JSON.parse(data);
            if(response !== null){
                cartStore.userBalance = response.trans_token.balance;
            }
        }).catch(error => {
            cartStore.errors = error.response.data;
            cartStore.openDialogErrors = true;
        });
    }

    @action async filteredUserlist(filter){
        return ArticlesModel.filteredUserlist(filter).then((data)=>{
            let res = JSON.parse(data);
            if(res !== null){
                this.userList = [];
                res.forEach((item,i)=>{
                    this.userList[i] = { label: item.name, id: item.id};
                });
            }    
        }).catch(error => {
            cartStore.errors = error.response.data;
            cartStore.openDialogErrors = true;
        });
    }
}

export default new Products();
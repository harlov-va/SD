/* global FormData */
import server from './server';

export async function all(){
    let response = await server.get();
    return response.data;
}

export async function one(id){
    let response = await server.get(`articles.php`, {
        params: {id}
    });

    return response.data;
}

export async function remove(id){
    let response = await server.delete('articles.php', {
        params: {id}
    });

    return response.data;
}

export async function add(article){
    let formData = new FormData();

    for(let name in article){
        formData.append(name, article[name]);
    }

    let response = await server.post('articles.php', formData);
    
    return response.data;
}

export async function edit(id, article){
    let forServer = {
        ...article,
        id
    };

    let response = await server.put('articles.php', forServer);
    return response.data;
}

export async function auth(email,password,username){
    let formData = new FormData();
    let response;
    if (username===undefined){
        formData.append('email', email);
        formData.append('password', password);
        response = await server.post('/sessions/create',formData);
    }
    else{
        formData.append('username', username);
        formData.append('password', password);
        formData.append('email', email);
        response = await server.post('/users',formData);
    }  
    return response.data;
}

export async function listTransactions(){
    let response = await server.get('/api/protected/transactions');
    return response.data;
}

export async function userInfo(){
    let response = await server.get('/api/protected/user-info');
    return response.data;
}

export async function filteredUserlist(filter){
    let formData = new FormData();
    formData.append('filter', filter);
    let response = await server.post('/api/protected/users/list',formData);
    return response.data;   
}

export async function createTransaction(name, amount){
    let formData = new FormData();
    //formData.append('id', id);
    formData.append('amount', amount);
    formData.append('name', name);
    let response = await server.post('/api/protected/transactions',formData);
    return response.data; 
}

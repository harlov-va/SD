/* global FormData */
import server from './server';

export async function all(){
    let response = await server.get('articles.php');
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
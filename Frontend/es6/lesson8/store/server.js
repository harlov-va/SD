import axios from 'axios';


let server = axios.create({
	baseURL: 'http://localhost:14825'
});

server.interceptors.request.use(function(request){
    //request.headers.Autorization = '50537266ded1d3eb1e6923f7f4b2f484';
    let addToken = localStorage.getItem('id_token');
    if(addToken !== null){
        request.headers.Authorization = addToken;
      }
    return request;
});

server.interceptors.response.use(function(response){
    /*if(typeof response.data !== "object"){
        throw new Error("server did not send json");
    }*/
    console.log(response);
    let res = JSON.parse(response.data);
    if(("id_token" in res)){
        localStorage.setItem('id_token','Baerer ' + res.id_token);
    }
    return response;
});

export default server;
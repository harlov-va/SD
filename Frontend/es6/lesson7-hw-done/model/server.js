import axios from 'axios';

let server = axios.create({
	baseURL: '/js-hw-api/'
});

server.interceptors.request.use(function(request){
    request.headers.Autorization =  '50537266ded1d3eb1e6923f7f4b2f484';
    return request;
});

server.interceptors.response.use(function(response){
    /*if(typeof response.data !== "object"){
        throw new Error("server did not send json");
    }*/

    return response;
});

export default server;
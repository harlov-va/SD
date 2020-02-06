@nz
class Some{
    @readonly @final power = 2;
    
    @checkNumbers
    pow(x){
        return x ** this.power;
    }
}

function readonly(target, name, descriptor){
    descriptor.writable = false;
}

function final(target, name, descriptor){
    descriptor.configurable = false;
}

let some = new Some();
console.log(some.power);
some.power = 3;
console.log(some.power);
console.log(some.pow(3));
console.log(some.pow("3"));

function checkNumbers(target, name, descriptor){
    let realFunc = descriptor.value;
    
    descriptor.value = function(...args){
        args.forEach((item) => {
            if(typeof item !== "number"){
                throw new Error('type incorrect');
            }
        });

        return realFunc.apply(this, args);
    }
}

function nz(classe){
    console.dir(classe);
}

/*
let sum = checkNumbers(function(a, b){
    return a + b;
});

let sub = checkNumbers(function(a, b){
    return a - b;
});

console.log(sum(1, 2));
console.log(sub(1, 2));
//console.log(sum(1, "2"));
*/
import * as petsClient from './modules/petsClient.js';

//top level await only in nodejs 14.8 modules
//for nodejs 12

(async function main(){
    const firstPetName = await petsClient.getPetNameAsync(1);
    console.log(firstPetName);
}());





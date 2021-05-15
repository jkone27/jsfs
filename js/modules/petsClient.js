import axios from 'axios';


/*
curl -X 'GET' \
  'https://petstore.swagger.io/v2/pet/1' \
  -H 'accept: application/json' \
  -H 'api_key: special-key'
*/

const baseUri = 'https://petstore.swagger.io/v2/';

async function getPetNameAsync(petId) {

    let pet = await axios.get(`${baseUri}pet/${petId}`, {
        headers : {
            api_key : 'special-key'
        }
    });

    return pet.data.name;
};

// LOL needs node-fetch package as fetch is not available in node.js (:P)
// https://stackoverflow.com/questions/48433783/referenceerror-fetch-is-not-defined
async function getPetNameVanillaAsync(petId) {

    let response = await fetch(`${baseUri}pet/${petId}`, {
        headers : {
            'api_key' : 'special-key'
        }
    });

    let json = await response.json();

    return json.name;
};

export {
    baseUri,
    getPetNameAsync,
    getPetNameVanillaAsync
};


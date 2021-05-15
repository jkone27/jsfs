namespace Modules

open System
open FSharp.Control.Tasks.V2
open System.Net.Http
open System.Text.Json
open System.Text.Json.Serialization

module PetsClient =


    // important! https://stackoverflow.com/questions/23438416/why-is-httpclient-baseaddress-not-working
    
    [<Literal>]
    let baseUriString = "https://petstore.swagger.io/v2/"

    (*
    curl -X 'GET' \
      'https://petstore.swagger.io/v2/pet/1' \
      -H 'accept: application/json' \
      -H 'api_key: special-key'
    *)

    let baseUri = new System.Uri(baseUriString)

    let httpClient = new HttpClient()

    httpClient.BaseAddress <- baseUri

    type Pet = { name : string }

    let getNameByIdAsync petId =
      task {

          let path = "pet/" + petId.ToString()
          let request = new HttpRequestMessage(HttpMethod.Get, path)
          request.Headers.Add("api_key","special-key")
          
          let! httpResponse = httpClient.SendAsync(request)
          httpResponse.EnsureSuccessStatusCode() |> ignore
          
          use! responseStream = httpResponse.Content.ReadAsStreamAsync()
          let! pet = JsonSerializer.DeserializeAsync<Pet>(responseStream)
          return pet.name
      }
       
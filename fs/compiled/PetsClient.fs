namespace Modules

open System
open FSharp.Control.Tasks.V2
open System.Net.Http
open System.Text.Json
open System.Text.Json.Serialization
open System.Net.Http.Json

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

    type RequestHandler() =
        inherit DelegatingHandler(new HttpClientHandler (UseCookies = false))
        override this.SendAsync(request, cancellationToken) =
            // Put break point here is want to debug HTTP calls
            request.Headers.Add("api_key","special-key")
            base.SendAsync(request, cancellationToken)

    let baseUri = new System.Uri(baseUriString)

    let authHandler = new RequestHandler()

    let httpClient = new HttpClient(authHandler, true)
    httpClient.BaseAddress <- baseUri

    type Pet = { name : string }

    let getNameByIdAsync petId =
      task {

          let path = $"pet/{petId}"

          let! pet = httpClient.GetFromJsonAsync<Pet>(path)

          (* let request = new HttpRequestMessage(HttpMethod.Get, path)
          request.Headers.Add("api_key","special-key")
          
          let! httpResponse = httpClient.SendAsync(request)
          httpResponse.EnsureSuccessStatusCode() |> ignore
          
          use! responseStream = httpResponse.Content.ReadAsStreamAsync()
          let! pet = JsonSerializer.DeserializeAsync<Pet>(responseStream)

          *)

          return pet.name
      }
       
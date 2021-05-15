#r "nuget:FSharp.Data"
#r "nuget:SwaggerProvider"
open FSharp.Data
open SwaggerProvider

type PetsApi = OpenApiClientProvider<"https://petstore.swagger.io/v2/swagger.json">

let c = PetsApi.Client()

let p = new PetsApi.Pet("pet1", [||], Some(1L), 
    PetsApi.Category(Some(1L), "home"))

c.AddPet(p).GetAwaiter().GetResult()

c.GetInventory().GetAwaiter().GetResult()

c.FindPetsByStatus([| "available" |]).GetAwaiter().GetResult()

try
    let petId = 1
    Http.RequestString($"https://petstore.swagger.io/v2/pet/{petId}",
        headers=[("api_key","special-key")])
with ex ->
    ex.Message
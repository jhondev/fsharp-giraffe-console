open System
open Giraffe.Core
open Microsoft.AspNetCore.Builder
open Giraffe
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Hosting
// open MongoDB.Driver
open TodoInMemory
open System.Collections
// open TodoInMongoDb
// open Todos

let routes =
    choose [ TodosHttp.handlers ]

let configureApp (app: IApplicationBuilder) = app.UseGiraffe routes

let configureServices (services: IServiceCollection) =
    let inMemory = Hashtable()
    // let mongo = MongoClient(Environment.GetEnvironmentVariable "MONGO_URL")
    // let db = mongo.GetDatabase "todos"

    services.AddGiraffe() |> ignore
    services.AddTodoInMemory inMemory |> ignore
    // services.AddTodoInMongoDB(db.GetCollection<Todo>("todos")) |> ignore

[<EntryPoint>]
let main _ =
    WebHostBuilder()
        .UseKestrel()
        .Configure(configureApp)
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0 // return an integer exit code

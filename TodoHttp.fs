module TodosHttp

open Giraffe.Core
open Giraffe.Routing
open Giraffe.ResponseWriters
open Giraffe.Common
open Giraffe
open Microsoft.AspNetCore.Http
open FSharp.Control.Tasks.V2
open System
open Todos

let handlers: HttpFunc -> HttpContext -> HttpFuncResult =
    choose
        [ POST >=> route "/todos" >=> fun next context ->
            task {
                let save = context.GetService<TodoSaveFn>()
                let! todo = context.BindJsonAsync<Todo>()
                let todo = { todo with Id = ShortGuid.fromGuid (Guid.NewGuid()) }
                return! json (save todo) next context
            }

          GET >=> route "/todos" >=> fun next context ->
              let find = context.GetService<TodoFindFn>()
              let todos = find All
              json todos next context

          PUT >=> routef "/todos/%s" (fun id next context ->
                      task {
                          let save = context.GetService<TodoSaveFn>()
                          let! todo = context.BindJsonAsync<Todo>()
                          let todo = { todo with Id = id }
                          return! json (save todo) next context
                      })

          DELETE >=> routef "/todos/%s" (fun id next context ->
                         let delete = context.GetService<TodoDeleteFn>()
                         json (delete id) next context) ]

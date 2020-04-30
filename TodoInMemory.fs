module TodoInMemory

open Microsoft.Extensions.DependencyInjection
open Todos
open System.Collections

let find (inMemory: Hashtable) (criteria: TodoCriteria): Todo [] =
    match criteria with
    | All ->
        inMemory.Values
        |> Seq.cast
        |> Array.ofSeq

let save (inMemory: Hashtable) (todo: Todo): Todo =
    inMemory.Add(todo.Id, todo) |> ignore
    todo

type IServiceCollection with
    member this.AddTodoInMemory(inMemory: Hashtable) =
        this.AddSingleton<TodoFindFn>(find inMemory) |> ignore
        this.AddSingleton<TodoSaveFn>(save inMemory) |> ignore

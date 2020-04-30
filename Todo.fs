module Todos

type Todo =
    { Id: string
      Text: string
      Done: bool }

type TodoSaveFn = Todo -> Todo

type TodoCriteria = | All

type TodoFindFn = TodoCriteria -> Todo []

type TodoDeleteFn = string -> bool

[<ReflectedDefinition>]
module FSharpIDE

open FunScript
open FunScript.TypeScript
open FunScript.TypeScript.fs
open FunScript.TypeScript.child_process
open FunScript.TypeScript.AtomCore
open FunScript.TypeScript.text_buffer
open FunScript.TypeScript.path

open Atom
open Atom.FSharp

type FSharpIDE() =
    member x.provide () =
        AutocompleteProvider.create ()

    member x.provideErrors () =
        ErrorLinterProvider.create ()

    member x.getSuggestion(options : AutocompleteProvider.GetSuggestionOptions) =
        AutocompleteProvider.getSuggestion options

    member x.consumeYeomanEnvironment (gen : YeomanHandler.generator) =
        YeomanHandler.activate gen

    member x.activate(state:obj) =
        LanguageService.start ()
        Parser.activate ()
        TooltipHandler.activate ()
        ToolbarHandler.activate()             // needs to follow error panel so it appears above it
        FindDeclaration.activate ()
        FAKE.activate ()
        Interactive.activate ()
        HighlightUse.activate ()
        AddFileHandler.activate ()
        ()

    member x.deactivate() =
        Parser.deactivate ()
        TooltipHandler.deactivate ()
        ToolbarHandler.deactivate()
        FindDeclaration.deactivate ()
        FAKE.deactivate ()
        Interactive.deactivate ()
        HighlightUse.deactivate ()
        LanguageService.stop ()
        ()

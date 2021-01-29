module Model

open Thoth.Elmish
open Fable.Core
open MonacoEditor

[<StringEnum>]
type EditorLanguage =
  | [<CompiledName("plaintext")>] PlainText
  | [<CompiledName("markdown")>] Markdown
  | [<CompiledName("javascript")>] JavaScript
  | [<CompiledName("typescript")>] TypeScript
  | [<CompiledName("json")>] Json
  | [<CompiledName("xml")>] Xml
  | [<CompiledName("yaml")>] Yaml
  | [<CompiledName("sql")>] Sql
  member x.displayText =
    match x with
    | PlainText -> "Plain text"
    | Markdown -> "Markdown"
    | JavaScript -> "JavaScript"
    | TypeScript -> "TypeScript"
    | Json -> "JSON"
    | Xml -> "XML"
    | Yaml -> "YAML"
    | Sql -> "SQL"
  static member all = [PlainText; Markdown; JavaScript; TypeScript; Json; Xml; Yaml; Sql]

type Height = | Height of int

type EditorOptions = { WrapText: bool }
  with 
    static member initial = { WrapText = false }

type DragModel = { DragenterCount: int; DragleaveCount: int }
  with 
    static member initial = { DragenterCount = 0; DragleaveCount = 0 }
    member x.isDragging = x.DragenterCount > x.DragleaveCount

type TabItemModel = { Name: string; ModelIndex: int; Language: EditorLanguage; UntitledIndex: int }
  with
    static member initial = { Name = "Untitled 1"; ModelIndex = 0; Language = PlainText; UntitledIndex = 1 }

[<RequireQualifiedAccess>]
type ControlId =
  | None
  | WrapText
  | EditorLanguage
  | IncreaseFontSize
  | DecreaseFontSize

[<RequireQualifiedAccess>]
type ThemeKind =
  | Dark
  | Light

type CssClasses = { RootDiv: string }

// Model holds the current state.
type Model = 
  { SelectedTabId: int
    TabItems: TabItemModel list
    EditorHeight: int
    EditorOptions: EditorOptions
    EditorLanguage: EditorLanguage
    ShowTooltipControlId: ControlId
    WindowInnerWidth: float
    DevicePixelRatio: float
    Debouncer: Debouncer.State
    DragModel: DragModel
    ThemeKind: ThemeKind }

 // Not included in Model type because quite large.
let mutable monacoEditor: IMonacoEditor option = None
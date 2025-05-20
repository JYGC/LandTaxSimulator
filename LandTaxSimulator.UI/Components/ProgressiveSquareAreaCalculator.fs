namespace LandTaxSimulator.UI.Components

open FSharp.Data.Adaptive
open Fun.Blazor
open MudBlazor

module ProgressiveSquareAreaCalculator =
    type private Bracket = {
        PricePerSqmThreshold: float
        Rate: float
    }

    let private replaceElementByIndex index newElement list =
        Array.concat [
            (Array.sub list 0 index);
            [| newElement |];
            (Array.sub list (index + 1) (list.Length - (index + 1)))
        ]
    
    let private renderBracketEditRow
      (bracket: Bracket)
      (editButtonCallback: Bracket -> unit)
      (editButtonLabel: string) =
        adapt {
            let! currentBracket, setCurrentBracket = cval(bracket).WithSetter()
            MudItem'' {
                xs 5
                sm 5
                md 5
                lg 5
                xl 5
                MudTextField'' {
                    Label "Price per square meters"
                    InputType InputType.Number
                    Variant Variant.Text
                    Value currentBracket.PricePerSqmThreshold
                    ValueChanged (fun e -> setCurrentBracket({ currentBracket with PricePerSqmThreshold = e }))
                }
            }
            MudItem'' {
                xs 5
                sm 5
                md 5
                lg 5
                xl 5
                MudTextField'' {
                    Label "Rate"
                    InputType InputType.Number
                    Variant Variant.Text
                    Value currentBracket.Rate
                    ValueChanged (fun e -> setCurrentBracket({ currentBracket with Rate = e }))
                }
            }
            MudItem'' {
                xs 2
                sm 2
                md 2
                lg 2
                xl 2
                MudButton'' {
                    Color Color.Primary
                    Variant Variant.Outlined
                    OnClick (fun _ -> editButtonCallback(currentBracket))
                    editButtonLabel
                }
            }
        }

    let view () = adapt {
        let! brackets, setBrackets = cval<Bracket array>([|
            { PricePerSqmThreshold = 0; Rate = 0.00 };
            { PricePerSqmThreshold = 1; Rate = 0.01 } |]).WithSetter()

        fragment {
            MudGrid'' {
                fragment {
                    for index in [0..(brackets.Length - 1)] ->
                        let bracket = brackets[index]
                        renderBracketEditRow
                            bracket
                            (fun changedBracket ->
                                replaceElementByIndex
                                    index
                                    changedBracket
                                    brackets
                                |> setBrackets
                            )
                            "Save"
                }
                hr {}
                renderBracketEditRow
                    { PricePerSqmThreshold = 0; Rate = 0.00 }
                    (fun newBracket ->
                        Array.concat [ brackets; [| newBracket |] ]
                        |> setBrackets
                    )
                    "Add"
            }
        }

        for bracket in brackets do
            p { $"PricePerSqmThreshold:{bracket.PricePerSqmThreshold} Rate:{bracket.Rate}" }
    }


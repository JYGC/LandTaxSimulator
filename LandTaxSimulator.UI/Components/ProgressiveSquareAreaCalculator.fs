namespace LandTaxSimulator.UI.Components

open FSharp.Data.Adaptive
open Fun.Blazor
open MudBlazor

module ProgressiveSquareAreaCalculator =
    type private Bracket = {
        PricePerSqmThreshold: float
        Rate: float
    }

    type private BracketContainer = {
        Bracket: Bracket
        Selected: bool
    }

    let private replaceElementByIndex index newElement list =
        Array.concat [
            (Array.sub list 0 index);
            [| newElement |];
            (Array.sub list (index + 1) (list.Length - (index + 1)))
        ]

    let private removeElementByIndex index list =
        Array.concat [
            (Array.sub list 0 index);
            (Array.sub list (index + 1) (list.Length - (index + 1)))
        ]

    let private renderBracketViewRow
      (bracket: Bracket)
      (editButtonCallback: _ -> unit)
      (deleteButtonCallback: _ -> unit) = fragment {
        MudItem'' {
            xs 12
            sm 12
            md 12
            lg 12
            xl 12
            MudPaper'' {
                onclick (fun _ -> editButtonCallback ())
                $"Click to edit"
                $"PricePerSqmThreshold: {bracket.PricePerSqmThreshold}"
                $"Rate: {bracket.Rate}"
                MudItem'' {
                    xs 2
                    sm 2
                    md 2
                    lg 2
                    xl 2
                    MudButton'' {
                        Color Color.Secondary
                        Variant Variant.Filled
                        OnClick (fun _ -> deleteButtonCallback ())
                        "Delete"
                    }
                }
            }
        }
      }
    
    let private renderBracketEditRow
      (bracket: Bracket)
      (saveButtonCallback: Bracket -> unit)
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
                    Variant Variant.Filled
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
                    Variant Variant.Filled
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
                    OnClick (fun _ -> saveButtonCallback(currentBracket))
                    editButtonLabel
                }
            }
        }

    let view () = adapt {
        let! bracketContainers, setBracketContainers = cval<BracketContainer array>([|
            { Bracket = { PricePerSqmThreshold = 0; Rate = 0.00 }; Selected = false };
            { Bracket = { PricePerSqmThreshold = 1; Rate = 0.01 }; Selected = false } |]).WithSetter()

        MudGrid'' {
            fragment {
                for index in [0..(bracketContainers.Length - 1)] ->
                    let bracketContainer = bracketContainers[index]
                    match bracketContainer.Selected with
                    | false ->
                        renderBracketViewRow
                            bracketContainer.Bracket
                            (fun _ ->
                                [|
                                    for index1 in [0..(bracketContainers.Length - 1)] do
                                        { bracketContainers[index1] with Selected = index1 = index }
                                |]
                                |> setBracketContainers
                            )
                            (fun _ ->
                                removeElementByIndex index bracketContainers
                                |> setBracketContainers
                            )
                    | true ->
                        renderBracketEditRow
                            bracketContainer.Bracket
                            (fun changedBracket ->
                                replaceElementByIndex
                                    index
                                    changedBracket
                                    (bracketContainers |> Array.map (fun bc -> bc.Bracket))
                                |> Array.map (fun b -> { Bracket = b; Selected = false })
                                |> setBracketContainers
                            )
                            "Save"
            }
            hr {}
            renderBracketEditRow
                { PricePerSqmThreshold = 0; Rate = 0.00 }
                (fun newBracket ->
                    Array.concat [ bracketContainers; [| { Bracket = newBracket; Selected = false } |] ]
                    |> setBracketContainers
                )
                "Add"
        }

        for bracketContainer in bracketContainers do
            p { $"PricePerSqmThreshold:{bracketContainer.Bracket.PricePerSqmThreshold} Rate:{bracketContainer.Bracket.Rate}" }
    }


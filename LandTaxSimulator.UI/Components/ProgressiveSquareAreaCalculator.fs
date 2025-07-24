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
                MudGrid'' {
                    MudItem'' {
                        xs 3
                        sm 3
                        md 3
                        lg 3
                        xl 3
                        MudText'' {
                            $"Click to edit"
                        }
                    }
                    MudItem'' {
                        xs 3
                        sm 3
                        md 3
                        lg 3
                        xl 3
                        MudText'' {
                            $"PricePerSqmThreshold: {bracket.PricePerSqmThreshold}"
                        }
                    }
                    MudItem'' {
                        xs 3
                        sm 3
                        md 3
                        lg 3
                        xl 3
                        MudText'' {
                            $"Rate: {bracket.Rate}"
                        }
                    }
                    MudItem'' {
                        xs 3
                        sm 3
                        md 3
                        lg 3
                        xl 3
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
      }
    
    let private renderBracketEditRow
      (bracket: Bracket)
      (minimumPricePerSqmThreshold: double)
      (minimumRate: double)
      (saveButtonCallback: Bracket -> unit)
      (editButtonLabel: string) =
        let currentBracket = cval(bracket)
        adapt {
            let! currentBracketValue, setCurrentBracketValue = currentBracket.WithSetter()
            MudItem'' {
                xs 5
                sm 5
                md 5
                lg 5
                xl 5
                MudTextField'' {
                    Label "Price per square meters"
                    InputType InputType.Number
                    step 1
                    min minimumPricePerSqmThreshold
                    Variant Variant.Filled
                    Value currentBracketValue.PricePerSqmThreshold
                    ValueChanged (fun e -> 
                        let newValue = if e < minimumPricePerSqmThreshold then minimumPricePerSqmThreshold else e
                        setCurrentBracketValue({ currentBracketValue with PricePerSqmThreshold = newValue })
                    )
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
                    step 0.01
                    min minimumRate
                    Variant Variant.Filled
                    Value currentBracketValue.Rate
                    ValueChanged (fun e ->
                        let newValue = if e < minimumRate then minimumRate else e
                        setCurrentBracketValue({ currentBracketValue with Rate = newValue })
                    )
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
                    Variant Variant.Filled
                    OnClick (fun _ -> saveButtonCallback(currentBracketValue))
                    editButtonLabel
                }
            }
        }

    let view () = 
        let bracketContainers = cval<BracketContainer array>([|
            { Bracket = { PricePerSqmThreshold = 0; Rate = 0.00 }; Selected = false };
            { Bracket = { PricePerSqmThreshold = 1; Rate = 0.01 }; Selected = false } |])
        adapt {
            let! bracketContainersValue, setBracketContainersValue = bracketContainers.WithSetter()

            MudGrid'' {
                fragment {
                    for index in [0..(bracketContainersValue.Length - 1)] ->
                        let bracketContainer = bracketContainersValue[index]
                        match bracketContainer.Selected with
                        | false ->
                            renderBracketViewRow
                                bracketContainer.Bracket
                                (fun _ ->
                                    [|
                                        for index1 in [0..(bracketContainersValue.Length - 1)] do
                                            { bracketContainersValue[index1] with Selected = index1 = index }
                                    |]
                                    |> setBracketContainersValue
                                )
                                (fun _ ->
                                    removeElementByIndex index bracketContainersValue
                                    |> setBracketContainersValue
                                )
                        | true ->
                            let (minimumPricePerSqmThreshold, minimumRate) =
                                match index with
                                | 0 -> (0.00, 0.00)
                                | _ -> (bracketContainersValue[index - 1].Bracket.PricePerSqmThreshold + 1.00, bracketContainersValue[index - 1].Bracket.Rate + 0.01)

                            renderBracketEditRow
                                bracketContainer.Bracket
                                minimumPricePerSqmThreshold
                                minimumRate
                                (fun changedBracket ->
                                    replaceElementByIndex
                                        index
                                        changedBracket
                                        (bracketContainersValue |> Array.map (fun bc -> bc.Bracket))
                                    |> Array.map (fun b -> { Bracket = b; Selected = false })
                                    |> setBracketContainersValue
                                )
                                "Save"
                }
                hr {}
                let (minimumPricePerSqmThreshold, minimumRate) =
                    match bracketContainersValue.Length with
                    | 0 -> (0.00, 0.00)
                    | _ -> (
                        bracketContainersValue[bracketContainersValue.Length - 1].Bracket.PricePerSqmThreshold + 1.00,
                        bracketContainersValue[bracketContainersValue.Length - 1].Bracket.Rate + 0.01
                    )
                renderBracketEditRow
                    { PricePerSqmThreshold = minimumPricePerSqmThreshold; Rate = minimumRate }
                    minimumPricePerSqmThreshold
                    minimumRate
                    (fun newBracket ->
                        Array.concat [ bracketContainersValue; [| { Bracket = newBracket; Selected = false } |] ]
                        |> setBracketContainersValue
                    )
                    "Add"
            }

            for bracketContainer in bracketContainersValue do
                p { $"PricePerSqmThreshold:{bracketContainer.Bracket.PricePerSqmThreshold} Rate:{bracketContainer.Bracket.Rate}" }
        }


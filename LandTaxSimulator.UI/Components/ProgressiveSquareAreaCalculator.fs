namespace LandTaxSimulator.UI.Components

open System
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
      (currentBracket: Bracket)
      (deleteButtonCallback: Bracket -> unit) =
        fragment {
            MudTd'' {
                DataLabel "PricePerSqmThreshold"
                MudText'' {
                    currentBracket.PricePerSqmThreshold
                }
            }
            MudTd'' {
                DataLabel "Rate"
                MudText'' {
                    currentBracket.Rate
                }
            }
            MudTd'' {
                MudButton'' {
                    Color Color.Secondary
                    Variant Variant.Filled
                    OnClick (fun _ -> deleteButtonCallback currentBracket)
                    "Delete"
                }
            }
        }
    
    let private renderBracketEditRow
      (currentBracketValue: Bracket)
      (setCurrentBracketValuePricePerSqmThreshold: double -> unit)
      (setCurrentBracketValueRate: double -> unit)
      (endEditButtonCallback: _ -> unit)
      (editButtonLabel: string) =
        fragment {
            MudTd'' {
                DataLabel "PricePerSqmThreshold"
                MudTextField'' {
                    Label "Price per square meters"
                    InputType InputType.Number
                    step 10
                    Variant Variant.Filled
                    Value currentBracketValue.PricePerSqmThreshold
                    ValueChanged setCurrentBracketValuePricePerSqmThreshold
                }
            }
            MudTd'' {
                DataLabel "Rate"
                MudTextField'' {
                    Label "Rate"
                    InputType InputType.Number
                    step 0.01
                    Variant Variant.Filled
                    Value currentBracketValue.Rate
                    ValueChanged setCurrentBracketValueRate
                }
            }
            MudTd'' {
                MudButton'' {
                    Color Color.Primary
                    Variant Variant.Filled
                    OnClick endEditButtonCallback
                    editButtonLabel
                }
            }
        }

    let rec private calculateTaxPerSqmOverBracketFloor
      (indexWithBrackets: (int * Bracket) array)
      (currentIndex: int)
      (costPerSqmOverBracket: float) =
        let currentBracket = snd indexWithBrackets[currentIndex]
        let sizeBetweenCurrentAndNextBracketOption =
            (
                match currentIndex with
                | v when v < indexWithBrackets.Length - 1 ->
                    indexWithBrackets[currentIndex + 1] |> snd |> Some
                | _ -> None
            )
            |> Option.bind (fun nb -> Some (nb.PricePerSqmThreshold - currentBracket.PricePerSqmThreshold))
        let isCurrentBracketFilled =
            match sizeBetweenCurrentAndNextBracketOption with
            | Some size -> size <= costPerSqmOverBracket
            | None -> false
        
        match (sizeBetweenCurrentAndNextBracketOption, isCurrentBracketFilled) with
        | (Some size, true) ->
            (currentBracket.Rate * 0.01 * size) + (
                calculateTaxPerSqmOverBracketFloor
                    indexWithBrackets
                    (currentIndex + 1)
                    (costPerSqmOverBracket - size)
            )
        | _ -> currentBracket.Rate * 0.01 * costPerSqmOverBracket

    let private calculateTaxPerSqm
      (indexWithBrackets: (int * Bracket) array)
      (costPerSqm: float) =
        let firstBracketOption =
            match indexWithBrackets.Length with
            | 0 -> None
            | _ -> indexWithBrackets[0] |> snd |> Some
        let isCostPerSqmPassFirstBracket =
            match firstBracketOption with
            | Some firstBracket -> costPerSqm >= firstBracket.PricePerSqmThreshold
            | None -> false
        match (firstBracketOption, isCostPerSqmPassFirstBracket) with
        | (Some firstBracket, true) ->
            calculateTaxPerSqmOverBracketFloor
                indexWithBrackets
                0
                (costPerSqm - firstBracket.PricePerSqmThreshold)
        | _ ->
            0.00

    let view () = 
        let indexWithBrackets =
            [|
                { PricePerSqmThreshold = 500; Rate = 0.50 };
                { PricePerSqmThreshold = 1000; Rate = 1.00 };
                { PricePerSqmThreshold = 2000; Rate = 2.25 };
                { PricePerSqmThreshold = 5000; Rate = 5.00 };
                { PricePerSqmThreshold = 8000; Rate = 7.50 };
                { PricePerSqmThreshold = 12000; Rate = 10.00 }
            |]
            |> Array.indexed
            |> cval<(int * Bracket) array>
        let selectedIndexOption = cval<int option> None
        let landSize = cval<float> 500.00
        let landCost = cval<float> 800000.00

        adapt {
            let! selectedIndexOptionValue, setSelectedIndexOptionValue = selectedIndexOption.WithSetter()
            let! indexWithBracketsValue, setIndexWithBracketsValue = indexWithBrackets.WithSetter()

            let minimumNewPricePerSqmThreshold, minimumNewRate =
                match indexWithBracketsValue.Length with
                | 0 -> (0.00, 0.00)
                | _ ->
                    indexWithBracketsValue
                    |> Array.last
                    |> snd
                    |> fun b -> (
                        (if b.PricePerSqmThreshold = 0 then 0.01 else b.PricePerSqmThreshold + 0.01),
                        (if b.Rate = 0 then 0.01 else b.Rate + 0.01)
                    )

            let! newBracketValue, setNewBracketValue =
                (
                    {
                        PricePerSqmThreshold = minimumNewPricePerSqmThreshold;
                        Rate = minimumNewRate
                    }
                    |> cval<Bracket>
                ).WithSetter()

            let! landSizeValue, setLandSizeValue = landSize.WithSetter()
            let! landCostValue, setLandCostValue = landCost.WithSetter()

            MudGrid'' {
                MudItem'' {
                    xs 6
                    sm 6
                    md 6
                    lg 6
                    xl 6
                    MudTable'' {
                        Items indexWithBracketsValue
                        OnRowClick (fun selectedRow -> selectedRow.Item |> fst |> Some |> setSelectedIndexOptionValue)
                        HeaderContent (seq {
                            MudTh'' {
                                "Price per square meters threshold"
                            }
                            MudTh'' {
                                "Rate"
                            }
                            MudTh'' {
                                "Actions"
                            }
                        })
                        RowTemplate (fun context ->
                            let currentIndex, currentBracket = context
                            match selectedIndexOptionValue with
                            | Some selectedIndex when selectedIndex = currentIndex ->
                                let minimumPricePerSqmThreshold, minimumRate =
                                    match currentIndex with
                                    | 0 -> (0.00, 0.00)
                                    | _ ->
                                        let _, previousBracket = indexWithBracketsValue[currentIndex - 1]
                                        (previousBracket.PricePerSqmThreshold + 1.00, previousBracket.Rate + 0.01)
                                let maximumPricePerSqmThreshold, maximumRate =
                                    match currentIndex with
                                    | v when v = indexWithBracketsValue.Length - 1 -> (None, None)
                                    | v ->
                                        let _, nextBracket = indexWithBracketsValue[v + 1]
                                        (
                                            Some (nextBracket.PricePerSqmThreshold - 1.00),
                                            Some (nextBracket.Rate - 0.01)
                                        )
                                renderBracketEditRow
                                    currentBracket
                                    (fun inputFromField ->
                                        let newValue =
                                            match inputFromField, maximumPricePerSqmThreshold with
                                            | v, _ when v < minimumPricePerSqmThreshold -> minimumPricePerSqmThreshold
                                            | v, Some maximumPricePerSqmThresholdValue when v > maximumPricePerSqmThresholdValue ->
                                                maximumPricePerSqmThresholdValue
                                            | v, _ -> v
                                        let changedBracket = { currentBracket with PricePerSqmThreshold = newValue }
                                        indexWithBracketsValue
                                        |> Array.map (fun ib -> snd ib)
                                        |> replaceElementByIndex currentIndex changedBracket
                                        |> Array.indexed
                                        |> setIndexWithBracketsValue
                                    )
                                    (fun inputFromField ->
                                        let newValue =
                                            match inputFromField, maximumRate with
                                            | v, _ when v < minimumRate -> minimumRate
                                            | v, Some maximumRateValue when v > maximumRateValue -> maximumRateValue
                                            | v, _ -> v
                                        let changedBracket = { currentBracket with Rate = newValue }
                                        indexWithBracketsValue
                                        |> Array.map (fun ib -> snd ib)
                                        |> replaceElementByIndex currentIndex changedBracket
                                        |> Array.indexed
                                        |> setIndexWithBracketsValue
                                    )
                                    (fun _ -> setSelectedIndexOptionValue None)
                                    "Stop Editing"
                            | _ ->
                                renderBracketViewRow
                                    currentBracket
                                    (fun _ ->
                                        removeElementByIndex currentIndex indexWithBracketsValue
                                        |> Array.map (fun ib -> snd ib)
                                        |> Array.indexed
                                        |> setIndexWithBracketsValue
                                    )
                        )
                        FooterContent (seq {
                            renderBracketEditRow
                                newBracketValue
                                (fun inputFromField ->
                                    let newValue =
                                        match inputFromField with
                                        | v when v < minimumNewPricePerSqmThreshold -> minimumNewPricePerSqmThreshold
                                        | v -> v
                                    setNewBracketValue { newBracketValue with PricePerSqmThreshold = newValue }
                                )
                                (fun inputFromField ->
                                    let newValue =
                                        match inputFromField with
                                        | v when v < minimumNewRate -> minimumNewRate
                                        | v -> v
                                    setNewBracketValue { newBracketValue with Rate = newValue }
                                )
                                (fun _ ->
                                    indexWithBracketsValue
                                    |> Array.map (fun ib -> snd ib)
                                    |> (fun brackets -> Array.concat [ brackets; [| newBracketValue |] ])
                                    |> Array.indexed
                                    |> setIndexWithBracketsValue
                                )
                                "Add"
                        })
                    }
                }
                MudItem'' {
                    xs 6
                    sm 6
                    md 6
                    lg 6
                    xl 6
                    MudStack'' {
                        MudStack''{
                            Row
                            MudTextField'' {
                                Label "Land size (m2)"
                                InputType InputType.Number
                                step 10
                                Variant Variant.Filled
                                Value landSizeValue
                                ValueChanged setLandSizeValue
                            }
                        }
                        MudStack''{
                            Row
                            MudTextField'' {
                                Label "Land cost ($)"
                                InputType InputType.Number
                                step 100
                                Variant Variant.Filled
                                Value landCostValue
                                ValueChanged setLandCostValue
                            }
                        }
                        let costPerSqm = landCostValue / landSizeValue
                        MudText'' {
                            $"Cost per m2 ($/m2): {Math.Round(costPerSqm, 2)}"
                        }
                        let taxPerSqm = calculateTaxPerSqm indexWithBracketsValue costPerSqm
                        MudText'' {
                            $"Tax per m2 ($/m2): {Math.Round(taxPerSqm, 2)}"
                        }
                        MudText'' {
                            $"Total Tax ($): {Math.Round(taxPerSqm * landSizeValue, 2)}"
                        }
                        MudText'' {
                            $"Total Tax (%%): {Math.Round(taxPerSqm * landSizeValue * 100.00 / landCostValue, 2)}"
                        }
                    }
                }
            }
        }


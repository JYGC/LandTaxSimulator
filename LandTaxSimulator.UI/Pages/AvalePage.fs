namespace LandTaxSimulator.UI.Pages

open Fun.Blazor
open FSharp.Data.Adaptive
open MudBlazor

module AvalePage =
    let routerView () =
        let first = cval 0
        let second = cval 0
        adapt {
            let! firstValue, setFirstValue = first.WithSetter()
            let! secondValue, setSecondValue = second.WithSetter()
            let sum = firstValue + secondValue

            p { $"firstValue: {firstValue}" }
            p { $"secondValue: {secondValue}" }
            p { $"sum: {sum}" }

            MudButton'' {
                OnClick (fun _ -> setFirstValue(firstValue + 1))
                "Increment First"
            }

            MudButton'' {
                OnClick (fun _ -> setSecondValue(secondValue + 1))
                "Increment Second"
            }
        }


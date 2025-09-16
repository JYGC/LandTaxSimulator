namespace LandTaxSimulator.UI.Pages

open Fun.Blazor
open FSharp.Data.Adaptive
open MudBlazor

module AvalePage =
    let routerView () =
        let firstState = cval 0
        let secondState = cval 0
        let thridState = cval 0
        let fourState = cval 0
        let fithState = cval 0
        let sumAval = aval {
            let! a = firstState
            let! b = secondState
            let! c = thridState
            let! d = fourState
            let! e = fithState
            return a + b + c + d + e
        }
        adapt {
            let! first, setFirst = firstState.WithSetter()
            let! second, setSecond = secondState.WithSetter()
            let! third, setThird = thridState.WithSetter()
            let! four, setFour = fourState.WithSetter()
            let! fith, setFith = fithState.WithSetter()

            let! sum = sumAval

            p { $"firstValue: {first}" }
            p { $"secondValue: {second}" }
            p { $"thirdValue: {third}" }
            p { $"fourValue: {four}" }
            p { $"fithValue: {fith}" }
            p { $"sum: {sum}" }

            MudGrid'' {
                MudItem'' {
                    xs 12
                    sm 12
                    md 12
                    lg 12
                    xl 12
                    MudButton'' {
                        OnClick (fun _ -> setFirst(first + 1))
                        "Increment First"
                    }

                    MudButton'' {
                        OnClick (fun _ -> setSecond(second + 1))
                        "Increment Second"
                    }

                    MudButton'' {
                        OnClick (fun _ -> setThird(third + 1))
                        "Increment Third"
                    }

                    MudButton'' {
                        OnClick (fun _ -> setFour(four + 1))
                        "Increment Four"
                    }

                    MudButton'' {
                        OnClick (fun _ -> setFith(fith + 1))
                        "Increment Fith"
                    }
                }
                MudItem'' {
                    xs 12
                    sm 12
                    md 12
                    lg 12
                    xl 12
                    MudButton'' {
                        OnClick (fun _ -> setFirst(first - 1))
                        "Decrement First"
                    }

                    MudButton'' {
                        OnClick (fun _ -> setSecond(second - 1))
                        "Decrement Second"
                    }

                    MudButton'' {
                        OnClick (fun _ -> setThird(third - 1))
                        "Decrement Third"
                    }

                    MudButton'' {
                        OnClick (fun _ -> setFour(four - 1))
                        "Decrement Four"
                    }

                    MudButton'' {
                        OnClick (fun _ -> setFith(fith - 1))
                        "Decrement Fith"
                    }
                }
            }
        }


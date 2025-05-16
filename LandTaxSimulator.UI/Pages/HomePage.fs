namespace LandTaxSimulator.UI.Pages

open Fun.Blazor
open FSharp.Data.Adaptive
open Microsoft.AspNetCore.Components.Web

module HomePage =
    let routerView () =  fragment {
        PageTitle'' { "Home" }
        h1 { "Welcome to the Land Tax Simulator" }
        adapt {
            let! counter, setCounter = cval(0).WithSetter()
            p {
                "Counter: "
                strong { counter }
            }
            button {
                onclick (fun _ -> (counter + 1) |> setCounter)
                "Click me"
            }
        }
    }
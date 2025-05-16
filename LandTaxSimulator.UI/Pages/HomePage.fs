namespace LandTaxSimulator.UI.Pages

open Microsoft.AspNetCore.Components
open Fun.Blazor
open FSharp.Data.Adaptive
open Microsoft.AspNetCore.Components.Web

[<Route "/">]
type HomePage() =
    inherit FunComponent()
    override _.Render () = fragment {
        PageTitle'' { "Home" }
        h1 { "Welcome to the Land Tax Simulator" }
        adaptiview() {
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
namespace LandTaxSimulator.UI

open Fun.Blazor
open LandTaxSimulator.UI.Layout
open LandTaxSimulator.UI.Pages
open Fun.Blazor.Router

type Routes() =
    inherit FunComponent()

    override _.Render() = html.inject (fun (hook: IComponentHook) -> fragment {
        html.route [
            HomePage.routerView()
            |> routeCi "/"
            ProgressiveSquareAreaPage.routerView()
            |> routeCi "/progressive-square-area"
        ]
        |> MainLayout.view
    })
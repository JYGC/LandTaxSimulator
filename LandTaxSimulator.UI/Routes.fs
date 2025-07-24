namespace LandTaxSimulator.UI

open Fun.Blazor
open LandTaxSimulator.UI.Layout
open LandTaxSimulator.UI.Pages
open Fun.Blazor.Router
open Microsoft.AspNetCore.Components.Web
open MudBlazor

type Routes() =
    inherit FunComponent()

    override _.Render() = html.inject (fun (hook: IComponentHook) -> ErrorBoundary'() {
        ErrorContent(fun ex -> MudPaper'' {
            style {
                padding 10
                margin 20
            }
            Elevation 10
            MudText'' {
                Color Color.Error
                Typo Typo.subtitle1
                "Some error hanppened, you can try to refresh."
            }
            MudAlert'' {
                Severity Severity.Error
                ex.Message
            }
        })
        html.route [
            HomePage.routerView() |> routeCi "/"
            ProgressiveSquareAreaPage.routerView() |> routeCi "/progressive-square-area"
            AvalePage.routerView() |> routeCi "/avale"
        ]
        |> MainLayout.view
    })
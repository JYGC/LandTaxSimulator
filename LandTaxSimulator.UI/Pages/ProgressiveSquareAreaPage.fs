namespace LandTaxSimulator.UI.Pages

open Fun.Blazor
open Microsoft.AspNetCore.Components.Web
open LandTaxSimulator.UI.Components

module ProgressiveSquareAreaPage =
    let routerView () = fragment {
        PageTitle'' { "Progressive per Square Meter" }
        ProgressiveSquareAreaCalculator.view()
    }

namespace LandTaxSimulator.UI.Pages

open Fun.Blazor
open Microsoft.AspNetCore.Components.Web

module ProgressiveSquareAreaPage =
    let routerView () = fragment {
        PageTitle'' { "Progressive Square Area" }
        h1 { "Progressive Square Area" }
        p { "This is a placeholder for the Progressive Square Area page." }
        p { "You can add your content here." }
    }

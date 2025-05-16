namespace LandTaxSimulator.UI.Layout

open Microsoft.AspNetCore.Components
open Fun.Blazor

module MainLayout =
    let view (body: NodeRenderFragment) =
        div {
            nav {
                a {
                    href "/"
                    "Home"
                }
                a {
                    href "/progressive-square-area"
                    "Progressive per Square Meter"
                }
            }
            main { body }
        }

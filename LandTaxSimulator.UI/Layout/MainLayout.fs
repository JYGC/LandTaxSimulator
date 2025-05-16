namespace LandTaxSimulator.UI.Layout

open Microsoft.AspNetCore.Components
open Fun.Blazor

type MainLayout() as this =
    inherit LayoutComponentBase()

    let content = div {
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
        main { this.Body }
    }

    override _.BuildRenderTree(builder) = content.Invoke(this, builder, 0) |> ignore

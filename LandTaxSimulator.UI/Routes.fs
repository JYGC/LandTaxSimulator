namespace LandTaxSimulator.UI

open Fun.Blazor
open System.Reflection
open LandTaxSimulator.UI.Layout

type Routes() =
    inherit FunComponent()

    override _.Render() = Router'' {
        AppAssembly(Assembly.GetExecutingAssembly())
        Found(fun routeData -> RouteView'' {
            RouteData routeData
            DefaultLayout typeof<MainLayout>
        })
    }
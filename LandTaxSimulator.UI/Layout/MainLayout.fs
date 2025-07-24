namespace LandTaxSimulator.UI.Layout

open Microsoft.AspNetCore.Components
open Fun.Blazor
open MudBlazor

module MainLayout =
    let view (body: NodeRenderFragment) =
        fragment {
            MudGrid''{
                height "100%"
                MudItem'' {
                    xs 3
                    sm 3
                    md 3
                    lg 3
                    xl 3
                    MudPaper''{
                        class' "pa-2"
                        MudNavMenu'' {
                            MudNavLink'' {
                                Href "/"
                                "Home"
                            }
                            MudNavLink'' {
                                Href "/progressive-square-area"
                                "Progressive per Square Meter"
                            }
                            MudNavLink'' {
                                Href "/avale"
                                "Avale"
                            }
                        }
                    }
                }
                MudItem'' {
                    xs 9
                    sm 9
                    md 9
                    lg 9
                    xl 9
                    body
                }
            }
        }

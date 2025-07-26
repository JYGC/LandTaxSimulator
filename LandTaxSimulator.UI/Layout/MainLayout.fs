namespace LandTaxSimulator.UI.Layout

open Fun.Blazor
open MudBlazor

module MainLayout =
    let view (body: NodeRenderFragment) =
        fragment {
            MudGrid''{
                height "100%"
                MudItem'' {
                    xs 2
                    sm 2
                    md 2
                    lg 2
                    xl 2
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
                    xs 10
                    sm 10
                    md 10
                    lg 10
                    xl 10
                    body
                }
            }
        }

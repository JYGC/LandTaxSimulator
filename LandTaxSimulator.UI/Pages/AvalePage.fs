namespace LandTaxSimulator.UI.Pages

open Fun.Blazor
open FSharp.Data.Adaptive
open MudBlazor

module AvalePage =
    let routerView () =
        let firstState = cval 0
        let secondState = cval 0
        let thridState = cval 0
        let fourState = cval 0
        let fithState = cval 0
        let sixthState = cval 0
        let sumAval = aval {
            let! a = firstState
            let! b = secondState
            let! c = thridState
            let! d = fourState
            let! e = fithState
            let! f = sixthState
            return a + b + c + d + e + f
        }
        fragment {
            adapt {
                let! first = firstState
                p { $"firstValue: {first}" }
            }
            adapt {
                let! second = secondState
                p { $"secondValue: {second}" }
            }
            adapt {
                let! third = thridState
                p { $"thirdValue: {third}" }
            }
            adapt {
                let! four = fourState
                p { $"fourValue: {four}" }
            }
            adapt {
                let! fith = fithState
                p { $"fithValue: {fith}" }
            }
            adapt {
                let! sixth = sixthState
                p { $"fithValue: {sixth}" }
            }
            adapt {
                let! sum = sumAval
                p { $"sum: {sum}" }
            }

            MudGrid'' {
                MudItem'' {
                    xs 2
                    sm 2
                    md 2
                    lg 2
                    xl 2

                    adapt {
                        let! first, setFirst = firstState.WithSetter()
                        MudStack'' {
                            MudButton'' {
                                OnClick (fun _ -> setFirst(first + 1))
                                "Increment First"
                            }
                            MudButton'' {
                                OnClick (fun _ -> setFirst(first - 1))
                                "Decrement First"
                            }
                        }
                    }
                }
                
                MudItem'' {
                    xs 2
                    sm 2
                    md 2
                    lg 2
                    xl 2
                    adapt {
                        let! second, setSecond = secondState.WithSetter()
                        MudStack'' {
                            MudButton'' {
                                OnClick (fun _ -> setSecond(second + 1))
                                "Increment Second"
                            }
                            MudButton'' {
                                OnClick (fun _ -> setSecond(second - 1))
                                "Decrement Second"
                            }
                        }
                    }
                }
                    
                MudItem'' {
                    xs 2
                    sm 2
                    md 2
                    lg 2
                    xl 2
                    adapt {
                        let! third, setThird = thridState.WithSetter()
                        MudStack'' {
                            MudButton'' {
                                OnClick (fun _ -> setThird(third + 1))
                                "Increment Third"
                            }
                            MudButton'' {
                                OnClick (fun _ -> setThird(third - 1))
                                "Decrement Third"
                            }
                        }
                    }
                }
                    
                MudItem'' {
                    xs 2
                    sm 2
                    md 2
                    lg 2
                    xl 2
                    adapt {
                        let! four, setFour = fourState.WithSetter()
                        MudStack'' {
                            MudButton'' {
                                OnClick (fun _ -> setFour(four + 1))
                                "Increment Four"
                            }
                            MudButton'' {
                                OnClick (fun _ -> setFour(four - 1))
                                "Decrement Four"
                            }
                        }
                    }
                }
                    
                MudItem'' {
                    xs 2
                    sm 2
                    md 2
                    lg 2
                    xl 2
                    adapt {
                        let! fith, setFith = fithState.WithSetter()
                        MudStack'' {
                            MudButton'' {
                                OnClick (fun _ -> setFith(fith + 1))
                                "Increment Fith"
                            }
                            MudButton'' {
                                OnClick (fun _ -> setFith(fith - 1))
                                "Decrement Fith"
                            }
                        }
                    }
                }
                    
                MudItem'' {
                    xs 2
                    sm 2
                    md 2
                    lg 2
                    xl 2
                    adapt {
                        let! sixth, setSixth = sixthState.WithSetter()
                        MudStack'' {
                            MudButton'' {
                                OnClick (fun _ -> setSixth(sixth + 1))
                                "Increment Sixth"
                            }
                            MudButton'' {
                                OnClick (fun _ -> setSixth(sixth - 1))
                                "Decrement Sixth"
                            }
                        }
                    }
                }
            }
        }

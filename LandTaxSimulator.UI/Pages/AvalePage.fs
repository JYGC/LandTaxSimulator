namespace LandTaxSimulator.UI.Pages

open Fun.Blazor
open FSharp.Data.Adaptive
open MudBlazor

module AvalePage =
    let routerView () =
        let firstCval = cval 0
        let secondCval = cval 0
        let thridCval = cval 0
        let fourCval = cval 0
        let fithCval = cval 0
        let sixthCval = cval 0
        let sumAval = aval {
            let! a = firstCval
            let! b = secondCval
            let! c = thridCval
            let! d = fourCval
            let! e = fithCval
            let! f = sixthCval
            return a + b + c + d + e + f
        }
        fragment {
            adapt {
                let! first = firstCval
                p { $"firstValue: {first}" }
            }
            adapt {
                let! second = secondCval
                p { $"secondValue: {second}" }
            }
            adapt {
                let! third = thridCval
                p { $"thirdValue: {third}" }
            }
            adapt {
                let! four = fourCval
                p { $"fourValue: {four}" }
            }
            adapt {
                let! fith = fithCval
                p { $"fithValue: {fith}" }
            }
            adapt {
                let! sixth = sixthCval
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
                        let! first, setFirst = firstCval.WithSetter()
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
                        let! second, setSecond = secondCval.WithSetter()
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
                        let! third, setThird = thridCval.WithSetter()
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
                        let! four, setFour = fourCval.WithSetter()
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
                        let! fith, setFith = fithCval.WithSetter()
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
                        let! sixth, setSixth = sixthCval.WithSetter()
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

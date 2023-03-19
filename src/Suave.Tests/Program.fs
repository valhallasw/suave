module Suave.Tests.Program

open System
open Suave
open Suave.Web
open Suave.Logging
open ExpectoExtensions

[<EntryPoint>]
let main args =

  let arch s = if s then "64-bit" else "32-bit"

  Console.WriteLine("OSVersion: {0}; running {1} process on {2} operating system."
    , Environment.OSVersion.ToString()
    , arch Environment.Is64BitProcess
    , arch Environment.Is64BitOperatingSystem)

  let testConfig =
    { defaultConfig with
        bindings = [ HttpBinding.createSimple HTTP "127.0.0.1" 9001 ]
        logger   = Targets.create Warn [| "Suave"; "Tests" |] }

  let mutable firstRun = 0
  let runDefaultEngine() =
    Console.WriteLine "Running tests with default TCP engine."
    firstRun <- defaultMainThisAssemblyWithParam testConfig args
    Console.WriteLine "Done."

  runDefaultEngine()
  firstRun


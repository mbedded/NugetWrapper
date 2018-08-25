# NugetWrapper

A NuGet-Wrapper for Jenkins. Validates if a packages is release before it is pushed.

## Intention

You have a NuGet-Plugin in Jenkins (a build-server). When you compile
your project you are able to auto-publish your NuGet-Packages.

It may be possible that you have multiple NuGet-Packages in your solution.
So this plugin will `push` already published packages.

*Sidenode: This is the case when you use a general filter like
`**\*.nupkg`*


## Installation

You can use two ways:

- Download the source and compile this project
- Download the ZIP of the current release

**Hint:** Currently it is a .Net Framework project. It will work on Windows only.


## Usage

- Copy this tool anywhere you like
- Open `NugetWrapper.exe.config`
- Adjust the value of `PathNuget` so it points to `NuGet.exe`
- Call the executable in the following way:
  `NuGetWrapper.exe push YourPackage.nupkg API_KEY -Source https://YourNugetServer.com -NonInteractive`


## Limitations

This tool was written within a short time to wrap only the call of the
NuGet-Plugin for Jenkins. So there is no errorhandling like "too few" arguments
or the nuget.exe could not be found.

Besides that limitations you get a console output so you may drill down
how an error occurs.

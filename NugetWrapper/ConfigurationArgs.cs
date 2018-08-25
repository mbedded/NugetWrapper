namespace NugetWrapper {
  class ConfigurationArgs {

    public string FurtherNugetArguments { get; set; }
    public string ApiKey { get; set; }
    public string NupkgPath { get; set; }
    public string NugetCommand { get; set; }
    public string ServerArgument { get; set; }
    public string CompleteCall { get; set; }
    public string[] Args { get; set; }

    // args from Jenkins NuGet-Plugin (example)
    // 0: push
    // 1: Project\bin\Debug\com.project.1.0.0.nupkg
    // 2: API_KEY
    // 3: -Source
    // 4: http://nugetserver.com/nuget
    // 5: -NonInteractive
    public static ConfigurationArgs Get(string[] args) {
      return new ConfigurationArgs() {
        Args = args,
        NugetCommand = args[0],
        NupkgPath = args[1],
        ApiKey = args[2],
        FurtherNugetArguments = string.Join(" ", new[] { args[3], args[4], args[5] }),
        ServerArgument = $"{args[3]} {args[4]}",
        CompleteCall = string.Join(" ", args)
      };
    }

  }

}

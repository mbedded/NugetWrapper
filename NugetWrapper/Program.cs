using System;

namespace NugetWrapper {
  class Program {


    static void Main(string[] args) {
      Console.WriteLine("NugetWrapper");
      Console.WriteLine($"CurrentDirectory: {System.IO.Directory.GetCurrentDirectory()}");
      Console.WriteLine("");

      ConfigurationArgs config = ConfigurationArgs.Get(args);
      PackageInfo info = PackageInfo.Get(config.NupkgPath);

      if (NugetHandler.IsPackageExisting(info, config)) {
        Console.WriteLine($"\tSkipping '{info.FullName}'. Package already existing.");
        return;
      }

      NugetHandler.PushPackage(config);
    }

  }
}

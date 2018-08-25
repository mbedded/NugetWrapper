using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace NugetWrapper {

  internal class NugetHandler {

    private static readonly string NUGET_EXE = ConfigurationManager.AppSettings["PathNuget"];

    public static bool IsPackageExisting(PackageInfo info, ConfigurationArgs config) {
      string nugetArgument = $"list {info.Name} {config.ServerArgument} -AllVersions -Prerelease";

      Console.WriteLine($"\tVerify if '{info.Name}' version '{info.Version}' is existing..");
      Console.WriteLine($"\texec '{NUGET_EXE} {nugetArgument}'");

      var proc = new Process {
        StartInfo = {
          FileName = NUGET_EXE,
          Arguments = nugetArgument,
          CreateNoWindow = true,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false
        }
      };

      proc.Start();

      string stdout = proc.StandardOutput.ReadToEnd();
      string stderr = proc.StandardError.ReadToEnd();

      Console.WriteLine($"\t[nuget.exe] {stdout}");

      if (string.IsNullOrEmpty(stderr) == false) {
        throw new Exception($"\t[ERROR nuget.exe] {stderr}");
      }

      proc.WaitForExit();

      List<string> packages = stdout.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

      return packages.Contains(info.FullName);
    }

    public static void PushPackage(ConfigurationArgs config) {
      Console.WriteLine($"\tPushing package to nuget");
      Console.WriteLine($"\texec '{NUGET_EXE} {config.NugetCommand} {config.NupkgPath} ********** {config.FurtherNugetArguments}'");

      var proc = new Process {
        StartInfo = {
          FileName = NUGET_EXE,
          Arguments = config.CompleteCall,
          CreateNoWindow = true,
          RedirectStandardOutput = true,
          RedirectStandardError = true,
          UseShellExecute = false
        }
      };

      proc.Start();

      string stdout = proc.StandardOutput.ReadToEnd();
      string stderr = proc.StandardError.ReadToEnd();

      Console.WriteLine($"\t[nuget.exe] {stdout}");

      if (string.IsNullOrEmpty(stderr) == false) {
        throw new Exception($"\t[ERROR nuget.exe] {stderr}");
      }

      proc.WaitForExit();
    }

  }

}
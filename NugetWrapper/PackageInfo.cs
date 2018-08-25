using System;
using System.IO;
using System.IO.Compression;
using System.Xml;


namespace NugetWrapper {

  class PackageInfo {

    public string Name { get; set; }
    public string Version { get; set; }

    public string FullName => $"{Name} {Version}";

    public static PackageInfo Get(string nupkgFilePath) {

      string unzipped = UnzipPackage(nupkgFilePath);
      PackageInfo info = ReadPackage(unzipped);

      Directory.Delete(unzipped, true);

      return info;
    }

    private static string UnzipPackage(string nupkgFilePath) {
      string unzipped = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));

      ZipFile.ExtractToDirectory(nupkgFilePath, unzipped);

      return unzipped;
    }

    private static PackageInfo ReadPackage(string unzippedFolder) {
      string[] files = Directory.GetFiles(unzippedFolder, "*.nuspec");
      string nuspec = files[0];

      XmlDocument doc = new XmlDocument();
      doc.Load(nuspec);

      string id = doc["package"]["metadata"]["id"].InnerText;
      string version = doc["package"]["metadata"]["version"].InnerText;

      return new PackageInfo() {
        Name = id,
        Version = version
      };
    }
  }

}

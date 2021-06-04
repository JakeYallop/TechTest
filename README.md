## Building the project

### Prerequisites
* Visual Studio 16.8+ (which includes msbuild 16.8+ and support for C# 9.0 features in the .Net SDK that is bundled with the installation).
* (Optional) Use Visual Studio 16.10+ to utilise the new blank line .editorconfig rules added in the 16.10 release.
* The metadata.csv and stats.csv files stored in a `./Assets` folder, relative to the working directory of the application. These are copied to the output directory when the project is built (e.g `bin\Debug\netcoreapp3.1`), and the app will look in `AppContext.BaseDirectory` + `/Assets` for these files by default.
  * The path to these can be changed as required via the `Assets:MetadataPath` and `Assets:StatsPath` configuration options.

### Running the project

* Make sure Movies.Api is set as the startup project.
* Run the project in Visual Studio.

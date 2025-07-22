# sELedit .NET 8 Port

A port of sELedit (Perfect World elements editor) from .NET Framework 4.7.2 to .NET 8 with cross-platform support.

## Overview

This is a migration of the original [sELedit](https://github.com/halysondev/sELedit) project to run on modern .NET 8. The migration enables the application to run on Linux and other platforms that support .NET 8.

Original video: https://youtu.be/yItqNC-_tAc

## Key Changes

- **Framework Update**: Migrated from .NET Framework 4.7.2 to .NET 8
- **Cross-Platform Support**: Added `EnableWindowsTargeting` for Linux compatibility
- **DevIL Replacement**: Replaced DevIL image library with built-in DDSReader
- **Dependency Updates**: 
  - Replaced `zlib.net` with `DotNetZip`
  - Created stub implementations for missing dependencies (`ColorProgressBar`, `tasks.dll`)
- **Code Cleanup**: Fixed compiler-generated code artifacts from decompiled sources
- **Project Structure**: Converted to SDK-style project format

## Building

### Prerequisites

- .NET 8 SDK or later
- Windows, Linux, or macOS

### Build Instructions

```bash
# Clone the repository
git clone https://github.com/crucifix86/sELedit-NET8.git
cd sELedit-NET8

# Build the solution
dotnet build

# Run the application
dotnet run --project sELedit/sELedit.csproj
```

## Known Issues

- Some NuGet packages show security vulnerability warnings (DotNetZip, SixLabors.ImageSharp)
- HelixToolkit.Wpf and RibbonWinForms are using .NET Framework compatibility mode
- Some features dependent on `tasks.dll` and `colorprogressbar.dll` are using stub implementations

## Original Project

This is based on the original sELedit project by halysondev: https://github.com/halysondev/sELedit

UM EDITOR DE ELEMENT.DATA DO PERFECT WORLD, COM MAIS RECURSOS QUE A VERSÃO GRÁTIS DO SOFTWARE

## License

This project maintains the same license as the original sELedit project.

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests.
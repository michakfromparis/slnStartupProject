# Sln StartUp Project

A command line utility to set the default StartUp Project of a [Visual Studio](http://msdn.microsoft.com/en-us/vstudio/) solution file


## Usage
``` bash
slnStartupProject slnFilename projectName
```

**slnFilename**: path to the Visual Studio solution file to modify

**projectName**: Name of the project to set as the default StartUp project

 slnStartupProject searches **slnFilename** for a project called **projectName** and moves its definition at the top of the file.


## Why?

![Visual Studio Set as StartUp Project](/www/screenshot.png?raw=true "Never do that again!")

When Visual Studio opens a sln file for the first time, it sets the very first project as the startup project and then stores it in the associated .suo file. This often make the first build fail and forces the developer to locate the project, right-click on it and select "Set as StartUp Project" for the build to succeed and launch. It is pretty annoying for maniacs like me. This becomes extremely relevant when you generate your solution using cmake as [cmake](http://www.cmake.org/) always sets the dummy project ALL_BUILD as the first project.

You can now integrate slnStartupProject in your build script and make sure the projects you generate will build and launch everytime a developer hits **F5**

Hope this helps you.

## Library

slnStartupProject can also be used as a referenced library in your project to manipulate the startup project of a .sln file directly from a .NET program.

``` cs
slnStartupProject.Parser.SetStartupProject(string slnFilename, string projectName);
```

I recommend installing the library through the Visual Studio package manager or the nuget command line utility.

## Compiling

The source is in the src directory and you will find a Visual Studio 2013 sln / csproj in the projects directory

I also provided an additional sln for Visual Studio 2008 ready to be upgraded to your needs

## Nuget

The binary and library distribution can be found on [nuget.org](http://nuget.org):

https://www.nuget.org/packages/slnStartupProject/

``` pm
PM> Install-Package slnStartupProject
```
https://www.nuget.org/packages/slnStartupProjectLibrary/

``` pm
PM> Install-Package slnStartupProjectLibrary
```

## License

slnStartupProject is licensed under the MIT License, see LICENSE for more information.

@echo off

cd vs2013\slnStartupProjectLibrary
nuget pack slnStartupProjectLibrary.csproj -Build -prop Platform=AnyCPU -prop Configuration=Release
nuget push slnStartupProjectLibrary.1.1.1.0.nupkg

cd ..\..

cd vs2013\slnStartupProject
nuget pack slnStartupProject.csproj -Build -prop Platform=AnyCPU -prop Configuration=Release
nuget push slnStartupProject.1.1.1.0.nupkg

cd ..\..

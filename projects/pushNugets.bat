@echo off

cd vs2013\slnStartupProjectLibrary
nuget pack -prop Platform=AnyCPU slnStartupProjectLibrary.csproj
nuget push slnStartupProjectLibrary.1.1.1.0.nupkg

cd ..\..

cd vs2013\slnStartupProject
nuget pack -prop Platform=AnyCPU slnStartupProject.csproj
nuget push slnStartupProject.1.1.1.0.nupkg

cd ..\..

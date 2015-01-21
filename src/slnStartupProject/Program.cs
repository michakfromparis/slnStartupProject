using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using slnStartupProjectLibrary;

namespace slnStartupProject
{
    class Program
    {
        static string   slnFilename;
        static string   projectName;

        static void Main(string[] args)
        {
            ParseArguments(args);
            Parser.SetStartupProject(slnFilename, projectName);
        }

        private static void ParseArguments(string[] args)
        {
            try
            {
                if (args.Length != 2)
                    throw new Exception("Invalid number of arguments");

                slnFilename = args[0];
                projectName = args[1];

                if (!File.Exists(slnFilename))
                    throw new Exception(string.Format("\"{0}\" does not exists", slnFilename));
            }
            catch (Exception ex)
            {
                Usage(ex.Message);
            }
        }

        private static void Usage(string message)
        {
            Console.WriteLine("");
            Console.WriteLine("Error: " + message);
            Console.WriteLine("");
            Console.WriteLine("Usage: slnStartupProject slnFilename projectName");
            Console.WriteLine("");
            Console.WriteLine("Searches slnFilename for a project called projectName and");
            Console.WriteLine("moves its definition at the top of the file so Visual Studio");
            Console.WriteLine("sets it as the Startup Project automatically when you open");
            Console.WriteLine("the solution file for the first time");
            Console.WriteLine();
            Environment.Exit(-1);
        }
    }
}

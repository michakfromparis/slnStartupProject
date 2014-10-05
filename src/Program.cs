using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace slnStartupProject
{
    class Program
    {
        static string   slnFilename;
        static string   projectName;

        static void Main(string[] args)
        {
            ParseArguments(args);
            ParseSln();
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

        private static void ParseSln()
        {
            string text             = null;
            int projectsStartOffset = -1;
            int projectsEndOffset   = -1;
            List<Project> projects  = new List<Project>();

            try
            {
                text = File.ReadAllText(slnFilename, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to read sln: " + ex.Message);
                Environment.Exit(-1);
            }
            try
            {
                Regex projectRegex = new Regex(@"(Project\(.*?EndProjectSection\s*EndProject)", RegexOptions.Singleline);
                MatchCollection projectMatches = projectRegex.Matches(text);
                if (projectMatches.Count == 0)
                    throw new Exception("Could not find any projects in the solution");
                foreach (Match projectMatch in projectMatches)
                    projects.Add(new Project(projectMatch.Value, projectMatch.Index));
                if (!projects.Any(p => p.ProjectName == projectName))
                    throw new Exception(string.Format("Could not find a project named \"{0}\" in \"{1}\"", projectName, slnFilename));
                int count = projects.Count(p => p.ProjectName == projectName);
                if (count != 1)
                    throw new Exception(string.Format("Project \"{0}\" found more than once. \"{1}\" is probably malformed", projectName, slnFilename));
                Project startupProject = projects.First(p => p.ProjectName == projectName);
                projectsStartOffset = projects[0].Offset;
                projectsEndOffset = projects[projects.Count - 1].Offset + projects[projects.Count - 1].CDATA.Length;
                projects.Remove(startupProject);
                projects.Insert(0, startupProject);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to parse sln: " + ex.Message);
                Environment.Exit(-1);
            }
            try
            {
                string newSln = text.Substring(0, projectsStartOffset);
                foreach (Project project in projects)
                    newSln += project.CDATA + Environment.NewLine;
                newSln += text.Substring(projectsEndOffset, text.Length - projectsEndOffset);
                File.WriteAllText(slnFilename, newSln, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Failed to write sln: " + ex.Message);
                Environment.Exit(-1);
            }
        }
    }
}

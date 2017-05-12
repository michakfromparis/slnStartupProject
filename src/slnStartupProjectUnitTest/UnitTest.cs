using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;
using NUnit.Framework;
using slnStartupProjectLibrary;

namespace slnStartupUnitTest
{
    [TestFixture]
    public class UnitTest
    {
        readonly Assembly assemby = Assembly.GetAssembly(typeof(UnitTest));
        private string testSolutionDirectory;

        /// <summary>
        /// SetUp
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            testSolutionDirectory = GetTestSolutionDirectory();
        }

        /// <summary>
        /// TearDown
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        /// <summary>
        /// Run Test when the solution has dependencies and doesn't have BOM.
        /// </summary>
        [Test]
        public void Run()
        {
            var baseSolution = Path.Combine(testSolutionDirectory, "ExampleProject.sln");
            var processedSolution = Path.Combine(testSolutionDirectory, "ExampleProjectProcessed.sln");
            var tempSolution = Path.Combine(testSolutionDirectory, "ExampleProjectTemp.sln");

            if (File.Exists(tempSolution))
            {
                File.Delete(tempSolution);
            }
            File.Copy(baseSolution, tempSolution);
            Parser.SetStartupProject(tempSolution, "ExampleProject");

            FileAssert.AreEqual(processedSolution, tempSolution);

            File.Delete(tempSolution);
        }


        /// <summary>
        /// Run Test when the solution has dependencies and has BOM.
        /// </summary>
        [Test]
        public void RunWithBOM()
        {
            var baseSolution = Path.Combine(testSolutionDirectory, "ExampleProjectBOM.sln");
            var processedSolution = Path.Combine(testSolutionDirectory, "ExampleProjectProcessedBOM.sln");
            var tempSolution = Path.Combine(testSolutionDirectory, "ExampleProjectTemp.sln");

            if (File.Exists(tempSolution))
            {
                File.Delete(tempSolution);
            }
            File.Copy(baseSolution, tempSolution);
            Parser.SetStartupProject(tempSolution, "ExampleProject");

            FileAssert.AreEqual(processedSolution, tempSolution);

            File.Delete(tempSolution);
        }

        /// <summary>
        /// Get the Directory of the Assemby
        /// </summary>
        /// <returns></returns>
        private string GetAssembyDirectory()
        {
            var assembyPath = this.assemby.Location;
            var assembyDirecctory = Path.GetDirectoryName(assembyPath);
            return assembyDirecctory;
        }

        /// <summary>
        /// Get the Directory of test solution
        /// </summary>
        /// <returns></returns>
        private string GetTestSolutionDirectory()
        {
            var testSolutionDirecotry = Path.GetFullPath(Path.Combine(GetAssembyDirectory(), @"..\..\..\..\..\tests\data\ExampleProject"));
            return testSolutionDirecotry;
        }
    }
}

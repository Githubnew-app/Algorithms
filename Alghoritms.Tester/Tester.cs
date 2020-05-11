using Alghoritms.Solutions.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Alghoritms.Tests
{
    /// <summary>
    /// How to use it:
    /// <list type="number">
    /// <item>Add new task with test cases as separate sub-folder into the root folder <c>Tasks</c></item>
    /// <item>Add new class inherited from the <see cref="ISolution"/> interface and with <see cref="SolutionDescriptionAttribute"/> attribute to the project <c>Alghoritms.Solutions</c>. The <c>Subfolder</c> property should be specified in accordance with the tasks sub-folder name.</item>
    /// <item>Implement a solution for the task</item>
    /// <item>Run Test and see results on Test Detail Summary tab</item>
    /// </list>
    /// The solution class can be marked as <c>Actual</c> via appropriate parameter in <see cref="SolutionDescriptionAttribute"/> attribute. If at least one solution is marked as Actual, only Actual solution(s) will be taken account during Test running. The parameter <c>Certain</c> can help to be focused on the single specified test case.
    /// <c>RootTestFolder</c> can be specified in <c>.runsettings</c> file.
    /// </summary>
    [TestClass]
    public class Tester
    {
        private static String rootTestFolder;

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            //Initialization root test folder via property RootTestFolder from .runsettings file
            rootTestFolder = context.Properties["RootTestFolder"]?.ToString();
            if (String.IsNullOrEmpty(rootTestFolder))
            {
                rootTestFolder = @"..\..\..\..\Tasks";
            }
            if (!Directory.Exists(rootTestFolder))
            {
                throw new Exception("Root of test folder is not specified or doesn't exist. Please specify a valid root for test folder via .runsettings file");
            }
        }

        /// <summary>
        /// Main method to check results of the solutions against expected results
        /// </summary>
        /// <param name="index">Number of the test case</param>
        /// <param name="folder">Sub-folder name</param>
        /// <param name="task">Implementation of the task</param>
        /// <param name="inputData">Input data form test case</param>
        /// <param name="expectedResult">Expected result</param>
        [DataTestMethod]
        [DynamicData(nameof(GetData), DynamicDataSourceType.Method, DynamicDataDisplayName = "DisplayName")]
        public void TotalCheck(int _0, String _1, ISolution task, String[] inputData, String[] expectedResult)
        {
            String[] actual = task.Run(inputData);
            CollectionAssert.AreEqual(expectedResult, actual, BuildErrorMessage(inputData, expectedResult, actual));
        }

        private static string BuildErrorMessage(string[] inputData, string[] expectedResult, string[] actual)
        {
            bool isMultiLine = expectedResult.Length > 1;
            return $"\nInput Data: {String.Join((isMultiLine ? '\n' : ';'), inputData)}\nExpected:   {(isMultiLine ? "\n" : String.Empty)}{String.Join(('\n'), expectedResult)}\nActual:     {(isMultiLine ? "\n" : String.Empty)}{String.Join('\n', actual)}\n";
        }

        /// <summary>
        /// Display name of each test
        /// </summary>
        /// <param name="methodInfo">Meta information about called method</param>
        /// <param name="data">Set of data to perform test</param>
        /// <returns></returns>
        public static string DisplayName(MethodInfo _, Object[] data)
        {
            const int limit = 20;
            var actualValue = (String[])data[3/*Actual value*/];
            String shortPresentationOfTestData = actualValue.Length > 0
                ? String.Join(';', actualValue)
                : String.Empty;
            shortPresentationOfTestData = $"{new String(shortPresentationOfTestData.Take(limit).ToArray())}{(shortPresentationOfTestData.Length >= limit ? "..." : String.Empty)}";

            var expectedValue = (String[])data[4/*Expected value*/];
            String shortPresentationOfExpectedResult = expectedValue.Length > 0 ? expectedValue[0] : String.Empty;
            shortPresentationOfExpectedResult = $"{new String(shortPresentationOfExpectedResult.Take(limit).ToArray())}{(shortPresentationOfExpectedResult.Length >= limit ? "..." : String.Empty)}";

            return $"{(String)data[1]}. Test #{data[0/*Task number*/]} ({shortPresentationOfTestData}/{shortPresentationOfExpectedResult})";
        }

        /// <summary>
        /// Method for dynamically generating a test data sets for solutions
        /// </summary>
        /// <returns>Test data</returns>
        public static IEnumerable<Object[]> GetData()
        {
            string baseFolderName = rootTestFolder;
            var solutions = typeof(ISolution).Assembly.GetExportedTypes()
                .Where(t => typeof(ISolution).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract
                    && t.GetCustomAttributes<SolutionDescriptionAttribute>().Any())
                .SelectMany(t => t.GetCustomAttributes<SolutionDescriptionAttribute>(), (t, a) => new { 
                    Task = Activator.CreateInstance(t) as ISolution, 
                    Attribute = a })
                .Where(x => x.Attribute != null)
                .OrderBy(x => x.Attribute.Subfolder)
                .ToArray();
            bool currentOnly = solutions.Any(t => t.Attribute.Actual);
            var solutionsForTest = solutions.Where(s => !currentOnly || s.Attribute.Actual);
            foreach (var test in solutionsForTest)
            {
                String path = Path.Combine(baseFolderName, test.Attribute.Subfolder);
                if (Directory.Exists(path))
                {
                    bool singleTest = test.Attribute.Certain >= 0;
                    int index = singleTest ? test.Attribute.Certain : 0;
                    do
                    {
                        String filePathIn = Path.Combine(path, $"test.{index}.in");
                        if (!File.Exists(filePathIn))
                            break;
                        String filePathOut = Path.Combine(path, $"test.{index}.out");
                        String[] inputData = File.ReadAllLines(filePathIn);
                        String[] expectedResult = File.Exists(filePathOut)
                            ? File.ReadAllLines(filePathOut)
                            : new String[] { "<No data>" };
                        yield return new object[] { index, test.Attribute.Subfolder, test.Task, inputData, expectedResult };
                        index++;
                    } while (!singleTest);
                }
                else
                {
                    throw new Exception($"Sub-folder '{test.Attribute.Subfolder}' was not fount");
                }
            }
        }
    }
}
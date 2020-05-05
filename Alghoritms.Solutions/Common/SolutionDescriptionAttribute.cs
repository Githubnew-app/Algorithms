using System;

namespace Alghoritms.Solutions.Common
{
    /// <summary>
    /// The solution attribute. Each solution should be marked by the attribute and be inherited from the <see cref="ISolution"/> interface. The attribute allows:
    /// <list type="bullet">
    /// <item>To specify <c>Subfolder</c> with test cases (Required)</item>
    /// <item>To mark a class (solution) as <c>Actual</c>. If at least one solution is marked as Actual, only Actual solution(s) will be check.
    /// </item>
    /// <item>To specify <c>Certain</c> number of test case to launch. (Optional)</item>
    /// </list>
    /// /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SolutionDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Name of <c>Subfolder</c> with test cases.
        /// Required parameter 
        /// </summary>
        public String Subfolder { get; private set; }

        /// <summary>
        /// A solution can be marked as <c>Actual</c>. If at least one solution is marked as Actual, only Actual solution(s) will be check.
        /// It can be helpful during working on the solution.
        /// Optional parameter
        /// </summary>
        public bool Actual { get; private set; }

        /// <summary>
        /// Certain test case to launch. Only the specified test will be executed, the rest will be ignored.
        /// It can be helpful during working on the solution.
        /// Optional parameter
        /// </summary>
        public int Certain { get; private set; }


        /// <summary>
        /// The solution attribute. Each solution should be marked by the attribute and be inherited from the <see cref="ISolution"/> interface.
        /// The <paramref name="subfolder"/> parameter specify <c>Subfolder</c> with test cases. (Required)
        /// A solution can be marked as <paramref name="actual"/>. If at least one solution is marked as Actual, only Actual solution(s) will be check.
        /// The <paramref name="certain"/> parameter helps to launch single test case from the set.
        
        /// </summary>
        public SolutionDescriptionAttribute(String subfolder, bool actual = false, int certain = -1)
        {
            Subfolder = subfolder;
            Actual = actual;
            Certain = certain;
        }
    }
}

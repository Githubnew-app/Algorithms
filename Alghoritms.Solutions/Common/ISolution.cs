using System;

namespace Alghoritms.Solutions.Common
{
    /// <summary>
    /// Common interface for task solutions. Each solution should be inherited from the interface and marked by the <see cref="SolutionDescriptionAttribute"/> attribute.
    /// </summary>
    public interface ISolution
    {
        /// <summary>
        /// Method to launch a solution
        /// </summary>
        /// <param name="input">Input data as array of strings</param>
        /// <returns>Output result as array of strings</returns>
        String[] Run(String[] input);
    }
}

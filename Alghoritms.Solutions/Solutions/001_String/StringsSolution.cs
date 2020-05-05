namespace Alghoritms.Solutions
{
    [Common.SolutionDescription("0.String")]
    public class StringsSolution : Common.ISolution
    {
        public string[] Run(string[] input) => new[] { LengthOfString(input[0]).ToString() };

        /// <summary>
        /// Solution for the example task 'Strings': To find the length of string.
        /// </summary>
        /// <param name="input">Some string</param>
        /// <returns>Length of the sting</returns>
        public int LengthOfString(string input) => input.Length;
    }
}

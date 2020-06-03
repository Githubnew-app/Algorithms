using Alghoritms.Solutions.Common;
using System;

namespace Alghoritms.Solutions.Solutions
{
    [SolutionDescription(@"7.DynamicArrays")]
    public class SingleArrayTest : CommonArrayTest, ISolution
    {
        public string[] Run(string[] input) => Run(new SingleArray<String>(), input);
    }
}

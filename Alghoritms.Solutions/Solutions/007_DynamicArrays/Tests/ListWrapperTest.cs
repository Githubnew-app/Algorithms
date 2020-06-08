using Alghoritms.Solutions.Common;
using System;

namespace Alghoritms.Solutions.Solutions.Tests
{
    [SolutionDescription(@"7.DynamicArrays")]
    public class ListWrapperTest : CommonArrayTest, ISolution
    {
        public string[] Run(string[] input) => Run(new ListWrapper<String>(), input);
    }
}

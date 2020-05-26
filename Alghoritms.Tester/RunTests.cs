using Alghoritms.Solutions.Solutions._007_DynamicArrays;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alghoritms.Tester
{
    public static class RunTests
    {
        public static void Main()
        {
            FactorArray<String> testedArray = new FactorArray<string>(3, 2f);
            for (int i = 0; i < 10; i++)
            {
                testedArray.Add(i.ToString());
            }
            testedArray.Insert(2, "New!");

            testedArray.Insert(0, "New2!");
            testedArray.Insert(testedArray.Count - 1, "New3!");
            testedArray.RemoveAt(5);
        }
    }
}

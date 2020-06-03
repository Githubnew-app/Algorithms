using System;

namespace Alghoritms.Solutions.Solutions
{

    public abstract class CommonArrayTest
    {
        public string[] Run(ISimpleList<String> array, string[] input)
        {
            var instructions = input;
            foreach (var instruction in instructions)
            {
                char command = instruction[0];
                String value = instruction.Substring(1);
                switch (command)
                {
                    case '+':
                        array.Add(value);
                        break;
                    case '-':
                        array.RemoveAt(Int32.Parse(value));
                        break;
                    case '~':
                        array.Clear();
                        break;
                    case '^':
                        var t = value.Split(' ');
                        int index = Int32.Parse(t[0]);
                        String item = t[1];
                        array.Insert(index, item);
                        break;
                    default:
                        break;
                }
            }
            int lenght = array.Count;
            var result = new String[lenght];
            for (int i = 0; i < lenght; i++)
            {
                result[i] = array[i];
            }
            return result;
        }
    }
}

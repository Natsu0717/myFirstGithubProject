using System;
using System.Collections;
using System.Text.RegularExpressions;

public class Example
{
    public static void Main()
    {
        string words = "letter alphabetical missing lack release " +
                       "penchant slack acryllic laundry cease";
        string pattern = @"\w+";
        MatchEvaluator evaluator = new MatchEvaluator(WordScrambler);
        Console.WriteLine("Original words:");
        Console.WriteLine(words);
        Console.WriteLine();
        Console.WriteLine("Scrambled words:");
        Console.WriteLine(Regex.Replace(words, pattern, evaluator));

        Console.ReadKey();
    }

    public static string WordScrambler(Match match)
    {
        int arraySize = match.Value.Length;
        // Define two arrays equal to the number of letters in the match.
        double[] keys = new double[arraySize];
        char[] letters = new char[arraySize];

        // Instantiate random number generator'
        Random rnd = new Random();

        for (int ctr = 0; ctr < match.Value.Length; ctr++)
        {
            // Populate the array of keys with random numbers.
            keys[ctr] = rnd.NextDouble();
            // Assign letter to array of letters.
            letters[ctr] = match.Value[ctr];
        }
        Array.Sort(keys, letters, 0, arraySize, Comparer.Default);
        return new String(letters);
    }
}

//文档地址：
//https://docs.microsoft.com/zh-cn/dotnet/api/system.text.regularexpressions.regex.replace?redirectedfrom=MSDN&view=netframework-4.7.2#System_Text_RegularExpressions_Regex_Replace_System_String_System_String_System_Text_RegularExpressions_MatchEvaluator_
//Regex.Replace(String, String, MatchEvaluator)方法可用于替换正则表达式匹配，如果以下条件为真：

//1.正则表达式替换模式不能轻松地指定替换字符串。--这个还没用例子

//2.替换字符串中匹配的字符串上进行某些处理结果。--上述例子

//3.来自有条件处理的替换字符串结果。----如下  按条件来替换出

//https://www.cnblogs.com/smhy8187/articles/1201288.html  ↓

//public static void Rex()
//{
//    string str = "<You're angle & evil>";
//    string pattern = "'|&|<|>";

//    Regex regex = new Regex(pattern);

//    //Program prog = new Program();
//    MatchEvaluator evaluator = new MatchEvaluator(Program.ConvertToXML);
//    Console.WriteLine(regex.Replace(str, evaluator));
//    Console.Read();
//}


//public static string ConvertToXML(Match m)
//{
//    switch (m.Value)
//    {
//        case "'":
//            return "&apos";
//        case "&":
//            return "&amp";
//        case "<":
//            return "&lt";
//        case ">":
//            return "&gt";
//        default:
//            return "";
//    }
//}

// The example displays output similar to the following:
//    Original words:
//    letter alphabetical missing lack release penchant slack acryllic laundry cease
//    
//    Scrambled words:
//    elrtte iaeabatlpchl igmnssi lcka aerslee hnpatnce ksacl lialcryc dylruna ecase
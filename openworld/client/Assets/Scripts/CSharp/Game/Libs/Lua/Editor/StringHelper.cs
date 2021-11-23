using System.Text.RegularExpressions;
using UnityEngine;


public class StringHelper
{

    public static string MatchSplit(string str, char leftSplit = '(', char rightSplit = ')')
    {
        string format = string.Format(@"(?is)(?<=\{0})(.*)(?=\{1}", leftSplit, rightSplit);
        Match match = Regex.Match(str, format);
        if (match == null) return string.Empty;
        return match.Value;
    }

}
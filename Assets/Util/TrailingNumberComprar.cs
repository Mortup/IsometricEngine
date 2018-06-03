using System;
using System.Collections.Generic;

public class TrailingNumberComparer : IComparer<string> {
    public int Compare(string s1, string s2) {
        Stack<Char> stack1 = new Stack<Char>();
        Stack<Char> stack2 = new Stack<Char>();

        for (int i = s1.Length - 1; i >= 0; i--) {
            if (!char.IsNumber(s1[i])) {
                break;
            }

            stack1.Push(s1[i]);
        }

        for (int i = s2.Length - 1; i >= 0; i--) {
            if (!char.IsNumber(s2[i])) {
                break;
            }

            stack2.Push(s2[i]);
        }

        if (stack1.Count > 0 && stack2.Count == 0)
            return -1;

        if (stack1.Count == 0 && stack2.Count > 0)
            return 1;

        string num1 = new string(stack1.ToArray());
        string num2 = new string(stack2.ToArray());

        if (IsNumeric(num1) && IsNumeric(num2)) {
            if (Convert.ToInt32(num1) > Convert.ToInt32(num2)) return 1;
            if (Convert.ToInt32(num1) < Convert.ToInt32(num2)) return -1;
            if (Convert.ToInt32(num1) == Convert.ToInt32(num2)) return 0;
        }

        return string.Compare(s1, s2, true);
    }

    public static bool IsNumeric(object value) {
        try {
            int i = Convert.ToInt32(value.ToString());
            return true;
        }
        catch (FormatException) {
            return false;
        }
    }
}
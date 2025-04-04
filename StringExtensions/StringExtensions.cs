// Copyright and trademark notices at the end of this file.

using System.Collections.Immutable;
using System.Globalization;
using System.Text;

using SharperHacks.CoreLibs.Constants;
using SharperHacks.CoreLibs.Constraints;
using SharperHacks.CoreLibs.Math.Interfaces;

namespace SharperHacks.CoreLibs.StringExtensions;

/// <summary>
/// A collection of useful string extensions.
/// </summary>
public static class StringExtensions
{

    /// <summary>
    /// Determines whether any of the elements in values exists in str.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    public static bool ContainsAny(this string str, params char[] values)
    {
        Verify.IsNotNull(str, nameof(str));

        foreach (var item in values)
        {
            if (str.Contains(item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Determines whether any of the elements in values exists in str.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    public static bool ContainsAny(this string str, params string[] values)
    {
        Verify.IsNotNull(str, nameof(str));

        foreach (var item in values)
        {
            if (str.Contains(item))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Change all '/' or '\' chacters as appropriate for the current OS..
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string CorrectOSPathSeparators(this string str) =>
        Path.DirectorySeparatorChar == '\\' ? str.Replace('/', '\\') : str.Replace('\\', '/');

    /// <summary>
    /// Determines whether a string is a member of strings.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strings"></param>
    /// <param name="comparer"></param>
    /// <returns>True if str in array, false otherwise.</returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    public static bool In(
        this string str,
        IEnumerable<string> strings,
        IEqualityComparer<string>? comparer = default)
    {
        Verify.IsNotNull(str);
        return strings.Contains(str, comparer);
    }

    /// <summary>
    /// Determines whether a string is a member of strings.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strings"></param>
    /// <returns>True if str in array, false otherwise.</returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    public static bool In(this string str, HashSet<string> strings)
    {
        Verify.IsNotNull(str);

        return strings.Contains(str);
    }

    /// <summary>
    /// Determines whether dictionary contains key, or a key or value that match
    /// or contain str.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="dictionary"></param>
    /// <param name="matchWholeKey"></param>
    /// <param name="matchWholeValue"></param>
    /// <returns></returns>
    public static bool InDictionary(
        this string str,
        IDictionary<string, string> dictionary,
        bool matchWholeKey = true,
        bool matchWholeValue = true)
    {
        Verify.IsNotNull(str);

        if (dictionary.ContainsKey(str)) return true;

        foreach (var kv in dictionary)
        {
            if (!matchWholeKey && kv.Key.Contains(str)) return true;
            if (kv.Value.Equals(str, StringComparison.Ordinal)) return true;
            if (!matchWholeValue && kv.Value.Contains(str)) return true;
        }

        return false;
    }

    /// <summary>
    /// Determines whether dictionary contains a key matching str.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static bool InKeys<Tv>(
        this string str,
        IDictionary<string, Tv> dictionary)
    {
        Verify.IsNotNullOrEmpty(str);

        return dictionary.ContainsKey(str);
    }

    /// <summary>
    /// Determines whether dictionary contains a value matching or containg str.
    /// </summary>
    /// <typeparam name="Tk"></typeparam>
    /// <param name="str"></param>
    /// <param name="dictionary"></param>
    /// <param name="matchWholeValue"></param>
    /// <returns></returns>
    public static bool InValues<Tk>(
        this string str,
        IDictionary<Tk, string> dictionary,
        bool matchWholeValue = true)
    {
        Verify.IsNotNull(str);

        foreach(var kv in dictionary)
        {
            if (kv.Value.Equals(str, StringComparison.Ordinal) ||
                (!matchWholeValue &&
                kv.Value.Contains(str)
                ))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Determine if string contains only decimal digits.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    public static bool IsAllDecimalDigits(this string str)
    {
        Verify.IsNotNull(str);

        return str.IsLimitedToSetOf(StandardSets.DecimalDigits);
    }

    /// <summary>
    /// Determine if string contains only hex digits (case insensitive).
    /// </summary>
    /// <param name="str">The string to test. Must not be null.</param>
    /// <param name="prefixRequired">
    /// Whether the prefix "0x" is required.
    /// </param>
    /// <returns>True if string contains only valid hex characters.</returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    /// <remarks>
    /// "0x" and "0X" by themselves are considered empty and therefore invalid.
    /// </remarks>
    public static bool IsAllHexDigits(
        this string str,
        bool prefixRequired = false)
    {
        Verify.IsNotNull(str);

        return str.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase)
            ? str[2..].IsLimitedToSetOf(StandardSets.HexDigits)
            : !prefixRequired && str.IsLimitedToSetOf(StandardSets.HexDigits);
    }

    /// <summary>
    /// Determine whether the string contains only the characters in the specified set.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="set"></param>
    /// <returns></returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    public static bool IsLimitedToSetOf(this string str, ImmutableHashSet<char> set)
    {
        Verify.IsNotNull(str);

        if (str == string.Empty)
        {
            return set.Count == 0; // Empty string matches empty set.
        }

        foreach (var ch in str)
        {
            if (!set.Contains(ch)) return false;
        }

        return true;
    }

    /// <summary>
    /// Determines whether the string contains only the characters in the specified interval.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public static bool IsLimitedToRange(this string str, IInterval<char> range)
    {
        Verify.IsNotNull(str);

        if (str.Equals(string.Empty, System.StringComparison.Ordinal))
        {
            return range.IsEmpty;
        }

        foreach(var ch in str)
        {
            if (!range.Contains(ch)) return false;
        }

        return true;
    }

    /// <summary>
    /// Determine whether str is not a member of strings.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strings"></param>
    /// <param name="comparer"></param>
    /// <returns>True if str not in array, false otherwise.</returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    public static bool NotIn(
        this string str,
        IEnumerable<string> strings,
        IEqualityComparer<string>? comparer = default) => !strings.Contains(str, comparer);

    /// <summary>
    /// Determine whether str is not a member of strings.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="strings"></param>
    /// <returns>True if str not in array, false otherwise.</returns>
    /// <exception cref="VerifyException">Thrown if str is null.</exception>
    public static bool NotIn(this string str, HashSet<string> strings) => !strings.Contains(str);

    /// <summary>
    /// Determines whether dictionary contains key, or a key or value that match
    /// or contain str.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="dictionary"></param>
    /// <param name="matchWholeKey"></param>
    /// <param name="matchWholeValue"></param>
    /// <returns></returns>
    public static bool NotInDictionary(
        this string str,
        IDictionary<string, string> dictionary,
        bool matchWholeKey = true,
        bool matchWholeValue = true) => !str.InDictionary(dictionary, matchWholeKey, matchWholeValue);

    /// <summary>
    /// Determines whether dictionary does not contain a key matching str.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    public static bool NotInKeys<Tv>(
        this string str,
        IDictionary<string, Tv> dictionary) => !str.InKeys(dictionary);

    /// <summary>
    /// Determines whether dictionary does not, contain a value matching or containg str.
    /// </summary>
    /// <typeparam name="Tk"></typeparam>
    /// <param name="str"></param>
    /// <param name="dictionary"></param>
    /// <param name="matchWholeValue"></param>
    /// <returns></returns>
    public static bool NotInValues<Tk>(
        this string str,
        IDictionary<Tk, string> dictionary,
        bool matchWholeValue = true) => !str.InValues(dictionary, matchWholeValue);

    /// <summary>
    /// Convert a string representation of an encoding to the appropriate Encoding type.
    /// </summary>
    /// <param name="str">
    /// One of "ASCII", "UTF8", "UTF32" or "UNICODE".
    /// </param>
    /// <returns>The appropriate Encoding instance.</returns>
    /// <exception cref="ArgumentException">If string is not a recognized encoding type.</exception>
    /// <remarks>UTF7 is obsolete and therefore not supported.</remarks>
    public static Encoding ToEncoding(this string str)
    {
        return str.ToUpper(CultureInfo.InvariantCulture) switch
        {
            "ASCII" => Encoding.ASCII,
            "UTF8" => Encoding.UTF8,
            "UTF32" => Encoding.UTF32,
            "UNICODE" => Encoding.Unicode,
            _ => throw new ArgumentException("Argument not supported", nameof(str)),
        };
    }
}

// Copyright Joseph W Donahue and Sharper Hacks LLC (US-WA)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// SharperHacks is a trademark of Sharper Hacks LLC (US-Wa), and may not be
// applied to distributions of derivative works, without the express written
// permission of a registered officer of Sharper Hacks LLC (US-WA).

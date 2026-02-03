![SharperHacks logo](https://raw.githubusercontent.com/SharperHacks-org/Assets/main/Images/SHLLC-Logo.png)
# StringExtensions Library for .NET
## SharperHacks.CoreLibs.StringExtensions

Some useful string extensions.

Licensed under the Apache License, Version 2.0. See [LICENSE](LICENSE).

Contact: joseph@sharperhacks.org

Nuget: https://www.nuget.org/packages/SharperHacks.CoreLibs.Constraints

### Targets
- net8.0
- net9.0
- net10.0

### Classes

#### StringExtensions
Static class implementing static string extension methods:

``` 
/// Determines whether any of the elements in values exists in str (no LINQ).
public static bool ContainsAny(this string str, params char[] values) ... 
public static bool ContainsAny(this string str, ImmutableHashSet<char> values) ...
public static bool ContainsAny(this string str, params string[] values) ...

/// Determines where any of the elements str, are whitespace.
public static bool HasWhiteSpace(this string str) ...

/// Determines whether a string is a member of strings.
public static bool In(
    this string str, 
    IEnumerable<string> strings,
    IEqualityComparer<string>? comparer = default) ...
public static bool In(this string str, HashSet<string> strings) ...

/// Determines whether dictionary contains key and/or a key that match
public static bool InDictionary(
    this string str,
    IDictionary<string, string> dictionary,
    bool matchWholeKey = true,
    bool matchWholeValue = true) ...

public static bool InKeys<Tv>(
    this string str,
    IDictionary<string, Tv> dictionary) ...

public static bool InValues<Tk>(
    this string str,
    IDictionary<Tk, string> dictionary,
    bool matchWholeValue = true) ...

/// Determine if string contains only decimal digits.
public static bool IsAllDecimalDigits(this string str) ...

/// Determine if string contains only hex digits (case insensitive).
public static bool IsAllHexDigits(this string str, bool prefixRequired = false) ...

/// Determine whether the string contains only the characters in the specified set.
public static bool IsLimitedToSetOf(this string str, ImmutableHashSet<char> set) ...

/// Determines whether the string contains only the characters in the specified interval.
public static bool IsLimitedToRange(this string str, IInterval<char> range) ...

/// Determines whether a string can be used as a file name.
public static bool IsValidFileName(this string str) ...

/// Determines whether a string can be used as a directory name.
public static bool IsValidDirectoryName(this string str) ...

/// Determines whether the string is all white space.
public static bool IsWhiteSpace(this string str) ...

/// Determine whether str is not a member of strings.
public static bool NotIn(
        this string str,
        IEnumerable<string> strings,
        IEqualityComparer<string>? comparer = default) ...
public static bool NotIn(this string str, HashSet<string> strings) ...

/// Determines whether dictionary does not contain key and/or a key that match
public static bool NotInDictionary(
    this string str,
    IDictionary<string, string> dictionary,
    bool matchWholeKey = true,
    bool matchWholeValue = true) ...

public static bool NotInKeys<Tv>(
    this string str,
    IDictionary<string, Tv> dictionary) ...

public static bool NotInValues<Tk>(
    this string str,
    IDictionary<Tk, string> dictionary,
    bool matchWholeValue = true) ...

/// Convert a string representation of an encoding to the appropriate Encoding type.
public static Encoding ToEncoding(this string str) ...

```

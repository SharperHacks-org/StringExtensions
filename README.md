![SharperHacks logo](SHLLC-Logo.jpg)
# StringExtensions Library for .NET
## SharperHacks.CoreLibs.StringExtensions

Defines classes and methods to verify runtime contraints are met.

Licensed under the Apache License, Version 2.0. See [LICENSE](LICENSE).

Contact: joseph@sharperhacks.org

Nuget: https://www.nuget.org/packages/SharperHacks.CoreLibs.Constraints

### Targets
- net7.0
- net8.0

### Classes

#### StringExtensions
Static class implementing static string extension methods:

``` 
/// Determines whether any of the elements in values exists in str.
public static bool ContainsAny(this string str, params char[] values) ... 
public static bool ContainsAny(this string str, params string[] values) ...

/// Determine if string contains only decimal digits.
public static bool IsAllDecimalDigits(this string str) ...

/// Determine if string contains only hex digits (case insensitive).
public static bool IsAllHexDigits(this string str, bool prefixRequired = false) ...

/// Determine whether the string contains only the characters in the specified set.
public static bool IsLimitedToSetOf(this string str, ImmutableHashSet<char> set) ...

/// Determines whether the string contains only the characters in the specified interval.
public static bool IsLimitedToRange(this string str, IInterval<char> range) ...

/// Convert a string representation of an encoding to the appropriate Encoding type.
public static Encoding ToEncoding(this string str) ...

```

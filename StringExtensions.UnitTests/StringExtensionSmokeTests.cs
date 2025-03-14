// Copyright and trademark notices at the end of this file.

using SharperHacks.CoreLibs.Constants;
using SharperHacks.CoreLibs.Math;

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SharperHacks.CoreLibs.StringExtensions.UnitTests;

[ExcludeFromCodeCoverage]
[TestClass]
public class StringExtensionSmokeTests
{
    private string _subsetOfDecimalDigits = "12348765";

    [TestMethod]
    public void ContainsAnyChars()
    {
        Assert.IsTrue(_subsetOfDecimalDigits.ContainsAny('9', '7', '5'));
        Assert.IsFalse(_subsetOfDecimalDigits.ContainsAny('a', 'b', 'z'));
    }

    [TestMethod]
    public void ContainsAnyStrings()
    {
        var str1 = "Mary had a little lamb, its flees as white as snow.";
        Assert.IsTrue(str1.ContainsAny("Mary", "little", "flees"));
        Assert.IsFalse(str1.ContainsAny("doubl", "ditto", "duo"));
        Assert.IsFalse(str1.IsAllDecimalDigits());
    }

    [TestMethod]
    public void InEnumerable()
    {
        var array = new[] { "One", "two", "three", string.Empty };

        Assert.IsTrue(array[0].In(array));
        Assert.IsTrue("One".In(array));
        Assert.IsTrue("two".In(array));
        Assert.IsTrue("three".In(array));
        Assert.IsTrue(string.Empty.In(array));
        Assert.IsFalse("four".In(array));
    }

    [TestMethod]
    public void InHashSet()
    {
        var set = new HashSet<string>() { "One", "two", "three", string.Empty };

        Assert.IsTrue("One".In(set));
        Assert.IsTrue("two".In(set));
        Assert.IsTrue("three".In(set));
        Assert.IsTrue(string.Empty.In(set));
        Assert.IsFalse("four".In(set));
    }

    [TestMethod]
    public void InDictionary()
    {
        var data = new Dictionary<string, string>()
        {
            { "One", "~One" },
            { "Fried", "Fish" },
            { "FlipFlop", "cheap sandles" },
            { "Nothing", string.Empty },
        };

        Assert.IsTrue("One".InDictionary(data));
        Assert.IsTrue("Fried".InDictionary(data));
        Assert.IsTrue("Fish".InDictionary(data));
        Assert.IsTrue("Flop".InDictionary(data, false, false));
        Assert.IsTrue("sandles".InDictionary(data, false, false));
        Assert.IsTrue("Nothing".InDictionary(data, false, false));
        Assert.IsFalse("Everything".InDictionary(data, false, false));
    }

    [TestMethod]
    public void InKeys()
    {
        var dictionary = new Dictionary<string, string>()
        {
            { "One", "~One" },
            { "Two", "~Two"},
            { "Nothing", string.Empty },
        };

        Assert.IsTrue("One".InKeys(dictionary));
        Assert.IsTrue("Two".InKeys(dictionary));
        Assert.IsTrue("Nothing".InKeys(dictionary));
        Assert.IsFalse("Everything".InKeys(dictionary));
    }

    [TestMethod]
    public void InValues()
    {
        var dictionary = new Dictionary<string, string>()
        {
            { "One", "~One" },
            { "Two", "~Two"},
            { "Nothing", string.Empty },
        };

        Assert.IsTrue("~One".InValues(dictionary));
        Assert.IsTrue("~Two".InValues(dictionary));
        Assert.IsTrue(string.Empty.InValues(dictionary));
        Assert.IsFalse("Everything".InValues(dictionary));

        Assert.IsTrue("One".InValues(dictionary, false));
    }

    [TestMethod]
    public void IsAllDecimalDgitis()
    {
        Assert.IsTrue(_subsetOfDecimalDigits.IsAllDecimalDigits());
    }

    [TestMethod]
    public void IsLimitedToSetOf()
    {
        Assert.IsTrue(_subsetOfDecimalDigits.IsLimitedToSetOf(Constants.StandardSets.DecimalDigits));
        Assert.IsFalse(string.Empty.IsLimitedToSetOf(StandardSets.UpperAlphaCharacters));
    }

    [TestMethod]
    public void IsLimitedToRange()
    {
        Assert.IsTrue(_subsetOfDecimalDigits.IsLimitedToRange(new Interval<char>("[0,9]")));
        Assert.IsTrue(string.Empty.IsLimitedToRange(new Interval<char>("(1,1)")));
        Assert.IsFalse(_subsetOfDecimalDigits.IsLimitedToRange(new Interval<char>("(a, z)")));
    }

    [TestMethod]
    public void NotInEnumerable()
    {
        var array = new[] { "One", "two", "three" };

        Assert.IsTrue("four".NotIn(array));
        Assert.IsTrue(string.Empty.NotIn(array));
        Assert.IsFalse("One".NotIn(array));
        Assert.IsFalse("two".NotIn(array));
        Assert.IsFalse("three".NotIn(array));
    }

    [TestMethod]
    public void NotInHashset()
    {
        var set = new HashSet<string>() { "One", "two", "three" };

        Assert.IsTrue("four".NotIn(set));
        Assert.IsTrue(string.Empty.NotIn(set));
        Assert.IsFalse("One".NotIn(set));
        Assert.IsFalse("two".NotIn(set));
        Assert.IsFalse("three".NotIn(set));
    }

    [TestMethod]
    public void NotInDictionary()
    {
        var data = new Dictionary<string, string>()
        {
            { "One", "~One" },
            { "Fried", "Fish" },
            { "FlipFlop", "cheap sandles" },
            { "Nothing", string.Empty },
        };

        Assert.IsFalse("One".NotInDictionary(data));
        Assert.IsFalse("Fried".NotInDictionary(data));
        Assert.IsFalse("Fish".NotInDictionary(data));
        Assert.IsFalse("Flop".NotInDictionary(data, false, false));
        Assert.IsFalse("sandles".NotInDictionary(data, false, false));
        Assert.IsFalse("Nothing".NotInDictionary(data, false, false));
        Assert.IsTrue("Everything".NotInDictionary(data, false, false));
    }

    [TestMethod]
    public void NotInKeys()
    {
        var dictionary = new Dictionary<string, string>()
        {
            { "One", "~One" },
            { "Two", "~Two"},
            { "Nothing", string.Empty },
        };

        Assert.IsFalse("One".NotInKeys(dictionary));
        Assert.IsFalse("Two".NotInKeys(dictionary));
        Assert.IsFalse("Nothing".NotInKeys(dictionary));
        Assert.IsTrue("Everything".NotInKeys(dictionary));
    }

    [TestMethod]
    public void NotInValues()
    {
        var dictionary = new Dictionary<string, string>()
        {
            { "One", "~One" },
            { "Two", "~Two"},
            { "Nothing", string.Empty },
        };

        Assert.IsFalse("~One".NotInValues(dictionary));
        Assert.IsFalse("~Two".NotInValues(dictionary));
        Assert.IsFalse(string.Empty.NotInValues(dictionary));
        Assert.IsTrue("Everything".NotInValues(dictionary));

        Assert.IsFalse("One".NotInValues(dictionary, false));
    }

    [TestMethod]
    public void ToEncoding()
    {
        // Note that UTF7 is no longer supported.
        var ascii = "ASCII";
        var utf8 = "UTF8";
        var utf32 = "utf32"; // Conversion from string is case insensitive.
        var unicode = "uniCode";

        Assert.AreEqual(Encoding.ASCII, ascii.ToEncoding());
        Assert.AreEqual(Encoding.UTF8, utf8.ToEncoding());
        Assert.AreEqual(Encoding.UTF32, utf32.ToEncoding());
        Assert.AreEqual(Encoding.Unicode, unicode.ToEncoding());

        try
        {
            var notSuppported = "not supported";
            _ = notSuppported.ToEncoding();
            Assert.Fail("Failed to throw ArgumentException.");
        }
        catch {}
    }

    [TestMethod]
    public void IsAllHexDigits()
    {
        var allHexDigits = "0xA1";
        var bareHexDigits = "E3";
        var barePrefix = "0x";

        Assert.IsTrue(allHexDigits.IsAllHexDigits(true));
        Assert.IsTrue(allHexDigits.IsAllHexDigits(false));
        Assert.IsTrue(bareHexDigits.IsAllHexDigits(false));
        Assert.IsFalse(bareHexDigits.IsAllHexDigits(true));
        Assert.IsTrue(_subsetOfDecimalDigits.IsAllHexDigits());
        Assert.IsFalse("zyx".IsAllHexDigits());
        Assert.IsFalse(barePrefix.IsAllHexDigits());
        Assert.IsFalse(string.Empty.IsAllHexDigits());
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

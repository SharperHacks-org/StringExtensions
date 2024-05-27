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

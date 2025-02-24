using NUnit.Framework;
using System.Text.RegularExpressions;

[TestFixture]
public class TestInputValidation
{

	[Test]
	public void TestForSQLInjection()
	{
		string maliciousInput = "'; DROP TABLE Users; --";
		string pattern = @"('|\-\-|;|/\*|\*/|xp_)";
		bool isSafe = !Regex.IsMatch(maliciousInput, pattern, RegexOptions.IgnoreCase);
		Assert.IsFalse(isSafe, "SQL Injection input was not properly detected.");
	}

	[Test]
	public void TestForXSS()
	{
		string maliciousInput = "<script>alert('XSS');</script>";
		string pattern = @"(<script.*?>.*?</script>|on\w+\s*=)";
		bool isSafe = !Regex.IsMatch(maliciousInput, pattern, RegexOptions.IgnoreCase);
		Assert.IsFalse(isSafe, "XSS input was not properly detected.");
	}
}

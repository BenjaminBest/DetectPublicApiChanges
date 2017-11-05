using DetectPublicApiChanges.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DetectPublicApiChanges.Tests.Common
{
	[TestClass]
	public class DateParserTests
	{
		[TestMethod]
		public void DateParser_TryParseDate_ShouldParse_Format_d()
		{
			var testDate = new DateTime(2017, 02, 01, 0, 0, 0);

			var testString = $"{testDate:d}";
			DateTime testResult;
			testString.TryParseDate(DateParser.DateTimeFormat.USA_DATE, out testResult);

			testResult.Date.Year.Should().Be(2017);
			testResult.Date.Month.Should().Be(02);
			testResult.Date.Day.Should().Be(01);
		}

		[TestMethod]
		public void DateParser_TryParseDate_ShouldParse_Format_u()
		{
			var testDate = new DateTime(2017, 02, 01, 0, 0, 0);

			var testString = $"{testDate:u}";
			DateTime testResult;
			testString.TryParseDate(DateParser.DateTimeFormat.UK_DATE, out testResult);

			testResult.Date.Year.Should().Be(2017);
			testResult.Date.Month.Should().Be(02);
			testResult.Date.Day.Should().Be(01);
		}

		[TestMethod]
		public void DateParser_TryParseDate_ShouldParse_Format_EnglishDate()
		{
			const string testString = "2017-02-01";
			DateTime testResult;
			testString.TryParseDate(DateParser.DateTimeFormat.UK_DATE, out testResult);

			testResult.Date.Year.Should().Be(2017);
			testResult.Date.Month.Should().Be(02);
			testResult.Date.Day.Should().Be(01);
		}

		[TestMethod]
		public void DateParser_TryParseDate_ShouldParse_Format_GermanDate()
		{
			const string testString = "01.02.2017";
			DateTime testResult;
			testString.TryParseDate(DateParser.DateTimeFormat.UK_DATE, out testResult);

			testResult.Date.Year.Should().Be(2017);
			testResult.Date.Month.Should().Be(02);
			testResult.Date.Day.Should().Be(01);
		}

		[TestMethod]
		public void DateParser_TryParseDate_ShouldParse_DateWithTextBefore()
		{
			const string testString = "This is a date 01.02.2017";
			DateTime testResult;
			testString.TryParseDate(DateParser.DateTimeFormat.UK_DATE, out testResult);

			testResult.Date.Year.Should().Be(2017);
			testResult.Date.Month.Should().Be(02);
			testResult.Date.Day.Should().Be(01);
		}

		[TestMethod]
		public void DateParser_TryParseDate_ShouldParse_DateWithTextAfter()
		{
			const string testString = "01.02.2017 This is a date";
			DateTime testResult;
			testString.TryParseDate(DateParser.DateTimeFormat.UK_DATE, out testResult);

			testResult.Date.Year.Should().Be(2017);
			testResult.Date.Month.Should().Be(02);
			testResult.Date.Day.Should().Be(01);
		}

		[TestMethod]
		public void DateParser_TryParseDate_ShouldParse_DateWithTextSurounding()
		{
			const string testString = "This is a date 01.02.2017 This is a date";
			DateTime testResult;
			testString.TryParseDate(DateParser.DateTimeFormat.UK_DATE, out testResult);

			testResult.Date.Year.Should().Be(2017);
			testResult.Date.Month.Should().Be(02);
			testResult.Date.Day.Should().Be(01);
		}

		[TestMethod]
		public void DateParser_TryParseDate_ShouldParse_Without_Date()
		{
			const string testString = "This is a date";
			DateTime testResult;
			testString.TryParseDate(DateParser.DateTimeFormat.UK_DATE, out testResult);

			testResult.Should().Be(DateTime.MinValue);
		}
	}
}

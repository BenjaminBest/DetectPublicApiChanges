using System;
using System.Text.RegularExpressions;

namespace DetectPublicApiChanges.Common
{
	/// <summary>
	/// Miscellaneous and parsing methods for DateTime
	/// </summary>
	public static class DateParser
	{
		/// <summary>
		/// Defines a substring where date-time was found and result of conversion
		/// </summary>
		public class ParsedDateTime
		{
			/// <summary>
			/// Index of first char of a date substring found in the string
			/// </summary>
			public readonly int IndexOfDate = -1;
			/// <summary>
			/// Length a date substring found in the string
			/// </summary>
			public readonly int LengthOfDate = -1;
			/// <summary>
			/// Index of first char of a time substring found in the string
			/// </summary>
			public readonly int IndexOfTime = -1;
			/// <summary>
			/// Length of a time substring found in the string
			/// </summary>
			public readonly int LengthOfTime = -1;
			/// <summary>
			/// DateTime found in the string
			/// </summary>
			public readonly DateTime DateTime;
			/// <summary>
			/// True if a date was found within the string
			/// </summary>
			public readonly bool IsDateFound;
			/// <summary>
			/// True if a time was found within the string
			/// </summary>
			public readonly bool IsTimeFound;
			/// <summary>
			/// UTC offset if it was found within the string
			/// </summary>
			public readonly TimeSpan UtcOffset;
			/// <summary>
			/// True if UTC offset was found in the string
			/// </summary>
			public readonly bool IsUtcOffsetFound;
			/// <summary>
			/// Utc gotten from DateTime if IsUtcOffsetFound is True
			/// </summary>
			public DateTime UtcDateTime;

			internal ParsedDateTime(int index_of_date, int length_of_date, int index_of_time, int length_of_time, DateTime date_time)
			{
				IndexOfDate = index_of_date;
				LengthOfDate = length_of_date;
				IndexOfTime = index_of_time;
				LengthOfTime = length_of_time;
				DateTime = date_time;
				IsDateFound = index_of_date > -1;
				IsTimeFound = index_of_time > -1;
				UtcOffset = new TimeSpan(25, 0, 0);
				IsUtcOffsetFound = false;
				UtcDateTime = new DateTime(1, 1, 1);
			}

			internal ParsedDateTime(int index_of_date, int length_of_date, int index_of_time, int length_of_time, DateTime date_time, TimeSpan utc_offset)
			{
				IndexOfDate = index_of_date;
				LengthOfDate = length_of_date;
				IndexOfTime = index_of_time;
				LengthOfTime = length_of_time;
				DateTime = date_time;
				IsDateFound = index_of_date > -1;
				IsTimeFound = index_of_time > -1;
				UtcOffset = utc_offset;
				IsUtcOffsetFound = Math.Abs(utc_offset.TotalHours) < 12;
				if (!IsUtcOffsetFound)
					UtcDateTime = new DateTime(1, 1, 1);
				else
				{
					if (index_of_date < 0)//to avoid negative date exception when date is undefined
					{
						TimeSpan ts = date_time.TimeOfDay + utc_offset;
						if (ts < new TimeSpan(0))
							UtcDateTime = new DateTime(1, 1, 2) + ts;
						else
							UtcDateTime = new DateTime(1, 1, 1) + ts;
					}
					else
						UtcDateTime = date_time + utc_offset;
				}
			}
		}

		/// <summary>
		/// Date that is accepted in the following cases:
		/// - no date was parsed by TryParseDateOrTime();
		/// - no year was found by TryParseDate();
		/// It is ignored if DefaultDateIsNow = true was set after DefaultDate 
		/// </summary>
		public static DateTime DefaultDate
		{
			set
			{
				_defaultDate = value;
				DefaultDateIsNow = false;
			}
			get
			{
				if (DefaultDateIsNow)
					return DateTime.Now;
				else
					return _defaultDate;
			}
		}
		static DateTime _defaultDate = DateTime.Now;

		/// <summary>
		/// If true then DefaultDate property is ignored and DefaultDate is always DateTime.Now
		/// </summary>
		public static bool DefaultDateIsNow = true;

		/// <summary>
		/// Defines default date-time format.
		/// </summary>
		public enum DateTimeFormat
		{
			/// <summary>
			/// month number goes before day number
			/// </summary>
			USA_DATE,
			/// <summary>
			/// day number goes before month number
			/// </summary>
			UK_DATE,
			///// <summary>
			///// time is specifed through AM or PM
			///// </summary>
			//USA_TIME,
		}

		/// <summary>
		/// Tries to find date within the passed string and return it as DateTime structure.
		/// It recognizes only date while ignoring time, so time in the returned DateTime is always 0:0:0.
		/// If year of the date was not found then it accepts the current year.
		/// </summary>
		/// <param name="str">string that contains date</param>
		/// <param name="defaultformat">format to be used preferably in ambivalent instances</param>
		/// <param name="date">parsed date output</param>
		/// <returns>
		/// true if date was found, else false
		/// </returns>
		public static bool TryParseDate(this string str, DateTimeFormat defaultformat, out DateTime date)
		{
			ParsedDateTime parseddate;
			if (!TryParseDate(str, defaultformat, out parseddate))
			{
				date = new DateTime(1, 1, 1);
				return false;
			}
			date = parseddate.DateTime;
			return true;
		}

		/// <summary>
		/// Tries to find date within the passed string and return it as ParsedDateTime object.
		/// It recognizes only date while ignoring time, so time in the returned ParsedDateTime is always 0:0:0.
		/// If year of the date was not found then it accepts the current year.
		/// </summary>
		/// <param name="str">string that contains date</param>
		/// <param name="defaultformat">format to be used preferably in ambivalent instances</param>
		/// <param name="parseddate">parsed date output</param>
		/// <returns>
		/// true if date was found, else false
		/// </returns>
		public static bool TryParseDate(this string str, DateTimeFormat defaultformat, out ParsedDateTime parseddate)
		{
			parseddate = null;

			if (string.IsNullOrEmpty(str))
				return false;

			//look for dd/mm/yy
			var m = Regex.Match(str, @"(?<=^|[^\d])(?'day'\d{1,2})\s*(?'separator'[\\/\.])+\s*(?'month'\d{1,2})\s*\'separator'+\s*(?'year'\d{2}|\d{4})(?=$|[^\d])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			if (m.Success)
			{
				DateTime date;
				if ((defaultformat ^ DateTimeFormat.USA_DATE) == DateTimeFormat.USA_DATE)
				{
					if (!ConvertToDate(int.Parse(m.Groups["year"].Value), int.Parse(m.Groups["day"].Value), int.Parse(m.Groups["month"].Value), out date))
						return false;
				}
				else
				{
					if (!ConvertToDate(int.Parse(m.Groups["year"].Value), int.Parse(m.Groups["month"].Value), int.Parse(m.Groups["day"].Value), out date))
						return false;
				}
				parseddate = new ParsedDateTime(m.Index, m.Length, -1, -1, date);
				return true;
			}

			//look for [yy]yy-mm-dd
			m = Regex.Match(str, @"(?<=^|[^\d])(?'year'\d{2}|\d{4})\s*(?'separator'[\-])\s*(?'month'\d{1,2})\s*\'separator'+\s*(?'day'\d{1,2})(?=$|[^\d])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			if (m.Success)
			{
				DateTime date;
				if (!ConvertToDate(int.Parse(m.Groups["year"].Value), int.Parse(m.Groups["month"].Value), int.Parse(m.Groups["day"].Value), out date))
					return false;
				parseddate = new ParsedDateTime(m.Index, m.Length, -1, -1, date);
				return true;
			}

			//look for month dd yyyy
			m = Regex.Match(str, @"(?:^|[^\d\w])(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*\s+(?'day'\d{1,2})(?:-?st|-?th|-?rd|-?nd)?\s*,?\s*(?'year'\d{4})(?=$|[^\d\w])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			if (!m.Success)
				//look for dd month [yy]yy
				m = Regex.Match(str, @"(?:^|[^\d\w:])(?'day'\d{1,2})(?:-?st\s+|-?th\s+|-?rd\s+|-?nd\s+|-|\s+)(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*(?:\s*,?\s*|-)'?(?'year'\d{2}|\d{4})(?=$|[^\d\w])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			if (!m.Success)
				//look for yyyy month dd
				m = Regex.Match(str, @"(?:^|[^\d\w])(?'year'\d{4})\s+(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*\s+(?'day'\d{1,2})(?:-?st|-?th|-?rd|-?nd)?(?=$|[^\d\w])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			if (!m.Success)
				//look for month dd hh:mm:ss MDT|UTC yyyy
				m = Regex.Match(str, @"(?:^|[^\d\w])(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*\s+(?'day'\d{1,2})\s+\d{2}\:\d{2}\:\d{2}\s+(?:MDT|UTC)\s+(?'year'\d{4})(?=$|[^\d\w])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			if (!m.Success)
				//look for  month dd [yyyy]
				m = Regex.Match(str, @"(?:^|[^\d\w])(?'month'Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)[uarychilestmbro]*\s+(?'day'\d{1,2})(?:-?st|-?th|-?rd|-?nd)?(?:\s*,?\s*(?'year'\d{4}))?(?=$|[^\d\w])", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			if (m.Success)
			{
				var month = -1;
				var index_of_date = m.Index;
				var length_of_date = m.Length;

				switch (m.Groups["month"].Value)
				{
					case "Jan":
					case "JAN":
						month = 1;
						break;
					case "Feb":
					case "FEB":
						month = 2;
						break;
					case "Mar":
					case "MAR":
						month = 3;
						break;
					case "Apr":
					case "APR":
						month = 4;
						break;
					case "May":
					case "MAY":
						month = 5;
						break;
					case "Jun":
					case "JUN":
						month = 6;
						break;
					case "Jul":
						month = 7;
						break;
					case "Aug":
					case "AUG":
						month = 8;
						break;
					case "Sep":
					case "SEP":
						month = 9;
						break;
					case "Oct":
					case "OCT":
						month = 10;
						break;
					case "Nov":
					case "NOV":
						month = 11;
						break;
					case "Dec":
					case "DEC":
						month = 12;
						break;
				}

				int year;
				if (!string.IsNullOrEmpty(m.Groups["year"].Value))
					year = int.Parse(m.Groups["year"].Value);
				else
					year = DefaultDate.Year;

				DateTime date;
				if (!ConvertToDate(year, month, int.Parse(m.Groups["day"].Value), out date))
					return false;
				parseddate = new ParsedDateTime(index_of_date, length_of_date, -1, -1, date);
				return true;
			}

			return false;
		}

		private static bool ConvertToDate(int year, int month, int day, out DateTime date)
		{
			if (year >= 100)
			{
				if (year < 1000)
				{
					date = new DateTime(1, 1, 1);
					return false;
				}
			}
			else
			if (year > 30)
				year += 1900;
			else
				year += 2000;

			try
			{
				date = new DateTime(year, month, day);
			}
			catch
			{
				date = new DateTime(1, 1, 1);
				return false;
			}
			return true;
		}
	}
}

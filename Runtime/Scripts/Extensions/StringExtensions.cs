using System.Collections;

namespace LordSheo.JJTK
{
	public static class StringExtensions
	{
		public static string ToTitleCase(this string source, bool isUpperCase = false)
		{
			if (isUpperCase)
			{
				source = source.ToLower();
			}
			
			var culture = System.Globalization.CultureInfo.CurrentCulture;
			return culture.TextInfo.ToTitleCase(source);
		}

		public static bool IsNullOrEmpty(this string source)
		{
			return string.IsNullOrEmpty(source);
		}
		
		public static string ToShortDisplayString(this int value)
		{
			if (value < 1_000)
			{
				return value.ToString();
			}

			if (value < 1_000_000)
			{
				return (value / 1_000f).ToString("F2") + "k";
			}
			
			return (value / 1_000_000f).ToString("F2") + "m";
		}
	}
}
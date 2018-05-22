using System;
using Moq;
using NUnit.Framework;

namespace DateTimeTests
{
    [TestFixture]
    public class DateTimeNowAddDaysTests
    {
	private const double day = 1;
	private const double week = 7;
	private const double month = 30;
	private const double year = 360;

	Mock<IDateTime> mockDate;
	#region /* TEST CASE INPUT */
	DateTime beginningOfTime = new DateTime(1000, 01, 01);
	DateTime dayAfterLeapDay = new DateTime(2000, 03, 01);
	DateTime dayAfterNonLeapDay = new DateTime(2002, 03, 01);
	DateTime dayBeforeLeapDay = new DateTime(2000, 02, 28);
	DateTime dayBeforeNonLeapDay = new DateTime(2002, 02, 28);
	DateTime endOfTime = new DateTime(2012, 12, 21);
	DateTime firstDayOfNotFirstMonth = new DateTime(2000, 02, 01);
	DateTime firstDayOfNotFirstWeek = new DateTime(2000, 04, 10);
	DateTime firstDayOfYear = new DateTime(2000, 01, 01);
	DateTime lastDayOfNotLastMonth = new DateTime(2000, 11, 30);
	DateTime lastDayOfNotLastWeek = new DateTime(2000, 08, 20);
	DateTime lastDayOfYear = new DateTime(2000, 12, 31);
	DateTime midMonth = new DateTime(2000, 07, 16);
	DateTime midWeek = new DateTime(2000, 05, 25);
	DateTime midYear = new DateTime(2000, 06, 29);
	#endregion

	[SetUp]
	public void Init()
	{
	    mockDate = new Mock<IDateTime>();
	}

	[Test]
	public void FutureDateInSameWeek()
	{
	    mockDate.Setup(date => date.Now).Returns(firstDayOfNotFirstWeek);
	    DateTime expectedDate = firstDayOfNotFirstWeek.AddDays(day);
	    DateTime actualDate = mockDate.Object.Now.AddDays(day);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	public void FutureDateInNextWeek(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(lastDayOfNotLastWeek);
	    DateTime expectedDate = lastDayOfNotLastWeek.AddDays(daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void FutureDateInNextWeek()
	{
	    mockDate.Setup(date => date.Now).Returns(midWeek);
	    DateTime expectedDate = midWeek.AddDays(week);
	    DateTime actualDate = mockDate.Object.Now.AddDays(week);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	public void FutureDateInSameMonth1(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(firstDayOfNotFirstMonth);
	    DateTime expectedDate = firstDayOfNotFirstMonth.AddDays(daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	public void FutureDateInSameMonth2(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(midMonth);
	    DateTime expectedDate = midMonth.AddDays(daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	[TestCase(month)]
	public void FutureDateInNextMonth(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(lastDayOfNotLastMonth);
	    DateTime expectedDate = lastDayOfNotLastMonth.AddDays(daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void FutureDateInNextMonth()
	{
	    mockDate.Setup(date => date.Now).Returns(midMonth);
	    DateTime expectedDate = midMonth.AddDays(month);
	    DateTime actualDate = mockDate.Object.Now.AddDays(month);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	[TestCase(month)]
	public void FutureDateInSameYear(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(midYear);
	    DateTime expectedDate = midYear.AddDays(daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	[TestCase(month)]
	[TestCase(year)]
	public void FutureDateInNextYear(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(lastDayOfYear);
	    DateTime expectedDate = lastDayOfYear.AddDays(daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void FutureDateInNextYear()
	{
	    mockDate.Setup(date => date.Now).Returns(midYear);
	    DateTime expectedDate = midYear.AddDays(year);
	    DateTime actualDate = mockDate.Object.Now.AddDays(year);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void FutureLeapDateInLeapYear()
	{
	    mockDate.Setup(date => date.Now).Returns(dayBeforeLeapDay);
	    DateTime expectedDate = dayBeforeLeapDay.AddDays(day);
	    DateTime actualDate = mockDate.Object.Now.AddDays(day);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void FutureLeapDateInNonLeapYear()
	{
	    mockDate.Setup(date => date.Now).Returns(dayBeforeNonLeapDay);
	    DateTime expectedDate = dayBeforeNonLeapDay.AddDays(day);
	    DateTime actualDate = mockDate.Object.Now.AddDays(day);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void PastDateInSameWeek()
	{
	    mockDate.Setup(date => date.Now).Returns(lastDayOfNotLastWeek);
	    DateTime expectedDate = lastDayOfNotLastWeek.AddDays(-day);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-day);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	public void PastDateInPreviousWeek(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(firstDayOfNotFirstWeek);
	    DateTime expectedDate = firstDayOfNotFirstWeek.AddDays(-daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void PastDateInPreviousWeek()
	{
	    mockDate.Setup(date => date.Now).Returns(midWeek);
	    DateTime expectedDate = midWeek.AddDays(-week);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-week);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	public void PastDateInSameMonth1(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(lastDayOfNotLastMonth);
	    DateTime expectedDate = lastDayOfNotLastMonth.AddDays(-daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	public void PastDateInSameMonth2(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(midMonth);
	    DateTime expectedDate = midMonth.AddDays(-daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	[TestCase(month)]
	public void PastDateInPreviousMonth(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(firstDayOfNotFirstMonth);
	    DateTime expectedDate = firstDayOfNotFirstMonth.AddDays(-daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void PastDateInPreviousMonth()
	{
	    mockDate.Setup(date => date.Now).Returns(midMonth);
	    DateTime expectedDate = midMonth.AddDays(-month);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-month);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	[TestCase(month)]
	public void PastDateInSameYear(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(midYear);
	    DateTime expectedDate = midYear.AddDays(-daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	[TestCase(day)]
	[TestCase(week)]
	[TestCase(month)]
	[TestCase(year)]
	public void PastDateInPreviousYear(double daysCount)
	{
	    mockDate.Setup(date => date.Now).Returns(firstDayOfYear);
	    DateTime expectedDate = firstDayOfYear.AddDays(-daysCount);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-daysCount);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void PastDateInPreviousYear()
	{
	    mockDate.Setup(date => date.Now).Returns(midYear);
	    DateTime expectedDate = midYear.AddDays(-year);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-year);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void PastLeapDateInLeapYear()
	{
	    mockDate.Setup(date => date.Now).Returns(dayAfterLeapDay);
	    DateTime expectedDate = dayAfterLeapDay.AddDays(-day);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-day);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void PastLeapDateInNonLeapYear()
	{
	    mockDate.Setup(date => date.Now).Returns(dayAfterNonLeapDay);
	    DateTime expectedDate = dayAfterNonLeapDay.AddDays(-day);
	    DateTime actualDate = mockDate.Object.Now.AddDays(-day);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void DateBeforeBeginningOfTime()
	{
	    mockDate.Setup(date => date.First).Returns(beginningOfTime);
	    DateTime expectedDate = beginningOfTime.AddDays(-day);
	    DateTime actualDate = mockDate.Object.First.AddDays(-day);
	    Assert.AreEqual(expectedDate, actualDate);
	}

	[Test]
	public void DateAfterEndOfTime()
	{
	    mockDate.Setup(date => date.Last).Returns(endOfTime);
	    DateTime expectedDate = endOfTime.AddDays(day);
	    DateTime actualDate = mockDate.Object.Last.AddDays(day);
	    Assert.AreEqual(expectedDate, actualDate);
	}
    }
}

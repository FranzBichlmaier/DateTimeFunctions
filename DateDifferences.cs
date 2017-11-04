using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeFunctions
{
    public class DateDifferences
    {
        private DateTime startDate;
        private DateTime endDate;

        /// <summary>
        /// return number of years between two dates (Date only).
        /// if startDate is after endDate a negative number is returned
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public double Years(DateTime start, DateTime end)
        {
            int factor = 1;
            int yearDiff = 0;
            DateTime helpDate;
            TimeSpan timeSpan;
            if (start > end)
            {
                factor = -1;
                this.startDate = end.Date;
                this.endDate = start.Date;
            }
            else
            {
                factor = 1;
                this.startDate = start.Date;
                this.endDate = end.Date;
            }

            if (this.startDate == this.endDate) return 0;
            yearDiff = this.endDate.Year - this.startDate.Year;
            if (yearDiff > 0 && this.endDate.AddYears(yearDiff * -1) < this.startDate) yearDiff -= 1;
            helpDate = this.endDate.AddYears(yearDiff * -1);
            timeSpan = this.endDate.Subtract(helpDate);

            return (yearDiff + (timeSpan.Days / 365))*factor;
        }
        /// <summary>
        /// returns number of months between two dates (Date only).
        /// if startDate is after endDate a negative number is returned
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public double Months(DateTime start, DateTime end)
        {
            int factor = 1;
            int monthDiff = 0;
            DateTime helpDate;
            TimeSpan timeSpan;
            if (start > end)
            {
                factor = -1;
                this.startDate = end.Date;
                this.endDate = start.Date;
            }
            else
            {
                factor = 1;
                this.startDate = start.Date;
                this.endDate = end.Date;
            }
            if (this.startDate == this.endDate) return 0;

            monthDiff = (this.endDate.Year * 12 + this.endDate.Month) - (this.startDate.Year * 12 + this.startDate.Month);
            if (monthDiff > 0 && this.endDate.AddMonths(monthDiff * -1) < this.startDate) monthDiff -= 1;
            helpDate = this.endDate.AddMonths(monthDiff * -1);
            timeSpan = this.endDate.Subtract(helpDate);
            int divisor = DateTime.DaysInMonth(this.startDate.Year, this.startDate.Month);

            return (monthDiff + (timeSpan.Days / divisor)) * factor;
        }
        /// <summary>
        /// returns number of weeks between two dates (Date only).
        /// if startDate is after endDate a negativenumber is returned
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public double Weeks(DateTime start, DateTime end)
        {
            int factor = 1;
            int weekDiff = 0;
            DateTime helpDate;
            TimeSpan timeSpan;
            if (start > end)
            {
                factor = -1;
                this.startDate = end.Date;
                this.endDate = start.Date;
            }
            else
            {
                factor = 1;
                this.startDate = start.Date;
                this.endDate = end.Date;
            }
            if (this.startDate == this.endDate) return 0;

            timeSpan = this.endDate.Subtract(this.startDate);
            weekDiff = (int)Math.Floor((double)timeSpan.Days / 7.0);
            helpDate = this.endDate.AddDays(weekDiff * 7 * -1);
            timeSpan = this.endDate.Subtract(helpDate);

            return (weekDiff + (timeSpan.Days / 7)) * factor;
        }
        /// <summary>
        /// counts the number of quarters (including startQuarter) starting with startDate to endDate. If dates are in the same quarter the result is '1'. 
        /// If endDate is before startDate a negativ number is returned. 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int Quarters(DateTime start, DateTime end)
        {
            string s = GetQuarterAsString(start);
            string e = GetQuarterAsString(end);
            int quarterStart = int.Parse(s.Substring(0, 1));
            int quarterStartYear = int.Parse(s.Substring(2, 4));
            int quarterEnd = int.Parse(e.Substring(0, 1));
            int quarterEndYear = int.Parse(e.Substring(2, 4));
            if (quarterStartYear == quarterEndYear)
            {
                if (quarterEnd >= quarterStart)
                {
                    return quarterEnd - quarterStart + 1;
                }
                else
                {
                    return quarterStart - quarterEnd;
                }                
            }
            else if (quarterStartYear < quarterEndYear)
            {
                int nrQuarters = (quarterEndYear - quarterStartYear - 1) * 4;
                nrQuarters = nrQuarters + (5 - quarterStart) + quarterEnd;
                return nrQuarters;
            }
            else
            {
                int nrQuarters = (quarterStartYear - quarterEndYear - 1) * 4;
                nrQuarters = nrQuarters + (5 - quarterEnd) + quarterStart;
                return nrQuarters*-1;
            }
        }
        /// <summary>
        /// returns DateTime of the month end of a Date. If the Date is month end the same date is returned.
        /// </summary>
        /// <param name="myDate"></param>
        /// <returns></returns>
        public DateTime MonthEnd(DateTime myDate)
        {
            return new DateTime(myDate.Year, myDate.Month, DateTime.DaysInMonth(myDate.Year, myDate.Month));
        }
        /// <summary>
        /// returns DateTime of the month end of a Date. If the Date is month end the month end of the following month is returned.
        /// </summary>
        /// <param name="myDate"></param>
        /// <returns></returns>
        public DateTime NextMonthEnd(DateTime myDate)
        {
            myDate = myDate.AddDays(1);
            return new DateTime(myDate.Year, myDate.Month, DateTime.DaysInMonth(myDate.Year, myDate.Month));
        }
        public DateTime PreviousMonthEnd (DateTime myDate)
        {
            
            myDate = new DateTime(myDate.Year, myDate.Month, 1);
            myDate = myDate.AddDays(-1);
            return myDate;
        }

        public DateTime NextQuarter(DateTime myDate)
        {
            myDate = myDate.AddDays(1);
            if (myDate.Month <= 3) return new DateTime(myDate.Year, 3, 31);
            if (myDate.Month <= 6) return new DateTime(myDate.Year, 6, 30);
            if (myDate.Month <= 9) return new DateTime(myDate.Year, 9, 30);
            return new DateTime(myDate.Year, 12, 31);
        }

        public DateTime PreviousQuarter(DateTime myDate)
        {
          
            if (myDate.Month <= 3) return new DateTime(myDate.Year-1, 12, 31);
            if (myDate.Month <= 6) return new DateTime(myDate.Year, 3, 31);
            if (myDate.Month <= 9) return new DateTime(myDate.Year, 6, 30);
            return new DateTime(myDate.Year, 9, 30);
        }

        public DateTime NextHalfYear(DateTime myDate)
        {
            myDate = myDate.AddDays(1);
            if (myDate.Month <= 6) return new DateTime(myDate.Year, 6, 30);
            return new DateTime(myDate.Year, 12, 31);
        }

        public DateTime PreviousHalfYear(DateTime myDate)
        {
            myDate = myDate.AddDays(-1);
            if (myDate.Month > 6) return new DateTime(myDate.Year, 6, 30);
            return new DateTime(myDate.Year-1, 12, 31);
        }

        public DateTime NextYearEnd(DateTime myDate)
        {
            myDate = myDate.AddDays(1);          
            return new DateTime(myDate.Year, 12, 31);
        }

        public DateTime PreviousYearEnd(DateTime myDate)
        {
            myDate = myDate.AddDays(-1);
            return new DateTime(myDate.Year-1, 12, 31);
        }

        public DateTime NextWeek(DateTime myDate, int weekday)
        {
            int currentDay = (int)myDate.DayOfWeek;
            int wantedDay = weekday;
            int nrOfDays =0;
            if (wantedDay > currentDay) nrOfDays = wantedDay - currentDay;
            else nrOfDays = 7 + (wantedDay - currentDay);
            return myDate.AddDays(nrOfDays);
        }

        //public DateTime PreviousWeek(DateTime myDate, int weekday)
        //{
        //    myDate = myDate.AddDays(-7);
        //    return NextWeek(myDate, weekday);
        //}

        public DateTime NextWeek(DateTime myDate, DayOfWeek weekday)
        {
            int currentDay = (int)myDate.DayOfWeek;
            int wantedDay = (int)weekday;
            int nrOfDays = 0;
            if (wantedDay > currentDay) nrOfDays = wantedDay - currentDay;
            else nrOfDays = 7 + (wantedDay - currentDay);
            return myDate.AddDays(nrOfDays);
        }

        public DateTime PreviousWeek(DateTime myDate, DayOfWeek weekday)
        {
            myDate = myDate.AddDays(-7);
            return NextWeek(myDate, weekday);
        }
        public int QuarterNumber(DateTime myDate)
        {
            
            if (myDate.Month <= 3) return 1;
            if (myDate.Month <= 6) return 2;
            if (myDate.Month<=9 ) return 3;
            return 4;
        }

        public string GetQuarterAsString (DateTime myDate)
        {
            return string.Format("{0}/{1}", QuarterNumber(myDate), myDate.Year);
        }

        public string NextQuarterAsString (string quarterAsString)
        {
            int quarter = int.Parse(quarterAsString.Substring(0, 1));
            int year = int.Parse(quarterAsString.Substring(2, 4));

            quarter += 1;
            if (quarter==5)
            {
                quarter = 1;
                year += 1;
            }
            return string.Format("{0}/{1}", quarter, year);
        }

        public Tuple<DateTime, DateTime> GetQuarterPeriodFromString (string quarterAsString)
        {
            int quarter = int.Parse(quarterAsString.Substring(0, 1));
            int year = int.Parse(quarterAsString.Substring(2, 4));

            if (quarter == 1)
            {
                return new Tuple<DateTime, DateTime>(new DateTime(year, 1, 1), new DateTime(year, 3, 31));
            }
            else if (quarter == 2)
            {
                return new Tuple<DateTime, DateTime>(new DateTime(year, 4, 1), new DateTime(year, 6, 30));
            }
            else if (quarter == 3)
            {
                return new Tuple<DateTime, DateTime>(new DateTime(year, 7, 1), new DateTime(year, 9, 30));
            }
            else
            {
                return new Tuple<DateTime, DateTime>(new DateTime(year, 9, 1), new DateTime(year, 12, 31));
            }             
          
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace uTimeSystem
{
	public class TimeManager : MonoBehaviour
	{
		public DateTime CurrentTime { get; private set; }
		public string MilitaryTimeOfDay
		{
			get
			{
				return CurrentTime.ToLongTimeString ();
			}
		}

		public string StandardTimeOfDay
		{
			get
			{
				return CurrentTime.ToString (@"hh\:mm\:ss tt");
			}
		}

		[Range (1, 24)]
		public int startYear = 2000;

		[Range (1, 12)]
		public int startMonth = 1;

		[Range (1, 30)]
		public int startDay = 1;

		[Range (1, 24)]
		public int startHour = 10;

		public float realSecondsPerGameDay = 600; // 600 seconds is a day in game

		private float internalHourlyTimer = 0;
		private float realSecondsPerGameHour = 0;
		private float realSecondsPerGameMinute = 0;
		private float realSecondsPerGameSecond = 0;

		public delegate void TimePassedEvent (DateTime currentTime);
		public event TimePassedEvent OnSecondPassed = delegate { };
		public event TimePassedEvent OnMinutePassed = delegate { };
		public event TimePassedEvent OnHourPassed = delegate { };
		public event TimePassedEvent OnDayPassed = delegate { };
		public event TimePassedEvent OnMonthPassed = delegate { };
		public event TimePassedEvent OnYearPassed = delegate { };

		void Start ()
		{
			CurrentTime = new DateTime (startYear, startMonth, startDay, startHour, 0, 0);

			// 600 seconds =game=> 1 day = 24 hours = 24 * 60 minutes = 24 * 60 * 60 seconds
			realSecondsPerGameHour = realSecondsPerGameDay / 24;
			realSecondsPerGameMinute = realSecondsPerGameHour / 60;
			realSecondsPerGameSecond = realSecondsPerGameMinute / 60;
		}

		void Update ()
		{
			// tick game timer 
			float gameSeconds = Time.deltaTime / realSecondsPerGameSecond;

			DateTime newTime = CurrentTime.AddSeconds (gameSeconds);

			if (newTime.Year - CurrentTime.Year > 0)
			{
				OnYearPassed (newTime);
			}
			if (newTime.Month - CurrentTime.Month > 0)
			{
				OnMonthPassed (newTime);
			}
			if (newTime.Day - CurrentTime.Day > 0)
			{
				OnDayPassed (newTime);
			}
			if (newTime.Hour - CurrentTime.Hour > 0)
			{
				OnHourPassed (newTime);
			}
			if (newTime.Minute - CurrentTime.Minute > 0)
			{
				OnMinutePassed (newTime);
			}
			if (gameSeconds >= 0)
			{
				OnSecondPassed (newTime);
			}
			CurrentTime = newTime;
		}
	}
}
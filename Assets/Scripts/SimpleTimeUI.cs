using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace uTimeSystem
{
	[RequireComponent (typeof (Text))]
	public class SimpleTimeUI : MonoBehaviour
	{
		public Text timeTextUI;
		public TimeManager timeManager;

		void Start ()
		{
			timeTextUI = GetComponent<Text> ();
			Assert.IsNotNull (timeManager, "time manager is not given");

			timeManager.OnSecondPassed += UpdateTimeUI;
			enabled = false;
		}

		void UpdateTimeUI (DateTime time)
		{
			// string timeStr = string.Format ("{0:D4}y:{1:D2}m:{2:D2}d:{3:D2}h:{4:D2}m:{5:D2}s",
			// 	time.Year,
			// 	time.Month,
			// 	time.Day,
			// 	time.Hour,
			// 	time.Minute,
			// 	time.Second
			// );
			timeTextUI.text = time.ToShortDateString () + " " + time.ToString (@"hh\:mm tt");
		}
	}
}
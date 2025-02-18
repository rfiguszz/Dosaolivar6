﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.ComponentModel;

namespace SmartHotel.Clients.Core.Controls
{
	public class SpecialDate : INotifyPropertyChanged
    {
        public SpecialDate(DateTime date) => Date = date;

        public DateTime Date { get; set; }
		public Color? TextColor { get; set; }
		public Color? BackgroundColor { get; set; }
		public Color? BorderColor { get; set; }
		public FontAttributes? FontAttributes { get; set; }
		public string FontFamily { get; set; }
		public int? BorderWidth { get; set; }
		public double? FontSize { get; set; }
		public bool Selectable { get; set; }

		/// <summary>
		/// Gets or sets the background image (only working on iOS and Android).
		/// </summary>
		/// <value>The background pattern.</value>
		public FileImageSource BackgroundImage { get; set; }

		/// <summary>
		/// Gets or sets the background pattern (only working on iOS and Android).
		/// </summary>
		/// <value>The background pattern.</value>
		public BackgroundPattern BackgroundPattern{ get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

	public class BackgroundPattern
	{
		protected int columns;
        public BackgroundPattern(int columns) => this.columns = columns;

        public List<Pattern> Pattern;

		public float GetTop(int t)
		{
			float r = 0;
			for (var i = t-columns; i > -1; i-=columns)
			{
				r += Pattern[i].HightPercent;
			}
			return r;
		}

		public float GetLeft(int l)
		{
			float r = 0;
			for (var i = l-1; i > -1 && (i+1) % columns != 0; i--)
			{
				r += Pattern[i].WidthPercent;
			}
			return r;
		}
	}

	public struct Pattern
	{
		public float WidthPercent;
		public float HightPercent;
		public Color Color;
	}
}

﻿using Microcharts;
using SkiaSharp;
using System;
using System.Linq;

namespace SmartHotel.Clients.Core.Controls
{
    public class TemperatureChart : Chart
    {
        public TemperatureChart()
        {
            BackgroundColor = SKColor.Parse("#F2F2F2");

            LabelTextSize = 24f;
        }

        public float CaptionMargin { get; set; } = 12;

        public float LineSize { get; set; } = 28;

        public float StartAngle { get; set; } = -180;

		public Entry CurrentValueEntry { get; set; }

		public Entry DesiredValueEntry { get; set; }

		protected float AbsoluteMinimum => Entries.Select(x => x.Value).Concat(new[] { MaxValue, MinValue, InternalMinValue ?? 0 }).Min(x => Math.Abs(x));

        float AbsoluteMaximum => Entries.Select(x => x.Value).Concat(new[] { MaxValue, MinValue, InternalMinValue ?? 0 }).Max(x => Math.Abs(x));

        protected float ValueRange => AbsoluteMaximum - AbsoluteMinimum;

        public override void DrawContent(SKCanvas canvas, int width, int height)
        {
            var relativeScaleWidth = width / 465.0f;
            var strokeWidth = relativeScaleWidth * LineSize;

            var radius = (width) * 2.0f / 4f;
            var cx = (int)(radius);
            var cy = Convert.ToInt32((height / 2.0f) + radius / 3.7f); 
            var radiusSpace = radius - 4 * strokeWidth; 

            DrawChart(canvas, width, height, cx, cy, radiusSpace, strokeWidth, relativeScaleWidth);
        }

        protected void DrawChart(SKCanvas canvas, int width, int height, int cx, int cy, float radiusSpace, float strokeWidth, float relativeScaleWidth)
        {

            foreach (var entry in Entries)
            {
                DrawChart(canvas, entry, radiusSpace, cx, cy, strokeWidth);
            }

            DrawCaption(canvas, cx, cy, radiusSpace, relativeScaleWidth, strokeWidth);
        }

        protected virtual void DrawChart(SKCanvas canvas, Entry entry, float radius, int cx, int cy, float strokeWidth)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = strokeWidth,
                StrokeCap = SKStrokeCap.Round,
                Color = entry.Color,
                IsAntialias = true
            })
            {
                using (var path = new SKPath())
                {
                    var sweepAngle = 180 * (Math.Abs(entry.Value) - AbsoluteMinimum) / ValueRange;
                    path.AddArc(SKRect.Create(cx - radius, cy - radius, 2 * radius, 2 * radius), StartAngle, sweepAngle);

                    canvas.DrawPath(path, paint); 
                }
            }
        }

        protected virtual void DrawCaption(SKCanvas canvas, int cx, int cy, float radius, float relativeScaleWidth,
            float strokeWidth)
        {
            var medium = AbsoluteMinimum + ((AbsoluteMaximum - AbsoluteMinimum) / 2);

            canvas.DrawCaptionLabels(string.Empty, SKColor.Empty, $"{AbsoluteMinimum}°", SKColors.Black, LabelTextSize * relativeScaleWidth, new SKPoint(cx - radius - strokeWidth - CaptionMargin, cy), SKTextAlign.Center);
            canvas.DrawCaptionLabels(string.Empty, SKColor.Empty, $"{medium}°", SKColors.Black, LabelTextSize * relativeScaleWidth, new SKPoint(cx, cy - radius - strokeWidth - 2 * relativeScaleWidth), SKTextAlign.Center);
            canvas.DrawCaptionLabels(string.Empty, SKColor.Empty, $"{AbsoluteMaximum}°", SKColors.Black, LabelTextSize * relativeScaleWidth, new SKPoint(cx + radius + strokeWidth + CaptionMargin, cy), SKTextAlign.Center);


            var degreeSign = '°';
	        if (CurrentValueEntry != null)
	        {
		        canvas.DrawCaptionLabels(string.Empty, SKColor.Empty, $"Current: {CurrentValueEntry.Value}{degreeSign}",
			        SKColor.Parse("#174A51"), LabelTextSize * relativeScaleWidth, new SKPoint(cx, cy - radius * 1.8f / 4f),
			        SKTextAlign.Center);
	        }

	        if (DesiredValueEntry != null)
	        {
		        canvas.DrawCaptionLabels(string.Empty, SKColor.Empty, $"Desired: {DesiredValueEntry?.Value}{degreeSign}",
			        SKColor.Parse("#378D93"), LabelTextSize * relativeScaleWidth, new SKPoint(cx, cy - radius * 0.9f / 4f),
			        SKTextAlign.Center);
	        }
        }
        
    }
}
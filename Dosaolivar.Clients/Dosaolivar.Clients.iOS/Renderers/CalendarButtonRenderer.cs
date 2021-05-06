﻿using Xamarin.Forms.Platform.iOS;
using UIKit;
using CoreGraphics;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using SmartHotel.Clients.Core.Controls;
using SmartHotel.Clients.iOS.Renderers;
#if __UNIFIED__
using Foundation;
#else
using MonoTouch.Foundation;
#endif

[assembly: ExportRenderer(typeof(CalendarButton), typeof(CalendarButtonRenderer))]
namespace SmartHotel.Clients.iOS.Renderers
{
    [Preserve(AllMembers = true)]
    public class CalendarButtonRenderer : ButtonRenderer
    {
        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Console.WriteLine(">>>>>");
            Console.WriteLine("LLAMADO A EVENTO DE CAMBIO DE PROPIEDAD " + sender.ToString());
            Console.WriteLine("LLAMADO A EVENTO DE CAMBIO DE PROPIEDAD " + e);
            base.OnElementPropertyChanged(sender, e);
            var element = Element as CalendarButton;
            bool notwork = false;
            if (ReferenceEquals(e, null))
            {
                Console.WriteLine("0 boton calendario nulo");
            }
            try
            {
                Console.WriteLine("LONGITUD " + e.PropertyName);
                Console.WriteLine("LONGITUD: " + element.TextWithoutMeasure.Length);
                Console.WriteLine("LONGITUD: " + element.TextColor);
            }
            catch (NullReferenceException ex)
            {
                notwork = true;

                Console.WriteLine($"0Error with CALENDAR refresh attempt: {ex}");
            }
            /*   if (element != null)
               {
                   Console.WriteLine("LONGITUD: " + element.TextWithoutMeasure.Length);
               }*/

            Console.WriteLine("TIPO " + e.PropertyName + " " + e.GetType());



            if (!notwork && (e.PropertyName == nameof(element.TextWithoutMeasure) || e.PropertyName == "Renderer") && !ReferenceEquals(element.Text, null) && !ReferenceEquals(element.TextWithoutMeasure.Length, null))
            {
                if (ReferenceEquals(element.Text, null) || ReferenceEquals(element.TextWithoutMeasure.Length, null))
                {
                    Console.WriteLine("0boton calendario nulo");
                }
                else
                {
                    try
                    {

                        Console.WriteLine("El texto del boton era...: " + element.TextWithoutMeasure);
                        Console.WriteLine("-------------------------------------------------");
                        Control.SetTitle(element.TextWithoutMeasure, UIControlState.Normal);
                        Control.SetTitle(element.TextWithoutMeasure, UIControlState.Disabled);
                    }
                    catch (NullReferenceException ex)
                    {
                        notwork = true;
                        System.Diagnostics.Debug.WriteLine($"1Error with CALENDAR refresh attempt: {ex}");
                    }
                }
            }
            Console.WriteLine("OTRO MAS:" + element.TextColor);
            if (!notwork && (e.PropertyName == nameof(element.TextColor) || e.PropertyName == "Renderer"))
            {
                if (ReferenceEquals(element.TextColor, null))
                {
                    Console.WriteLine("1boton calendario nulo");
                }
                else
                {
                    Control.SetTitleColor(element.TextColor.ToUIColor(), UIControlState.Disabled);
                    Control.SetTitleColor(element.TextColor.ToUIColor(), UIControlState.Normal);
                }
            }
            if (!notwork && (e.PropertyName == nameof(element.BackgroundPattern)))
            {
                if (ReferenceEquals(element.Text, null) || ReferenceEquals(element.TextWithoutMeasure.Length, null))
                {
                    Console.WriteLine("boton calendario nulo");
                }
                else
                {
                    DrawBackgroundPattern();
                }
            }
            if (e.PropertyName == nameof(element.BackgroundImage))
            {
                if (ReferenceEquals(element.Text, null) || ReferenceEquals(element.TextWithoutMeasure.Length, null))
                {
                    Console.WriteLine("3boton calendario nulo");
                }
                else
                {
                    DrawBackgroundImage();
                }
            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            Control.SetBackgroundImage(null, UIControlState.Normal);
            Control.SetBackgroundImage(null, UIControlState.Disabled);
            DrawBackgroundImage();
            DrawBackgroundPattern();
        }

        protected async void DrawBackgroundImage()
        {
            var element = Element as CalendarButton;
            if (element == null) return;
            if (element.BackgroundImage != null)
            {
                var image = await GetImage(element.BackgroundImage);
                Control.SetBackgroundImage(image, UIControlState.Normal);
                Control.SetBackgroundImage(image, UIControlState.Disabled);
            }
            else
            {
                Control.SetBackgroundImage(null, UIControlState.Normal);
                Control.SetBackgroundImage(null, UIControlState.Disabled);
            }
        }

        protected void DrawBackgroundPattern()
        {
            if (!(Element is CalendarButton element) || element.BackgroundPattern == null || Control.Frame.Width == 0) return;

            UIImage image;
            UIGraphics.BeginImageContext(Control.Frame.Size);
            using (var g = UIGraphics.GetCurrentContext())
            {
                for (var i = 0; i < element.BackgroundPattern.Pattern.Count; i++)
                {
                    var p = element.BackgroundPattern.Pattern[i];
                    g.SetFillColor(p.Color.ToCGColor());
                    var l = (int)Math.Ceiling(Control.Frame.Width * element.BackgroundPattern.GetLeft(i));
                    var t = (int)Math.Ceiling(Control.Frame.Height * element.BackgroundPattern.GetTop(i));
                    var w = (int)Math.Ceiling(Control.Frame.Width * element.BackgroundPattern.Pattern[i].WidthPercent);
                    var h = (int)Math.Ceiling(Control.Frame.Height * element.BackgroundPattern.Pattern[i].HightPercent);
                    g.FillRect(new CGRect { X = l, Y = t, Width = w, Height = h });
                }

                image = UIGraphics.GetImageFromCurrentImageContext();
            }
            UIGraphics.EndImageContext();
            Control.SetBackgroundImage(image, UIControlState.Normal);
            Control.SetBackgroundImage(image, UIControlState.Disabled);
        }

        Task<UIImage> GetImage(FileImageSource image)
        {
            var handler = new FileImageSourceHandler();
            return handler.LoadImageAsync(image);
        }
    }

    public static class Calendar
    {
        public static void Init()
        {
            var t1 = string.Empty;
        }
    }
}


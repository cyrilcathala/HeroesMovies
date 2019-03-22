using System;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace Sample.Controls
{
    public enum GradientOrientation
    {
        Vertical, Horizontal
    }

    public class GradientView : ContentView
    {
        public Color StartColor { get; set; } = Color.Transparent;
        public Color EndColor { get; set; } = Color.Transparent;

        public GradientOrientation Orientation { get; set; } = GradientOrientation.Vertical;

        public GradientView()
        {
            var canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;

            Content = canvasView;
        }

        private void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            var info = args.Info;
            var surface = args.Surface;
            var canvas = surface.Canvas;

            canvas.Clear();

            var colors = new[] { StartColor.ToSKColor(), EndColor.ToSKColor() };
            var startPoint = new SKPoint(0, 0);
            var endPoint = Orientation == GradientOrientation.Horizontal ? new SKPoint(info.Width, 0) : new SKPoint(0, info.Height);

            var shader = SKShader.CreateLinearGradient(startPoint, endPoint, colors, null, SKShaderTileMode.Clamp);

            var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Shader = shader
            };

            canvas.DrawRect(new SKRect(0, 0, info.Width, info.Height), paint);
        }
    }
}

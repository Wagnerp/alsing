﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using GenArt.AST;
using System;

namespace GenArt.Classes
{
    public static class Renderer
    {
        //Render a Drawing
        public static void Render(DnaDrawing drawing, Graphics g, int scale)
        {
            g.Clear(Color.Black);

            foreach (DnaPolygon polygon in drawing.Polygons)
                Render(polygon, g, scale);
        }


        //Render a polygon
        private static void Render(DnaPolygon polygon, Graphics g, int scale)
        {
            if (polygon.IsComplex)
                return;

            Point[] points = GetGdiPoints(polygon.Points, scale);
            using (Brush brush = GetGdiBrush(polygon.Brush))
            {
                if (polygon.Splines)
                {
                    if (polygon.Filled)
                    {
                        g.FillClosedCurve(brush, points, FillMode.Winding);
                    }
                    else
                    {
                        using (Pen pen = new Pen(brush, Math.Max(1, polygon.Width)))
                        {
                            g.DrawCurve(pen, points, 3F);
                        }
                    }
                }
                else
                {
                    if (polygon.Filled)
                    {
                        g.FillPolygon(brush, points, FillMode.Winding);
                    }
                    else
                    {
                        using (Pen pen = new Pen(brush, Math.Max(1, polygon.Width)))
                        {
                            g.DrawPolygon(pen, points);
                        }
                    }
                }
            }
        }

        //Convert a list of DnaPoint to a list of System.Drawing.Point's
        private static Point[] GetGdiPoints(IList<DnaPoint> points, int scale)
        {
            var pts = new Point[points.Count];
            int i = 0;
            foreach (DnaPoint pt in points)
            {
                pts[i++] = new Point(pt.X*scale, pt.Y*scale);
            }
            return pts;
        }

        //Convert a DnaBrush to a System.Drawing.Brush
        private static Brush GetGdiBrush(DnaBrush b)
        {
            //var pb = new PathGradientBrush(points);
            //pb.CenterColor = Color.FromArgb(b.Alpha, b.Red, b.Green, b.Blue);
            //pb.SurroundColors = new Color[] {Color.Transparent};

            //return pb;

            return new SolidBrush(Color.FromArgb(b.Alpha, b.Red, b.Green, b.Blue));
        }
    }
}
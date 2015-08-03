﻿using Microsoft.Xna.Framework;

namespace Penumbra.Mathematics
{
    internal static class VectorUtil
    {
        // NB! We are using inverted y axis (y runs from top to bottom).

        public static Vector2 Rotate90CW(Vector2 v)
        {
            //return new Vector2(-v.Y, v.X); // inverted y
            return new Vector2(v.Y, -v.X);
        }

        public static Vector2 Rotate90CCW(Vector2 v)
        {
            //return new Vector2(v.Y, -v.X); // inverted y
            return new Vector2(-v.Y, v.X);
        }

        // Assumes a polygon where no two edges intersect.
        public static Vector2 CalculateCentroid(this Vector2[] points)
        {
            float area = 0.0f;
            float cx = 0.0f;
            float cy = 0.0f;

            for (int i = 0; i < points.Length; i++)
            {
                var k = (i + 1) % (points.Length);
                var tmp = points[i].X * points[k].Y -
                            points[k].X * points[i].Y;
                area += tmp;
                cx += (points[i].X + points[k].X) * tmp;
                cy += (points[i].Y + points[k].Y) * tmp;
            }
            area *= 0.5f;
            cx *= 1.0f / (6.0f * area);
            cy *= 1.0f / (6.0f * area);

            return new Vector2(cx, cy);
        }

        //public static Vector2 Project(Vector2 v, Vector2 onto)
        //{
        //    return onto * (Vector2.Dot(onto, v) / onto.LengthSquared());
        //}

        //public static float ProjectLength(Vector2 v, Vector2 onto)
        //{
        //    return Vector2.Dot(onto, v) / onto.Length();
        //}

        public static Vector2 Rotate(Vector2 v, float angle)
        {
            float num = Calc.Cos(angle); 
            float num2 = Calc.Sin(angle);
            return new Vector2(v.X * num + v.Y * num2, -v.X * num2 + v.Y * num);
        }

        //TODO: rename
        public static bool Intersects(Vector2 dirMiddle, Vector2 dirTest, Vector2 dirTestAgainst)
        {
            float dot1 = Vector2.Dot(dirMiddle, dirTest);
            float dot2 = Vector2.Dot(dirMiddle, dirTestAgainst);
            return dot1 < dot2;
        }

        // ref: http://stackoverflow.com/a/6075960/1466456
        public static void Barycentric(ref Vector2 p, ref Vector2 a, ref Vector2 b, ref Vector2 c, out Vector3 baryCoords)
        {
            float abcArea = Area(ref a, ref b, ref c);

            float u = Area(ref p, ref b, ref c) / abcArea;
            float v = Area(ref a, ref p, ref c) / abcArea;
            //float w = Area(a, b, p) / abcArea;
            float w = 1 - u - v;

            baryCoords = new Vector3(u, v, w);
        }

        public static float Cross(Vector2 a, Vector2 b)
        {
            return a.X * b.Y - a.Y * b.X;
        }

        public static void Cross(ref Vector2 a, ref Vector2 b, out float c)
        {
            c = a.X * b.Y - a.Y * b.X;
        }

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <returns>Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.</returns>
        public static float Area(Vector2 a, Vector2 b, Vector2 c)
        {
            return Area(ref a, ref b, ref c);
        }

        /// <summary>
        /// Returns a positive number if c is to the left of the line going from a to b.
        /// </summary>
        /// <returns>Positive number if point is left, negative if point is right, 
        /// and 0 if points are collinear.</returns>
        public static float Area(ref Vector2 a, ref Vector2 b, ref Vector2 c)
        {
            return a.X * (b.Y - c.Y) + b.X * (c.Y - a.Y) + c.X * (a.Y - b.Y);
        }

        /// <summary>
        /// Determines if three vertices are collinear (ie. on a straight line)
        /// </summary>
        /// <param name="a">First vertex</param>
        /// <param name="b">Second vertex</param>
        /// <param name="c">Third vertex</param>
        /// <returns></returns>
        public static bool Collinear(ref Vector2 a, ref Vector2 b, ref Vector2 c)
        {
            return Collinear(ref a, ref b, ref c, 0);
        }

        public static bool Collinear(ref Vector2 a, ref Vector2 b, ref Vector2 c, float tolerance)
        {
            return Calc.FloatInRange(Area(ref a, ref b, ref c), -tolerance, tolerance);
        }

        public static bool NearEqual(Vector2 lhv, Vector2 rhv)
        {
            return Calc.NearEqual(lhv.X, rhv.X) && Calc.NearEqual(lhv.Y, rhv.Y);
        }
    }
}
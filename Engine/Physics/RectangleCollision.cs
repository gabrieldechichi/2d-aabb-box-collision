using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ArbitraryCollisionRectangle.Engine.Physics
{
    public struct Hit2d
    {
        public Vector2 Point;
        public Vector2 Normal;
        public Vector2 Penetration;

        public float Time;

        public bool DidHit => Penetration != Vector2.Zero;
    }

    public struct Sweep2D
    {
        public Hit2d Hit;
        public Vector2 Position;
    }

    public static class RectangleCollision
    {
        public static Hit2d CalculatePointRectangelPenetration(in Vector2 point, in RectangleF rect)
        {
            var hit = new Hit2d();

            var dx = point.X - rect.Center.X;
            var px = rect.Extents.X - Math.Abs(dx);

            if (px <= 0)
            {
                return hit;
            }

            var dy = point.Y - rect.Center.Y;
            var py = rect.Extents.Y - Math.Abs(dy);

            if (py <= 0)
            {
                return hit;
            }

            if (px < py)
            {
                var sign = SignFixed(dx);
                hit.Normal.X = sign;
                hit.Point.X = rect.Center.X + rect.Extents.X * sign;
                hit.Point.Y = point.Y;
                hit.Penetration.X = sign * px;
            }
            else
            {
                var sign = SignFixed(dy);
                hit.Normal.Y = sign;
                hit.Point.Y = rect.Center.Y + rect.Extents.Y * sign;
                hit.Point.X = point.X;
                hit.Penetration.Y = sign * py;
            }

            return hit;
        }

        /// <summary>
        /// Calculates penetration of r1 into r2
        /// </summary>
        public static Hit2d CalculateRectangleRectanglePenetration(in RectangleF r1, in RectangleF r2)
        {
            var hit = new Hit2d();

            var dx = r1.Center.X - r2.Center.X;
            var px = (r1.Extents.X + r2.Extents.X) - Math.Abs(dx);

            if (px <= 0)
            {
                return hit;
            }

            var dy = r1.Center.Y - r2.Center.Y;
            var py = (r1.Extents.Y + r2.Extents.Y) - Math.Abs(dy);

            if (py <= 0)
            {
                return hit;
            }

            if (px < py)
            {
                var sign = SignFixed(dx);
                hit.Normal.X = sign;
                hit.Point.X = r2.Center.X + r2.Extents.X * sign;
                hit.Point.Y = r1.Center.Y;
                hit.Penetration.X = px * sign;
            }
            else
            {
                var sign = SignFixed(dy);
                hit.Normal.Y = sign;
                hit.Point.Y = r2.Center.Y + r2.Extents.Y * sign;
                hit.Point.X = r1.Center.X;
                hit.Penetration.Y = py * sign;
            }

            return hit;
        }

        public static Hit2d CalculateRayRectangleCollision(in Ray2D ray, in RectangleF rect)
        {
            return CalculateRayRectangleCollision(ray, rect, Vector2.Zero);
        }

        public static Hit2d CalculateRayRectangleCollision(in Ray2D ray, in RectangleF rect, in Vector2 padding)
        {
            var rayScaleX = 1.0f / ray.StartToEnd.X;
            var rayScaleY = 1.0f / ray.StartToEnd.Y;
            var raySignX = SignFixed(rayScaleX);
            var raySignY = SignFixed(rayScaleY);

            var extents = rect.Extents + padding;
            var nearTimeX = (rect.Center.X - raySignX * extents.X - ray.Start.X) * rayScaleX;
            var nearTimeY = (rect.Center.Y - raySignY * extents.Y - ray.Start.Y) * rayScaleY;

            var farTimeX = (rect.Center.X + raySignX * extents.X - ray.Start.X) * rayScaleX;
            var farTimeY = (rect.Center.Y + raySignY * extents.Y - ray.Start.Y) * rayScaleY;

            var nearTime = nearTimeX > nearTimeY ? nearTimeX : nearTimeY;
            var farTime = farTimeX > farTimeY ? farTimeY : farTimeX;

            var hit = new Hit2d();
            if (!(nearTime < 0 && farTime < 0) && nearTime < farTime)
            {
                hit.Time = Math.Clamp(nearTime, 0, 1);
                hit.Point = ray.GetPoint(hit.Time);
                if (nearTimeX > nearTimeY)
                {
                    hit.Normal.X = -raySignX;
                    hit.Normal.Y = 0;
                }
                else
                {
                    hit.Normal.X = 0;
                    hit.Normal.Y = -raySignY;
                }
                hit.Penetration = -(1 - hit.Time) * (ray.StartToEnd);
            }

            return hit;
        }

        public static Sweep2D RectRectSweep(in RectangleF r1, in Vector2 deltaR1, in RectangleF r2)
        {
            var sweep = new Sweep2D();
            if (deltaR1 == Vector2.Zero)
            {
                sweep.Hit = CalculateRectangleRectanglePenetration(r1, r2);
                sweep.Position = r1.Center + sweep.Hit.Penetration;
            }
            else
            {
                var ray = new Ray2D
                {
                    Start = r1.Center,
                    End = r1.Center + deltaR1
                };

                sweep.Hit = CalculateRayRectangleCollision(ray, r2, r1.Extents);

                if (sweep.Hit.DidHit)
                {
                    const float epsilon = 0.00001f;
                    sweep.Hit.Time = Math.Clamp(sweep.Hit.Time - epsilon, 0, 1);
                    sweep.Position = r1.Center + deltaR1 * sweep.Hit.Time;

                    var deltaNormalized = deltaR1;
                    deltaNormalized.Normalize();
                    sweep.Hit.Point += deltaNormalized * r1.Extents;
                }
                else
                {
                    sweep.Position = r1.Center + deltaR1;
                }
            }

            return sweep;
        }

        private static float SignFixed(float n)
        {
            return n >= 0 ? 1.0f : -1.0f;
        }
    }
}
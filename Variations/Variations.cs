using System;
using System.Linq;
using Microsoft.Xna.Framework;

namespace FieryMonkey {
    class Variations : Ifs {
        public Variations() {
            // Identity (Linear)
            Functions.Add((v) => {
                var x = v.X;
                var y = v.Y;
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Sin
            Functions.Add((v) => {
                var x = (float)Math.Sin(v.X);
                var y = (float)Math.Sin(v.Y);
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Spherical
            Functions.Add((v) => {
                var rSquared = RSquared(v);
                var x = (1f / rSquared) * v.X;
                var y = (1f / rSquared) * v.Y;
                return new Vector2(x, y);
            });
            Weights.Add(0.97f);

            // Swirl
            Functions.Add((v) => {
                var rSquared = RSquared(v);
                var rs = (float)Math.Sin(rSquared);
                var rc = (float)Math.Cos(rSquared);
                var x =  v.X * rs - v.Y * rc;
                var y = v.X * rc + v.Y * rs;
                return new Vector2(x, y);
            });
            Weights.Add(0.03f);

            // Horseshoe
            Functions.Add((v) => {
                var r = R(v);
                var x = (1f / r) * ((v.X - v.Y) * (v.X + v.Y));
                var y = (1f / r) * 2.0f * v.X * v.Y;
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Polar
            Functions.Add((v) => {
                var theta = Theta(v);
                var r = R(v);
                var x = (float)(theta / Math.PI);
                var y = r - 1f;
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Handkerchief
            Functions.Add((v) => {
                var theta = Theta(v);
                var r = R(v);
                var x = r * ((float)Math.Sin(theta + r));
                var y = r * ((float)Math.Cos(theta - r));
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Heart
            Functions.Add((v) => {
                var r = R(v);
                var theta = Theta(v);
                var x = r * (float)Math.Sin(theta * r);
                var y = r * -((float)Math.Cos(theta * r));
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Disc
            Functions.Add((v) => {
                var r = R(v);
                var theta = Theta(v);
                var x = (float)((theta / Math.PI) * (Math.Sin(Math.PI * r)));
                var y = (float)((theta / Math.PI) * (Math.Cos(Math.PI * r)));
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Spiral
            Functions.Add((v) => {
                var r = R(v);
                var theta = Theta(v);
                var x = (float)((1f / r) * (Math.Cos(theta) + Math.Sin(r)));
                var y = (float)((1f / r) * (Math.Sin(theta) - Math.Cos(r)));
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Hyperbolic
            Functions.Add((v) => {
                var r = R(v);
                var theta = Theta(v);
                var x = (float)Math.Sin(theta) / r;
                var y = r * (float)Math.Cos(theta);
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);

            // Diamond
            Functions.Add((v) => {
                var theta = Theta(v);
                var r = R(v);
                var x = (float)(Math.Sin(theta) * Math.Cos(r));
                var y = (float)(Math.Cos(theta) * Math.Sin(r));
                return new Vector2(x, y);
            });
            Weights.Add(0.0f);
        }

        private static double Theta(Vector2 v) {
            return Math.Atan2(v.Y, v.X);
        }

        private static float R(Vector2 v) {
            return v.Length();
        }

        private static float RSquared(Vector2 v) {
            return v.LengthSquared();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FieryMonkey {
    class Sierpinski : Ifs {
        public Sierpinski() {
            Functions.Add((v) => {
                var x = 0.5f * v.X;
                var y = 0.5f * v.Y;
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);

            Functions.Add((v) => {
                var x = 0.5f * v.X + 0.25f;
                var y = 0.5f * v.Y + 0.5f * ((float)Math.Sqrt(3) / 2.0f);
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);

            Functions.Add((v) => {
                var x = 0.5f * v.X + 0.5f;
                var y = 0.5f * v.Y;
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);

            // Sin
            FinalTransformation = (v) => {
                var rSquared = 1.0f / v.LengthSquared();
                var x = rSquared * v.X * 0.1f;
                var y = rSquared * v.Y * 0.1f;
                return new Vector2(x, y);
            };
        }
    }
}

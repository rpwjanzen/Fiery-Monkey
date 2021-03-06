﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FieryMonkey {
    class Fern : Ifs {
        public Fern() {
            Functions.Add((v) => {
                return new Vector2(0f, 0.16f * v.Y);
            });
            Weights.Add(0.01f);

            Functions.Add((v) => {
                var x = 0.2f * v.X - 0.26f * v.Y;
                var y = 0.23f * v.X + 0.22f * v.Y + 1.6f;
                return new Vector2(x, y);
            });
            Weights.Add(0.07f);

            Functions.Add((v) => {
                var x = -0.15f * v.X + 0.28f * v.Y;
                var y = 0.26f * v.X + 0.24f * v.Y + 0.44f;
                return new Vector2(x, y);
            });
            Weights.Add(0.07f);

            Functions.Add((v) => {
                var x = 0.85f * v.X + 0.04f * v.Y;
                var y = -0.04f * v.X + 0.85f * v.Y + 1.6f;
                return new Vector2(x, y);
            });
            Weights.Add(0.85f);

            Iterations = 50;

            MinX = -4f;
            MaxX = 4f;
            MinY = -1f;
            MaxY = 11f;

            // Spherical
            //FinalTransformation = (v) => {
            //    var rSquared = 1.0f / v.LengthSquared();
            //    var x = rSquared * v.X * 10f;
            //    var y = rSquared * v.Y * 10f;
            //    return new Vector2(x, y);
            //};
        }
    }
}

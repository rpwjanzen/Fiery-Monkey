using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FieryMonkey {
    class ToyIFS : IFS {
        public ToyIFS() {
            Functions.Add((v) => {
                var x = (float)Math.Sin(v.X);
                var y = (float)Math.Sin(0.5f * v.Y);
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);
            Functions.Add((v) => {
                var x = (float)Math.Sin(v.X);
                var y = (float)Math.Sin(0.5f * v.Y);
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);
            Functions.Add((v) => {
                var x = (float)Math.Sin(v.X);
                var y = (float)Math.Sin(0.5f * v.Y);
                return new Vector2(x, y);
            });
            Weights.Add(0.33f);
        }
    }
}

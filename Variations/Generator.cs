using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FieryMonkey {
    class Generator {
        public static Ifs Generate(Random r) {
            Ifs ifs = new BaseIfs();
            int num = r.Next(2, 6);

            for (int i = 0; i < num; i++) {
                var a = r.NextDouble() - 0.5;
                var b = r.NextDouble() - 0.5;
                var c = r.NextDouble() - 0.5;

                var d = r.NextDouble() - 0.5;
                var e = r.NextDouble() - 0.5;
                var f = r.NextDouble() - 0.5;

                ifs.Functions.Add((v) => {
                    var x = (float)(a * v.X + b * v.Y + c);
                    var y = (float)(d * v.X + e * v.Y + f);
                    return new Vector2(x, y);
                });
            }

            // generate weights
            var ws = new double[num];
            double total = 0.0;
            for (int i = 0; i < num; i++) {
                ws[i] = r.NextDouble();
                total += ws[i];
            }

            // normalize
            for (int i = 0; i < ws.Length; i++) {
                ws[i] /= total;
                ifs.Weights.Add((float)ws[i]);
            }

            ifs.MaxX = 0.5f;
            ifs.MinX = -0.5f;
            ifs.MaxY = 0.5f;
            ifs.MinY = -0.5f;

            return ifs;
        }
    }
}

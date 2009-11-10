using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FieryMonkey {
    class Flame {
        Entry[,] histogram;
        const int FreqIndex = 0;
        const int ColorIndex = 1;
        Random random;
        int width;
        int height;

        Color[] colors;

        IFS IFS;
        float minX;
        float maxX;
        float minY;
        float maxY;

        public Flame(int w, int h, Random r, float minX, float maxX, float minY, float maxY) {
            width = w;
            height = h;
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;

            histogram = new Entry[width, height];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    histogram[x,y] = new Entry();
                }
            }
            random = r;
            //IFS = new Sierpinski();
            IFS = new Fern();

            colors = new Color[4];
            colors[0] = Color.Red;
            colors[1] = Color.Green;
            colors[2] = Color.Blue;
            colors[3] = Color.Yellow;
        }

        public void Solve(int times) {
            var weights = GenerateWeightedArray(IFS.Weights.ToArray());
            for (int i = 0; i < times; i++) {
                var rp = RandomPoint();
                int lastF = 0;
                for (int j = 0; j < 20; j++) {
                    lastF = Iterate(ref rp, IFS, weights);
                }
                Plot(rp, colors[lastF]);
            }
        }


        void Plot(Vector2 v, Color c) {
            var ap = ToArraySpace(v);
            if (ap.X > 0 && ap.X < width && ap.Y > 0 && ap.Y < height) {
                histogram[ap.X, ap.Y].Color = c;
            }
        }

        int Iterate(ref Vector2 rp, IFS ifs, float[] ws) {
            var fi = GenerateRandomWeightedIndex(ws);
            rp = ifs.Functions[fi](rp);
            return fi;
        }

        Vector2 RandomPoint() {
            var x = MathHelper.Lerp(minX, maxX, (float)random.NextDouble());
            var y = MathHelper.Lerp(minY, maxY, (float)random.NextDouble());
            return new Vector2(x, y);
        }

        Point ToArraySpace(Vector2 v) {
            // need v.X in range of 0.0 - 1.0
            var sx = Map(v.X, minX, maxX, 0, 1);
            var x = MathHelper.Lerp(0, width, sx);
            
            // need v.Y in range of 0.0 - 1.0
            var sy = Map(v.Y, minY, maxY, 0, 1);
            var y = MathHelper.Lerp(0, height, sy);            
            return new Point((int)x, (int)y);
        }

        Vector2 ToBiunitSpace(Point p) {
            var x = MathHelper.Lerp(minX, maxX, (p.X / width));
            var y = MathHelper.Lerp(minY, maxY, (p.Y / height));
            return new Vector2(x, y);
        }

        public Texture2D CreateTexture(GraphicsDevice gd) {
            var tex = new Texture2D(gd, width, height);
            var cs = new Color[width * height];
            for (int i = 0; i < cs.Length; i++) {
                var x = i % width;
                var y = (int)(i / width);
                cs[i] = histogram[x, y].Color;
            }
            tex.SetData<Color>(cs);
            return tex;
        }

        Color AvgColor(Color a, Color b) {
            var aa = (byte)((a.A + b.A) / 2.0);
            var ar = (byte)((a.R + b.R) / 2.0);
            var ag = (byte)((a.G + b.G) / 2.0);
            var ab = (byte)((a.B + b.B) / 2.0);
            return new Color(ar, ag, ab, aa);
        }

        static public float Map(float value,
                              float istart, float istop,
                              float ostart, float ostop) {
            return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
        }

        int GenerateRandomWeightedIndex(float[] weights) {
            var r = random.NextDouble();
            for (int i = 0; i < weights.Length; i++) {
                if (r <= weights[i]) {
                    return i;
                }
            }
            return weights.Length - 1;
        }

        float[] GenerateWeightedArray(float[] ws) {
            // get sums of weights
            var sums = new float[ws.Length];
            var total = 0.0f;
            for (int i = 0; i < ws.Length; i++) {
                total += ws[i];
                sums[i] = total;
            }

            return sums;
        }
    }
}

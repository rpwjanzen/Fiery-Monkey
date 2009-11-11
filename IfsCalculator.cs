using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FieryMonkey {
    class IfsCalculator {
        Entry[,] histogram;
        Random random;
        int width;
        int height;

        Color[] colors;

        Ifs ifs;
        float[] weights;

        public IfsCalculator(int w, int h, Random r, Ifs ifs) {
            width = w;
            height = h;
            random = r;

            histogram = new Entry[width, height];

            SetColors();

            SetIfs(ifs);
        }

        private void SetColors() {
            colors = new Color[7];
            colors[0] = Color.Red;
            colors[1] = Color.Orange;
            colors[2] = Color.Yellow;
            colors[3] = Color.Green;
            colors[4] = Color.Blue;
            colors[5] = Color.Indigo;
            colors[6] = Color.Purple;
        }

        public void SetIfs(Ifs ifs) {
            this.ifs = ifs;            
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    histogram[x, y] = new Entry();
                }
            }
            weights = GenerateWeightedArray(ifs.Weights.ToArray());
        }

        public void Solve(int pointsToDraw) {            
            for (int i = 0; i < pointsToDraw; i++) {
                var rp = RandomPoint();
                int lastF = 0;
                Color c = Color.Black;
                for (int j = 0; j < ifs.Iterations; j++) {
                    lastF = Iterate(ref rp, ifs, weights);

                    if (j == 0) {
                        c = colors[lastF % colors.Length];
                    } else {
                        c = AvgColor(c, colors[lastF % colors.Length]);
                    }
                }
                rp = ifs.FinalTransformation(rp);
                Plot(rp, c);
            }
        }

        void Plot(Vector2 v, Color c) {
            var ap = ToArraySpace(v);
            if (ap.X > 0 && ap.X < width && ap.Y > 0 && ap.Y < height) {
                histogram[ap.X, ap.Y].Color = c;
                histogram[ap.X, ap.Y].Frequency++;
            }
        }

        int Iterate(ref Vector2 rp, Ifs ifs, float[] ws) {
            var fi = GenerateRandomWeightedIndex(ws);
            rp = ifs.Functions[fi](rp);
            return fi;
        }

        Vector2 RandomPoint() {
            var x = MathHelper.Lerp(ifs.MinX, ifs.MaxX, (float)random.NextDouble());
            var y = MathHelper.Lerp(ifs.MinY, ifs.MaxY, (float)random.NextDouble());

            return new Vector2(x, y);
        }

        Point ToArraySpace(Vector2 v) {
            // need v.X in range of 0.0 - 1.0
            var sx = Map(v.X, ifs.MinX, ifs.MaxX, 0, 1);
            var x = MathHelper.Lerp(0, width, sx);
            
            // need v.Y in range of 0.0 - 1.0
            var sy = Map(v.Y, ifs.MinY, ifs.MaxY, 0, 1);
            var y = MathHelper.Lerp(0, height, sy);            
            return new Point((int)x, (int)y);
        }

        Vector2 ToBiunitSpace(Point p) {
            var x = MathHelper.Lerp(ifs.MinX, ifs.MaxX, (p.X / width));
            var y = MathHelper.Lerp(ifs.MinY, ifs.MaxY, (p.Y / height));
            return new Vector2(x, y);
        }

        public Texture2D CreateTexture(GraphicsDevice gd) {
            var tex = new Texture2D(gd, width, height);
            SetTexture(tex);
            return tex;
        }

        public void SetTexture(Texture2D tex) {
            var cs = new Color[width * height];
            var minF = 0;
            var maxF = 0;
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    minF = Math.Min(minF, histogram[x, y].Frequency);
                    maxF = Math.Max(maxF, histogram[x, y].Frequency);
                }
            }
            var minL = minF == 0 ? 0 : Math.Log(minF);
            var maxL = Math.Log(maxF);

            for (int i = 0; i < cs.Length; i++) {
                var x = i % width;
                var y = (int)(i / width);

                var maybeColor = histogram[x, y].Color;
                if (maybeColor.HasValue) {
                    var alpha = (byte)Map(Math.Log(histogram[x, y].Frequency), minL, maxL, 0, 255);
                    var hc = maybeColor.Value;
                    hc.A = alpha;
                    cs[i] = hc;
                } else {
                    cs[i] = Color.Black;
                }
            }
            tex.SetData<Color>(cs);
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

        static public double Map(double value,
                      double istart, double istop,
                      double ostart, double ostop) {
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

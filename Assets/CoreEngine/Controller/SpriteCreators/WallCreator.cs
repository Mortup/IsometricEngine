using UnityEngine;

using com.gStudios.isometric.controller.config;
using com.gStudios.isometric.controller.isometricTransform;
using com.gStudios.isometric.model.world.wall;

namespace com.gStudios.isometric.controller.spriteCreators {

    public static class WallCreator {

        static Color borderColor = Color.black;

        private static Vector2Int FrontTopZ0 = new Vector2Int(4, 111);
        private static Vector2Int FrontBottomZ0 = new Vector2Int(4, 0);
        private static Vector2Int LeftTopZ0 = new Vector2Int(FrontTopZ0.x - 4, FrontTopZ0.y + 2);
        private static Vector2Int LeftBottomZ0 = new Vector2Int(FrontBottomZ0.x - 4, FrontBottomZ0.y + 2);
        private static Vector2Int RightTopZ0 = new Vector2Int(37, 127);
        private static Vector2Int RightBottomZ0 = new Vector2Int(37, 16);
        private static Vector2Int BackTopZ0 = new Vector2Int(33, 129);

        public static Sprite DrawSpriteBorders(Sprite spr, int z, InmediateWallNeighbors neighbors) {

            Texture2D tex = new Texture2D(spr.texture.width, spr.texture.height, spr.texture.format, Settings.mipmapEnabled);
            tex.filterMode = Settings.filterMode;
            tex.wrapMode = Settings.wrapMode;

            Graphics.CopyTexture(spr.texture, tex);

            DrawBorders(tex, z, neighbors);

            tex.Apply();

            Sprite end = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Settings.wallPivot, Settings.PPU);

            return end;
        }

        private static void DrawBorders(Texture2D tex, int z, InmediateWallNeighbors neighbors) {
            if (z == 1) {
                FlipTex(tex);
                neighbors.FlipSideNeighbors();
            }

            DrawIsoLine(tex, borderColor, FrontBottomZ0, RightBottomZ0);
            DrawIsoLine(tex, borderColor, FrontTopZ0, RightTopZ0);
            DrawIsoLine(tex, borderColor, new Vector2Int(LeftTopZ0.x + 2, LeftTopZ0.y + 1), new Vector2Int(BackTopZ0.x - 2, BackTopZ0.y - 1));

            if (neighbors.Bottom.IsEmpty()) {

                if (neighbors.BottomLeft.IsEmpty()) {

                    DrawVerticalLine(tex, borderColor, LeftBottomZ0, LeftTopZ0);
                    DrawVerticalLine(tex, borderColor, FrontBottomZ0, FrontTopZ0);

                    if (neighbors.BottomRight.IsEmpty()) {
                        DrawIsoLine(tex, borderColor, new Vector2Int(FrontTopZ0.x + 1, FrontTopZ0.y), LeftTopZ0);
                        DrawIsoLine(tex, borderColor, new Vector2Int(FrontBottomZ0.x + 1, FrontBottomZ0.y), LeftBottomZ0);
                    }
                }
                else {
                }
            }

            if (neighbors.Top.IsEmpty()) {
                DrawVerticalLine(tex, borderColor, RightBottomZ0, RightTopZ0);
            }

            if (neighbors.TopLeft.IsEmpty()) {
                DrawIsoLine(tex, borderColor, BackTopZ0, new Vector2Int(BackTopZ0.x-1, BackTopZ0.y));
            }

            if (neighbors.Top.IsEmpty() && neighbors.TopLeft.IsEmpty() && neighbors.TopRight.IsEmpty()) {
                DrawIsoLine(tex, borderColor, new Vector2Int(BackTopZ0.x + 1, BackTopZ0.y - 1), new Vector2Int(BackTopZ0.x + 2, BackTopZ0.y - 1));
            }

            if (neighbors.TopRight.IsEmpty() == false) {
                DrawVerticalLine(tex, Color.clear, new Vector2Int(tex.width - 1, 0), new Vector2Int(tex.width - 1, tex.height));
                DrawVerticalLine(tex, Color.clear, new Vector2Int(tex.width - 2, 0), new Vector2Int(tex.width - 2, tex.height));
                DrawVerticalLine(tex, Color.clear, new Vector2Int(tex.width - 3, 0), new Vector2Int(tex.width - 3, tex.height));
                DrawVerticalLine(tex, borderColor, new Vector2Int(RightTopZ0.x - 3, RightTopZ0.y - 2), new Vector2Int(RightBottomZ0.x - 3, RightBottomZ0.y));
            }

            if (z == 1) {
                FlipTex(tex);
                neighbors.FlipSideNeighbors();
            }
        }

        private static void DrawIsoLine(Texture2D srcTex, Color color, Vector2Int start, Vector2Int end) {
            int x = start.x;
            int y = start.y;

            for (int i = 0; i <= Mathf.Abs(start.x - end.x); i++) {
                srcTex.SetPixel(x, Mathf.FloorToInt(y), color);

                if (start.x < end.x)
                    x++;
                else
                    x--;

                if (i % 2 == 1) {
                    if (start.y < end.y)
                        y++;
                    else
                        y--;
                }
            }

            if (y > Mathf.Max(start.y, end.y) + 1)
                Debug.LogError("Trying to draw non-isometric line");
        }

        private static void DrawVerticalLine(Texture2D srcTex, Color color, Vector2Int start, Vector2Int end) {
            if (start.x == end.x) {
                for (int y = Mathf.Min(start.y, end.y); y <= Mathf.Max(start.y, end.y); y++) {
                    srcTex.SetPixel(start.x, y, color);
                }
            }
            else {
                Debug.LogError("Trying to draw non-vertical line");
            }
        }

        private static void FlipTex(Texture2D tex) {
            for (int y = 0; y < tex.height; y++) {
                for (int x = 0; x < tex.width / 2; x++) {
                    Color buffer = tex.GetPixel(x, y);
                    tex.SetPixel(x, y, tex.GetPixel(tex.width - 1 - x, y));
                    tex.SetPixel(tex.width - 1 - x, y, buffer);
                }
            }            
        }

    }
}
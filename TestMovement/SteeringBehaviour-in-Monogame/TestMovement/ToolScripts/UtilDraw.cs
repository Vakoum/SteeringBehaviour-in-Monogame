using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestMovement
{
	public static class UtilDraw
	{
        public static void DrawLineBetween(
        SpriteBatch spriteBatch,
        Vector2 startPos,
        Vector2 endPos,
        int thickness,
        Color color)
        {
            // Create a texture as wide as the distance between two points and as high as
            // the desired thickness of the line.
            var distance = (int)Vector2.Distance(startPos, endPos);
            var texture = new Texture2D(spriteBatch.GraphicsDevice, distance, thickness);

            // Fill texture with given color.
            var data = new Color[distance * thickness];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = color;
            }
            texture.SetData(data);

            // Rotate about the beginning middle of the line.
            var rotation = (float)Math.Atan2(endPos.Y - startPos.Y, endPos.X - startPos.X);
            var origin = new Vector2(0, thickness / 2);

            spriteBatch.Draw(
                texture,
                startPos,
                null,
                Color.Black,
                rotation,
                origin,
                1.0f,
                SpriteEffects.None,
                1.0f);
        }
    }

}

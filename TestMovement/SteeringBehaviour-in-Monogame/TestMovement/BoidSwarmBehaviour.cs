using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestMovement
{
	public class BoidSwarmBehaviour
	{
		SpriteBatch _spriteBatch;
		public readonly Texture2D boidTexture;
		MouseState mouseState;

        List<Boid> boidSwarm;

		private float boidAvoidDist = 50;

		public BoidSwarmBehaviour(SpriteBatch _spriteBatch, Texture2D boidTexture)
		{
			this._spriteBatch = _spriteBatch;
			this.boidTexture = boidTexture;
		}
		public void CreateBoids()
		{
			boidSwarm = new List<Boid>();
			for (int x = 0; x < 5; x++)
			{
				for (int y = 0; y < 3; y++)
				{
					Boid boid = new Boid(new Vector2(x * 100, y * 100), 10f, Vector2.Zero); 
					boidSwarm.Add(boid);
				}
			}
		}
		public void MoveBoidSwarm() 
		{
			foreach(Boid boid in boidSwarm)
			{
				boid.MoveBoid();
			}
		}
		public void BoidsFollowMouse()
		{
            mouseState = Mouse.GetState();
            foreach (Boid boid in boidSwarm)
			{
				boid.FollowBehaviour(new Vector2(mouseState.X, mouseState.Y), 0.5f);
			}
		}
		public void BoidsAvoidBoids()
		{
			foreach(Boid currentBoid in boidSwarm)
			{
				foreach (Boid comparedBoid in boidSwarm)
				{
					if (currentBoid == comparedBoid) continue;
					if (Vector2.Distance(comparedBoid.boidPos, currentBoid.boidPos) > boidAvoidDist) continue;
						
					currentBoid.AvoidBehaviour(comparedBoid.boidPos, 0.1f);
					

				}
			}
		}
		public void DrawBoidSwarm()
		{
			foreach(Boid boid in boidSwarm)
			{
				boid.DrawBoid(_spriteBatch, boidTexture);
			}
		}

	}
}

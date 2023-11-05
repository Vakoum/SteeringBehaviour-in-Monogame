using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TestMovement
{
	public class Boid
	{
		public Vector2 boidPos { get; private set; }
		private float maxSpeed;
		private Vector2 currentVelocity;
		private List<Vector2> steeringForces;


		public Boid(Vector2 boidPos, float maxSpeed, Vector2 currentVelocity)
		{
			this.boidPos = boidPos;
			this.maxSpeed = maxSpeed;
			this.currentVelocity = currentVelocity;
			steeringForces = new List<Vector2>();

		}
		public void FollowBehaviour(Vector2 followPos, float forceStrength)
		{
			Vector2 followDir = followPos - boidPos;
			followDir.Normalize();
			AddForce(followDir * forceStrength);
		}
		public void AvoidBehaviour(Vector2 avoidPos, float forceStrength)
		{
			// formula for avoidung
            Vector2 avoidDir = boidPos - avoidPos;
            avoidDir.Normalize();
            AddForce(avoidDir * forceStrength);
        }

		public void AddForce(Vector2 force)
		{
			steeringForces.Add(force);
		}
		public void MoveBoid()
		{
			Vector2 force = new Vector2(0, 0);
			foreach(Vector2 steeringForce in steeringForces)
			{
				force += steeringForce;
			}
			// add forces and clamp speed
			currentVelocity += force;
			steeringForces.Clear();
			float currentVelocityLength = MathHelper.Clamp(currentVelocity.Length(),0, maxSpeed);
			currentVelocity.Normalize();
			currentVelocity = currentVelocity * currentVelocityLength;

			// add velocity to current pos
			boidPos += currentVelocity;
		}
		public void DrawBoid(SpriteBatch _spriteBatch, Texture2D boidSprite)
		{
			Vector2 boidSpriteOrigin = new Vector2(boidSprite.Width / 2f, boidSprite.Height / 2f);
			// makes it drawn at the origin
			Vector2 drawPos = boidPos - boidSpriteOrigin;
			float boidRot = (float)Math.Atan2(currentVelocity.Y, currentVelocity.X) + MathHelper.ToRadians(90);
            _spriteBatch.Draw(boidSprite, drawPos, null, Color.White, boidRot, boidSpriteOrigin, 1.0f, SpriteEffects.None, 1f);
        }
    }

}

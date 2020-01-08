using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleResponsibilityShapesAfter
{
	using Contracts;

	public class DrawableRectangle
	{
		private Rectangle rect;

		public DrawableRectangle(Rectangle rectangle)
		{
			this.rect = rectangle;
		}

		public void Draw(IRenderer render, IDrawingContext context)
		{
			//code here

		}


	}
}

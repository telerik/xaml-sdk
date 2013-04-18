using System;
using System.Windows;
using Telerik.Windows.Diagrams.Core;
using Telerik.Windows.Controls;

namespace CustomServices
{
	public class MyRotation : RotationService
	{
		private int rotationStep;

		public MyRotation(RadDiagram owner)
			: base(owner as IGraphInternal)
		{
			this.RotationStep = 1;
		}

		public int RotationStep
		{
			get
			{
				return this.rotationStep;
			}
			set
			{
				this.rotationStep = value;
			}
		}

		protected override double CalculateRotationAngle(Point newPoint)
		{
			var angle = base.CalculateRotationAngle(newPoint);
			return angle = Math.Floor(angle / this.RotationStep) * this.RotationStep;
		}
	}
}

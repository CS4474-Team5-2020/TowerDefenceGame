using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{
	public int X { get; set; }

	public float Y { get; set; }

	public int Z { get; set; }

	public Point(int x, float y, int z)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
	}

}

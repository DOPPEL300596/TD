//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;

/* 	This struct is vital, as it will be used for entire game.
 	It is also connected to pathfinding algorithm.*/
public struct Point
{
	/* Properties */

	public int X{ get; set; } /* Vertical */
	public int Y{ get; set; } /* Horizontal */

	/* Constructor - for instantiation */
	public Point(int x, int y){
		X = x;
		Y = y;
	}

	/* Defines relational operator so it can be used for this class */
	public static bool operator == (Point first, Point second){
		return (first.X == second.X) && (first.Y == second.Y);
	}

	public static bool operator != (Point first, Point second){
		return (first.X != second.X) || (first.Y != second.Y);
	}

	public static Point operator - (Point x, Point y){
		return new Point(x.X - y.X, x.Y - y.Y);
	}

}

/**
	Difference between struct and class.

	Struct is basically a lightweight class and usually embedded to other class(es) a lot. 
	Sometimes it's hard to decide whether class or struct will be used. 

	Use struct if:
	- used a lot in a program in lots of places as well
	- have a short live (easily destructible)
	- embedded in another class

	The rest is theory, which is very useful in final assignment.

	Class is reference type, so its value can be set using procedure.
	Struct is a value type, so its value cannot be changed using procedure.
 */
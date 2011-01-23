/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 23/02/2009
 * Time: 15:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace CyclicSimulatuin
{
	/// <summary>
	/// Description of Well.
	/// </summary>
	public struct Well
	{
		public double NumberOfNormal;
		public double NumberOfResistant;	
		public double NumberOfPersistent;
		public Well (double _NumberOfNormal,double _NumberOfPersistent, double _NumberOfResistant)
		{
			NumberOfNormal = _NumberOfNormal;
			NumberOfPersistent = _NumberOfPersistent;
			NumberOfResistant = _NumberOfResistant;
		}
		
		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<Struct1>" declaration.
		
		public override bool Equals(object obj)
		{
			if (obj is Well)
				return Equals((Well)obj); // use Equals method below
			else
				return false;
		}
		
		public bool Equals(Well w)
		{
			// add comparisions for all members here
			return this.NumberOfNormal==w.NumberOfNormal&&this.NumberOfPersistent==w.NumberOfPersistent&&this.NumberOfResistant==w.NumberOfResistant;
		}
		
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return this.NumberOfNormal.GetHashCode()^this.NumberOfPersistent.GetHashCode()^this.NumberOfResistant.GetHashCode();
		}
		
		public static bool operator ==(Well lhs, Well rhs)
		{
			return lhs.Equals(rhs);
		}
		
		public static bool operator !=(Well lhs, Well rhs)
		{
			return !(lhs.Equals(rhs)); // use operator == and negate result
		}
		#endregion
		
		public double NumberOfBacteria {
			get { return NumberOfNormal+NumberOfResistant+NumberOfPersistent; }
		}
			
		
	}
}

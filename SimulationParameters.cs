/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 23/01/2011
 * Time: 16:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace CyclicSimulation
{
	/// <summary>
	/// Description of SimulationParameters.
	/// </summary>
	public struct SimulationParameters : IEquatable<SimulationParameters>
	{
		public double NfGrow;
		public double NfKill;
		public double MutationRatio;
		public double tauNormalKill;
		public double tauPersisterKill;
		public double tauGrowNormal;
		public double tauGrowResistant;
		public double Persistersfraction;
		
		public SimulationParameters(
			double _NfGrow,
			double _NfKill,
			double _MutationRatio,
			double _tauNormalKill,
			double _tauPersisterKill,
			double _tauGrowNormal,
			double _tauGrowResistant,
			double _Persistersfraction)
		{
			
			NfGrow = _NfGrow;
			NfKill=_NfKill;
			MutationRatio =  _MutationRatio;
			tauNormalKill=_tauNormalKill;
			tauPersisterKill=_tauPersisterKill;
			tauGrowNormal=_tauGrowNormal;
			tauGrowResistant= _tauGrowResistant;
			Persistersfraction=_Persistersfraction;
				
		}
		
		
		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<SimulationParameters>" declaration.
		
		public override bool Equals(object obj)
		{
			if (obj is SimulationParameters)
				return Equals((SimulationParameters)obj); // use Equals method below
			else
				return false;
		}
		
		public bool Equals(SimulationParameters other)
		{
			// add comparisions for all members here
			return this.NfGrow == other.NfGrow &
				this.NfKill == other.NfKill &
				this.MutationRatio == other.MutationRatio &
				this.tauNormalKill == other.tauNormalKill &
				this.tauPersisterKill == other.tauPersisterKill &
				this.tauGrowNormal == other.tauGrowNormal &
				this.tauGrowResistant == other.tauGrowResistant &
				this.Persistersfraction == other.Persistersfraction ;
		}
		
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return NfGrow.GetHashCode()^
				NfKill.GetHashCode()^
				MutationRatio.GetHashCode()^
				tauNormalKill.GetHashCode()^
				tauPersisterKill.GetHashCode()^
				tauGrowNormal.GetHashCode()^
				tauGrowResistant.GetHashCode()^
				Persistersfraction.GetHashCode();
		}
		
		public static bool operator ==(SimulationParameters left, SimulationParameters right)
		{
			return left.Equals(right);
		}
		
		public static bool operator !=(SimulationParameters left, SimulationParameters right)
		{
			return !left.Equals(right);
		}
		#endregion
		
		
		
		
		
	}
}

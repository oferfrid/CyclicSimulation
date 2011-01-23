/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 22/02/2009
 * Time: 10:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace CyclicSimulatuin
{
	/// <summary>
	/// Description of Simulation.
	/// </summary>
	public class Simulation
	{
		
		
		public Well SimulatedWall;
		public double Nf;
		public double MutationRatio;
		public double tauNormalKill ;
		public double tauPersisterKill;
		public double tauGrowNormal ;
		public double tauGrowResistent ;
		public double Persistersfraction ;
		
		
		public int Cycle;
		
		
		public Simulation(Well _SimulatedWall,int seed,double _Nf,double _MutationRatio,double _tauNormalKill,double _tauPersisterKill ,double _tauGrowNormal,double _tauGrowResistent ,double _Persistersfraction)
		{
			Utils.Init(seed);
			Nf=_Nf;
			
			SimulatedWall = _SimulatedWall;
			
			MutationRatio = _MutationRatio;
			tauNormalKill = _tauNormalKill;
			tauPersisterKill = _tauPersisterKill;
			tauGrowNormal = _tauGrowNormal;
			tauGrowResistent=_tauGrowResistent;
			Persistersfraction = _Persistersfraction;
			Cycle=0;
		}
		
		public Well DoSycle()
		{
			Cycle++;
			SimulatedWall=DoDilution(SimulatedWall,100);
			SimulatedWall=DoAMPKilling(SimulatedWall,260);
			SimulatedWall=DoGrowing(SimulatedWall);
			//SimulatedWall=DoDilution(SimulatedWall,200);
			//SimulatedWall=DoGrowing(SimulatedWall);
			
			return SimulatedWall;
		}
		
		
		
		
		public Well DoAMPKilling(Well _SimulatedWall,double Time)
		{
			
			double PNormalLive = Math.Pow(2,-Time/tauNormalKill);
			double PPersisterLive = Math.Pow(2,-Time/tauPersisterKill);
			
			double  NumberOfNormalLive = Utils.RandBinomial(_SimulatedWall.NumberOfNormal,PNormalLive);
			double  NumberOfPersisterLive = Utils.RandBinomial(_SimulatedWall.NumberOfPersistent,PPersisterLive);
			
			_SimulatedWall.NumberOfNormal =NumberOfNormalLive;
			_SimulatedWall.NumberOfPersistent = NumberOfPersisterLive;
			
			_SimulatedWall.NumberOfResistant = Utils.NLogistic(Time,tauGrowResistent, _SimulatedWall.NumberOfResistant,Nf - NumberOfNormalLive - NumberOfPersisterLive);
			SimulatedWall = _SimulatedWall;
			return _SimulatedWall;
		}
		
		public  Well DoGrowing(Well _SimulatedWall)
		{
			_SimulatedWall.NumberOfNormal += _SimulatedWall.NumberOfPersistent;
			_SimulatedWall.NumberOfPersistent = 0;
			
			double maxNumberOfNormalDivitions = (Nf - _SimulatedWall.NumberOfBacteria);
			int maxNumberOfMutations = Convert.ToInt32(Utils.RandBinomial(maxNumberOfNormalDivitions,MutationRatio));

			double[] genOfMutation ;
			genOfMutation = new double[maxNumberOfMutations] ;
			
			for(int i=0;i<maxNumberOfMutations;i++)
			{
				//find the actual generation of the mutation.
				genOfMutation[i] = Utils.RandExponantial(Nf,_SimulatedWall.NumberOfNormal);
			}
			
			Array.Sort(genOfMutation);
			
			double[] genOfMutationDiff ;
			genOfMutationDiff = new double[maxNumberOfMutations] ;
			if(maxNumberOfMutations>0)
			{
				genOfMutationDiff[0]=genOfMutation[0];
			}
			for(int i=1;i<maxNumberOfMutations;i++)
			{
				genOfMutationDiff[i]=genOfMutation[i]-genOfMutation[i-1];
			}
			
			double ResistentgenRatio = tauGrowResistent/tauGrowNormal ;
			int j=0;
			do
			{
				Solver.N1 = _SimulatedWall.NumberOfNormal;
				Solver.N2 = _SimulatedWall.NumberOfResistant;
				Solver.Nf = Nf;
				Solver.gen2retio = ResistentgenRatio;
				
				double gen=(Solver.rtsafe(Solver.Function,Solver.dFunction,0,30,0.0001));
				//Console.WriteLine("gen={0} F(gen={0})={1}",gen,Solver.Function(gen));
				if(	j>=maxNumberOfMutations || genOfMutation[j]>=gen)
				{
					_SimulatedWall.NumberOfNormal = Utils.NExponantial(gen,_SimulatedWall.NumberOfNormal);
					_SimulatedWall.NumberOfResistant = Utils.NExponantial(gen*ResistentgenRatio,_SimulatedWall.NumberOfResistant);
				}
				else
				{
					_SimulatedWall.NumberOfNormal = Utils.NExponantial(genOfMutationDiff[j],_SimulatedWall.NumberOfNormal)-1;
					_SimulatedWall.NumberOfResistant = Utils.NExponantial(genOfMutationDiff[j]*ResistentgenRatio,_SimulatedWall.NumberOfResistant)+1;
					
				}
				
				j++;
			}
			while(_SimulatedWall.NumberOfBacteria<Nf);
			
//			for(int i=0;i<maxNumberOfMutations;i++)
//			{
//				Well PostMutation;
//				PostMutation.NumberOfNormal = Utils.NExponantial(genOfMutationDiff[i],_SimulatedWall.NumberOfNormal)-1;
//				PostMutation.NumberOfResistant = Utils.NExponantial(genOfMutationDiff[i]*ResistentgenRatio,_SimulatedWall.NumberOfResistant)+1;
//				if (PostMutation.NumberOfBacteria < Nf)
//				{
//					_SimulatedWall = PostMutation;
//				}
//				else
//				{
//					Solver.N1 = _SimulatedWall.NumberOfNormal;
//					Solver.N2 = _SimulatedWall.NumberOfResistant;
//					Solver.Nf = Nf;
//					Solver.gen2retio = ResistentgenRatio;
//
//					double gen=(Solver.rtsafe(Solver.Function,Solver.dFunction,1,30,0.0001));
//					Console.WriteLine("gen={0} F(gen={0})={1}",gen,Solver.Function(gen));
//
//					_SimulatedWall.NumberOfNormal = Utils.NExponantial(gen,_SimulatedWall.NumberOfNormal);
//					_SimulatedWall.NumberOfResistant = Utils.NExponantial(gen*ResistentgenRatio,_SimulatedWall.NumberOfResistant);
//					break;
//				}
//			}
			
			_SimulatedWall.NumberOfPersistent = Persistersfraction*_SimulatedWall.NumberOfNormal;
			_SimulatedWall.NumberOfNormal -=_SimulatedWall.NumberOfPersistent;
				
			SimulatedWall = _SimulatedWall;
			return _SimulatedWall;
			
		}

		public  Well DoDilution(Well _SimulatedWall,double Ratio)
		{
			_SimulatedWall.NumberOfNormal =  Utils.RandBinomial(_SimulatedWall.NumberOfNormal,(double)1/Ratio);
			_SimulatedWall.NumberOfResistant= Utils.RandBinomial(_SimulatedWall.NumberOfResistant,(double)1/Ratio);
			SimulatedWall = _SimulatedWall;
			if (_SimulatedWall.NumberOfNormal + _SimulatedWall.NumberOfResistant ==0)
			{
				System.Diagnostics.Debug.WriteLine("");
			}
			return _SimulatedWall;
		}

	}
	
}

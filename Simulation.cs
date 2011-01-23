/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 22/02/2009
 * Time: 10:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace CyclicSimulation
{
	/// <summary>
	/// Description of Simulation.
	/// </summary>
	public class Simulation
	{
		
		
		public Well SimulatedWall;
		public SimulationParameters SP;
		
		
		public int Cycle;
		
		
		public Simulation(Well _SimulatedWall,
		                  int seed, 
		                  SimulationParameters _SP)
		{
			Utils.Init(seed);
			
			
			SimulatedWall = _SimulatedWall;
			SP = _SP;
			
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
			
			double PNormalLive = Math.Pow(2,-Time/SP.tauNormalKill); 
			double PPersisterLive = Math.Pow(2,-Time/SP.tauPersisterKill); 
			
			double  NumberOfNormalLive = Utils.RandBinomial(_SimulatedWall.NumberOfNormal,PNormalLive);
			double  NumberOfPersisterLive = Utils.RandBinomial(_SimulatedWall.NumberOfPersistent,PPersisterLive);
			
			_SimulatedWall.NumberOfNormal =NumberOfNormalLive;
			_SimulatedWall.NumberOfPersistent = NumberOfPersisterLive;
			
			_SimulatedWall.NumberOfResistant = Utils.NLogistic(Time,SP.tauGrowResistant, _SimulatedWall.NumberOfResistant,SP.NfKill - (NumberOfNormalLive + NumberOfPersisterLive));
			SimulatedWall = _SimulatedWall;
			return _SimulatedWall;
		}
		/// <summary>
		/// Grow well in the Well
		/// </summary>
		/// <param name="_SimulatedWall">Thew Well parameters to grow</param>
		/// <returns>Well after growing stage</returns>
		public  Well DoGrowing(Well _SimulatedWall)
		{
		//TODO: Change to persisters exponantial statistics.
			_SimulatedWall.NumberOfNormal += _SimulatedWall.NumberOfPersistent;
			_SimulatedWall.NumberOfPersistent = 0;
			
			double maxNumberOfNormalDivitions = (SP.NfGrow - _SimulatedWall.NumberOfBacteria);
			
			//TODO: add random generation of the generation of Each Persister -> Normal state convertion.
			int maxNumberOfMutations = Convert.ToInt32(Utils.RandBinomial(maxNumberOfNormalDivitions,SP.MutationRatio));

			double[] genOfMutation ;
			genOfMutation = new double[maxNumberOfMutations] ;
			
			for(int i=0;i<maxNumberOfMutations;i++)
			{
				//find the actual generation of the mutation.
				genOfMutation[i] = Utils.RandExponantial(SP.NfGrow,_SimulatedWall.NumberOfNormal);
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
			
			double ResistantgenRatio = SP.tauGrowResistant/SP.tauGrowNormal ;
			int j=0;
			do
			{
				Solver.N1 = _SimulatedWall.NumberOfNormal;
				Solver.N2 = _SimulatedWall.NumberOfResistant;
				Solver.Nf = SP.NfGrow;
				Solver.gen2retio = ResistantgenRatio;
				
				double gen=(Solver.rtsafe(Solver.Function,Solver.dFunction,0,30,0.0001));
				//Console.WriteLine("gen={0} F(gen={0})={1}",gen,Solver.Function(gen));
				if(	j>=maxNumberOfMutations || genOfMutation[j]>=gen)
				{
					_SimulatedWall.NumberOfNormal = Utils.NExponantial(gen,_SimulatedWall.NumberOfNormal);
					_SimulatedWall.NumberOfResistant = Utils.NExponantial(gen*ResistantgenRatio,_SimulatedWall.NumberOfResistant);
				}
				else
				{
					_SimulatedWall.NumberOfNormal = Utils.NExponantial(genOfMutationDiff[j],_SimulatedWall.NumberOfNormal)-1;
					_SimulatedWall.NumberOfResistant = Utils.NExponantial(genOfMutationDiff[j]*ResistantgenRatio,_SimulatedWall.NumberOfResistant)+1;
					
				}
				
				j++;
			}
			while(_SimulatedWall.NumberOfBacteria<SP.NfGrow);
			
//			for(int i=0;i<maxNumberOfMutations;i++)
//			{
//				Well PostMutation;
//				PostMutation.NumberOfNormal = Utils.NExponantial(genOfMutationDiff[i],_SimulatedWall.NumberOfNormal)-1;
//				PostMutation.NumberOfResistant = Utils.NExponantial(genOfMutationDiff[i]*ResistantgenRatio,_SimulatedWall.NumberOfResistant)+1;
//				if (PostMutation.NumberOfBacteria < Nf)
//				{
//					_SimulatedWall = PostMutation;
//				}
//				else
//				{
//					Solver.N1 = _SimulatedWall.NumberOfNormal;
//					Solver.N2 = _SimulatedWall.NumberOfResistant;
//					Solver.Nf = Nf;
//					Solver.gen2retio = ResistantgenRatio;
//
//					double gen=(Solver.rtsafe(Solver.Function,Solver.dFunction,1,30,0.0001));
//					Console.WriteLine("gen={0} F(gen={0})={1}",gen,Solver.Function(gen));
//
//					_SimulatedWall.NumberOfNormal = Utils.NExponantial(gen,_SimulatedWall.NumberOfNormal);
//					_SimulatedWall.NumberOfResistant = Utils.NExponantial(gen*ResistantgenRatio,_SimulatedWall.NumberOfResistant);
//					break;
//				}
//			}
			
			//init the number of persisters from the known fraction.
			_SimulatedWall.NumberOfPersistent = SP.Persistersfraction*_SimulatedWall.NumberOfNormal;
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

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
		public double tau ;
		public double AMPNf;
		
		public Simulation(double N0,double _Nf,double _AMPNf)
		{
			Utils.Init();
			SimulatedWall.NumberOfNormal = N0;
			SimulatedWall.NumberOfResistant = 0;
			Nf=_Nf;
			AMPNf = _AMPNf;
			MutationRatio = 1e-10;
			tau = 20 / Math.Log(2);
		}
		
		public Well DoSycle()
		{
			DoDilution(200);
			DoAMPKilling(300);
			DoGrowing();
			
			return SimulatedWall;
		}
		
		public Well DoAMPKilling(double Time)
		{
			double Pkill = 1-(double)AMPNf/SimulatedWall.NumberOfNormal;
			double  NumberOfNormalKilled = Utils.RandBinomial(SimulatedWall.NumberOfNormal,Pkill);
			SimulatedWall.NumberOfNormal -= Convert.ToInt32(NumberOfNormalKilled);
			SimulatedWall.NumberOfResistant = Utils.NLogistic(Time,tau, SimulatedWall.NumberOfResistant,Nf);
			
			return SimulatedWall;
		}
		
		public Well DoGrowing()
		{
			double NumberOfDivitions = Nf - SimulatedWall.NumberOfBacteria;
			double NumberOfGrowingResistant  = (SimulatedWall.NumberOfResistant/SimulatedWall.NumberOfBacteria)*Nf;
			
			//TODO:Only on normal bacterua;
			int NumberOfMutations = Convert.ToInt32(Utils.RandBinomial(NumberOfDivitions,MutationRatio ));
			
			
			double FinalNumberOfResistence =0;
				
			for(int i=0;i<NumberOfMutations;i++)
			{
				//find the time of the mutation.
				double TimeOfMutation = Utils.RandLogistic(tau,SimulatedWall.NumberOfBacteria,Nf,1000);
				double NumberOfBacteriaAtTime =  Utils.NLogistic(TimeOfMutation, tau, SimulatedWall.NumberOfBacteria ,Nf);
				double FinalNumberOfResistencePerMutation = ((double)1/NumberOfBacteriaAtTime)*Nf;
				FinalNumberOfResistence += FinalNumberOfResistencePerMutation;
			}
			SimulatedWall.NumberOfResistant =NumberOfGrowingResistant + FinalNumberOfResistence;
			SimulatedWall.NumberOfNormal = Nf - SimulatedWall.NumberOfResistant;
			
			return SimulatedWall;
			
		}
		
		public Well DoDilution(int Ratio)
		{
			SimulatedWall.NumberOfNormal =  Utils.RandBinomial(SimulatedWall.NumberOfNormal,(double)1/Ratio);
			SimulatedWall.NumberOfResistant= Utils.RandBinomial(SimulatedWall.NumberOfResistant,(double)1/Ratio);
			return SimulatedWall;
		}
		
	}
	
}

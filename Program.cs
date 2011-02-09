/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 22/02/2009
 * Time: 10:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace CyclicSimulation
{
	class Program
	{
		public static void Main(string[] args)
		{
			
			DeferentDilutionCyclesSimulation(Convert.ToDouble(args[0]),Convert.ToDouble(args[1]),Convert.ToDouble(args[2]));
			
		}

		
		private static void DeferentDilutionCycleSimulation(double MutationRatio,double Dilution,double N0Persisters)
		{
			double Nf=1e9;
			
			int seed=1;
			double tauNormalKill=10;
			double tauPersisterKill=100;
			
			double tauGrowNormal=20;
			double tauGrowResistant=tauGrowNormal;
			
			
			
			//double	N0Persisters = 1e8;
			double	N0normal=Nf - N0Persisters;
			double	N0Resisters = 0;
			double	NormalPersistersFraction = N0Persisters/N0normal;
			
			
			string FileName = "OneExp_" + MutationRatio.ToString("0.##E+000") +"_"+Dilution.ToString() + "_" +N0Persisters.ToString("0.##E+000") +".txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			
			
			
			
			SimulationParameters SP = new SimulationParameters(Nf,10*Nf,MutationRatio,tauNormalKill,tauPersisterKill ,tauGrowNormal,tauGrowResistant ,NormalPersistersFraction,Dilution);
			Well NormalWell = new Well(N0normal,N0Persisters,N0Resisters);
			Simulation NormalSimulation= new Simulation(NormalWell,seed,SP);
			int cycle=0;
			do
			{
				cycle++;
				NormalWell = NormalSimulation.DoSycle();
				SR.WriteLine("{0}\t{1}\t{2}\t{3}",cycle,NormalWell.NumberOfNormal,NormalWell.NumberOfPersistent,NormalWell.NumberOfResistant);
				
			}
			while (NormalWell.NumberOfResistant<Nf);
			
			
			
			
			SR.Close();
			
			
		}
		
		private static void DeferentDilutionCyclesSimulation(double MutationRatio,double Dilution,double N0Persisters)
		{
			
			double Nf=1e9;
			
			int seed;
			double tauNormalKill=10;
			double tauPersisterKill=100;
			
			double tauGrowNormal=20;
			double tauGrowResistant=tauGrowNormal;
			
			
			
			//double	N0Persisters = 1e6;
			double	N0normal=Nf - N0Persisters;
			double	N0Resisters = 0;
			double	NormalPersistersFraction = N0Persisters/N0normal;
			
			
			//string FileName = "CyclesSimulation"+ MutationRatio.ToString("0.##E+0")  +".txt";
			string FileName = "DeferentDilutionCycleOfFixation_" + MutationRatio.ToString("0.00E+000") +"_"+Dilution.ToString()+ "_" +N0Persisters.ToString("0.##E+000")+".txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			
			
			
			int Simulations = 10000;
			


			for (int sim=0;sim < Simulations;sim++)
			{
				seed = sim;
				
				
				SimulationParameters SP = new SimulationParameters(Nf,10*Nf,MutationRatio,tauNormalKill,tauPersisterKill ,tauGrowNormal,tauGrowResistant ,NormalPersistersFraction,Dilution);
				Well NormalWell = new Well(N0normal,N0Persisters,N0Resisters);
				Simulation NormalSimulation= new Simulation(NormalWell,seed,SP);
				int cycle=0;
				do
				{
					cycle++;
					NormalWell = NormalSimulation.DoSycle();
					
					
				}
				while (NormalWell.NumberOfResistant<Nf);
				
				SR.WriteLine(cycle);
			}
			
			SR.Close();
		}
		
		
		private static void CyclesSimulation()
		{
			double Nf=1e9;
			double MutationRatio=1e-8 ;
			int seed ;
			double tauNormalKill=10;
			double tauPersisterKill=100;
			
			double tauGrowNormal=20;
			double tauGrowResistant=tauGrowNormal;
			
			
			double N0Persisters ;
			double N0normal;
			double N0Resisters;
			double NormalPersistersFraction;
			double HighPersistersFraction;
			
			
			string FileName = "CyclesSimulation"+ MutationRatio.ToString("0.##E+0")  +".txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			
			
			
			int Simulations = 10000;
			
			for (int sim=0;sim < Simulations;sim++)
			{
				seed = sim;
				
				N0Persisters = 1e6;
				N0normal=Nf - N0Persisters;
				N0Resisters = 0;
				NormalPersistersFraction = N0Persisters/N0normal;
				
				SimulationParameters SP ;
				SP= new SimulationParameters(Nf,Nf,MutationRatio,tauNormalKill,tauPersisterKill ,tauGrowNormal,tauGrowResistant ,NormalPersistersFraction,200);
				
				Well NormalWell = new Well(N0normal,N0Persisters,N0Resisters);
				Simulation NormalSimulation= new Simulation(NormalWell,seed,SP);
				
				do
				{
					NormalWell = NormalSimulation.DoSycle();
				}
				while (NormalWell.NumberOfResistant<Nf);
				
				
				
				N0Persisters = 1e8;
				N0normal=Nf - N0Persisters;
				N0Resisters = 0;
				HighPersistersFraction = N0Persisters/N0normal;
				
				SP = new SimulationParameters(Nf,Nf,MutationRatio,tauNormalKill,tauPersisterKill ,tauGrowNormal,tauGrowResistant,HighPersistersFraction ,200);
				
				Well HighPersistenceWell = new Well(N0normal,N0Persisters,N0Resisters);
				
				Simulation HighPersistenceSimulation = new Simulation(HighPersistenceWell,seed,SP);
				do
				{
					HighPersistenceWell = HighPersistenceSimulation.DoSycle();
				}
				while (HighPersistenceWell.NumberOfResistant<Nf);
				
				
				SR.WriteLine("{0}\t{1}",NormalSimulation.Cycle,HighPersistenceSimulation.Cycle);
				NormalSimulation = null;
				HighPersistenceSimulation = null;
				PrintPresentege(sim,Simulations);
			}
			
			
			
			
			SR.Close();
			
			
		}

		

		
//		public static void article_diferent_mu()
//		{
//			double Nf=1e10;
//			double[] MutationRatio={1e-6,3e-7,1e-7,1e-8,1e-9,1e-10,1e-11} ;
//			//int seed =1;
//			double tauKill=10;
//
//			double tauGrowNormal=20;
//			double tauGrowResistant=tauGrowNormal*1.05;
//			double N0mutation = 0;
//			double N0normal=1e10;
//
//			string FileName = "article.txt";//Simulation"+ MutationRate.ToString("0.##E+0")  +".txt";
//			System.IO.FileInfo myFile = new FileInfo(FileName);
//			myFile.Delete();
//			StreamWriter SR = new StreamWriter(FileName,false);
//			int sims=MutationRatio.Length;
//
//			int rep = 30;
//			Well[,] myTest=new Well[sims,rep] ;
//			Simulation[,] mySimulation = new Simulation[sims,rep];
//
//			for (int sim=0;sim<sims;sim++)
//			{
//				for(int i=0;i<rep;i++)
//				{
//					mySimulation[sim,i]= new Simulation(Nf,MutationRatio[sim],i,tauKill ,tauGrowNormal,tauGrowResistant );
//					myTest[sim,i] = new Well(N0normal,0,N0mutation);
//				}
//
//			}
//
//			int rounds=300;
//			for(int round=0;round<rounds;round++)
//			{
//				double gen=round*Math.Log(100,2);
//				SR.Write("{0}\t",gen);
//
//				for (int sim=0;sim<sims;sim++)
//				{
//
//					for(int i=0;i<rep;i++)
//					{
//
//						myTest[sim,i]=mySimulation[sim,i].DoGrowing(myTest[sim,i]);
//						SR.Write("{0}\t",myTest[sim,i].NumberOfResistant);
//						myTest[sim,i]=mySimulation[sim,i].DoDilution(myTest[sim,i],100);
//
//					}
//
//				}
//				SR.Write("\n");
//
//				PrintPresentege(round,rounds);
//			}
//
//
//
//
//			SR.Close();
//		}
//		public static void driftstatistic()
//		{
//			double Nf=10000;
//			double MutationRatio=1e-14;
//			double tauKill=10;
//
//			double tauGrowNormal=20;
//			double tauGrowResistant=20;
//			double N0mutation = 5000;
//			double N0normal=5000;
//
//			Well myTest ;
//			string FileName = "ofer.txt";//Simulation"+ MutationRate.ToString("0.##E+0")  +".txt";
//			System.IO.FileInfo myFile = new FileInfo(FileName);
//			myFile.Delete();
//			StreamWriter SR = new StreamWriter(FileName,false);
//
//
//
//			//SR.WriteLine(myTest.NumberOfResistant);
//			for(int i=0;i<1000;i++)
//			{
//				Simulation mySimulation= new Simulation(Nf,MutationRatio,i,tauKill ,tauGrowNormal,tauGrowResistant );
//				myTest = new Well(N0normal,0,N0mutation);
//				int j=0;
//				while(myTest.NumberOfResistant !=0 && myTest.NumberOfNormal !=0)
//				{
//					myTest=mySimulation.DoGrowing(myTest);
//					//Console.WriteLine(myTest.NumberOfResistant + " " +  myTest.NumberOfNormal);
//					myTest=mySimulation.DoDilution(myTest,100);
//					j++;
//				}
//				SR.WriteLine(j);
//				PrintPresentege(i,1000);
//				mySimulation=null;
//			}
//			SR.Close();
//		}
//
//		public static void drift()
//		{
//			double Nf=1e4;
//			double MutationRatio=3e-7;
//			//int seed =1;
//			double tauKill=10;
//
//			double tauGrowNormal=20;
//			double tauGrowResistant=tauGrowNormal;//*1.05;
//			double N0mutation = 5e3;
//			double N0normal=5e3;
//
//			string FileName = "drift.txt";//Simulation"+ MutationRate.ToString("0.##E+0")  +".txt";
//			System.IO.FileInfo myFile = new FileInfo(FileName);
//			myFile.Delete();
//			StreamWriter SR = new StreamWriter(FileName,false);
//			int sims=10;
//			Well[] myTest=new Well[sims] ;
//			Simulation[] mySimulation = new Simulation[sims];
//
//			for (int sim=0;sim<sims;sim++)
//			{
//				mySimulation[sim]= new Simulation(Nf,MutationRatio,sim,tauKill ,tauGrowNormal,tauGrowResistant );
//				myTest[sim] = new Well(N0normal,0,N0mutation);
//
//			}
//
//			int rounds=200;
//			for(int round=0;round<rounds;round++)
//			{
//				double gen=round*Math.Log(100,2);
//				SR.Write("{0}\t",gen);
//				for (int sim=0;sim<sims;sim++)
//				{
//
//					myTest[sim]=mySimulation[sim].DoGrowing(myTest[sim]);
//					SR.Write("{0}\t",myTest[sim].NumberOfResistant);
//					myTest[sim]=mySimulation[sim].DoDilution(myTest[sim],100);
//
//				}
//				SR.Write("\n");
//				PrintPresentege(round,rounds);
//			}
//
//
//
//
//			SR.Close();
//		}
//
//		public static void drift_selection_mutation()
//		{
//
//			double Nf=1e4;
//			double MutationRatio=3e-5;
//			//int seed =1;
//			double tauKill=10;
//
//			double tauGrowNormal=20;
//			double tauGrowResistant=tauGrowNormal*1.03;
//			double N0mutation = 0;
//			double N0normal=1e4;
//
//			string FileName = "drift+selection+mutation.txt";//Simulation"+ MutationRate.ToString("0.##E+0")  +".txt";
//			System.IO.FileInfo myFile = new FileInfo(FileName);
//			myFile.Delete();
//			StreamWriter SR = new StreamWriter(FileName,false);
//			int sims=10;
//			Well[] myTest=new Well[sims] ;
//			Simulation[] mySimulation = new Simulation[sims];
//
//			for (int sim=0;sim<sims;sim++)
//			{
//				mySimulation[sim]= new Simulation(Nf,MutationRatio,sim,tauKill ,tauGrowNormal,tauGrowResistant );
//				myTest[sim] = new Well(N0normal,0,N0mutation);
//
//			}
//
//			int rounds=200;
//			for(int round=0;round<rounds;round++)
//			{
//				double gen=round*Math.Log(100,2);
//				SR.Write("{0}\t",gen);
//				for (int sim=0;sim<sims;sim++)
//				{
//
//					myTest[sim]=mySimulation[sim].DoGrowing(myTest[sim]);
//					SR.Write("{0}\t",myTest[sim].NumberOfResistant);
//					myTest[sim]=mySimulation[sim].DoDilution(myTest[sim],100);
//
//				}
//				SR.Write("\n");
//				PrintPresentege(round,rounds);
//			}
//
//
//
//
//			SR.Close();
//
//		}
		public static void drift_selection()
		{
			double Nf=1e4;
			double MutationRatio=3e-10;
			//int seed =1;
			double tauKill=10;

			double tauGrowNormal=20;
			double tauGrowResistant=tauGrowNormal*1.005;
			double N0mutation = 5e3;
			double N0normal=5e3;

			string FileName = "drift+selection.txt";//Simulation"+ MutationRate.ToString("0.##E+0")  +".txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			int sims=10;
			Well[] myTest=new Well[sims] ;
			Simulation[] mySimulation = new Simulation[sims];

			SimulationParameters SP = new SimulationParameters(Nf,Nf,
			                                                   MutationRatio,
			                                                   tauKill,
			                                                   tauKill ,
			                                                   tauGrowNormal,
			                                                   tauGrowResistant ,
			                                                   0,200);
			
			
			for (int sim=0;sim<sims;sim++)
			{
				myTest[sim] = new Well(N0normal,0,N0mutation);
				
				
				
				mySimulation[sim]= new
					Simulation(myTest[sim],sim,SP);
			}

			int rounds=200;
			for(int round=0;round<rounds;round++)
			{
				double gen=round*Math.Log(100,2);
				SR.Write("{0}\t",gen);
				for (int sim=0;sim<sims;sim++)
				{

					myTest[sim]=mySimulation[sim].DoGrowing(myTest[sim]);
					SR.Write("{0}\t",myTest[sim].NumberOfResistant);
					myTest[sim]=mySimulation[sim].DoDilution(myTest[sim],100);

				}
				SR.Write("\n");
				PrintPresentege(round,rounds);
			}




			SR.Close();
		}
		
		public static void drift()
		{
			double Nf=1e4;
			double MutationRatio=3e-10;
			//int seed =1;
			double tauKill=10;

			double tauGrowNormal=20;
			double tauGrowResistent=tauGrowNormal;
			double N0mutation = 5e3;
			double N0normal=5e3;

			string FileName = "drift.txt";//Simulation"+ MutationRate.ToString("0.##E+0")  +".txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			int sims=10;
			Well[] myTest=new Well[sims] ;
			Simulation[] mySimulation = new Simulation[sims];

			SimulationParameters SP= new SimulationParameters(			Nf,Nf,
			                                                  MutationRatio,
			                                                  tauKill,
			                                                  tauKill ,
			                                                  tauGrowNormal,
			                                                  tauGrowResistent ,
			                                                  0,200);
			
			for (int sim=0;sim<sims;sim++)
			{
				myTest[sim] = new Well(N0normal,0,N0mutation);
				
				mySimulation[sim]= new
					Simulation(myTest[sim],
					           sim+90,
					           SP);
			}

			int rounds=200;
			for(int round=0;round<rounds;round++)
			{
				double gen=round*Math.Log(100,2);
				SR.Write("{0}\t",gen);
				for (int sim=0;sim<sims;sim++)
				{

					myTest[sim]=mySimulation[sim].DoGrowing(myTest[sim]);
					SR.Write("{0}\t",myTest[sim].NumberOfResistant);
					myTest[sim]=mySimulation[sim].DoDilution(myTest[sim],100);

				}
				SR.Write("\n");
				PrintPresentege(round,rounds);
			}




			SR.Close();
		}
//
//		public static void OneWellinDTiale(double MutationRate)
//		{
//			string FileName = "Simulation"+ MutationRate.ToString("0.##E+0")  +".txt";
//			System.IO.FileInfo myFile = new FileInfo(FileName);
//			myFile.Delete();
//			StreamWriter SR = new StreamWriter(FileName,false);
//
//			double tauKill = 20 / Math.Log(2);
//			double tauGrow = 25 / Math.Log(2);
//
//
//			Simulation s = new Simulation(1e9,MutationRate,1,tauKill,tauGrow,tauGrow);
//			Well tWell;
//			for(int i=0;i<1000;i++)
//			{
//
//
//				tWell = s.DoSycle();
//				SR.WriteLine("{0:0.0e+00}\t{1:0.0e+00}\t{2:0.0e+00}",tWell.NumberOfBacteria ,tWell.NumberOfNormal,tWell.NumberOfResistant);
//
//				PrintPresentege(i,1000);
//			}
//
//
//			SR.Close();
//			Console.WriteLine("End");
//			Console.Beep();
//		}
//
//		public static void NumberOfSycleToResistent(double MutationRate,double dilution)
//		{
//			string FileName = "SimulationSycleToResistentI"+ dilution + "_" + MutationRate.ToString("0.##E+0") + ".txt";
//			System.IO.FileInfo myFile = new FileInfo(FileName);
//			myFile.Delete();
//			StreamWriter SR = new StreamWriter(FileName,false);
//
//
//			double tauKill = 20 / Math.Log(2);
//			double tauGrow = 25 / Math.Log(2);
//
//
//			int simulation = 10000;
//			for(int j=0;j<simulation;j++)
//			{
//				Simulation s = new Simulation(1e9,MutationRate,j,tauKill,tauGrow,tauGrow);
//				Well tWell;
//				do
//				{
//					tWell = s.DoSycle(dilution,260);
//					//	SR.WriteLine("{0:0.0e+00}\t{1:0.0e+00}\t{2:0.0e+00}",tWell.NumberOfBacteria ,tWell.NumberOfNormal,tWell.NumberOfResistant);
//
//				}
//				while (tWell.NumberOfResistant<tWell.NumberOfNormal);
//				SR.WriteLine(s.Sycle.ToString() );
//				PrintPresentege(j,simulation);
//			}
//
//
//			SR.Close();
//			Console.WriteLine("End");
//			Console.Beep();
//		}
//
//		public static void NumberOfSycleToOver(double MutationRate)
//		{
//			string FileName = "SimulationSycleToOver" + MutationRate.ToString("0.##E+0") + ".txt";
//			System.IO.FileInfo myFile = new FileInfo(FileName);
//			myFile.Delete();
//			StreamWriter SR = new StreamWriter(FileName,false);
//
//
//			double tauKill = 20 / Math.Log(2);
//			double tauGrow = 25 / Math.Log(2);
//
//
//			int simulation = 10000;
//			for(int j=0;j<simulation;j++)
//			{
//				Simulation s = new Simulation(1e9,MutationRate,j,tauKill,tauGrow,tauGrow);
//				Well tWell;
//				do
//				{
//					tWell = s.DoSycle();
//					//	SR.WriteLine("{0:0.0e+00}\t{1:0.0e+00}\t{2:0.0e+00}",tWell.NumberOfBacteria ,tWell.NumberOfNormal,tWell.NumberOfResistant);
//
//				}
//				while (tWell.NumberOfResistant<tWell.NumberOfNormal);
//				SR.WriteLine(s.Sycle.ToString() );
//				PrintPresentege(j,simulation);
//			}
//
//
//			SR.Close();
//			Console.WriteLine("End");
//			Console.Beep();
//		}
		
		
		
		public static void PrintPresentege(int ind,int from)
		{
			//Console.WriteLine(Convert.ToInt32((double)ind/from*1000));
			int fraction = Convert.ToInt32((double)ind/from*1000);
			int postfraction = Convert.ToInt32((double)(ind-1)/from*1000);
			if ((fraction-postfraction)>=1)
			{
				Console.CursorLeft = 0;
				Console.Write("{0}%",((double)fraction/10).ToString("00.0"));
			}
		}
		public static void PrintPresentege(double ind,double from)
		{
			//Console.WriteLine(Convert.ToInt32((double)ind/from*1000));
			int fraction = Convert.ToInt32((double)ind/from*1000);
			int postfraction = Convert.ToInt32((double)(ind-1)/from*1000);
			
			Console.CursorLeft = 0;
			Console.Write("{0}%",((double)fraction/10).ToString("00.0"));
			
		}
		
	}
}


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

namespace CyclicSimulatuin
{
	class Program
	{
		public static void Main(string[] args)
		{
			CyclesSimulation();
			
		}
		
		private static void GurCyclesSimulation()
		{
			//double Nf=1e5;
			double MutationRatio=0 ;

			
			double tauNormalKill=10;
			double tauPersisterKill=100;
			
			double tauGrowNormal=20;
			double tauGrowResistent=tauGrowNormal;
			
			
			string FileName = "GurCyclesSimulationAsN.txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			
						
			double[] N = new double[10];
			for(int i=0;i<10;i++)
			{
				N[i] = 10000*Math.Pow(10,((double)i/(double)2));
			}
			for (int n=0;n < N.Length;n++)
			{
				
				double Nf=Math.Floor((double)N[n]);
				
				
				int Simulations = 1000;
				
				
				for (int sim=0;sim < Simulations;sim++)
				{
					int	seed = sim;
					double N0Persisters = 0;
					double N0normal =Nf/2- N0Persisters;
					double N0Resisters =Nf/2;
					double NormalPersistersFraction=N0Persisters/N0normal;
					
					
					N0Persisters = 0;
					N0normal=Nf/2 - N0Persisters;
					N0Resisters = Nf/2;
					NormalPersistersFraction = N0Persisters/N0normal;
					
					Well NormalWell = new Well(N0normal,N0Persisters,N0Resisters);
					Simulation NormalSimulation= new Simulation(NormalWell,seed,Nf,MutationRatio,tauNormalKill,tauPersisterKill ,tauGrowNormal,tauGrowResistent ,NormalPersistersFraction);
					int cycle=0;
					do
					{
						cycle++;
						
						NormalWell = NormalSimulation.DoDilution(NormalWell,100);
						NormalWell = NormalSimulation.DoGrowing(NormalWell);
						
						//PrintPresentege(NormalWell.NumberOfNormal,Nf);
					}
					while (NormalWell.NumberOfResistant<Nf && NormalWell.NumberOfNormal<Nf);
					SR.Write("{0}\t",cycle);
					NormalSimulation = null;
					PrintPresentege(sim+n*Simulations,Simulations*N.Length);
				}
				SR.WriteLine();
			}
			
			
			SR.Close();
			
			
		}
		
		private static void GurRandCyclesSimulation()
		{
			//double Nf=1e5;
			double MutationRatio=0 ;

			
			double tauNormalKill=10;
			double tauPersisterKill=100;
			
			double tauGrowNormal=20;
			double tauGrowResistent=tauGrowNormal;
			
			
			string FileName = "GurRandCyclesSimulationAsN.txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			
						
						
				int Simulations = 800;
				Random nrand = new Random(1);
				
				for (int sim=0;sim < Simulations;sim++)
				{
					double N = 10000*Math.Pow(10,(nrand.NextDouble()*4.5));
					double Nf=Math.Floor(N);
					
					int	seed = sim+1600;
					double N0Persisters = 0;
					double N0normal = Math.Floor(Nf/2)- N0Persisters;
					double N0Resisters =Math.Floor(Nf/2);
					double NormalPersistersFraction=N0Persisters/N0normal;
					
					
					N0Persisters = 0;
					N0normal=Nf/2 - N0Persisters;
					N0Resisters = Nf/2;
					NormalPersistersFraction = N0Persisters/N0normal;
					
					Well NormalWell = new Well(N0normal,N0Persisters,N0Resisters);
					Simulation NormalSimulation= new Simulation(NormalWell,seed,Nf,MutationRatio,tauNormalKill,tauPersisterKill ,tauGrowNormal,tauGrowResistent ,NormalPersistersFraction);
					int cycle=0;
					do
					{
						cycle++;
						
						NormalWell = NormalSimulation.DoDilution(NormalWell,100);
						NormalWell = NormalSimulation.DoGrowing(NormalWell);
						
						//PrintPresentege(NormalWell.NumberOfNormal,Nf);
					}
					while (NormalWell.NumberOfResistant<Nf && NormalWell.NumberOfNormal<Nf);
					SR.WriteLine("{0}\t{1}",Nf,cycle);
					NormalSimulation = null;
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
//			double tauGrowResistent=tauGrowNormal*1.05;
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
//					mySimulation[sim,i]= new Simulation(Nf,MutationRatio[sim],i,tauKill ,tauGrowNormal,tauGrowResistent );
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
//			double tauGrowResistent=20;
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
//				Simulation mySimulation= new Simulation(Nf,MutationRatio,i,tauKill ,tauGrowNormal,tauGrowResistent );
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
//			double tauGrowResistent=tauGrowNormal;//*1.05;
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
//				mySimulation[sim]= new Simulation(Nf,MutationRatio,sim,tauKill ,tauGrowNormal,tauGrowResistent );
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
//			double tauGrowResistent=tauGrowNormal*1.03;
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
//				mySimulation[sim]= new Simulation(Nf,MutationRatio,sim,tauKill ,tauGrowNormal,tauGrowResistent );
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
			double tauGrowResistent=tauGrowNormal*1.005;
			double N0mutation = 5e3;
			double N0normal=5e3;

			string FileName = "drift+selection.txt";//Simulation"+ MutationRate.ToString("0.##E+0")  +".txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			int sims=10;
			Well[] myTest=new Well[sims] ;
			Simulation[] mySimulation = new Simulation[sims];

			for (int sim=0;sim<sims;sim++)
			{
				myTest[sim] = new Well(N0normal,0,N0mutation);
				
				mySimulation[sim]= new
					Simulation(myTest[sim],
					           sim,
					           Nf,
					           MutationRatio,
					           tauKill,
					           tauKill ,
					           tauGrowNormal,
					           tauGrowResistent ,
					           0);
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

			for (int sim=0;sim<sims;sim++)
			{
				myTest[sim] = new Well(N0normal,0,N0mutation);
				
				mySimulation[sim]= new
					Simulation(myTest[sim],
					           sim+90,
					           Nf,
					           MutationRatio,
					           tauKill,
					           tauKill ,
					           tauGrowNormal,
					           tauGrowResistent ,
					           0);
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
		
		
		private static void CyclesSimulation()
		{
			double Nf=1e9;
			double MutationRatio=1e-6 ;
			int seed ;
			double tauNormalKill=10;
			double tauPersisterKill=100;
			
			double tauGrowNormal=20;
			double tauGrowResistent=tauGrowNormal;
			
			
			double N0Persisters ;
			double N0normal;
			double N0Resisters;
			double NormalPersistersFraction;
			double HighPersistersFraction;
			
			
			
			string FileName = "CyclesSimulation"+ MutationRatio.ToString("0.##E+0")  +".txt";
			System.IO.FileInfo myFile = new FileInfo(FileName);
			myFile.Delete();
			StreamWriter SR = new StreamWriter(FileName,false);
			
			
			
			int Simulations = 1000;
			
			for (int sim=0;sim < Simulations;sim++)
			{
				seed = sim;
				
				N0Persisters = 1e6;
				N0normal=Nf - N0Persisters;
				N0Resisters = 0;
				NormalPersistersFraction = N0Persisters/N0normal;
				
				Well NormalWell = new Well(N0normal,N0Persisters,N0Resisters);
				Simulation NormalSimulation= new Simulation(NormalWell,seed,Nf,MutationRatio,tauNormalKill,tauPersisterKill ,tauGrowNormal,tauGrowResistent ,NormalPersistersFraction);
				
				do
				{
					NormalWell = NormalSimulation.DoSycle();
				}
				while (NormalWell.NumberOfResistant<Nf);
				
				
				
				N0Persisters = 1e8;
				N0normal=Nf - N0Persisters;
				N0Resisters = 0;
				HighPersistersFraction = N0Persisters/N0normal;
				
				Well HighPersistenceWell = new Well(N0normal,N0Persisters,N0Resisters);
				
				Simulation HighPersistenceSimulation = new Simulation(HighPersistenceWell,seed,Nf,MutationRatio,tauNormalKill,tauPersisterKill ,tauGrowNormal,tauGrowResistent,HighPersistersFraction );
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


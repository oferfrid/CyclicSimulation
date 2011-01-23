/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 04/05/2009
 * Time: 10:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;

namespace CyclicSimulatuin
{
	/// <summary>
	/// Description of Solver.
	/// </summary>
	public static class Solver
	{
		#region nonliniar Solve
				
		public static double N1 ;
		public static double N2 ;
		public static double Nf ;
		public static double gen2retio ;
		
		public static double Function(double gen)
		{
			
			double ret = N1* Math.Pow(2,gen)+N2*Math.Pow(2,gen*gen2retio)-Nf;
			return ret;
		}
		
		public static double dFunction(double gen)
		{
			double ret = Math.Log(2)*N1* Math.Pow(2,gen)+Math.Log(2)*gen2retio*N2*Math.Pow(2,gen*gen2retio);
			return ret;
		}
//usege:		
//		double x=(rtsafe(Function,dFunction,200,400,0.001));
//		Console.WriteLine("x={0} F({0})={1}",x,Function(x));
		public delegate TResult Func<T, TResult>(T arg);	
		public static double rtsafe(Func<double,double>  F,Func<double,double>  dF, double x1, double x2, double xacc)
		{
		//Using a combination of Newton-Raphson and bisection, find the root of a function bracketed
		//between x1 and x2. The root, returned as the function value rtsafe, will be rened until
		//its accuracy is known within xacc. F is a user-supplied routine that returns
		//function value and dF returns the derivative of the function.
				
			int MAXIT =1000;
			int j;
			double df,dx,dxold,f,fh,fl;
			double temp,xh,xl,rts;
			fl=F(x1);
			fh=F(x2);
			fh=dF(x2);
			if ((fl > 0.0 && fh > 0.0) || (fl < 0.0 && fh < 0.0))
			{
				throw new Exception("Root must be bracketed in rtsafe");
			}
			

			if (fl == 0.0)
			{
				return x1;
			}

			if (fh == 0.0)
			{
				return x2;
			}
			if (fl < 0.0) {
				xl=x1;
				xh=x2;
			} else {
				xh=x1;
				xl=x2;
			}

			rts=0.5*(x1+x2);

			dxold=Math.Abs(x2-x1);
			dx=dxold;

			f=F(rts);
			df=dF(rts);
			for (j=1;j<=MAXIT;j++)
			{
				if ((((rts-xh)*df-f)*((rts-xl)*df-f) > 0.0)|| (Math.Abs(2.0*f) > Math.Abs(dxold*df)))
				{
					dxold=dx;
					dx=0.5*(xh-xl);
					rts=xl+dx;
					if (xl == rts)
					{
						return rts;
					}
				}
				else
				{
					dxold=dx;
					dx=f/df;
					temp=rts;
					rts -= dx;
					if (temp == rts)
					{
						return rts;
					}
				}
				if (Math.Abs(dx) < xacc)
				{
					return rts;
				}
				f=F(rts);
				df=dF(rts);
				if (f < 0.0)
				{
					xl=rts;
				}
				else
				{
					xh=rts;
				}
				
			}
			throw new Exception("Maximum number of iterations exceeded in rtsafe");
			
		}
		

		#endregion


	}
}

using System;
namespace Poker
{
	class Program
	{
		static void Main(string[] args)
		{
            Jeton j = new Jeton(Jeton.Valeur.BLANC);
            if ((int)Jeton.Valeur.BLANC == 1) Console.WriteLine("J'ai réussi BURY !!!");
		}
	}
}

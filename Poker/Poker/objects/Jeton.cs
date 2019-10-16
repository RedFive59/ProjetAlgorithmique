using System;

public class Jeton
{
    public enum Valeur { BLANC = 1, ROUGE = 5, VERT = 10, BLEU = 25, NOIR = 50 };
    //Attributs
    private Valeur valeur;
    //Constructeurs
	public Jeton(Valeur valeur)
	{
        this.valeur = valeur;
    }
    //Méthodes
    public Valeur getValeur()
    {
        return this.valeur;
    }
    public bool memeValeur(Jeton j)
    {
        if ((int)this.valeur == (int)j.getValeur()) return true;
        return false;
    }
}

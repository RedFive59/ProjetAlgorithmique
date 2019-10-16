using System;

public class Paquet : Carte // ":" = héritage
{

    const int NOMBRE_CARTES = 52;
    private Carte[] paquet;

	public Paquet() 
    {
        paquet = new Carte[NOMBRE_CARTES];
    }

    public Carte[] getPaquet () 
    {
        return paquet;
    }

    public void genererPaquet()
    {
        int i = 0;
        foreach(COULEUR c in Enum.GetValues(typeof(COULEUR)))
        {
            foreach(VALEUR v in Enum.GetValues(typeof(VALEUR)))
            {
                paquet[i] = new Card { couleur = c, valeur = v };
                i++;
            }
        }
        melangerPaquet();
    }

    public void melangerPaquet() //on échange i fois les places de cartes aléatoires dans la pioche
    {
        Ramdom rand = new Random();
        Carte temp;
        for (int i = 0; i < 100; i++) //nombre d'échanges
        {
            for(int j = 0; j < NOMBRE_CARTES; j++)
            {
                int rand2 = rand.Next(13);
                temp = paquet[j]; paquet[j] = paquet[rand2]; paquet[rand2] = temp;
            }    
        }

    }
}

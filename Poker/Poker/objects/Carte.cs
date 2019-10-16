using System;

public class Carte
{

	enum COULEUR { COEUR, CARREAU, TREFLE, PIQUE };
	enum VALEUR { DEUX=2, TROIS, QUATRE, CINQ, SIX, SEPT, HUIT, NEUF, DIX, VALET, DAME, ROI, AS};

    public Carte() { }

    public COULEUR couleur { get; set; }
    public VALEUR valeur { get; set; }

}

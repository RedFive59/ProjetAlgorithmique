using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokerMenu : MonoBehaviour
{
    private GameObject nomJoueurRef; // Référence à l'input de référence
    private GameObject parent; // Référence à l'objet qui contiendra les inputs
    private GameObject mise; // Référence à l'input qui gère la mise
    private int maxMise = 10000; // Valeur maximale défini pour la mise
    private int minMise = 2500; // Valeur minimale défini pour la mise
    private GameObject alerteMise; // Référence à l'objet alerte mise
    private GameObject alerteNom; // Référence à l'objet alerte nom
    private GameObject lancerPartie; // Référence au bouton Lancer la partie
    // Coroutines pour avoir un timer au lancement de la méthode
    private Coroutine currentCoroutine = null;
    private Coroutine currentCoroutine2 = null;
    private bool canLaunchGame = false; // Booléen qui détermine si on peut lancer la partie
    private bool animationDone = false; // Booléen pour vérifier si une animation est en cours
    private GameObject gameSettings; // Référence à l'objet qui transmet les infos de la partie
    private int nbJoueurs; // Nombre de joueurs
    private string[] listNom; // Liste des noms de joueurs

    // Start is called before the first frame update
    void Start()
    {
        nomJoueurRef = GameObject.Find("NomJoueurRef");
        parent = GameObject.Find("InputsNomJoueur");
        mise = GameObject.Find("Mise");
        alerteMise = GameObject.Find("AlerteMise");
        alerteNom = GameObject.Find("AlerteNom");
        lancerPartie = GameObject.Find("LancerPartie");
        gameSettings = GameObject.Find("GameSettings");
        alerteMise.SetActive(false);
        createInputNameField();
        alerteNom.GetComponent<TextMeshProUGUI>().text = "Chaque joueur doit comporter un nom";
        DontDestroyOnLoad(gameSettings);
    }

    // Méthode appelée à chaque frame pour vérifier si l'on peut lancer la partie ou pas
    private void Update()
    {
        if (!canLaunchGame) lancerPartie.GetComponent<Button>().interactable = false;
        else lancerPartie.GetComponent<Button>().interactable = true;
    }

    
    public void createInputNameField()// Méthode de création des inputs name
    {
        destroyAllInputNameField();
        nomJoueurRef.SetActive(true);
        nbJoueurs = (int) GameObject.Find("NbJoueursSlider").GetComponent<Slider>().value;
        for(int i = 0; i < nbJoueurs; i++)
        {
            GameObject input = Instantiate(nomJoueurRef, parent.transform);
            input.transform.position = nomJoueurRef.transform.position - new Vector3(0f, i*1f);
            input.name = "NomJoueur_" + (i + 1);
            input.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text += i + 1;
        }
        float posY = 3.7f - nbJoueurs;
        alerteNom.transform.position = new Vector3(6.3f, posY);
        nomJoueurRef.SetActive(false);
    }
    public void verifInputs()// Méthode appelé à chaque frame pour vérifier si les noms sont bien entrés
    {
        canLaunchGame = false;
        nbJoueurs = (int)GameObject.Find("NbJoueursSlider").GetComponent<Slider>().value;
        listNom = new string[nbJoueurs];
        // Vérification que les pseudos soient remplis
        for (int i = 0; i < nbJoueurs; i++)
        {
            GameObject input = GameObject.Find("NomJoueur_" + (i+1));
            listNom[i] = input.GetComponent<TMP_InputField>().text;
            if (listNom[i] == "")
            {
                if (currentCoroutine2 != null) StopCoroutine(currentCoroutine2);
                alerteNom.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, 1f);
                alerteNom.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                alerteNom.SetActive(true);
                alerteNom.GetComponent<TextMeshProUGUI>().text = "Chaque joueur doit comporter un nom";
                animationDone = false;
                return;
            }
        }

        // Vérification de répétitions des pseudos
        for (int i = 0; i < nbJoueurs-1; i++)
        {
            for (int j = i+1; j < nbJoueurs; j++)
            {
                if (listNom[i] == listNom[j])
                {
                    if (currentCoroutine2 != null) StopCoroutine(currentCoroutine2);
                    alerteNom.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, 1f);
                    alerteNom.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                    alerteNom.SetActive(true);
                    alerteNom.GetComponent<TextMeshProUGUI>().text = "Chaque joueur doit comporter un nom différent";
                    animationDone = false;
                    return;
                }
            }
        }
        canLaunchGame = true;
        updateGameSettingsObject(nbJoueurs, listNom, valeurMise());
        //Animation de disparition de l'alerte des noms
        if (!animationDone)
        {
            if (currentCoroutine2 != null) StopCoroutine(currentCoroutine2);
            currentCoroutine2 = StartCoroutine(disableAlerteNom());
            animationDone = true;
        }
    }
    private void destroyAllInputNameField()// Méthode qui permet de détruire tous les inputs name afin de pouvoir en recréer un certain nombre
    {
        int nbChild = parent.transform.childCount;
        if(nbChild > 1)
        {
            for(int i = 1; i < nbChild; i++)
            {
                GameObject child = parent.transform.GetChild(i).gameObject;
                Destroy(child);
            }
        }
        alerteNom.SetActive(true);
        alerteNom.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, 1f);
        alerteNom.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        alerteNom.GetComponent<TextMeshProUGUI>().text = "Chaque joueur doit comporter un nom";
        canLaunchGame = false;
    }
    public void verifMiseOnChange()// Méthode qui vérifie si la mise est correcte (1)
    {
        int value;
        bool ok = int.TryParse(mise.GetComponent<TMP_InputField>().text, out value);
        if (mise.GetComponent<TMP_InputField>().text != "" && ok)
        {
            if (value < 0)
            {
                mise.GetComponent<TMP_InputField>().text = "0";
                if (currentCoroutine != null) StopCoroutine(currentCoroutine);
                currentCoroutine = StartCoroutine(updateAlerteMise("Impossible d'aller dans les négatifs"));
            }
            if (value > maxMise)
            {
                mise.GetComponent<TMP_InputField>().text = maxMise.ToString();
                if (currentCoroutine != null) StopCoroutine(currentCoroutine);
                currentCoroutine = StartCoroutine(updateAlerteMise("Valeur maximale dépassée"));
            }
        }
        if(!ok) mise.GetComponent<TMP_InputField>().text = "0";
        if(listNom != null && nbJoueurs != 0) updateGameSettingsObject(nbJoueurs, listNom, valeurMise());
    }
    public void verifMiseOnEndEdit()// Méthode qui vérifie si la mise est correcte (2)
    {
        int value;
        bool ok = int.TryParse(mise.GetComponent<TMP_InputField>().text, out value);
        if (mise.GetComponent<TMP_InputField>().text != "" && ok)
        {
            if (value < minMise)
            {
                mise.GetComponent<TMP_InputField>().text = minMise.ToString();
                if (currentCoroutine != null) StopCoroutine(currentCoroutine);
                currentCoroutine = StartCoroutine(updateAlerteMise("Valeur minimale franchie"));
            }
            if (value > maxMise)
            {
                mise.GetComponent<TMP_InputField>().text = maxMise.ToString();
                if (currentCoroutine != null) StopCoroutine(currentCoroutine);
                currentCoroutine = StartCoroutine(updateAlerteMise("Valeur maximale dépassée"));
            }
        }
        if (listNom != null && nbJoueurs != 0) updateGameSettingsObject(nbJoueurs, listNom, valeurMise());
    }
    private int valeurMise()// Méthode qui retourne la valeur de la mise entrée dans le champ d'insertion
    {
        string miseText = mise.GetComponent<TMP_InputField>().text;
        if (miseText == "") return minMise;
        else return int.Parse(miseText);
    }
    private IEnumerator updateAlerteMise(string message)// Animation de changement de texte
    {
        if (message != "")
        {
            alerteMise.SetActive(true);
            alerteMise.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, 1f);
            alerteMise.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            alerteMise.GetComponent<TextMeshProUGUI>().text = message;
            float countDown = 5f;
            int maxI = 100 + 255;
            int j = 0;
            int i = 0;
            while (countDown >= 0 && i < maxI)
            {
                if (i >= maxI - 255)
                {
                    float alpha = 1 - (float.Parse(j.ToString()) / 100);
                    j++;
                    alerteMise.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, alpha);
                    alerteMise.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                }
                i++;
                countDown -= Time.smoothDeltaTime;
                yield return new WaitForSeconds(0.01f);
            }
            alerteMise.SetActive(false);
        }
    }
    private IEnumerator disableAlerteNom()// Animation de disparition de l'alerte liée aux noms
    {
        if (alerteNom.activeSelf)
        {
            float countDown = 4f;
            int maxI = 255;
            int j = 0;
            int i = 0;
            while (countDown >= 0 && i < maxI)
            {
                if (i >= maxI - 255)
                {
                    float alpha = 1 - (float.Parse(j.ToString()) / 100);
                    j++;
                    alerteNom.GetComponent<TextMeshProUGUI>().color = new Color(1f, 0f, 0f, alpha);
                    alerteNom.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
                }
                i++;
                countDown -= Time.smoothDeltaTime;
                yield return new WaitForSeconds(0.01f);
            }
            alerteNom.SetActive(false);
        }
    }
    public void updateGameSettingsObject(int nbJoueurs, string[] listNom, int miseMax)// Méthode pour enregistrer les données entrées sur la scène dans l'objet GameSettings
    {
        gameSettings.GetComponent<PokerValue>().nbJoueurs = nbJoueurs;
        gameSettings.GetComponent<PokerValue>().bourse = miseMax;
        for (int i = 0; i < 5; i++)
        {
            if(i < nbJoueurs)
            {
                switch (i) {
                    case 0:
                        gameSettings.GetComponent<PokerValue>().nomJoueur1 = listNom[i];
                            break;
                    case 1:
                        gameSettings.GetComponent<PokerValue>().nomJoueur2 = listNom[i];
                        break;
                    case 2:
                        gameSettings.GetComponent<PokerValue>().nomJoueur3 = listNom[i];
                        break;
                    case 3:
                        gameSettings.GetComponent<PokerValue>().nomJoueur4 = listNom[i];
                        break;
                    case 4:
                        gameSettings.GetComponent<PokerValue>().nomJoueur5 = listNom[i];
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        gameSettings.GetComponent<PokerValue>().nomJoueur1 = null;
                        break;
                    case 1:
                        gameSettings.GetComponent<PokerValue>().nomJoueur2 = null;
                        break;
                    case 2:
                        gameSettings.GetComponent<PokerValue>().nomJoueur3 = null;
                        break;
                    case 3:
                        gameSettings.GetComponent<PokerValue>().nomJoueur4 = null;
                        break;
                    case 4:
                        gameSettings.GetComponent<PokerValue>().nomJoueur5 = null;
                        break;
                }
            }
        }
    }
    public void destroySaveObject()// Méthode pour supprimer l'objet contenant le choix de la difficulté en cas de retour au menu
    {
        Destroy(gameSettings);
    }
    public void switchLeaderboard()// Méthode qui permet de changer la position du leaderboard sur la place afin de le faire apparaître et disparaître
    {
        int posDepart = 425, posFin = 0;
        GameObject Leaderboard = GameObject.Find("AffichageLeaderboard");
        GameObject arrow = GameObject.Find("Fleche");
        if (Leaderboard.transform.localPosition.x == posDepart)
        {
            arrow.GetComponent<RectTransform>().transform.localRotation = new Quaternion(0f, 0f, 1f, 1f);
            Leaderboard.transform.localPosition = new Vector3(posFin, 0, 0);
        }
        else
        if (Leaderboard.transform.localPosition.x == posFin)
        {
            arrow.GetComponent<RectTransform>().transform.localRotation = new Quaternion(0f, 0f, 0.7f, -0.7f);
            Leaderboard.transform.localPosition = new Vector3(posDepart, 0, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PokerMenu : MonoBehaviour
{
    private GameObject nomJoueurRef;
    private GameObject parent;
    private GameObject mise;
    private int maxMise = 10000;
    private int minMise = 2500;
    private GameObject alerteMise;
    private GameObject alerteNom;
    private GameObject lancerPartie;
    private Coroutine currentCoroutine = null;
    private Coroutine currentCoroutine2 = null;
    private bool canLaunchGame = false;
    private bool animationDone = false;
    private GameObject gameSettings;
    private int nbJoueurs;
    private string[] listNom;

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

    private void Update()
    {
        if (!canLaunchGame) lancerPartie.GetComponent<Button>().interactable = false;
        else lancerPartie.GetComponent<Button>().interactable = true;
    }

    public void createInputNameField()
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

    public void verifInputs()
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

    private void destroyAllInputNameField()
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

    public void verifMiseOnChange()
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

    public void verifMiseOnEndEdit()
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

    private int valeurMise()
    {
        string miseText = mise.GetComponent<TMP_InputField>().text;
        if (miseText == "") return minMise;
        else return int.Parse(miseText);
    }

    private IEnumerator updateAlerteMise(string message)
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
                yield return null;
            }
            alerteMise.SetActive(false);
        }
    }

    private IEnumerator disableAlerteNom()
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
                yield return null;
            }
            alerteNom.SetActive(false);
        }
    }

    public void updateGameSettingsObject(int nbJoueurs, string[] listNom, int miseMax)
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
}

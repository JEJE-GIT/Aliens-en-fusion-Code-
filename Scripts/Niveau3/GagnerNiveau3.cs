using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GagnerNiveau3 : MonoBehaviour
{

    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager

    // Start is called before the first frame update
    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");       //Cette variable va contenir le gameObjet GestionnaireJeu     
        scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques
    }

    void GagnerLeNiveau3() {
        Debug.Log("Tu as fini le jeu !!!");    //Écrit cela dans la console pour mettre en évidence au joueur qu'il a gagné la partie
        scriptManager.SceneFinNiveau3Load();      //Load la scene FinNiveau3 via le script GameManager
    }



}

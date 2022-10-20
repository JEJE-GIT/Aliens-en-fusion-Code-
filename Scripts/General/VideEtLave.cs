using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideEtLave : MonoBehaviour
{

    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager

    // Start is called before the first frame update
    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");        //Cette variable va contenir le gameObjet GestionnaireJeu     
         scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques
    }


    void OnCollisionEnter2D(Collision2D objetEnCollision){      
        if(objetEnCollision.transform.tag == "Player") {        //Si il y a une collision avec le player...

            scriptManager.SonDamage();   //Joue le son audio damage quand le joueur reçois du dommage
            scriptManager.nbVies--;   //diminu de 1 le nombre de vie qui provient du gamemanager
        }

    }
}

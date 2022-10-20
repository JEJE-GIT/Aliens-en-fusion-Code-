using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalFinish : MonoBehaviour
{
    private GameObject gestionnaireDuJeu;   //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptPoints;       //Cette Cette variable va bientôt garder en mémoire la fonction pointsAdditionner du script du GameManager/1

    // Start is called before the first frame update
    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");     //Va chercher le gameobjet "GestionnaireJeu"
        scriptPoints = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques
        
    }

    void OnCollisionEnter2D(Collision2D objetEnCollision){      
        if(objetEnCollision.transform.tag == "Player") {        //Si il y a une collision avec le player...

            scriptPoints.Victoire();   //Apelle la fonction pointsAdditionner qui se trouve dans le script du GameManager

        }

        if(scriptPoints.points == 6) {      //Si le personnage a récupéré 6 points...
            Destroy(gameObject);        //Detruit le gameobjet terminale finish
        }

        
    }
}

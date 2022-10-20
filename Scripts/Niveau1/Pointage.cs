using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointage : MonoBehaviour
{

    private GameObject gestionnaireDuJeu;   //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"

    private GameManager scriptPoints;   //Cette Cette variable va bientôt garder en mémoire la fonction pointsAdditionner du script du GameManager

    // Start is called before the first frame update
    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");      //Va chercher le gameobjet "GestionnaireJeu"
        scriptPoints = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le script "GameManager" pour acceder a ses fonctions publiques
    }

    void OnCollisionEnter2D(Collision2D objetEnCollision){      
        if(objetEnCollision.transform.tag == "Player") {        //Si il y a une collision avec le player...
            scriptPoints.PointsAdditionner();   //Apelle la fonction PointsAdditionner qui se trouve dans le script du GameManager
            Destroy(gameObject);        //Détruit le gameobjet point, c'est-a-dire l'orbe d'éneirgie
        }

        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager 

    // Start is called before the first frame update
    void Start()
    {

        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");       //Cette variable va contenir le gameObjet GestionnaireJeu     
        scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques

    }

    void OnTriggerEnter2D(Collider2D objetEnCollision){

                if(objetEnCollision.transform.tag == "Player") //Si il y a une collision avec le player...
                {
                    scriptManager.nbVies = 3;           //Le nb de vie du joueur est égal à 3 
                    scriptManager.SonPowerUp();     //Joue le son powerup via le GameManager
                    GetComponent<Renderer>().material.color = new Color(0,0,0,0);   //le GameObjet va devenir transparent
                    GetComponent<CircleCollider2D>().enabled = false;       //Désactive le circle collider du GameObjet
                    Invoke("ReApparait", 2f);       //Appelle la gonction ReApparait après 2 secondes
                } 

    }

    void ReApparait() {       
        GetComponent<Renderer>().material.color = new Color(0,0,1,1);   //le GameObjet va devenir visible et bleu
        GetComponent<CircleCollider2D>().enabled = true;    //Active le circle collider du GameObjet
    }

}

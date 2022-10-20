using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    private GameObject[] powersUps = new GameObject[3];     //Le tableau qui contient les gameobjets possède une taille de 3 gameobjets. Ce tableau contient les références aux GameObject des power-ups du Niveau 2
    private GameObject Hero;     // contient la référence du gameobjet du hero, mais il est vide por l'instant.
    private GameObject powerUpChoisi;        //contient la référence des gameobjet des powers-ups, mais il est vide por l'instant. Il va bientôt choisir entre les gameobjets: "speed" "jump" et "shield" 

    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager 


    // Start is called before the first frame update
    void Start()
    {

        powersUps[0] = GameObject.Find("speed");       //Le premier GameObjet représente le power-up "speed"
        powersUps[1] = GameObject.Find("jump");       //Le deuxieme GameObjet représente le power-up "jump"
        powersUps[2] = GameObject.Find("shield");       //Le troisieme GameObjet représente le power-up "shield"

        Hero = GameObject.Find("Hero");

        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");       //Cette variable va contenir le gameObjet GestionnaireJeu     
        scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques

    }


    void OnTriggerEnter2D(Collider2D objetEnCollision){

                if(objetEnCollision.transform.tag == "Player") //Si il y a une collision avec le player...
                {

                    scriptManager.SonPowerUp();

                   if(gameObject == powersUps[0]) {;
                       Hero.GetComponent<Hero>().vitesseMarche += 3;
                       Destroy(powersUps[0]);
                   }

                   if(gameObject == powersUps[1]) {
                       Hero.GetComponent<Hero>().vitesseSaut += 3;
                       Destroy(powersUps[1]);
                   }

                   if(gameObject == powersUps[2]) {
                       Hero.GetComponent<Renderer>().material.color = new Color(0,0,1);
                       scriptManager.nbVies += 2;
                       Destroy(powersUps[2]);   
                   }

                } 

        
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager

    private Rigidbody2D rbEnemyShip;   //Cette variable va bientôt contenir les propriétés du Rigidbody2D du EnemyShip, c'est a dire sa physique
    private Collider2D colliderEnemyShip;  //Cette variable va bientôt contenir les propriétés du Collider2D du EnemyShip, c'est a dire sa HitBox
    private float vitesse = 5f;     //Cette variable représente la vitesse du EnemyShip, il est a 5f pour l'instant

    private float grandeurDepartX;      //Cette variable va bientôt contenir le scale originale du gameObjet EnemyShip
    private float modifieGrandeurX;     //Cette variable va bientôt contenir le scale modifié du gameObjet EnemyShip

    private float positionDepart;       //Cette variable va bientôt contenir la position de départ du gameObjet EnemyShip
    private float positionFinal;        //Cette variable va bientôt contenir la position de finale du gameObjet EnemyShip

    private GameObject HeroShip;     // contient la référence du gameobjet du hero, mais il est vide por l'instant.

    private float viesEnemyship = 5;        //Le nombre de PV du EnemyShip



    // Start is called before the first frame update
    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");       //Cette variable va contenir le gameObjet GestionnaireJeu     
        scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques

        HeroShip = GameObject.Find("HeroShip");         //Trouve le gameObjet Hero dans la scène

        grandeurDepartX = transform.localScale.x;       //Cette variable contient la valeur du scale originale du gameObjet EnemyShip
        modifieGrandeurX = -transform.localScale.x;      //Cette variable contient la valeur du scale originale du gameObjet EnemyShip, mais sera modifié plus tard
        colliderEnemyShip = GetComponent<Collider2D>();    //Prends les propriétés du Collider2D du EnemyShip, c'est a dire sa HitBox
        rbEnemyShip = GetComponent<Rigidbody2D>();         //Prends les propriétés du Rigidbody2D du EnemyShip, c'est a dire sa physique

        positionDepart = transform.position.x;      //Cette variable contient la position en x du EnemyShip
        positionFinal = positionDepart + 2;         //Cette variable contient la position en x + 1 du EnemyShip

         Invoke("MarcheDroit", 0.25f);      //après 0.25f, appelle la fonction MarcheDroit

    }


    void MarcheDroit() {

        modifieGrandeurX = grandeurDepartX;     //La variable modifieGrandeurX est égal a la variable grandeurDepartX
        transform.localScale = new Vector2(modifieGrandeurX, transform.localScale.y);   //Modifie le scale en x du EnemyShip et son scale en x est égale à modifieGrandeurX
        rbEnemyShip.velocity = new Vector2(vitesse, rbEnemyShip.velocity.y);      //Modifie la position en x du EnemyShip et sa position en x est sa vitesse 

        if(transform.position.x < positionFinal) {  //Si la position en x du EnemyShip est plus petit que sa position final en x...
            Invoke("MarcheDroit", 0.25f);       //après 0.25f, appelle la fonction MarcheDroit
        }

        else     //Sinon...
        {
            Invoke("MarcheGauche", 0.25f);      //après 0.25f, appelle la fonction MarcheGauche
        }
        
    }


     void MarcheGauche() {

        modifieGrandeurX = -grandeurDepartX;    //La variable modifieGrandeurX est -égal a la variable grandeurDepartX
        transform.localScale = new Vector2(modifieGrandeurX, transform.localScale.y);   //Modifie le scale en x du EnemyShip et son scale en x est égale à modifieGrandeurX mais en négatif
        rbEnemyShip.velocity = new Vector2(-vitesse, rbEnemyShip.velocity.y);      //Modifie la position en x du EnemyShip et sa position en x est sa vitesse mais en negatif
        
        if(transform.position.x > positionDepart) {     //Si la position en x du EnemyShip est plus grand que sa position final en x...
            Invoke("MarcheGauche", 0.25f);  //après 0.25f, appelle la fonction MarcheGauche
        }

        else    //Sinon
        {
            Invoke("MarcheDroit", 0.25f);    //après 0.25f, appelle la fonction MarcheDroit
        }
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.transform.tag == "Player")     //Si il y a une collision avec le joueur...
        {
            if(other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hexplosion")){      //Si le l'attaque du joueur touche l'enemy...

                scriptManager.SonDamage();   //Joue le son audio damage quand le EnemyShip reçois du dommage
                viesEnemyship--;   //Le EnemyShip perd une vie 

                if(viesEnemyship == 0) {   //Quand le EnemyShip n'a plus de vies...
                    GetComponent<Animator>().Play("Meurt");   //Joue l'animation de mort du EnemyShip
                    GetComponent<BoxCollider2D>().enabled = false;  //Désactive le collider du EnemyShip
                }

               
            }
            else{
                scriptManager.SonDamage();   //Joue le son audio damage quand le joueur reçois du dommage
               scriptManager.nbVies--;   //diminue de 1 le nombre de vie du joueur qui provient du gamemanager
            }
        }

    }


    void meurt() {
        Destroy(gameObject);    //Detruit le gameobjet du EnemyShip
    }

    void GagnerPoints() {
        Debug.Log("10+ point(s)");   //Ajoute 10 points
    }
}

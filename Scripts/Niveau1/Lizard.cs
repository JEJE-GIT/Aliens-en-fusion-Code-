using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    private Rigidbody2D rbLizard;   //Cette variable va bientôt contenir les propriétés du Rigidbody2D du lezard, c'est a dire sa physique
    private Collider2D colliderLizard;  //Cette variable va bientôt contenir les propriétés du Collider2D du lezard, c'est a dire sa HitBox
    private float vitesse = 2f;     //Cette variable représente la vitesse du lizard, il est a 2f pour l'instant

    private float grandeurDepartX;      //Cette variable va bientôt contenir le scale originale du gameObjet lizard
    private float modifieGrandeurX;     //Cette variable va bientôt contenir le scale modifié du gameObjet lizard

    private float positionDepart;       //Cette variable va bientôt contenir la position de départ du gameObjet lizard
    private float positionFinal;        //Cette variable va bientôt contenir la position de finale du gameObjet lizard

    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager



    // Start is called before the first frame update
    void Start()
    {

        grandeurDepartX = transform.localScale.x;       //Cette variable contient la valeur du scale originale du gameObjet lizard
        modifieGrandeurX = transform.localScale.x;      //Cette variable contient la valeur du scale originale du gameObjet lizard, mais sera modifié plus tard
        colliderLizard = GetComponent<Collider2D>();    //Prends les propriétés du Collider2D du lizard, c'est a dire sa HitBox
        rbLizard = GetComponent<Rigidbody2D>();         //Prends les propriétés du Rigidbody2D du lizard, c'est a dire sa physique

        positionDepart = transform.position.x;      //Cette variable contient la position en x du lizard
        positionFinal = positionDepart + 1;         //Cette variable contient la position en x + 1 du lizard

         Invoke("MarcheDroit", 0.25f);      //après 0.25f, appelle la fonction MarcheDroit

         gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");        //Cette variable va contenir le gameObjet GestionnaireJeu     
         scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques
    }

    void MarcheDroit() {

        modifieGrandeurX = grandeurDepartX;     //La variable modifieGrandeurX est égal a la variable grandeurDepartX
        rbLizard.velocity = new Vector2(vitesse, rbLizard.velocity.y);      //Modifie la position en x du lizard et sa position en x est sa vitesse 
        transform.localScale = new Vector2(modifieGrandeurX, transform.localScale.y);   //Modifie le scale en x du lizard et son scale en x est égale à modifieGrandeurX

        if(transform.position.x < positionFinal) {  //Si la position en x du lizard est plus petit que sa position final en x...
            Invoke("MarcheDroit", 0.25f);       //après 0.25f, appelle la fonction MarcheDroit
        }

        else     //Sinon...
        {
            Invoke("MarcheGauche", 0.25f);      //après 0.25f, appelle la fonction MarcheGauche
        }
        
    } 

    void MarcheGauche() {

        modifieGrandeurX = -grandeurDepartX;    //La variable modifieGrandeurX est -égal a la variable grandeurDepartX
        rbLizard.velocity = new Vector2(-vitesse, rbLizard.velocity.y);      //Modifie la position en x du lizard et sa position en x est sa vitesse mais en negatif
        transform.localScale = new Vector2(modifieGrandeurX, transform.localScale.y);   //Modifie le scale en x du lizard et son scale en x est égale à modifieGrandeurX mais en négatif
        
        if(transform.position.x > positionDepart) {     //Si la position en x du lizard est plus grand que sa position final en x...
            Invoke("MarcheGauche", 0.25f);  //après 0.25f, appelle la fonction MarcheGauche
        }

        else    //Sinon
        {
            Invoke("MarcheDroit", 0.25f);    //après 0.25f, appelle la fonction MarcheDroit
        }
        
    }

    void OnCollisionEnter2D(Collision2D objetEnCollision){      
        if(objetEnCollision.transform.tag == "Player") {        //Si il y a une collision avec le player...

            scriptManager.SonDamage();   //Joue le son audio damage quand le joueur reçois du dommage
            scriptManager.nbVies--;   //diminu de 1 le nombre de vie qui provient du gamemanager
        }

    }

}

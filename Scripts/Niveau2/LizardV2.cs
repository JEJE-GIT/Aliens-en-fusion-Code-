using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LizardV2 : MonoBehaviour
{

    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager

    private Rigidbody2D rbLizard;   //Cette variable va bientôt contenir les propriétés du Rigidbody2D du lezard, c'est a dire sa physique
    private Collider2D colliderLizard;  //Cette variable va bientôt contenir les propriétés du Collider2D du lezard, c'est a dire sa HitBox
    private float vitesse = 5f;     //Cette variable représente la vitesse du lizard, il est a 5f pour l'instant

    private float grandeurDepartX;      //Cette variable va bientôt contenir le scale originale du gameObjet lizard
    private float modifieGrandeurX;     //Cette variable va bientôt contenir le scale modifié du gameObjet lizard

    private float positionDepart;       //Cette variable va bientôt contenir la position de départ du gameObjet lizard
    private float positionFinal;        //Cette variable va bientôt contenir la position de finale du gameObjet lizard

    private GameObject Hero;     // contient la référence du gameobjet du hero, mais il est vide por l'instant.

    private float viesLizard = 5;        //Le nombre de PV du Lizard



    // Start is called before the first frame update
    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");       //Cette variable va contenir le gameObjet GestionnaireJeu     
        scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques

        Hero = GameObject.Find("Hero");         //Trouve le gameObjet Hero dans la scène

        grandeurDepartX = transform.localScale.x;       //Cette variable contient la valeur du scale originale du gameObjet lizard
        modifieGrandeurX = -transform.localScale.x;      //Cette variable contient la valeur du scale originale du gameObjet lizard, mais sera modifié plus tard
        colliderLizard = GetComponent<Collider2D>();    //Prends les propriétés du Collider2D du lizard, c'est a dire sa HitBox
        rbLizard = GetComponent<Rigidbody2D>();         //Prends les propriétés du Rigidbody2D du lizard, c'est a dire sa physique

        positionDepart = transform.position.x;      //Cette variable contient la position en x du lizard
        positionFinal = positionDepart + 2;         //Cette variable contient la position en x + 1 du lizard

         Invoke("MarcheDroit", 0.25f);      //après 0.25f, appelle la fonction MarcheDroit

    }


    void MarcheDroit() {

        modifieGrandeurX = grandeurDepartX;     //La variable modifieGrandeurX est égal a la variable grandeurDepartX
        transform.localScale = new Vector2(modifieGrandeurX, transform.localScale.y);   //Modifie le scale en x du lizard et son scale en x est égale à modifieGrandeurX
        rbLizard.velocity = new Vector2(vitesse, rbLizard.velocity.y);      //Modifie la position en x du lizard et sa position en x est sa vitesse 

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
        transform.localScale = new Vector2(modifieGrandeurX, transform.localScale.y);   //Modifie le scale en x du lizard et son scale en x est égale à modifieGrandeurX mais en négatif
        rbLizard.velocity = new Vector2(-vitesse, rbLizard.velocity.y);      //Modifie la position en x du lizard et sa position en x est sa vitesse mais en negatif
        
        if(transform.position.x > positionDepart) {     //Si la position en x du lizard est plus grand que sa position final en x...
            Invoke("MarcheGauche", 0.25f);  //après 0.25f, appelle la fonction MarcheGauche
        }

        else    //Sinon
        {
            Invoke("MarcheDroit", 0.25f);    //après 0.25f, appelle la fonction MarcheDroit
        }
        
    }


    void OnCollisionEnter2D(Collision2D other)
    {

        if(other.transform.tag == "Player")     //Si il y a une collision avec le joueur...
        {
            if(other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hexplosion")){      //Si le l'attaque du joueur touche l'enemy...

                scriptManager.SonDamage();   //Joue le son audio damage quand le lizzard reçois du dommage
                viesLizard--;   //Le lizzard perd une vie 

                if(viesLizard == 0) {   //Quand le lizzard n'a plus de vies...
                    GetComponent<Animator>().Play("Meurt");   //Joue l'animation de mort du lizzard
                    GetComponent<BoxCollider2D>().enabled = false;  //Désactive le collider du lizzard
                }

               
            }
            else{
                Hero.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10000f);   //Projette le joueur vers la gauche car il se fait toucher
                Hero.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500f);       //Projette le joueur vers le haut car il se fait toucher
                scriptManager.SonDamage();   //Joue le son audio damage quand le joueur reçois du dommage
               scriptManager.nbVies--;   //diminue de 1 le nombre de vie du joueur qui provient du gamemanager
            }
        }

    }


    void meurt() {
        Destroy(gameObject);    //Detruit le gameobjet du lizzard lizard
    }

    void GagnerPoints() {
        Debug.Log("10+ point(s)");   //Ajoute 10 points
    }
}

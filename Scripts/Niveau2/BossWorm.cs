using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWorm : MonoBehaviour
{


    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager

    private Rigidbody2D rbWorm;   //Cette variable va bientôt contenir les propriétés du Rigidbody2D du Worm, c'est a dire sa physique
    private Collider2D colliderWorm;  //Cette variable va bientôt contenir les propriétés du Collider2D du Worm, c'est a dire sa HitBox
    private float vitesse = 10f;     //Cette variable représente la vitesse du Worm, il est a 7f pour l'instant
    private float vitesseSaut = 10f; 

    private float grandeurDepartX;      //Cette variable va bientôt contenir le scale originale du gameObjet 
    private float modifieGrandeurX;     //Cette variable va bientôt contenir le scale modifié du gameObjet 

    private float positionDepart;       //Cette variable va bientôt contenir la position de départ du gameObjet 
    private float positionFinal;        //Cette variable va bientôt contenir la position de finale du gameObjet 

    private GameObject Hero;     // contient la référence du gameobjet du hero, mais il est vide por l'instant.

    private float viesBoss = 30;        //Le nombre de PV du Boss


    // Start is called before the first frame update
    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");       //Cette variable va contenir le gameObjet GestionnaireJeu     
        scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques

        Hero = GameObject.Find("Hero");         //Trouve le gameObjet Hero dans la scène

        grandeurDepartX = transform.localScale.x;       //Cette variable contient la valeur du scale originale du gameObjet Worm
        modifieGrandeurX = -transform.localScale.x;      //Cette variable contient la valeur du scale originale du gameObjet Worm, mais sera modifié plus tard
        colliderWorm = GetComponent<Collider2D>();    //Prends les propriétés du Collider2D du Worm, c'est a dire sa HitBox
        rbWorm = GetComponent<Rigidbody2D>();         //Prends les propriétés du Rigidbody2D du Worm, c'est a dire sa physique

        positionDepart = transform.position.x;      //Cette variable contient la position en x du Worm
        positionFinal = positionDepart + 2;         //Cette variable contient la position en x + 1 du Worm

         Invoke("MarcheDroit", 0.25f);      //après 0.25f, appelle la fonction MarcheDroit

    }

    void Update() {
        Saute();        //Le boss va sauter quand le worm va sauter
    }


    void MarcheDroit() {

        modifieGrandeurX = -grandeurDepartX;     //La variable modifieGrandeurX est égal a la variable grandeurDepartX
        transform.localScale = new Vector2(modifieGrandeurX, transform.localScale.y);   //Modifie le scale en x du Worm et son scale en x est égale à modifieGrandeurX
        rbWorm.velocity = new Vector2(vitesse, rbWorm.velocity.y);      //Modifie la position en x du Worm et sa position en x est sa vitesse 

        if(transform.position.x < positionFinal + 3) {  //Si la position en x du Worm est plus petit que sa position final en x...
            Invoke("MarcheDroit", 0.25f);       //après 0.25f, appelle la fonction MarcheDroit
        }

        else     //Sinon...
        {
            Invoke("MarcheGauche", 0.25f);      //après 0.25f, appelle la fonction MarcheGauche
        }
        
    }


     void MarcheGauche() {

        modifieGrandeurX = grandeurDepartX;    //La variable modifieGrandeurX est -égal a la variable grandeurDepartX
        transform.localScale = new Vector2(modifieGrandeurX, transform.localScale.y);   //Modifie le scale en x du Worm et son scale en x est égale à modifieGrandeurX mais en négatif
        rbWorm.velocity = new Vector2(-vitesse, rbWorm.velocity.y);      //Modifie la position en x du Worm et sa position en x est sa vitesse mais en negatif
        
        if(transform.position.x > positionDepart - 13) {     //Si la position en x du Worm est plus grand que sa position final en x...
            Invoke("MarcheGauche", 0.25f);  //après 0.25f, appelle la fonction MarcheGauche
        }

        else    //Sinon
        {
            Invoke("MarcheDroit", 0.25f);    //après 0.25f, appelle la fonction MarcheDroit
        }
        
    }


    void Saute(){

            int layerSauter = LayerMask.GetMask("Sol");                 //Met en valeur tout les gameobjets qui contient "Sol" comme layerMask

            if(!colliderWorm.IsTouchingLayers(layerSauter)) {       //Si le Worm n'est pas sur un layer qui peut sauter...
                return;         //Saute pas
            }

            else{       //Sinon

                if(Input.GetButtonDown("Jump")) {       //Si on appui sur le button sauter...
                    rbWorm.velocity = new Vector2(0,vitesseSaut);       //Change la valeur en y le rb du Worm par vitesseSaut, cela veut dire que le Worm va sauter
                }

            }

        }


    void OnCollisionEnter2D(Collision2D other)
    {

        if(other.transform.tag == "Player")     //Si il y a une collision avec le joueur...
        {
            if(other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hexplosion")){      //Si le l'attaque du joueur touche l'enemy...

                scriptManager.SonDamage();   //Joue le son audio damage quand le boss reçoi du dommage
                viesBoss--;     //Le boss perd une vie quand le joueur lui tire dessus

                if(viesBoss == 0) {
                    GetComponent<Animator>().Play("Meurt");   //Joue l'animation de mort du WormBoss
                    GetComponent<BoxCollider2D>().enabled = false;      //Désactive la hitbox du Boss parce qu'il est vaincu
                    scriptManager.Victoire();
                }

            }
            else{
               scriptManager.nbVies--;   //diminu de 1 le nombre de vie qui provient du gamemanager
               scriptManager.SonDamage();   //Joue le son audio damage quand le joueur reçois du dommage
               Hero.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10000f);    //Projette le joueur vers la gauche car il se fait toucher
                Hero.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500f);   //Projette le joueur vers le haut car il se fait toucher
            }
        }

    }


    void meurt() {
        Destroy(gameObject);    //Detruit le Worm
    }

    void GagnerPoints() {
        Debug.Log("50+ point(s)");   //Ajoute 50 points
    }


}

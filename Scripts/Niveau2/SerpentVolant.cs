using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerpentVolant : MonoBehaviour
{

    private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager

    private GameObject Hero;     // contient la référence du gameobjet du hero, mais il est vide por l'instant.

    // Start is called before the first frame update
    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");       //Cette variable va contenir le gameObjet GestionnaireJeu     
        scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques 

        Hero = GameObject.Find("Hero");     //Cherche le GameObjet Hero dans la scène
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if(other.transform.tag == "Player")     //Si il y a une collision avec le joueur...
        {
            if(other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("hexplosion")){      //Si le l'attaque du joueur touche l'enemy...
                scriptManager.SonDamage();   //Joue le son audio damage quand le serpent-volant reçois du dommage
                GetComponent<Animator>().Play("SerpentMort");   //Joue l'animation de mort du serpent-volant
                GetComponent<BoxCollider2D>().enabled = false;      //Désactive le collider du serpent-volant
            }
            else{
                Hero.GetComponent<Rigidbody2D>().AddForce(Vector2.left * 10000f);    //Projette le joueur vers la gauche car il se fait toucher
                Hero.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500f);       //Projette le joueur vers le haut car il se fait toucher
                scriptManager.SonDamage();   //Joue le son audio damage quand le joueur reçois du dommage
                scriptManager.nbVies--;   //diminu de 1 le nombre de vie qui provient du gamemanager
            }
        }

    }


    void meurt() {
        Destroy(gameObject);    //Detruit le serpent-volant
    }

    void GagnerPoints() {
        Debug.Log("5+ point(s)");   //Ajoute 5 points
    }

}

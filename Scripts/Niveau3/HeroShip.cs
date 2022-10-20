using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShip : MonoBehaviour
{

    [SerializeField] Rigidbody2D rbHeroShip;    //Il faut mettre le Rb du HeroShip dans l'inspecteur
    [SerializeField] private float vitesse = 13 ;   //La vitesse de HeroShip est à 13 par défaut et il peut être modifié via l'inspecteur
    [SerializeField] GameObject projectile;     //Il faut mettre le GameObjet laser dans l'inspecteur

     private GameObject gestionnaireDuJeu;       //Cette variable va bientôt garder en mémoire le GameObject "gestionnaireDuJeu"
    private GameManager scriptManager;      //Cette Cette variable va bientôt garder en mémoire la fonction défaite du script du GameManager

    private float touchesVerticale;     //Cette variable va bientôt contenir les touches pour faire bouger le HeroShip à la verticale
    private float touchesHorizontale;   //Cette variable va bientôt contenir les touches pour faire bouger le HeroShip à l'horizontale

    private float velociteX;     //Cette variable va bientôt contenir la vitesse de déplacement * les touches Honrizontales en X du HeroShip
    private float velociteY;     //Cette variable va bientôt contenir la vitesse de déplacement * les touches Honrizontales en Y du HeroShip

    private bool voleHeroShip = false;  //pour vérifier dans l'animator si le personnage doit bouger ou non. Il est sur false par défaut

    void Start()
    {
        gestionnaireDuJeu = GameObject.Find("GestionnaireJeu");       //Cette variable va contenir le gameObjet GestionnaireJeu     
        scriptManager = gestionnaireDuJeu.GetComponent<GameManager>();       //Va chercher le gameobjet "GameManager" pour acceder a ses fonctions publiques
    }

    void Update()
    {
        touchesHorizontale = Input.GetAxis("Horizontal");   //Cette variable contient les touches Honrizontales du HeroShip
        touchesVerticale = Input.GetAxis("Vertical");       //Cette variable contient les touches Verticales du HeroShip
        Voler();       //Cette fonction va servir à faire bouger le HeroShip
        Tirer();      //Cette fonction va servir tirer des projectiles
    }


    void Voler()
    {

        voleHeroShip = rbHeroShip.velocity.x != 0;     //Vérifie si le hero avance ou pas    0 = avance pas , != 0  = avance

            velociteX = vitesse * touchesHorizontale;       //La velocité du personnage est égal a la vitesse * les touchesHorizontale pour qui puisse bouger dans l'axe des X

            velociteY = vitesse * touchesVerticale;       //La velocité du personnage est égal a la vitesse * les touchesHorizontale pour qui puisse bouger dans l'axe des X

            rbHeroShip.velocity = new Vector2(velociteX, velociteY);    //Modifie la position en x et y du HeroShip
    }


    void Tirer()
    {
        if (Input.GetKeyDown("z"))      //Si le joueur appui sur la touche z..
        {
            scriptManager.SonTirer();  //Joue le son audioTirer via le GameManager
            GameObject NouveauProjectile = Instantiate(projectile, transform.position, transform.rotation);     //Créer le GameObjet laser à la même position que le joueur
            NouveauProjectile.GetComponent<SpriteRenderer>().sortingLayerName = "Troisième-Plan";   //Met le projectile sur le Troisième-Plan dans les sortingLayers
            NouveauProjectile.GetComponent<SpriteRenderer>().sortingOrder = 0;  //l'ordre du sortingLayer est à 0
        }
    }

}

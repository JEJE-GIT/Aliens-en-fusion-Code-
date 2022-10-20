using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] public float vitesseMarche = 2;  //Vitesse de déplacement du personnage qui est disponible dans l'inspecteur
    [SerializeField] public float vitesseSaut = 3;     //Vitesse du Saut du personnage qui est disponible dans l'inspecteur

    [SerializeField] private AudioClip audioSauter = null;
    [SerializeField] private AudioClip audioexplose = null;
    private AudioSource hero_AudioSource;

    private Animator animHero;      //Cette variable va bientôt contenir les animations du Hero qui provient de l'animator
    private Rigidbody2D rbHero;      //Cette variable va bientôt contenir les propriétés du Rigidbody2D du Hero, c'est a dire sa physique
    private Collider2D colliderHero;    //Cette variable va bientôt contenir les propriétés du Collider2D du Hero, c'est a dire sa HitBox

    private float grandeurDebutX;   //Cette variable va bientôt contenir le scale originale du gameObjet Hero
    private float modifierGrandeurX;    //Cette variable va bientôt contenir le scale modifié du gameObjet Hero
    private float velocite;     //Cette variable va bientôt contenir la vitesse de déplacement du personnage

    private float touchesHorizontal;    //Les touches pour déplacer le personnage horizontalement 

    private bool marcheHero = false;  //pour vérifier dans l'animator si le personnage doit bouger ou non. Il est sur false par défaut

    public string nomScene;     //Cette variable va bientôt contenir le nom de la scene 



    // Start is called before the first frame update
    void Start()
    {
        grandeurDebutX = transform.localScale.x;        //la variable grandeurDebutX contient la valeur du scale originale du gameObjet Hero 
        modifierGrandeurX = transform.localScale.x;     //la variable grandeurDebutX contient la valeur du scale originale du gameObjet Hero, mais sera modifié plus tard

        animHero = GetComponent<Animator>();        //Prends les animations du Hero qui provient de l'animator
        rbHero = GetComponent<Rigidbody2D>();       //Prends les propriétés du Rigidbody2D du Hero, c'est a dire sa physique
        colliderHero = GetComponent<Collider2D>();  //Prends les propriétés du Collider2D du Hero, c'est a dire sa HitBox

        hero_AudioSource = GetComponent<AudioSource>();     //Prends la source audio du hero

        nomScene = SceneManager.GetActiveScene().name;       //Récupère le nom de la scene qui est joué présentement

    }

    // Update is called once per frame
    void Update()
    {
        touchesHorizontal = Input.GetAxis("Horizontal");    //La variable touchesHorizontal contient les touches de deplacements horizontale en valeur

        Marche();       //Apelle la fonction Marche
        ChangeDirection();      //Apelle la fonction ChangeDirection
        Saute();        //Apelle la fonction Saute

        if(nomScene == "Niveau2") {     //Si vous êtes au niveau 2
            TirerExposion();       //Apelle la fonction TirerExposion (Tirer une explotion a courte distance)
        }

        //Tirer();       //Apelle la fonction Tirer à longue distance
        Cours();        //Apelle la fonction Cours

        

    }

//------------------------------------------------------------------------------\\

        void Marche() {

            marcheHero = rbHero.velocity.x != 0;     //Vérifie si le hero avance ou pas    0 = avance pas , != 0  = avance
            animHero.SetBool("okMarche", marcheHero);   //appele la booléenne "okMarche"

            velocite = vitesseMarche * touchesHorizontal;       //La velocité du personnage est égal a la vitesse de marche * les touchesHorizontal pour qui puisse avancer et reculer
            rbHero.velocity = new Vector2(velocite, rbHero.velocity.y);    //Modifie la position en x du personnage

        }

//------------------------------------------------------------------------------\\

        void ChangeDirection() {

            if(touchesHorizontal > 0) {     //Si la touchesHorizontal appuyé est positif, c'est a dire plus grand que 0... 
                modifierGrandeurX = grandeurDebutX;    //tourne le personnage Vers la droite
            }

            if(touchesHorizontal < 0) {     //Si la touchesHorizontal appuyé est negatif, c'est a dire plus petit que 0... 
                modifierGrandeurX = -grandeurDebutX;      //tourne le personnage Vers la droite
            }

            transform.localScale = new Vector2(modifierGrandeurX, transform.localScale.y);   //Modifie le scale en x du personnage

        }

//------------------------------------------------------------------------------\\

         void Cours() {

            

                    if(Input.GetKeyDown("left shift")) {             //Si la touche shift est appuyé
                    animHero.speed = animHero.speed * 2;            //Augmente 2 fois la vitesste de de l'animation 

                    if(nomScene == "Niveau2"){
                        vitesseMarche = vitesseMarche * 2.5f;             //Augmente 2 fois la vitesste de la vitesse de marche
                    }

                    else{
                        vitesseMarche = vitesseMarche * 2;             //Augmente 2 fois la vitesste de la vitesse de marche
                    }

                }

           

           if(Input.GetKeyUp("left shift")) {           //Si la touche shift n'est pas appuyé
          
                 animHero.speed = animHero.speed / 2;            //Augmente 2 fois la vitesste de de l'animation 

                    if(nomScene == "Niveau2"){
                        vitesseMarche = vitesseMarche / 2.5f;             //Augmente 2 fois la vitesste de la vitesse de marche
                    }

                    else{
                        vitesseMarche = vitesseMarche / 2;             //Augmente 2 fois la vitesste de la vitesse de marche
                    }
                }

            

        } 

//------------------------------------------------------------------------------\\

        void Saute(){

            int layerSauter = LayerMask.GetMask("Sol");                 //Met en valeur tout les gameobjets qui contient "Sol" comme layerMask

            if(!colliderHero.IsTouchingLayers(layerSauter)) {       //Si le personnage n'est pas sur un layer qui peut sauter...
                return;         //Saute pas
            }

            else{       //Sinon

                if(Input.GetButtonDown("Jump")) {       //Si on appui sur le button sauter...
                    rbHero.velocity = new Vector2(0,vitesseSaut);       //Change la valeur en y le rb du personnage par vitesseSaut, cela veut dire que le personnage va sauter
                    animHero.SetTrigger("enSaut");      //Active l'animation de saut du personnage dans l'animator
                    hero_AudioSource.PlayOneShot(audioSauter);  //Joue l'audio sauter une seule fois
                }

            }

        }


//------------------------------------------------------------------------------\\


        void TirerExposion() {

            if(Input.GetKeyDown("z")){
                animHero.SetTrigger("explosionAttack");
                GetComponent<CircleCollider2D>().enabled = true;
                hero_AudioSource.PlayOneShot(audioexplose);
                velocite = vitesseMarche * touchesHorizontal;       //La velocité du personnage est égal a la vitesse de marche * les touchesHorizontal pour qui puisse avancer et reculer

            if(transform.localScale.x == grandeurDebutX) {     //Si la touchesHorizontal appuyé est positif, c'est a dire plus grand que 0... 
                rbHero.AddForce(Vector2.left * 1500f);    //Modifie la position en x du personnage
            }

            if(transform.localScale.x == -grandeurDebutX) {     //Si la touchesHorizontal appuyé est negatif, c'est a dire plus petit que 0... 
                rbHero.AddForce(Vector2.right * 1500f);    //Modifie la position en x du personnage
            }

            }

        }


        void StopTirerExposion() {
            GetComponent<CircleCollider2D>().enabled = false;   //Désactive le circle collider quand le joueur arrête de tirer
        }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] private float tempsDuJeu = 59;     //Cette variable contient le nombre du temps du niveau 1
    public float nbVies = 3;
    private float tempTotalDuJeu;   //Cette variable contient le nombre du temps du niveau 1 ou celle du niveau 2
    static public string tempFinal;    //Cette variable va contenir le nombre que cela a prit au joueur de finir le niveau

    [SerializeField] private AudioClip audioGagner = null;
    [SerializeField] private AudioClip audioPerdre = null;
    [SerializeField] private AudioClip audioPowerUp = null;      
    [SerializeField] private AudioClip audioPoints = null;
    [SerializeField] private AudioClip audioDamage = null;      //Cette variable va contenir l'audio quand le boss va recevoir des dégâts
    [SerializeField] private AudioClip audioTirer = null;

    private AudioSource game_AudioSource;
    
    private GameObject Hero;     // contient la référence du gameobjet du hero, mais il est vide por l'instant.
    private GameObject HeroShip;     // contient la référence du gameobjet du heroShip, mais il est vide por l'instant.
    private GameObject Terminal;    //Cette variable va bientôt prendre le gameObjet "terminalFinish dans le niveau 
    [SerializeField] private GameObject afficherMessage;    //Cette variable va bientôt contenir le message dans la scene de fin du niveau 1

    private Animator animationHero;   //Cette variable va bientôt prendre les animations du Hero dans l'animator
    private Animator animationHeroShip;   //Cette variable va bientôt prendre les animations du HeroShip dans l'animator

    private Text texteTemps;    //Cette variable va bientôt contenir le texte du temps qui se trouve dans la scene du niveau 1
    private Text textePoints;   //Cette variable va bientôt contenir le texte des points qui se trouve dans la scene du niveau 1

    public int points;      //Cette variable contient le nombre de points accumulé dans le niveau 1

    static public string nomHero;   //Cette variable va bientôt contenir le nom du personnage
    public string nomScene;     //Cette variable va bientôt contenir le nom de la scene

    


    // Start is called before the first frame update
    void Start()
    {
        
        nomScene = SceneManager.GetActiveScene().name;       //Récupère le nom de la scene qui est joué présentement
        game_AudioSource = GetComponent<AudioSource>();

        if(nomScene == "Niveau1"){      //Si le nom de la scene est Niveau1
             Niveau1();     //Appelle la fonction niveau 1
        }

         if(nomScene == "Niveau2"){      //Si le nom de la scene est Niveau2
             Niveau2();     //Appelle la fonction niveau 2
        }

        if(nomScene == "Niveau3"){      //Si le nom de la scene est Niveau3
             Niveau3();     //Appelle la fonction niveau 3
        }

        if(nomScene == "FinNiveau1"){      //Si le nom de la scene est FinNiveau 1
             SceneFinNiveau1();     //Appelle la fonction SceneFinNiveau1 1
        }

        if(nomScene == "FinNiveau2"){      //Si le nom de la scene est FinNiveau 2
             SceneFinNiveau2();     //Appelle la fonction SceneFinNiveau1 2
        }

       /* if(nomScene == "FinNiveau3"){      //Si le nom de la scene est FinNiveau3
             SceneFinNiveau3();     //Appelle la fonction SceneFinNiveau 3 
        }  */
        
    }


    void Update() {
        DebutPartie();  //Appelle la fonction DebutPartie

        if(nomScene == "Niveau1" || nomScene == "Niveau2") {

            if(nbVies == 3 || nbVies == 2) {
            Hero.GetComponent<Renderer>().material.color = new Color(0,0,1);       
            }

                else{
                    Hero.GetComponent<Renderer>().material.color = new Color(1,1,1);  
                    }

            Defaite();  //Vérifie si le joueur n'a plus de vie

        }

        if(nomScene == "Niveau3") {

            if(nbVies == 3 || nbVies == 2) {
            HeroShip.GetComponent<Renderer>().material.color = new Color(0,0,1);       
            }

                else{
                    HeroShip.GetComponent<Renderer>().material.color = new Color(1,1,1);  
                    }

            Defaite();  //Vérifie si le joueur n'a plus de vie

        }

    }


    public void DebutPartie(){


        if(nomScene == "Acceuil"){      //Si le nom de la scene est Niveau1
            if(Input.GetKeyDown(KeyCode.Return)) {      //Si la touche enter est appuyé

        SceneManager.LoadScene("Niveau1");  //Charge la scene du niveau 1
        
        nomHero = GameObject.Find("ChampTexteHero").GetComponent<InputField>().text;    //Récupère le champ de texte qui contient le nom du perso nnage, on met " GameObject.Find("ChampTexteHero")" car ce script n'est pas appliqué directement au gameObjet ChampTexteHero qui se trouve dans la scene d'acceuil, donc il faut aller chercher son gameObjet
        Debug.Log(nomHero);     //Écrit le nom du personnage dans la console


        if(nomHero == ""){      //Si il n'est rien inscrit dans le champs de texte pour le nom du personnage...
             nomHero = "RB-Ver01";     //Met le nom du protagonist par défaut
             SceneManager.LoadScene("Niveau1");     //Charge la scene du Niveau1 et ensuite...
        }

        }
        }

        if(nomScene == "FinNiveau1"){      //Si le nom de la scene est Niveau1
            if(Input.GetKeyDown(KeyCode.Return)) {      //Si la touche enter est appuyé

            SceneManager.LoadScene("Niveau2");  //Charge la scene du niveau 1
            }
        }

        if(nomScene == "FinNiveau2"){      //Si le nom de la scene est Niveau1
            if(Input.GetKeyDown(KeyCode.Return)) {      //Si la touche enter est appuyé

            SceneManager.LoadScene("Niveau3");  //Charge la scene du niveau 1
            }
        }
         
    }

    public void Niveau1(){

        Hero = GameObject.Find("Hero");     //Cherche le GameObjet Hero dans la scene
        tempTotalDuJeu = tempsDuJeu;

        animationHero = GameObject.Find("Hero").GetComponent<Animator>();   //Récupère les animations du Hero, on met "GameObject.Find("Hero")" car ce script n'est pas appliqué directement au gameObjet Hero, donc il faut aller chercher son gameObjet
        Terminal = GameObject.Find("terminalFinish");   //Récupère le gameObjet terminalFinish dans la scene du niveau 1
        GameObject.Find("NomHero").GetComponent<Text>().text = nomHero;     //Récupère le gameobjet "NomHero" qui se trouve dans le Canvas du niveau et met en valeur le nom du personnage

        texteTemps = GameObject.Find("Temps").GetComponent<Text>();     //Chercher le gameobjet "temps" dans la scene du niveau 1 et met dans la variable texteTemps
        texteTemps.text = "0:" + tempsDuJeu;        //affiche le gameObjet temps : "0" + la valeur "tempsDuJeu"
        Invoke("CompteARebours", 1);        //Appeler la fonction CompteARebours a chaque seconde


        textePoints = GameObject.Find("Points").GetComponent<Text>();       //Chercher le texte des points dans la scene du niveau 1
        textePoints.text = "0";     //Le texte des points est a 0 au début   

    }

    public void Niveau2(){
        Hero = GameObject.Find("Hero");     //Cherche le GameObjet Hero dans la scene
        GameObject.Find("NomHero").GetComponent<Text>().text = nomHero;     //Récupère le gameobjet "NomHero" qui se trouve dans le Canvas du niveau et met en valeur le nom du personnage
        animationHero = Hero.GetComponent<Animator>();      //Récupère les animations du Hero de l'animator
    }

    public void Niveau3() {
        HeroShip = GameObject.Find("HeroShip");     //Cherche le GameObjet HeroShip dans la scene
        animationHeroShip = HeroShip.GetComponent<Animator>();  //Récupère les animations du Hero de l'animator
        GameObject.Find("NomHero").GetComponent<Text>().text = nomHero;     //Récupère le gameobjet "NomHero" qui se trouve dans le Canvas du niveau et met en valeur le nom du personnage
    }


    void CompteARebours() {

        --tempsDuJeu;       //Soustraire de 1 la valeur de tempsDuJeu

        if(tempsDuJeu < 10) {  
            texteTemps.text = "0:0" + tempsDuJeu;       //Met 00:0_ si la valeur tempsDuJeu est plus petit que 10
        }

        else{
            texteTemps.text = "0:" + tempsDuJeu;    //Met 00:__ si la valeur tempsDuJeu est plus grand que 10
        }

        if(tempsDuJeu == 0) {  //Si le compteur à rebours est a 0...
            Defaite();
        }

        else{
            Invoke("CompteARebours", 1);     //Sinon appelle la fonction CompteARebours a chaque 1 seconde
        }

    }

    public void PointsAdditionner(){

        points++;       //Additione de 1 la valeur de point
        game_AudioSource.PlayOneShot(audioPoints);
        textePoints.text = points.ToString() + "/6";   //la variable textePoints est egal a la variable points en string

    }

    public void Victoire() {

        if(nomScene == "Niveau1"){   //Si le nom de la scene est Niveau1

            if(points == 6) {        //Si le nombre de points ramassé est égal à 6...
            animationHero.SetBool("gagner", true);   //Joue l'animation de Victoire du personnage
            Debug.Log("Tu as gagné !!!");    //Écrit cela dans la console pour mettre en évidence au joueur qu'il a gagné la partie
            game_AudioSource.PlayOneShot(audioGagner);

            Invoke("SceneFinNiveau1Load", 3);      //Appele la scene fon Niveau 1 dans 3 seconde
                tempFinal = (tempTotalDuJeu - tempsDuJeu).ToString();  //la valeur de la variable tempFinal met la difference entre le tempTotalDuJeu et le tempsDuJeu pour indiquer le temps que cela a pris au joueur de finir le niveau

          }

        }

         if(nomScene == "Niveau2"){     //Si le nom de la scene est Niveau1

            animationHero.SetBool("gagner", true);   //Joue l'animation de Victoire du personnage
            Debug.Log("Tu as gagné !!!");    //Écrit cela dans la console pour mettre en évidence au joueur qu'il a gagné la partie
            Invoke("SceneFinNiveau2Load", 3);      //Appele la scene de fin Niveau2 dans 3 seconde

         }
       
    }       

    public void Defaite() {

            if(nbVies == 0 || nbVies == -1 || nbVies == -2 || nbVies == -3) {

                if(nomScene == "Niveau1" || nomScene == "Niveau2"){

                animationHero.SetBool("defaite", true);      //Joue l'animation de défaite du personnage
                Debug.Log("Tu as perdu");    //Écrit cela dans la console pour mettre en évidence au joueur qu'il a perdu la partie
                Invoke("SceneAcceuil", 0.3f);   //Après un délai de 1 seconde... charger la scene d'acceuil
                game_AudioSource.PlayOneShot(audioPerdre);

                }

                if(nomScene == "Niveau3"){

                animationHeroShip.SetBool("Meurt", true);      //Joue l'animation de défaite du personnage
                Debug.Log("Tu as perdu");    //Écrit cela dans la console pour mettre en évidence au joueur qu'il a perdu la partie
                Invoke("SceneAcceuil", 0.3f);   //Après un délai de 1 seconde... charger la scene d'acceuil
                game_AudioSource.PlayOneShot(audioPerdre);
                
                }

            }

                 
    }


    public void SonPowerUp() {
        game_AudioSource.PlayOneShot(audioPowerUp);      //Cette fonction se fait appeler dans le script PowerUp, Quand le joueur touche un PowerUp, l'audioPowerUp joue
    }

    public void SonDamage() {
        game_AudioSource.PlayOneShot(audioDamage);      //Cette fonction se fait appeler dans plusieurs scripts et l'audioDamage joue quand Quelqu'un reçoi des dommages
    }

    public void SonTirer() {
        game_AudioSource.PlayOneShot(audioTirer);       //Cette fonction se fait appeler dans le script BossWorm, l'audioDamage joue quand le HeroShip tire
    }




    public void SceneAcceuil() {
        SceneManager.LoadScene("Acceuil");  //Charge la scene d'acceuil
    }

    void SceneFinNiveau1() {
        afficherMessage.GetComponent<TMP_Text>().text = "Temps record:" + "<br>" + tempFinal + " " + "secondes";  //Quand la scene FinNiveau1 va charger, affiche le temps record du joueur
    }

     void SceneFinNiveau2() {
        GameObject.Find("Bravo").GetComponent<Text>().text = "Bravo" + nomHero;  //Quand la scene FinNiveau2 va charger, affiche le nom du joueur et un petit bravo
    }

    void SceneFinNiveau1Load() {
        SceneManager.LoadScene("FinNiveau1");  //Charge la scene FinNiveau1
    }

    void SceneFinNiveau2Load() {
        SceneManager.LoadScene("FinNiveau2");  //Charge la scene FinNiveau1
    }

    public void SceneFinNiveau3Load() {
        SceneManager.LoadScene("FinNiveau3");  //Charge la scene FinNiveau1
    }

}

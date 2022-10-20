using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FermerAide : MonoBehaviour
{
    [SerializeField] private GameObject aide;   //Cette variable va bientôt contenir la fenetre aide de la scene d'acceuil
    [SerializeField] private GameObject Canvas;  //Cette variable va bientôt contenir le canvas de la scene d'acceuil
    public string nomScene;     //Cette variable va bientôt contenir le nom de la scene

    void Start() {
        nomScene = SceneManager.GetActiveScene().name;       //Récupère le nom de la scene qui est joué présentement

        if(nomScene == "Acceuil"){      //Si le nom de la scene est Niveau1
            OnMouseDown();
        }

    } 

    void OnMouseDown(){     //Si on appui sur le x de la fenetre aide...

        aide.SetActive(false);      //ferme la fenetre aide 
        Canvas.SetActive(true);     //ouvre le canvas
    }
}

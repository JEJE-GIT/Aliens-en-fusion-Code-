using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    private GameObject HeroShip;    //Cette variable va bientôt contenir le GameObjet HeroShip
    private Rigidbody2D rbLaser;    //Cette variable va bientôt contenir le Rigidbody2D du GameObjet laser


    private float vitesse = 20;     //La vitesse du laser est à 20
    private Vector2 positionProjectile;     //Cette variable servira a modifier la position du laser

    void Start()
    {
        HeroShip = GameObject.Find("HeroShip");    //Cette variable contient le GameObjet HeroShip dans la scene
        rbLaser = GetComponent<Rigidbody2D>();     //Cette variable prends le component du Rigidbody2D du laser

        positionProjectile = transform.position;    //la variable positionProjectile est égal à la position du projectile via l'inspecteur.
        rbLaser.velocity = transform.up * vitesse;  //Fait bouger le laser vers le haut grâce à son Rigidbody2D

    }



    void OnTriggerEnter2D(Collider2D other)     //Vérifie si il y a d'autres collisions avec le collider
    {

        if (other.transform.tag == "EnemyShip")    //Si le laser a une collision avec des enemies
        {
            Destroy(other.gameObject);      //Dédruit l'enemie
            Destroy(gameObject);    //Dédruit le laser
        }

        if (other.transform.tag == "Limite")    //Si le laser a une collision avec la limite de distance de tir
        {
            Destroy(gameObject);    //Dédruit le laser
        }


    }


}

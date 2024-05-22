using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float bulletSpeed;
    

    void Start(){
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag=="Player") return;
        if(collider.gameObject.tag=="Goomba") Destroy(collider.gameObject);
        Destroy(gameObject);
    }
}

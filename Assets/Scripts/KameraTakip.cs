using System.Collections;
using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform player;// tanımlamalarımızı yaptık
    public float x, y;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;// palyer tagı olan a yap
    }


    void Update()   // karakterimiz hareket edince kamera da haraeket etsin karakterimizle
    {
        transform.position = new Vector3(player.position.x+x,player.position.y+y,-10);
    }
}

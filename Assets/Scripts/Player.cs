using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Unıty UI kütüphanesini ekledik
public class Player : MonoBehaviour
{
    // tanımlamalar
    private float speed = 7.0f; //karakter in  hızı
    private Rigidbody2D myRigiBody;  //isim atadık
    private float maxVelocity = 4.0f; //karakteimizin hız limiti
    private int jumpforce = 225;  //karakterin zıplama hızı
    private int ziplamasayisi = 2; //zıplama sayısı
    public int can, maxcan;// can ile maxcan 
    public GameObject[] canlar;//canların gösterimi
    public int altin;
    public Text altinmiktar; 
    public AudioClip[] sesler; // ses tanımlaması
    public bool yerdemi; 
    Animator anim; // animator tanıtıtk
  

    private void Update() // her defasında güncelleme 
    {

        altinmiktar.text = "" + altin; // altın miktarını text e yazdırma

        if (Input.GetKeyDown(KeyCode.R))   // yeniden başlatma R tuşu ile
        {
            Application.LoadLevel(Application.loadedLevel);
        }
            if(can <= 0)// can 0 a yada eşit ise olme komutu çalışacak
            {
                olme ();

            }
       }
    
    private void Start() // başlama
    {
        anim = GetComponent<Animator>();
        myRigiBody = GetComponent<Rigidbody2D>(); //obje ye bağdaştırdık
        can = maxcan;// canı oyun başlarken maxcan a eşitledik
        canSistemi();
    }
    private void FixedUpdate() // update fonksiyonundan daha hızlı çalıtığı kullanıldı.
    {
        
        anim.SetBool("Yer",yerdemi); // animasyonumuz yerdemi 
        if (ziplamasayisi > 0)//sıfırdan büyükse çalışır
        {
            if (Input.GetKeyDown(KeyCode.Space)) //bir kere çaışır zıplama
            {
                myRigiBody.AddForce(Vector2.up * jumpforce); //yukarı   doğru x,y al ve bunu jump force la çarp
                ziplamasayisi--;
            }
        }
        Kosma();
    }
    private void Kosma(){
        float force = 0.0f;
        float velocity = Mathf.Abs(myRigiBody.velocity.x);//sahnedeki anlık hız mutlak değeri aldı 
        float h = Input.GetAxis("Horizontal");// yatay yönde gitmek için yön belirttik.
        if (h > 0.1f){
            transform.localScale = new Vector2(1, 1); // D tusu yönüne gitmesi animasyonumuzun
        }
        if (h < -0.1f)// Ters tarafa dönsün
        {
            transform.localScale = new Vector2(-1, 1);// A tusu yönüne gitmesi animasyonumuzun Vector 2 xve y yönüne gitmesi
        }
        if (h > 0) //h sıfırdan büyükse sağa doğru gider
        {
            if (velocity < maxVelocity) //karakter belli bir hıza ulaşşın sonra belirli hızda gitsin
            {
                force = speed; //force  speed e eşitledik
                anim.SetFloat("Run", Mathf.Abs(h)); //anismasyon   oynaması
            }
            force = speed; //hızımızı sağa doğru belirtik
        }
        else if (h < 0) //h sıfırdan küçükse sola doğru gider yatay düzlemde.
        {
            if (velocity < maxVelocity)//karakter belli bir hızdan sonra yavaşlasın
            {
                force = -speed;
            }
            force = -speed; // hızı sola doğru 
        }
        myRigiBody.AddForce(new Vector2(force, 0)); //vektor 2  g eksini üzerinden  kuvvet uygulanmıyacak x ve y belirttik
    }
    private void OnCollisionEnter2D(Collision2D hedef) //değdiği nesne
    {
        if (hedef.gameObject.tag == "Tuzak") //game object tagı tuzak ise yapılacaklar
        {
            can -= 1; // canı isterse farklı düşürebilir arttırılırsa
            GetComponent<AudioSource>().PlayOneShot(sesler[2]); //altın objemizde Audio source da 1 kere oynatma  ses verdik
            myRigiBody.AddForce(Vector2.up * jumpforce); //tuzağa değdiği an zıplatma
            GetComponent<SpriteRenderer>().color = Color.red; //tuzağa değdiği  an kırmızı renk olacak
            Invoke("Duzelt", 0.5f);  //yarım saniyede rengi tekrar düzeltme
            canSistemi();
        }
     if(hedef.gameObject.tag =="Kapi") // nesneye gelince 
        {
            Application.LoadLevel("Level");// level e geçsin
        }
        
        if (hedef.gameObject.tag == "Tilemap") ;
        {
            ziplamasayisi = 2; //zıplamamız  sayımız 2 
        }
    }
    private void OnTriggerEnter2D(Collider2D nesne) 
    {

        if (nesne.gameObject.tag == "Altin")
        {
            altin++; //altını 1er artar
            GetComponent<AudioSource>().PlayOneShot(sesler[0]); //altın objemizde Audio source da 1 kere oynatma  ses verdik
            Destroy(nesne.gameObject);
        }
        if (nesne.gameObject.tag == "Kapi")
        {
            Debug.Log("değdi!!!");
        }
        if (nesne.gameObject.tag == "Can")
        {
            GetComponent<AudioSource>().PlayOneShot(sesler[1]); //altın objemizde Audio source da 1 kere oynatma  ses verdik
            if (can != maxcan) //can maxcana eşit değilse
            {
                can++; // can eksik ise birer  ekle
                canSistemi();// cansistemine gitmesi
                GetComponent<SpriteRenderer>().color = Color.green; //can kazandığında karakter yeşil renk olacak
                Invoke("Duzelt", 0.5f);  //yarım saniyede rengi tekrar düzeltme
                Destroy(nesne.gameObject);
            }
        }
    }
    void olme()
    {
        Application.LoadLevel (Application.loadedLevel);
    }
   

    void canSistemi()
    {
        for(int i = 0; i < maxcan; i++)//canvas taki canları kapatma
        {
            canlar[i].SetActive(false);
        }
        for(int i = 0; i < can; i++)//canvas taki canları açma
        {
            canlar[i].SetActive(true);
        }
    }

    void Duzelt()
    {
        GetComponent<SpriteRenderer>().color = Color.white; //tuzağa takıldığında olan kırmızı yarım saniye sonra eski rengine ulaşşın
    }
}
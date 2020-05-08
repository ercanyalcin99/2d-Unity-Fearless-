using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// geçiş kütüphanesi 

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;// doğru yada yanlış olduğuna bakıyoruz
    public GameObject pauseMenuUI; // game objectimizi tanımlıyoruz


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))// esc  
        {
            if (GameIsPaused)
            {
                Resume(); //esc basınca devam etsin
            }
            else // basarsa de
            {
                Pause(); //esc basınca dursun
            }
        }
    }
    public void Resume() //burada ne kadar süryle başlasın dursun 
    {
        pauseMenuUI.SetActive(false); ;//oyun nesnesini etkinleştirmek ve devre dışı bırakabilmek için etkinleştiriyoruz
        Time.timeScale = 1f;//zaman ölçeği ve zamanın geçme hızıdır
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);//oyun nesnesini etkinleştirmek ve devre dışı bırakabilmek için etkinleştiriyoruz
        Time.timeScale = 0f;//zaman ölçeği ve zamanın geçme hızıdır
        GameIsPaused = true;
    }
    public void LoadMenu() 
    {
        SceneManager.LoadScene("Giris"); // Giris sayfasına gitsin 
    } 
    public void QuıtGame()
    {
        Debug.Log("Quıtting Game..."); // console da  yazsın 
        Application.Quit(); // oyundan çıksın
    }
}

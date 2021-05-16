using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Menu : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        MuteAnimation(false);
    }

    public void LoadScene(string scene) //iniciar jogo
    {
        SceneManager.LoadScene(scene);
        MuteAnimation(true);
    }

    public void ApplicationQuit() // sair do jogo
    {
        Application.Quit();
    }

    public void MuteAnimation(bool start) //mudar menu
    {
        anim.SetBool("iniciar", start);
    }
}
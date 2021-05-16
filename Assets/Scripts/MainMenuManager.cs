using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Button btnJouer;
    public Button btnInstructions;
    public Button btnRetour;
    public GameObject menuPrincipal;
    public GameObject menuInstructions;
    // Start is called before the first frame update
    void Awake()
    {
        btnJouer.onClick.AddListener(btnJouer_onClick);
        btnInstructions.onClick.AddListener(btnInstructions_onClick);
        btnRetour.onClick.AddListener(btnRetour_onClick);
        menuInstructions.SetActive(false);

#if !UNITY_EDITOR && UNITY_WEBGL			
UnityEngine.WebGLInput.captureAllKeyboardInput = false;
#endif


    }

    // La scene de jeux est loader
    void btnJouer_onClick()
    {
        SceneManager.LoadScene("Main");
    }

    // Les instructions sont affiches
    void btnInstructions_onClick()
    {
        menuPrincipal.SetActive(false);
        menuInstructions.SetActive(true);
    }

    // Les instructions sont caches
    void btnRetour_onClick()
    {
        menuPrincipal.SetActive(true);
        menuInstructions.SetActive(false);
    }
}

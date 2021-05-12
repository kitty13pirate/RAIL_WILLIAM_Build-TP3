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
    void Start()
    {
        btnJouer.onClick.AddListener(btnJouer_onClick);
        btnInstructions.onClick.AddListener(btnInstructions_onClick);
        btnRetour.onClick.AddListener(btnRetour_onClick);
        menuInstructions.SetActive(false);
    }

    void btnJouer_onClick()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void btnInstructions_onClick()
    {
        menuPrincipal.SetActive(false);
        menuInstructions.SetActive(true);
    }

    void btnRetour_onClick()
    {
        menuPrincipal.SetActive(true);
        menuInstructions.SetActive(false);
    }
}

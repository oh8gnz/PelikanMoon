using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public Button button;
    private Button btn;
    public GameObject gameobject;

    // Start is called before the first frame update
    void Start()
    {
        btn = button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}

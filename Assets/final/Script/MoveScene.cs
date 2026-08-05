using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{

    public void Movetoscene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }



}
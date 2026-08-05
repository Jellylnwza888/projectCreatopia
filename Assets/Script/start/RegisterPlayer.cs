using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class register : MonoBehaviour
{
    public TMP_InputField username, password, p_name;
    public TMP_Text notic;
    public GameObject success, error;

    private string usernametxt, passwordtxt, p_nametxt;

    void Update()
    {
        usernametxt = username.text;
        passwordtxt = password.text;
        p_nametxt = p_name.text;
    }

    public void registerrequest()
    {
        if (string.IsNullOrEmpty(usernametxt) || string.IsNullOrEmpty(passwordtxt) || string.IsNullOrEmpty(p_nametxt))
        {
            error.SetActive(true);
            notic.text = "กรุณากรอกข้อมูลให้ครบถ้วน";
            notic.fontSize = 80;
            StartCoroutine(stopalerterror());
        }
        else
        {
            StartCoroutine(GetData());
        }
    }

    public IEnumerator GetData()
    {
        string url = "http://localhost/creatopia/add2.php";

        WWWForm form = new WWWForm();
        form.AddField("username", usernametxt);
        form.AddField("password", passwordtxt);
        form.AddField("p_name", p_nametxt); // แก้ตรงนี้!

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + www.error);
            }
            else
            {
                string[] posReturn = www.downloadHandler.text.Split(':');
                Debug.Log("Server Response: " + www.downloadHandler.text);

                if (posReturn.Length > 1)
                {
                    if (posReturn[1] == "0")
                    {
                        error.SetActive(true);
                        notic.text = "รหัสผ่านไม่ตรงกัน";
                        StartCoroutine(stopalerterror());
                    }
                    else if (posReturn[1] == "1")
                    {
                        error.SetActive(true);
                        notic.text = "มีผู้ใช้งานนี้แล้ว";
                        StartCoroutine(stopalerterror());
                    }
                    else
                    {
                        success.SetActive(true);
                        StartCoroutine(stopalertsuccess());
                    }
                }
                else
                {
                    error.SetActive(true);
                    notic.text = "เกิดข้อผิดพลาดจากฝั่งเซิร์ฟเวอร์";
                    StartCoroutine(stopalerterror());
                }
            }
        }
    }

    public IEnumerator stopalertsuccess()
    {
        yield return new WaitForSeconds(3);
        success.SetActive(false);
        SceneManager.LoadScene("login");
    }

    public IEnumerator stopalerterror()
    {
        yield return new WaitForSeconds(3);
        error.SetActive(false);
    }
}
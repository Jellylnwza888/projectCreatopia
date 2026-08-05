using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;//เรียกใช้ Library
using TMPro;
using UnityEngine.SceneManagement;

public class Web : MonoBehaviour
{

    // public TMP_InputField username,pass;
    // string usernametxt,passtxt;

    // // Update is called once per frame
    // void Update()
    // {
    //     usernametxt = username.text.ToString();
    //     passtxt = pass.text.ToString();


    // }

        

    IEnumerator GetData()
    {
        string url = "http://localhost/test/add2.php";

        WWWForm form = new WWWForm();
        form.AddField("myField", "myData");

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);//Error
            }
            else
            {
                string[] posReturn = www.downloadHandler.text.Split(new char[] { ':' });//นำ String มาสับเป็น array
                float data = float.Parse(posReturn[0].Substring(1));//นำข้อมูลมาแปลงตัวแรกต้องสับเพิ่ม
                float data1 = float.Parse(posReturn[1]);//ตัวที่สองเป็นต้นไปไม่ต้องสับ
            }
        }
    }

}

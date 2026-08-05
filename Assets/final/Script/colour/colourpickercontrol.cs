using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;
using ColorUtility = UnityEngine.ColorUtility;

public class colourpickercontrol : MonoBehaviour
{
    public float currentHue, currentSat, currentVal;

    [SerializeField]
    private RawImage hueImago, satValImage, outputImage;

    [SerializeField]
    private Slider hueSlider;

    [SerializeField]
    private TMP_InputField hexInputField;

    private Texture2D hueTexture, svTexture, outputTexture;

    [SerializeField]
    private SpwanSystem spawnSystem; // Reference to SpawnSystem to get clicked object

    private void Start()
    {
        CreatehueImage();
        CreateSVimage();
        CreateOutputImage();
        UpdateOutputImage();
    }

    private void CreatehueImage()
    {
        hueTexture = new Texture2D(1, 100);
        hueTexture.wrapMode = TextureWrapMode.Clamp;
        hueTexture.name = "HueTexture";

        for (int i = 0; i < hueTexture.height; i++)
        {
            hueTexture.SetPixel(0, i, Color.HSVToRGB((float)i / hueTexture.height, 1, 1f));
        }
        hueTexture.Apply();
        currentHue = 0;

        hueImago.texture = hueTexture;
    }

    private void CreateSVimage()
    {
        svTexture = new Texture2D(256, 256);
        svTexture.wrapMode = TextureWrapMode.Clamp;
        svTexture.name = "SatValTexture";

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(
                    currentHue,
                    (float)x / (svTexture.width - 1),
                    (float)y / (svTexture.height - 1)));
            }
        }
        svTexture.Apply();
        currentSat = 0;
        currentVal = 0;

        satValImage.texture = svTexture;
    }

    private void CreateOutputImage()
    {
        outputTexture = new Texture2D(1, 32);
        outputTexture.wrapMode = TextureWrapMode.Clamp;
        outputTexture.name = "OutputTexture";

        UpdateOutputImage();
        outputImage.texture = outputTexture;
    }

    private void UpdateOutputImage()
    {
        Color currentColour = Color.HSVToRGB(currentHue, currentSat, currentVal);
        for (int i = 0; i < outputTexture.height; i++)
        {
            outputTexture.SetPixel(0, i, currentColour);
        }

        outputTexture.Apply();

        if (hexInputField != null)
        {
            hexInputField.text = ColorUtility.ToHtmlStringRGB(currentColour);
        }

        // Get the clicked object from SpawnSystem instead of using fixed reference
        GameObject targetObject = GetTargetObject();
        if (targetObject != null)
        {
            MeshRenderer meshRenderer = targetObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                Material mat = meshRenderer.material;
                // ลองหลาย property names ที่เป็นไปได้
                if (mat.HasProperty("_BaseColor"))
                    mat.SetColor("_BaseColor", currentColour);
                else if (mat.HasProperty("_Color"))
                    mat.SetColor("_Color", currentColour);
                else if (mat.HasProperty("_MainColor"))
                    mat.SetColor("_MainColor", currentColour);
            }
            else
            {
                Debug.LogWarning($"No MeshRenderer found on {targetObject.name}");
            }
        }

        // Update SV image picker position
        var svControl = FindObjectOfType<svimagecontrol>();
        if (svControl != null)
        {
            svControl.UpdatePickerPosition();
        }
    }

    private GameObject GetTargetObject()
    {
        // First try to get from SpawnSystem
        if (spawnSystem != null && spawnSystem.ClickOBJ != null)
        {
            return spawnSystem.ClickOBJ;
        }

        // Alternative: Find any object with ClickableObject that was recently clicked
        ClickableObject[] clickableObjects = FindObjectsOfType<ClickableObject>();
        foreach (ClickableObject clickable in clickableObjects)
        {
            if (clickable.spwanSystem != null && clickable.spwanSystem.ClickOBJ != null)
            {
                return clickable.spwanSystem.ClickOBJ;
            }
        }

        return null;
    }

    public void UpdateSVimage()
    {
        currentHue = hueSlider.value;

        for (int y = 0; y < svTexture.height; y++)
        {
            for (int x = 0; x < svTexture.width; x++)
            {
                svTexture.SetPixel(x, y, Color.HSVToRGB(
                    currentHue,
                    // แก้ไข: ใช้ width สำหรับ x และ height สำหรับ y
                    (float)x / (svTexture.width - 1),
                    (float)y / (svTexture.height - 1)));
            }
        }

        svTexture.Apply();
        UpdateOutputImage();
    }

    // แก้ไข: ใส่ logic ใน SetSV method
    public void SetSV(float xNorm, float yNorm)
    {
        currentSat = Mathf.Clamp01(xNorm);
        currentVal = Mathf.Clamp01(yNorm);
        UpdateOutputImage();
    }

    public void OnTextInput()
    {
        string input = hexInputField.text.Replace("#", "").Trim();
        if (input.Length != 6) return;

        Color newCol;
        if (ColorUtility.TryParseHtmlString("#" + input, out newCol))
        {
            Color.RGBToHSV(newCol, out currentHue, out currentSat, out currentVal);
            
            if (hueSlider != null)
            {
                hueSlider.value = currentHue;
            }
            
            UpdateSVimage();
            UpdateOutputImage();
        }
        else
        {
            // หากไม่สามารถแปลงได้ ให้กลับไปเป็นค่าเดิม
            UpdateOutputImage();
        }
    }

    private void OnDestroy()
    {
        // Clean up textures เพื่อป้องกัน memory leak
        if (hueTexture != null) DestroyImmediate(hueTexture);
        if (svTexture != null) DestroyImmediate(svTexture);
        if (outputTexture != null) DestroyImmediate(outputTexture);
    }
}
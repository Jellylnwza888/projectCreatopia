using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class svimagecontrol : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    [SerializeField]
    private Image pickerImage;
    private RawImage SVimage;
    private colourpickercontrol CC;
    private RectTransform rectTransform, pickerTransform;

    private void Awake()
    {
        SVimage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();

        if (pickerImage != null)
        {
            pickerTransform = pickerImage.GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        CC = FindObjectOfType<colourpickercontrol>();
        
        if (pickerTransform != null)
        {
            // เริ่มต้นที่มุมซ้ายล่าง (Saturation=0, Value=0)
            pickerTransform.localPosition = new Vector2(
                -(rectTransform.sizeDelta.x * 0.5f), 
                -(rectTransform.sizeDelta.y * 0.5f));
        }
    }

    private void UpdateColour(PointerEventData eventData)
    {
        if (CC == null || pickerTransform == null) return;

        Vector3 pos = rectTransform.InverseTransformPoint(eventData.position);

        float deltaX = rectTransform.sizeDelta.x * 0.5f;
        float deltaY = rectTransform.sizeDelta.y * 0.5f;

        // จำกัดตำแหน่งภายในขอบเขต
        pos.x = Mathf.Clamp(pos.x, -deltaX, deltaX);
        pos.y = Mathf.Clamp(pos.y, -deltaY, deltaY);

        float x = pos.x + deltaX;
        float y = pos.y + deltaY;

        float xNorm = x / rectTransform.sizeDelta.x;
        float yNorm = y / rectTransform.sizeDelta.y;

        pickerTransform.localPosition = pos;
        
        // แสดงสีที่เลือกจริงๆ
        Color selectedColor = Color.HSVToRGB(CC.currentHue, xNorm, yNorm);
        pickerImage.color = selectedColor;

        CC.SetSV(xNorm, yNorm);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateColour(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UpdateColour(eventData);
    }

    // Update ตำแหน่ง picker เมื่อมีการเปลี่ยนสีจากแหล่งอื่น
    public void UpdatePickerPosition()
    {
        if (CC == null || pickerTransform == null) return;

        float x = CC.currentSat * rectTransform.sizeDelta.x - rectTransform.sizeDelta.x * 0.5f;
        float y = CC.currentVal * rectTransform.sizeDelta.y - rectTransform.sizeDelta.y * 0.5f;
        
        pickerTransform.localPosition = new Vector2(x, y);
        
        Color selectedColor = Color.HSVToRGB(CC.currentHue, CC.currentSat, CC.currentVal);
        pickerImage.color = selectedColor;
    }
}
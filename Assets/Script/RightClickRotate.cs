using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectRightClickHandler : MonoBehaviour, IPointerClickHandler
{
    public GameObject rotateButtonPanel; // Drag the rotate button panel in the inspector

    public void OnPointerClick(PointerEventData eventData)
    {
        // Check if it's a right-click (second mouse button)
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Toggle the rotate button panel
            if (rotateButtonPanel != null)
            {
                rotateButtonPanel.SetActive(!rotateButtonPanel.activeSelf);
            }

            // Prevent the event from propagating further
            eventData.Use();
        }
    }
}
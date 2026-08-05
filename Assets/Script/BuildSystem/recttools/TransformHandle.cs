using UnityEngine;

public class TransformHandle : MonoBehaviour
{
    private Transform target;
    private TransformTool tool;

    private Camera cam;

    public void Init(Transform target, TransformTool tool)
    {
        this.target = target;
        this.tool = tool;
        cam = Camera.main;
    }

    void OnMouseDrag()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, target.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);

            switch (tool.mode)
            {
                case ToolMode.Move:
                    target.position = point;
                    break;
                case ToolMode.Rotate:
                    Vector3 dir = point - target.position;
                    if (dir != Vector3.zero)
                        target.rotation = Quaternion.LookRotation(dir);
                    break;
                case ToolMode.Scale:
                    float scaleFactor = Vector3.Distance(point, target.position);
                    target.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                    break;
            }
        }
    }
}

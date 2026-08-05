using UnityEngine;

public class TransformTool : MonoBehaviour
{
    public ToolMode mode = ToolMode.Move;
    public Transform target;

    public GameObject handleMovePrefab;
    public GameObject handleRotatePrefab;
    public GameObject handleScalePrefab;

    private GameObject currentHandle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetMode(ToolMode.Move);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetMode(ToolMode.Rotate);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetMode(ToolMode.Scale);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        SpawnHandle();
    }

    public void SetMode(ToolMode newMode)
    {
        if (mode == newMode) return;
        mode = newMode;
        SpawnHandle();
    }

    private void SpawnHandle()
    {
        if (currentHandle) Destroy(currentHandle);
        if (target == null) return;

        switch (mode)
        {
            case ToolMode.Move:
                currentHandle = Instantiate(handleMovePrefab, target.position, Quaternion.identity);
                break;
            case ToolMode.Rotate:
                currentHandle = Instantiate(handleRotatePrefab, target.position, Quaternion.identity);
                break;
            case ToolMode.Scale:
                currentHandle = Instantiate(handleScalePrefab, target.position, Quaternion.identity);
                break;
        }

        currentHandle.GetComponent<TransformHandle>().Init(target, this);
    }

    private void LateUpdate()
    {
        if (currentHandle && target)
            currentHandle.transform.position = target.position;
    }
}

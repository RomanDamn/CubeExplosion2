using UnityEngine;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private int _leftMouseButtonNumber = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButtonNumber))
        {
            Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.TryGetComponent(out Cube cube))
                {
                    cube.HandleCubeClick();
                }
            }
        }
    }
}

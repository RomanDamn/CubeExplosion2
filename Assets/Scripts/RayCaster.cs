using UnityEngine;

public class RayCaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;

    private RaycastHit hit;
    private string _cubeTagName = "Cube";
    private int _leftMouseButtonNumber = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButtonNumber))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out hit, Mathf.Infinity))
            {
                hit.collider.gameObject.TryGetComponent(out MeshRenderer hitMesh);
                hit.collider.gameObject.TryGetComponent(out Cube cube);

                if (hitMesh != null && cube != null && hitMesh.tag == _cubeTagName)
                {
                    cube.HandleCubeClick();
                }
            }
        }
    }
}

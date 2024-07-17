using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube objectToSpawn;

    private int _minCubeNumber = 2;
    private int _maxCubeNumber = 6;

    private int _cubeScaleDevider = 2;
    private int _cubeExplosionDevider = 2;
    private float _explosionForce = 100f;
    private float _explosionRadius = 20f;

    public void GenerateCubes(GameObject originObject)
    {
        int cubeNumber = Random.Range(_minCubeNumber, _maxCubeNumber);

        for (int i = 0; i <= cubeNumber; i++)
        {
            Cube cube = Instantiate(objectToSpawn);
            cube.transform.position = originObject.transform.position;
            cube.transform.localScale /= _cubeScaleDevider;
            cube.DecreaseExplosionSplitChance(_cubeExplosionDevider);

            if (cube.TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Random.ColorHSV();
            }

            if (cube.TryGetComponent(out Exploder exploder) && cube.TryGetComponent(out Collider collider))
            {
                exploder.Explode(collider, cube.transform.position, _explosionForce, _explosionRadius);
            }
        }
    }
}

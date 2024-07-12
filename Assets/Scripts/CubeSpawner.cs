using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
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
            GameObject cube = Instantiate(originObject);
            cube.transform.position = originObject.transform.position;
            cube.transform.localScale = cube.transform.localScale / _cubeScaleDevider;
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
			cube.GetComponent<Cube>().ExplosionSplitChance /= _cubeExplosionDevider;
			cube.TryGetComponent(out Rigidbody rigidbody);

			if (rigidbody != null)
            {
				cube.GetComponent<Exploder>().Explode(rigidbody, rigidbody.transform.position, _explosionForce, _explosionRadius);
			}
        }
    }
}

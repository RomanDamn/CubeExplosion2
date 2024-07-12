using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Cube))]
[RequireComponent(typeof(Exploder))]
[RequireComponent(typeof(CubeSpawner))]
public class Cube : MonoBehaviour
{
	[SerializeField] private RayCaster _rayCast;

	public float ExplosionSplitChance = 100f;

	public void HandleCubeClick()
	{
		float splitChance = Random.Range(1f, 100f);

		if (splitChance <= ExplosionSplitChance)
		{
			if (gameObject.TryGetComponent(out CubeSpawner cubeSpawner))
			{
				cubeSpawner.GenerateCubes(gameObject);
			}
		}
		else
		{
			gameObject.TryGetComponent(out Exploder exploder);

			if (exploder != null)
			{
				exploder.ExplodeAround(gameObject);
			}
		}

		Destroy(gameObject);
	}
}

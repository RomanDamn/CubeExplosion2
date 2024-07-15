using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Cube))]
[RequireComponent(typeof(Exploder))]
[RequireComponent(typeof(CubeSpawner))]
public class Cube : MonoBehaviour
{
	private float _explosionSplitChance = 100f;

	public void HandleCubeClick()
	{
		float splitChance = Random.Range(1f, 100f);

		if (splitChance <= _explosionSplitChance)
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

	public void DecreaseExplosionSplitChance(int devider)
	{
		_explosionSplitChance /= devider;
	}
}

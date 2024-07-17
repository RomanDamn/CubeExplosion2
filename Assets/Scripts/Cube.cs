using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Cube), typeof(Exploder))]
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
            if (gameObject.TryGetComponent(out Exploder exploder))
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

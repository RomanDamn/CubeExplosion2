using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private GameObject _explosionObject;

    private int _explosionForceFacteur = 1000;
    private int _explosionRadiusFacteur = 10;

    public void Explode(Collider objectToExplode, Vector3 originPosition, float explosionForce, float explosionRadius)
    {
        if (objectToExplode.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.AddExplosionForce(explosionForce, originPosition, explosionRadius);
        }
    }

    public void ExplodeAround(GameObject objectToExplode)
    {
        float explosionRadius = CountExplosionRadius(objectToExplode.transform.localScale);
        Collider[] colliders = Physics.OverlapSphere(objectToExplode.transform.position, 8);

        foreach (Collider explosionItem in colliders)
        {
            if (explosionItem.TryGetComponent(out Exploder exploder))
            {
                float explosionForce = CountExplosionForce(explosionItem, objectToExplode);
                exploder.Explode(explosionItem, objectToExplode.transform.position, explosionForce, explosionRadius);
            }
        }

        Instantiate(_explosionObject, objectToExplode.transform.position, Quaternion.identity);
    }

    private float CountExplosionForce(Collider explosionItem, GameObject objectToExplode)
    {
        Vector3 scale = explosionItem.transform.localScale;
        Vector3 sourcePosition = objectToExplode.transform.position; 
        Vector3 targetPosition = explosionItem.transform.position; 
        float dist = (targetPosition - sourcePosition).magnitude;
        float reversiveScale = 1f - scale.magnitude;
        return reversiveScale * _explosionForceFacteur - dist;
    }

    private float CountExplosionRadius(Vector3 scale)
    {
        float reversiveScale = 1f - scale.magnitude;
        return reversiveScale * _explosionRadiusFacteur;
    }
}


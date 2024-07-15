using UnityEngine;
using static Unity.VisualScripting.Member;

public class Exploder : MonoBehaviour
{
	[SerializeField] private GameObject _explosionObject;

	private int _explosionForceFacteur = 1000;
	private int _explosionRadiusFacteur = 10;

	public void Explode(Rigidbody rigidbody, Vector3 explosionPosition, float explosionForce, float explosionRadius)
	{
		rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
	}

	public void ExplodeAround(GameObject objectToExplode)
	{
		float explosionRadius = CountExplosionRadius(objectToExplode.transform.localScale);
		objectToExplode.TryGetComponent(out MeshRenderer originMesh);
		Collider[] colliders = Physics.OverlapSphere(objectToExplode.transform.position, 8);

		foreach (Collider explosionItem in colliders)
		{
			if (explosionItem.GetComponent<Cube>() != null)
			{
				explosionItem.TryGetComponent(out Rigidbody rigidbody);

				if (rigidbody != null && explosionItem != objectToExplode)
				{
					float explosionForce = CountExplosionForce(explosionItem.transform.localScale, objectToExplode.transform.position, explosionItem.transform.position);
					explosionItem.GetComponent<Exploder>().Explode(rigidbody, objectToExplode.transform.position, explosionForce, explosionRadius);
				}
			}

			Instantiate(_explosionObject, objectToExplode.transform.position, Quaternion.identity);
		}
	}

	private float CountExplosionForce(Vector3 scale, Vector3 sourcePosition, Vector3 targetPosition)
	{
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

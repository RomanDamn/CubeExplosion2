using UnityEngine;
using static Unity.VisualScripting.Member;

public class Exploder : MonoBehaviour
{
	[SerializeField] private GameObject _ExplosionObject;

	private int _explosionForceFacteur = 1000;
	private int _explosionRadiusFacteur = 10;

	public void Explode(Rigidbody rigidbody, Vector3 explosionPosition, float explosionForce, float explosionRadius)
	{
		rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
	}

	public void ExplodeAround(GameObject gameObject)
	{
		float explosionRadius = CountExplosionRadius(gameObject.transform.localScale);
		gameObject.TryGetComponent(out MeshRenderer originMesh);
		Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 8);

		foreach (Collider explosionItem in colliders)
		{
			explosionItem.TryGetComponent(out MeshRenderer mesh);

			if (mesh != null && mesh.tag == originMesh.tag)
			{
				explosionItem.TryGetComponent(out Rigidbody rigidbody);

				if (rigidbody != null && explosionItem != gameObject)
				{
					float explosionForce = CountExplosionForce(explosionItem.transform.localScale, gameObject.transform.position, explosionItem.transform.position);
					explosionItem.GetComponent<Exploder>().Explode(rigidbody, gameObject.transform.position, explosionForce, explosionRadius);
				}
			}

			Instantiate(_ExplosionObject, gameObject.transform.position, Quaternion.identity);
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

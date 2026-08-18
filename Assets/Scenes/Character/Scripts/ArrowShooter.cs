using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab;
    RaycastHit hit;
    float arrowRange = 1000f;
    public Transform arrowSpawnPoint;

    void Update()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out hit, arrowRange))
        {
            GameObject ArrowInstantiate = GameObject.Instantiate(arrowPrefab, arrowSpawnPoint.transform.position, arrowSpawnPoint.transform.rotation);
            ArrowInstantiate.GetComponent<Arrow>().SetTarget(hit.point);
        }
    }
}
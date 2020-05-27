using UnityEngine;

public class SpawnBonuses : MonoBehaviour
{
    public GameObject[] bonuses;

    public void SpawnBonus(Vector2 position)
    {
        int random = Random.RandomRange(0, bonuses.Length);
        GameObject bonus = Instantiate(bonuses[random], position, Quaternion.identity);
    }
}

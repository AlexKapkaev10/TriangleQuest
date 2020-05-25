using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSystem : MonoBehaviour
{
    public GameObject[] bonuses;

    public enum Type
    {
        live,
        immunity
    }

    public Type MyType;

    public void SpawnBonus(Vector2 position)
    {
        int random = Random.RandomRange(0, bonuses.Length);
        GameObject bonus = Instantiate(bonuses[random], position, Quaternion.identity);
    }
}

using UnityEngine;

// For IDE creation tool of Scriptable Object
[CreateAssetMenu(fileName = "NewEntity", menuName = "CreateEnityScriptable", order = 0)]
public class EntityStats : ScriptableObject
{

    #region Fields

    [SerializeField] private int health;
    [SerializeField] private int damage;
    [SerializeField] private int speed;

    #endregion Fields

    #region Get/Set

    public int GetHealth { get => health; }
    public int GetDamage { get => damage; }
    public int GetSpeed { get => speed; }

    #endregion Get/Set

}

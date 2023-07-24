using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{

    #region Fields

    public static PlayerCurrency instance;

    public int oilBarrels = 0;

    #endregion Fields

    #region Mono

    public void Awake()
    {
        if(instance == null)
            instance = this;
    }

    #endregion Mono

    #region Methods

    public void AddBarrels(int amount)
    {
        if (amount <= 0)
            return;

        oilBarrels += amount;

        // Refresh UI
        UIContainer.Instance.playerOilBarrelsText.text = "Oil Barrels: " + oilBarrels;
    }

    #endregion Methods

}

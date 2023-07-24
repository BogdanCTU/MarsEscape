using UnityEngine;
using UnityEngine.UI;

public class UIContainer : MonoBehaviour
{

    #region Fields

    public static UIContainer Instance;

    [Space]
    [Header("Player UI:")]
    public Text playerHealthText;
    public Text playerDamageText;
    public Text playerOilBarrelsText;

    [Space]
    [Header("Wave UI:")]
    public Text waveTimeCountdownText;
    public Text waveCountText;
    public Text emeniesDefeatedText;

    [Space]
    [Header("End of game:")]
    public GameObject endScreen;
    public GameObject topBar;
    public GameObject bottomBar;
    public Text emeniesDefeatedEndText;
    public Text playerOilBarrelsEndText;


    #endregion Fields

    #region Mono

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        playerHealthText.text = "Health: 100 / 100";
        playerDamageText.text = "Damage: 25";
        playerOilBarrelsText.text = "Oil Barrels: 0";
    }

    #endregion Mono

}

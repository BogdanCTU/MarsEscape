using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{

    #region Fields

    [SerializeField] private Image soundButtonImage;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    [SerializeField] private bool soundIsOn = true;

    #endregion Fieldds

    #region Methods

    public void SoundButtonClicked()
    {
        if (soundIsOn)
        {
            soundIsOn = false;

            SoundManager.instance.MuteAudio();

            soundButtonImage.sprite = soundOffSprite;
        }
        else
        {
            soundIsOn = true;

            SoundManager.instance.UnmuteAudio();

            soundButtonImage.sprite = soundOnSprite;
        }
    }

    #endregion Methods

}

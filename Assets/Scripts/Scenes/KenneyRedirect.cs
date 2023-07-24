using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenneyRedirect : MonoBehaviour
{

    #region Fields

    public string kenneyURL = "https://kenney.nl/";
    public string kenneyJamURL = "https://itch.io/jam/kenney-jam-2023";

    #endregion Fields

    #region Methods

    public void KenneyClicked()
    {
        Application.OpenURL(kenneyURL);
    }

    public void KenneyJamClicked()
    {
        Application.OpenURL(kenneyJamURL);
    }

    #endregion Methods

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsReload : MonoBehaviour
{
    public void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

}

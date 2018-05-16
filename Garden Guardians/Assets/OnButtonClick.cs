using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OnButtonClick : MonoBehaviour {

        public void On_Click(string command)
    {
        if (command == "Play")
            SceneManager.LoadScene("Tina");

        else if (command == "tutorial")
        {

        }
    }
}

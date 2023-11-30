using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTrap : MonoBehaviour
{

    public GameObject GameOverUI;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameOverUI.SetActive(true);
            //SceneManager.LoadScene("GameOver");
        }

        if(col.gameObject.tag == "Superior")
        {
            Destroy(col.gameObject);
        }
    }
}

//    IEnumerator GameOver()
//    {
//        yield return new WaitForSeconds(1f);
//        SceneManager.LoadScene("GameOver");
//    }
//}

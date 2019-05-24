using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField]
    private string PromptName = "default";
    public GameObject DialogManager;
    public bool Recurring = false;


    // Start is called before the first frame update
    void Start()
    {
        PromptName = this.gameObject.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            DialogManager.GetComponent<Dialog_Range>().SetPromptIndex(PromptName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            DialogManager.GetComponent<Dialog_Range>().CloseDialogBox();
            if (!Recurring)
                this.gameObject.SetActive(false);
        }
    }

}

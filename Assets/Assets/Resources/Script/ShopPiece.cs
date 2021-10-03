using UnityEngine;

public class ShopPiece : MonoBehaviour
{
    // Drag and drop each scriptable asset file to its field in the Unity editor
    // refer to the ShopSelection property that will receive and sent its data (get and set)
    [SerializeField]
    SOShopSelection shopSelection;

    void Awake()
    {
        // icon slot
        //Inside the function are two if statements; the first checks whether there is a SpriteRenderer component in any of its child game objects.
        //If there is, then it grabs reference from its shopSelection asset file and applies its icon sprite to display on the button.
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = shopSelection.icon;
        }
        //selection value
        // The alternative checks whether any of the child game objects of this class have a game object titled itemText. 
        // If there is a game object titled itemText, we update its TextMesh component's text value with the shopSelection cost value.
        if (transform.Find("itemText"))
        {
            GetComponentInChildren<TextMesh>().text = shopSelection.cost;
        }

    }
    public SOShopSelection ShopSelection
    {
        get { return shopSelection; }
        set { shopSelection = value; }
    }
}







using UnityEngine;
using UnityEngine.UI;

public class ShopPiece : MonoBehaviour
{
    /*                      PURPOSE OF SHOPPIECE
     * 
     * Each button in the selection grid will be given information from a
     * scriptable object that will customize the buttons: name, description, value, and image. 
     */

    // Drag and drop each scriptable asset file to its field in the Unity editor
    // refer to the ShopSelection property that will receive and sent its data (get and set)
    [SerializeField]
    SOShopSelection shopSelection;

    void Awake()
    {
        // applies our scriptable object icon image to our buttons image
        // The if statement grabs reference from the second child in the 00 button and checks to see if it has an image component. 
        if(transform.GetChild(3).GetComponent<Image>()!=null)
        {
            transform.GetChild(3).GetComponent<Image>().sprite = shopSelection.icon;
        }
        // The following if statement makes sure the 00 button has itemText.
        // When the itemText game object is found, its Text component receives the scriptable object price of the weapon
        if(transform.Find("itemText"))
        {
            GetComponentInChildren<Text>().text = shopSelection.cost.ToString();
        }
    }
    public SOShopSelection ShopSelection
    {
        get { return shopSelection; }
        set { shopSelection = value; }
    }
}







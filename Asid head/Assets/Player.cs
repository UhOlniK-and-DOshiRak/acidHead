using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private UI_Inventory uI_Inventory;
    public List<Item> items;
    public GameObject workSpace;
    public int money;
    public int authority;
    private Slider authorityUI;
    public Sprite lowAuthority, normalAuthority, highAuthority;
    public Image authoritySprite;

    public TextMeshProUGUI moneyUI;

    // Start is called before the first frame update
    void Start()
    {
        workSpace.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
        inventory = new Inventory(items);
        uI_Inventory.SetInventory(inventory);

        authority = PlayerPrefs.GetInt("Authority", 30);
        money = PlayerPrefs.GetInt("Money", 50);
        //money = GameController.money;
        //authority = GameController.authority;

        moneyUI.SetText(money.ToString() + "/" + FindObjectOfType<GameController>().targetMoney);
        authorityUI = GameObject.Find("Authority").GetComponent<Slider>();
        authorityUI.value = authority;

        SetAuthorityUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetAuthorityUI()
    {
        if (authority <= 25)
        {
            authoritySprite.sprite = lowAuthority;
        }
        else if (authority <= 75)
        {
            authoritySprite.sprite = normalAuthority;
        }
        else
        {
            authoritySprite.sprite = highAuthority;
        }
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int amount)
    {
        this.money = amount;
        moneyUI.SetText(money.ToString() + "/" + FindObjectOfType<GameController>().targetMoney);
    }

    public void AddMoney(int amount)
    {
        this.money += amount;
        moneyUI.SetText(money.ToString() + "/" + FindObjectOfType<GameController>().targetMoney);
    }

    public void ReduceMoney(int amount)
    {
        this.money -= amount;
        moneyUI.SetText(money.ToString() + "/" + FindObjectOfType<GameController>().targetMoney);
    }

    public int GetAuthority()
    {
        return authority;
    }

    public void SetAuthoruty(int amount)
    {
        this.authority = amount;
        authorityUI.value = amount;
        SetAuthorityUI();
    }

    public void AddAuthority(int amount)
    {
        if (this.authority + amount <= 100)
        {
            this.authority += amount;
        }
        else this.authority = 100;
        authorityUI.value = this.authority;
        SetAuthorityUI();
    }

    public void ReduceAuthority(int amount)
    {
        if (this.authority - amount >= 0)
        {
            this.authority -= amount;
        }
        else this.authority = 0;
        authorityUI.value = this.authority;
        SetAuthorityUI();
    }
}

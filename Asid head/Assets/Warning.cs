using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warning : MonoBehaviour
{
    Animator animator;
    public Text text;

    public enum WarningType
    {
        DoNotGetMoney,
        NotEnoughMoney,
        NoItem,
        ExtraItem,
        NoReasonRefuse,
        GetMoneyRefuse,
        WrongName,
        WrongAge,
        WrongSum
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("OnClick", 3f);
    }

    public void OnClick()
    {
        animator.SetTrigger("destroy");
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    } 

    public void SetText(WarningType type)
    {
        switch (type)
        {
            case WarningType.DoNotGetMoney: text.text = "Ты забыл взять деньги! Теперь платить придётся тебе."; break;
            case WarningType.ExtraItem: text.text = "Лишний товар! Тебе придётся заплатить за него самому."; break;
            case WarningType.NoItem: text.text = "Не хватает товара... Твой авторитет пострадает, если продолжишь обманывать клиентов. "; break;
            case WarningType.NotEnoughMoney: text.text = "Кажется, ты взял недостаточную плату. Твой кошелёк теперь постарадает :("; break;
            case WarningType.NoReasonRefuse: text.text = "Если продолжишь без причины отказывать клиентам, они больше не будут приходить. Авторитет снижен."; break;
            case WarningType.GetMoneyRefuse: text.text = "Ты взял деньги, но не отдал товар. Надеюсь, эта сумма стоит твоего авторитета."; break;
            case WarningType.WrongName: text.text = "Клиент сказал неправильное кодовое название. Впредь слушай внимательнее."; break;
            case WarningType.WrongAge: text.text = "Ты хочешь, чтобы детишки попадали в больницы? Разве тебе их не жалко?"; break;
            case WarningType.WrongSum: text.text = "Заказ на слишком большую сумму!"; break;
        }
    }
}

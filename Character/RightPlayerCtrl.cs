﻿
using Protocol.Dto.Fight;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPlayerCtrl : CharacterBase
{
    private void Awake()
    {
        Bind(CharacterEvent.INIT_RIGHT_CARD,
            CharacterEvent.ADD_RIGHT_CARD,
            CharacterEvent.REMOVE_RIGHT_CARD);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case CharacterEvent.INIT_RIGHT_CARD:
                StartCoroutine(initCardList());
                break;
            case CharacterEvent.ADD_RIGHT_CARD:
                addTableCard();
                break;
            case CharacterEvent.REMOVE_RIGHT_CARD:
                removeCard((message as List<CardDto>).Count);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 添加底牌的
    /// </summary>
    /// <param name="cardList"></param>
    private void addTableCard()
    {
        //再创建新的三张卡牌
        GameObject cardPrefab = Resources.Load<GameObject>("Card/OtherCard");
        for (int i = 0; i < 3; i++)
        {
            createGo(i, cardPrefab);
        }
    }


    /// <summary>
    /// 移除卡牌的游戏物体
    /// </summary>
    private void removeCard(int cardCount)
    {
        // ***** ******
        for (int i = cardCount; i < cardObjectList.Count; i++)
        {
            cardObjectList[i].SetActive(false);
        }
    }

    private List<GameObject> cardObjectList;

    /// <summary>
    /// 卡牌的父物体
    /// </summary>
    private Transform cardParent;

    // Use this for initialization
    void Start()
    {
        cardParent = transform.Find("CardPoint");
        cardObjectList = new List<GameObject>();
    }

    /// <summary>
    /// 初始化显示卡牌
    /// </summary>
    private IEnumerator initCardList()
    {
        GameObject cardPrefab = Resources.Load<GameObject>("Card/OtherCard");

        for (int i = 0; i < 17; i++)
        {
            createGo(i, cardPrefab);
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// 创建卡牌游戏物体
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    private void createGo(int index, GameObject cardPrefab)
    {
        GameObject cardGo = Object.Instantiate(cardPrefab, cardParent) as GameObject;
        cardGo.transform.localPosition = new Vector2((0.15f * index), 0);
        cardGo.GetComponent<SpriteRenderer>().sortingOrder = index;
    }
}

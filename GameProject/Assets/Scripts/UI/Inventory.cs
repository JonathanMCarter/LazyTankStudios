﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : A{
public int current;
public static int Coins = 0;
public List<Sprite> icons;
public List<int> items;
public Text t;
public Text p;
public int pots;
public Image i;
bool delay;
void Start(){
p.enabled = false;
p.text = pots + "";}
void OnLevelWasLoaded(int level){
addCoins(0);}
public void change(){
if (delay) return;
current = current < items.Count - 1 ? current + 1 : 0;
i.sprite = icons[items[current]];
if (!i.sprite)
change();
if (items[current] == 4) p.enabled = true;
else p.enabled = false;}
public int getCoins(){
return Coins;}
public void addCoins(int coins){
Coins += coins;
t.text = Coins + "";}
public void UsePotion(){
pots--;
p.text = pots + "";
if (pots <= 0){
items.Remove(4);
change();}}
public void AddPotion(){
if (!items.Contains(4)) items.Add(4);
pots++;
p.text = pots + "";}}
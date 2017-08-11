
//if somebody wanted to have same variable names
"use strict";

var coolValue = document.getElementById("takeTheValue").innerHTML;
//making it percentage
coolValue = coolValue / 5 * 100;

var insertThatFuckingValue = document.getElementById("insertTheValue").style.width = coolValue + "%";

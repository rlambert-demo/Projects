//Money Variables
var money = 2000;
var moneyStolen = 0;
var moneyPerSecond = 1;
var costMultiplier = 2;
//Item 1 Variables
var item1Amount = 0;
var item1Value = 1;
//var item1InitialCost = 2;
var item1Cost = 2;
//Item 2 Variables
var item2Amount = 0;
var item2Value = 5;
//var item2InitialCost = 500;
var item2Cost = 500;
//Item 3 Variables
var item3Amount = 0;
var item3Value = 20;
//var item3InitialCost = 2000;
var item3Cost = 2000;

//Other Varaibles
var timeSinceSaved = 0;

// var themeSelected = "dark";

//Function to update the current game values to the cookie file
function saveGame(){
    const d = new Date();
    document.cookie = "money=" + money + ";";
    document.cookie = "stolen=" + moneyStolen + ";";
    document.cookie = "item1Amount=" + item1Amount + ";";
    document.cookie = "item2Amount=" + item2Amount + ";";
    document.cookie = "item3Amount=" + item3Amount + ";";
    document.cookie = "lastSaved=" + d + ";";
}
//TESTING PURPOSES ONLY TO DELETE COOKIES
function deleteGame(){
    var Cookies = document.cookie.split(';');
 // set past expiry to all cookies
    for (var i = 0; i < Cookies.length; i++) {
        document.cookie = Cookies[i] + "=; expires="+ new Date(0).toUTCString();
    }
}
//Check to see if there is saved data via cookies
function checkSaveGame(){
    let x = document.cookie
    if (getCookie("money") != ""){
        loadGame();
    }
    else{
        alert("Welcome new player");
    }
}
//Function to load cookies if a save is found
function loadGame(){
    let x = document.cookie
    money = getCookie("money");
    money = Number(money);

    item1Amount = getCookie("item1Amount");
    item1Amount = Number(item1Amount);
    if (item1Amount>0){
        for (index = 0; index < item1Amount; index++) {
            item1Cost = item1Cost*2;
        }
    }
    item2Amount = getCookie("item2Amount");
    item2Amount = Number(item2Amount);
    if (item2Amount>0){
        for (index = 0; index < item2Amount; index++) {
            item2Cost = item2Cost*2;
        }
    }
    item3Amount = getCookie("item3Amount");
    item3Amount = Number(item3Amount);
    if (item3Amount>0){
        for (index = 0; index < item3Amount; index++) {
            item3Cost = item3Cost*2;
        }
    }
    moneyStolen = getCookie("stolen");
    moneyStolen = Number(moneyStolen);
    updateCostAndValue();
    updateMoney();

    //Calculate office time since last saved
    calculateMoneyGain();
    var offlineMoney = moneyPerSecond * timeSinceSaved;
    money += offlineMoney;
    // alert("Gained " + offlineMoney + " Gold while taking a nap!");
}
function getCookie(cname) {
    let name = cname + "=";
    let decodedCookie = decodeURIComponent(document.cookie);
    let ca = decodedCookie.split(';');
    for(let i = 0; i <ca.length; i++) {
      let c = ca[i];
      while (c.charAt(0) == ' ') {
        c = c.substring(1);
      }
      if (c.indexOf(name) == 0) {
        return c.substring(name.length, c.length);
      }
    }
    return 0;
  }
//Functions to check if the required money is available, then update the gold and purchase values
function buyItem1 (){
    if(money>=item1Cost) {
        item1Amount++;
        money -= item1Cost;
        item1Cost*=2;
    }
    document.getElementById("item1NextCost").innerHTML = "Cost: " + item1Cost;
    document.getElementById("item1Amount").innerHTML = "Purchased: " + item1Amount;
    updateMoney();
}
function buyItem2 (){
    if(money>=item2Cost) {
        item2Amount++;
        money -= item2Cost;
        item2Cost*=2;
    }
    document.getElementById("item2NextCost").innerHTML = "Cost: " + item2Cost;
    document.getElementById("item2Amount").innerHTML = "Purchased: " + item2Amount;
    updateMoney();
}
function buyItem3 (){
    if(money>=item3Cost) {
        item3Amount++;
        money -= item3Cost;
        item3Cost*=2;
    }
    document.getElementById("item3NextCost").innerHTML = "Cost: " + item3Cost;
    document.getElementById("item3Amount").innerHTML = "Purchased: " + item3Amount;
    updateMoney();
}
//Functions for gaining and updating money
function gainMoney(){
    money++;
    money += item1Value*item1Amount + item2Value*item2Amount + item3Value*item3Amount;
    calculateMoneyGain();
    updateMoney();
}
function clickMoney(){
    money++;
    moneyStolen++;
    updateMoney();
    document.getElementById("moneyStolen").innerHTML = "Money Stolen: " + moneyStolen;
}
function calculateMoneyGain(){
    moneyPerSecond = item1Value*item1Amount + item2Value*item2Amount + item3Value*item3Amount + 1;
}
function updateMoney(){
    document.getElementById("money").innerHTML = "Gold: " + money + " (+" + moneyPerSecond + "/s)";
    lastSaved();
}
//Update costs and amounts, mainly for after loading data
function updateCostAndValue(){
    document.getElementById("item1NextCost").innerHTML = "Cost: " + item1Cost;
    document.getElementById("item1Amount").innerHTML = "Purchased: " + item1Amount;
    document.getElementById("item2NextCost").innerHTML = "Cost: " + item2Cost;
    document.getElementById("item2Amount").innerHTML = "Purchased: " + item2Amount;
    document.getElementById("item3NextCost").innerHTML = "Cost: " + item3Cost;
    document.getElementById("item3Amount").innerHTML = "Purchased: " + item3Amount;
    document.getElementById("moneyStolen").innerHTML = "Money Stolen: " + moneyStolen;
    // lastSaved();
}
function lastSaved(){
    if (getCookie("money") != ""){
        var timeSaved = getCookie("lastSaved");
        timeSaved = Date.parse(timeSaved);
        var timeNow = new Date();
        timeNow = Date.parse(timeNow);
        //Calculate time in milliseconds, and then seconds
        timeSinceSaved = timeNow - timeSaved;
        timeSinceSaved /= 1000;
        document.getElementById("lastSaved").innerHTML = "Last Saved: " + timeSinceSaved + " Seconds Ago"
    }else{
        document.getElementById("lastSaved").innerHTML = "Last Saved: Never!"
    }
}

//Timers for game
window.setInterval(gainMoney, 100);
// window.setInterval(saveGame, 60000);

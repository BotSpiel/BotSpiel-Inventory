//The latest ECMAScript standard defines seven data types:
//Six data types that are primitives:
//Boolean
//Null
//Undefined
//Number
//String
//Symbol(new in ECMAScript 6)
//and Object
var SendMessage = /** @class */ (function () {
    function SendMessage() {
    }
    SendMessage.prototype.render = function (divId, text) {
        var el = document.getElementById(divId);
        el.innerText = text;
    };
    return SendMessage;
}());
window.onload = function () {
    var sendMessage = new SendMessage();
    var result = doCalculation(3, 2, 1);
    var conCatString = "";
    var arrayOfStrings = ["first", "second", "third"];
    var complexType;
    complexType = { id: 1, name: "Test" };
    //var openDoor = DoorState.Open;
    var openDoor = DoorState[1];
    var simpleClass = new SimpleClass();
    //simpleClass.printThis();
    for (var itemKey in arrayOfStrings) {
        conCatString = conCatString + itemKey + "=" + arrayOfStrings[itemKey];
    }
    //sendMessage.render("message", "Hello from BotSpiel");
    //sendMessage.render("message", "doCalculation result: " + result);
    //sendMessage.render("message", "conCatString: " + conCatString);
    //sendMessage.render("message", "openDoor is: " + openDoor);
    sendMessage.render("message", "openDoor is: " + simpleClass.printThis());
};
function doCalculation(a, b, c) {
    return (a * b) + c;
}
var DoorState;
(function (DoorState) {
    DoorState[DoorState["Open"] = 0] = "Open";
    DoorState[DoorState["Closed"] = 1] = "Closed";
    DoorState[DoorState["Ajar"] = 2] = "Ajar";
})(DoorState || (DoorState = {}));
var SimpleClass = /** @class */ (function () {
    function SimpleClass() {
    }
    SimpleClass.prototype.printThis = function () {
        return 'printThis() called';
    };
    return SimpleClass;
}());
//# sourceMappingURL=CommonUtilities.js.map
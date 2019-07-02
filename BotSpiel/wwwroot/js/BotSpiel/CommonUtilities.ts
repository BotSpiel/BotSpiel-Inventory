
//The latest ECMAScript standard defines seven data types:

//Six data types that are primitives:
//Boolean
//Null
//Undefined
//Number
//String
//Symbol(new in ECMAScript 6)
//and Object

class SendMessage {
    public render(divId: string, text: string)
    {
        let el: HTMLElement = document.getElementById(divId);
        el.innerText = text;
    }
}

window.onload = () => {
    var sendMessage = new SendMessage();
    var result = doCalculation(3, 2, 1);

    var conCatString: string = "";
    var arrayOfStrings: string[] = ["first", "second", "third"];

    let complexType: IComplexType;
    complexType = { id: 1, name: "Test" };

    //var openDoor = DoorState.Open;
    var openDoor = DoorState[1];

    let simpleClass = new SimpleClass();
    //simpleClass.printThis();

    for (var itemKey in arrayOfStrings)
    {
        conCatString = conCatString + itemKey + "=" + arrayOfStrings[itemKey];
    }

    //sendMessage.render("message", "Hello from BotSpiel");
    //sendMessage.render("message", "doCalculation result: " + result);
    //sendMessage.render("message", "conCatString: " + conCatString);
    //sendMessage.render("message", "openDoor is: " + openDoor);
    sendMessage.render("message", "openDoor is: " + simpleClass.printThis());
}

function doCalculation(
    a: number,
    b: number,
    c: number
)
{
    return (a * b) + c;
}


enum DoorState 
{
    Open,
    Closed,
    Ajar
}

interface IComplexType {
    id: number;
    name: string;
}

class SimpleClass {
    id: number;

    printThis(): string
    {
        return 'printThis() called';
    }

}



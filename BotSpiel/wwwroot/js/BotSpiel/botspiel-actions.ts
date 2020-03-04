﻿
interface IGridHeaderCheckBox {
    checkBoxId: string
    checkAll(rowCheckBoxes: HTMLCollectionOf<Element>)
    uncheckAll(rowCheckBoxes: HTMLCollectionOf<Element>)
}

interface IGridRowCheckBoxes {
    checkBoxId: string
    deleteSelectedRows(selectedIDs: string, dataElement: string)
}

class GridHeaderCheckBox implements IGridHeaderCheckBox {
    checkBoxId: string
    public checkAll(rowCheckBoxes: HTMLCollectionOf<Element>)
    {
        for (let rowCheckBox in rowCheckBoxes)
        {
            if (isNaN(parseInt(((<HTMLInputElement>rowCheckBoxes[rowCheckBox]).id).toString().replace("Check_", ""))) === false)
            {
                (<HTMLInputElement>rowCheckBoxes[rowCheckBox]).checked = true;
            }
        }
    }
    public uncheckAll(rowCheckBoxes: HTMLCollectionOf<Element>)
    {
        for (let rowCheckBox in rowCheckBoxes) {
            (<HTMLInputElement>rowCheckBoxes[rowCheckBox]).checked = false;
        }
    }
}

class GridRowCheckBoxes implements IGridRowCheckBoxes {
    checkBoxId: string

    deleteSelectedRows(selectedIDs: string, dataElement: string)
    {
        let xhr = new XMLHttpRequest();
        let elRefresh = <HTMLElement>document.getElementById("refresh-grid");

        xhr.open("POST", dataElement +  "/MultiRowDelete?Ids=" + selectedIDs)
        xhr.onload = function () {
            let sResponse = xhr.response;

            //if (xhr.status === 200 && JSON.parse(sResponse) === selectedIDs) {
            //    alert("The following IDs have been successfully deleted " + JSON.parse(sResponse).replace("|", " "));
            //}
            if (xhr.status === 200 ) {
                alert("The following IDs have been successfully deleted " + JSON.parse(sResponse).replace(",", ""));
                simulateClick(elRefresh);
            }
            else if (xhr.status !== 200)
            {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    }

    //Custom Code Start | Added Code Block 

    allocateSelectedRows(selectedIDs: string, dataElement: string) {
        let xhr = new XMLHttpRequest();
        let elRefresh = <HTMLElement>document.getElementById("refresh-grid");

        xhr.open("POST", dataElement + "/MultiRowPickBatchAllocate?Ids=" + selectedIDs)
        xhr.onload = function () {
            let sResponse = xhr.response;

            //if (xhr.status === 200 && JSON.parse(sResponse) === selectedIDs) {
            //    alert("The following IDs have been successfully deleted " + JSON.parse(sResponse).replace("|", " "));
            //}
            if (xhr.status === 200) {
                alert("The following IDs have been successfully allocated " + JSON.parse(sResponse).replace(",", ""));
                simulateClick(elRefresh);
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    }

    activateSelectedRows(selectedIDs: string, dataElement: string) {
        let xhr = new XMLHttpRequest();
        let elRefresh = <HTMLElement>document.getElementById("refresh-grid");

        xhr.open("POST", dataElement + "/MultiRowPickBatchActivate?Ids=" + selectedIDs)
        xhr.onload = function () {
            let sResponse = xhr.response;

            //if (xhr.status === 200 && JSON.parse(sResponse) === selectedIDs) {
            //    alert("The following IDs have been successfully deleted " + JSON.parse(sResponse).replace("|", " "));
            //}
            if (xhr.status === 200) {
                alert("The following IDs have been successfully activated " + JSON.parse(sResponse).replace(",", ""));
                simulateClick(elRefresh);
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    }

    //Custom Code End


}

//Custom Code Start | Added Code Block 

class CountrySubDivisionsForCountries {
    Id: string

    getCountrySubDivisionsForCountries(Id: string) {
        let xhr = new XMLHttpRequest();
        let dropDown = <HTMLSelectElement>document.getElementById("ixStateOrProvince");
        dropDown.innerHTML = '';

        xhr.open("POST", "/Addresses/getCountrySubDivisionsForCountries?Id=" + Id)
        xhr.onload = function () {
            let sResponse = xhr.response;

            if (xhr.status === 200) {
                const data = JSON.parse(sResponse);
                let option;
                for (let i = 0; i < data.length; i++) {
                    option = document.createElement('option');
                    option.text = data[i].sCountrySubDivision;
                    option.value = data[i].ixCountrySubDivision;
                    dropDown.add(option);
                }
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    }
}

class BaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration {
    Id: string
    nUnits: string

    getBaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration(Id: string, nUnits: string) {
        let xhr = new XMLHttpRequest();
        let nBaseUnitQuantityReceived = <HTMLInputElement>document.getElementById("nBaseUnitQuantityReceived");
        nBaseUnitQuantityReceived.value = '';

        xhr.open("POST", "/Receiving/getBaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration?Id=" + Id + "&nUnits=" + nUnits)
        xhr.onload = function () {
            let sResponse = xhr.response;

            if (xhr.status === 200) {
                const data = JSON.parse(sResponse);
                let option;
                for (let i = 0; i < data.length; i++) {
                    nBaseUnitQuantityReceived.value = data[i].nBaseUnitQuantityReceived;
                }
                nBaseUnitQuantityReceived.readOnly = true;
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    }
}

class BaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration {
    Id: string
    nUnits: string

    getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration(Id: string, nUnits: string) {
        let xhr = new XMLHttpRequest();
        let nBaseUnitQuantityExpected = <HTMLInputElement>document.getElementById("nBaseUnitQuantityExpected");
        nBaseUnitQuantityExpected.value = '';

        xhr.open("POST", "/InboundOrderLines/getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration?Id=" + Id + "&nUnits=" + nUnits)
        xhr.onload = function () {
            let sResponse = xhr.response;

            if (xhr.status === 200) {
                const data = JSON.parse(sResponse);
                let option;
                for (let i = 0; i < data.length; i++) {
                    nBaseUnitQuantityExpected.value = data[i].nBaseUnitQuantityExpected;
                }
                nBaseUnitQuantityExpected.readOnly = true;
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    }
}

class MaterialHandlingUnitConfigurationsForMaterial {
    Id: string

    getMaterialHandlingUnitConfigurationsForMaterial(Id: string) {
        let xhr = new XMLHttpRequest();
        let dropDown = <HTMLSelectElement>document.getElementById("ixMaterialHandlingUnitConfiguration");
        dropDown.innerHTML = '';

        xhr.open("POST", "/Receiving/getMaterialHandlingUnitConfigurationsForMaterial?Id=" + Id)
        xhr.onload = function () {
            let sResponse = xhr.response;

            if (xhr.status === 200) {
                const data = JSON.parse(sResponse);
                let option;
                for (let i = 0; i < data.length; i++) {
                    option = document.createElement('option');
                    option.text = data[i].sMaterialHandlingUnitConfiguration;
                    option.value = data[i].ixMaterialHandlingUnitConfiguration;
                    dropDown.add(option);
                }
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    }
}

class MaterialsForInboundOrders {
    Id: string

    getMaterialsForInboundOrders(Id: string) {
        let xhr = new XMLHttpRequest();
        let dropDown = <HTMLSelectElement>document.getElementById("ixMaterial");
        dropDown.innerHTML = '';

        xhr.open("POST", "/Receiving/getMaterialsForInboundOrders?Id=" + Id)
        xhr.onload = function () {
            let sResponse = xhr.response;

            if (xhr.status === 200) {
                const data = JSON.parse(sResponse);
                let option;
                for (let i = 0; i < data.length; i++) {
                    option = document.createElement('option');
                    option.text = data[i].sMaterial;
                    option.value = data[i].ixMaterial;
                    dropDown.add(option);
                }
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    }
}

//Custom Code End
function toggleAll()
{
    let gridHeaderCheckBox = new GridHeaderCheckBox;
    let elCheckAll = <HTMLInputElement>document.getElementById("CheckAll");
    let rowCheckBoxes: HTMLCollectionOf<Element> = document.getElementsByClassName("rowCheckBox");

    if (elCheckAll.checked) {
        gridHeaderCheckBox.checkAll(rowCheckBoxes);
    }
    else {
        gridHeaderCheckBox.uncheckAll(rowCheckBoxes);
    }
}

function deleteRows(dataElement: string) {
    let selectedIDs: string = "|";
    let nID: number;
    let gridRowCheckBoxes = new GridRowCheckBoxes();
    let rowCheckBoxes: HTMLCollectionOf<Element> = document.getElementsByClassName("rowCheckBox");

    for (let rowCheckBox in rowCheckBoxes) {
        if ((<HTMLInputElement>rowCheckBoxes[rowCheckBox]).checked)
        {
            nID = parseInt(((<HTMLInputElement>rowCheckBoxes[rowCheckBox]).id).toString().replace("Check_", ""));
            selectedIDs = selectedIDs + nID.toString() + "|";
        }
    }
    if (confirm('Are you sure you want to delete the selected rows?')) {
        gridRowCheckBoxes.deleteSelectedRows(selectedIDs, dataElement);
    } else {
        // Do nothing!
    }
}


var simulateClick = function (elem) {
    // Create our event (with options)
    var evt = new MouseEvent('click', {
        bubbles: true,
        cancelable: true,
        view: window
    });
    // If cancelled, don't dispatch our event
    var canceled = !elem.dispatchEvent(evt);
};
//Custom Code Start | Added Code Block 

function getCountrySubDivisionsForCountries() {
    let ixCountry = <HTMLSelectElement>document.getElementById("ixCountry");
    let nID: number;
    let countrySubDivisionsForCountries = new CountrySubDivisionsForCountries();

    if (isNaN(parseInt(ixCountry.options[ixCountry.selectedIndex].value)) === false) {
        countrySubDivisionsForCountries.getCountrySubDivisionsForCountries(ixCountry.options[ixCountry.selectedIndex].value);
    }
    else {
        let dropDown = <HTMLSelectElement>document.getElementById("ixStateOrProvince");
        dropDown.innerHTML = '';
    }
}

function getBaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration() {
    let ixMaterialHandlingUnitConfiguration = <HTMLSelectElement>document.getElementById("ixMaterialHandlingUnitConfiguration");
    let nHandlingUnitQuantity = (<HTMLInputElement>document.getElementById("nHandlingUnitQuantity")).value;
    let nID: number;
    let baseUnitQuantityReceivedForMaterialHandlingUnitConfiguration = new BaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration();

    if (isNaN(parseInt(ixMaterialHandlingUnitConfiguration.options[ixMaterialHandlingUnitConfiguration.selectedIndex].value)) === false) {
        baseUnitQuantityReceivedForMaterialHandlingUnitConfiguration.getBaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration(ixMaterialHandlingUnitConfiguration.options[ixMaterialHandlingUnitConfiguration.selectedIndex].value, nHandlingUnitQuantity);
    }
}

function getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration() {
    let ixMaterialHandlingUnitConfiguration = <HTMLSelectElement>document.getElementById("ixMaterialHandlingUnitConfiguration");
    let nHandlingUnitQuantity = (<HTMLInputElement>document.getElementById("nHandlingUnitQuantity")).value;
    let nID: number;
    let baseUnitQuantityExpectedForMaterialHandlingUnitConfiguration = new BaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration();

    if (isNaN(parseInt(ixMaterialHandlingUnitConfiguration.options[ixMaterialHandlingUnitConfiguration.selectedIndex].value)) === false) {
        baseUnitQuantityExpectedForMaterialHandlingUnitConfiguration.getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration(ixMaterialHandlingUnitConfiguration.options[ixMaterialHandlingUnitConfiguration.selectedIndex].value, nHandlingUnitQuantity);
    }
}

function getMaterialsForInboundOrders() {
    let ixInboundOrder = <HTMLSelectElement>document.getElementById("ixInboundOrder");
    let nID: number;
    let materialsForInboundOrders = new MaterialsForInboundOrders();

    if (isNaN(parseInt(ixInboundOrder.options[ixInboundOrder.selectedIndex].value)) === false) {
        materialsForInboundOrders.getMaterialsForInboundOrders(ixInboundOrder.options[ixInboundOrder.selectedIndex].value);
    }
    else {
        let dropDown = <HTMLSelectElement>document.getElementById("ixMaterial");
        dropDown.innerHTML = '';
    }
}

function getMaterialHandlingUnitConfigurationsForMaterial() {
    let ixMaterial = <HTMLSelectElement>document.getElementById("ixMaterial");
    let nID: number;
    let materialHandlingUnitConfigurationsForMaterial = new MaterialHandlingUnitConfigurationsForMaterial();

    if (isNaN(parseInt(ixMaterial.options[ixMaterial.selectedIndex].value)) === false) {
        materialHandlingUnitConfigurationsForMaterial.getMaterialHandlingUnitConfigurationsForMaterial(ixMaterial.options[ixMaterial.selectedIndex].value);
    }
    else {
        let dropDown = <HTMLSelectElement>document.getElementById("ixMaterialHandlingUnitConfiguration");
        dropDown.innerHTML = '';
    }
}

function allocateRows(dataElement: string) {
    let selectedIDs: string = "|";
    let nID: number;
    let gridRowCheckBoxes = new GridRowCheckBoxes();
    let rowCheckBoxes: HTMLCollectionOf<Element> = document.getElementsByClassName("rowCheckBox");

    for (let rowCheckBox in rowCheckBoxes) {
        if ((<HTMLInputElement>rowCheckBoxes[rowCheckBox]).checked) {
            nID = parseInt(((<HTMLInputElement>rowCheckBoxes[rowCheckBox]).id).toString().replace("Check_", ""));
            selectedIDs = selectedIDs + nID.toString() + "|";
        }
    }
    if (confirm('Are you sure you want to allocate the selected rows?')) {
        gridRowCheckBoxes.allocateSelectedRows(selectedIDs, dataElement);
    } else {
        // Do nothing!
    }
}

function activateRows(dataElement: string) {
    let selectedIDs: string = "|";
    let nID: number;
    let gridRowCheckBoxes = new GridRowCheckBoxes();
    let rowCheckBoxes: HTMLCollectionOf<Element> = document.getElementsByClassName("rowCheckBox");

    for (let rowCheckBox in rowCheckBoxes) {
        if ((<HTMLInputElement>rowCheckBoxes[rowCheckBox]).checked) {
            nID = parseInt(((<HTMLInputElement>rowCheckBoxes[rowCheckBox]).id).toString().replace("Check_", ""));
            selectedIDs = selectedIDs + nID.toString() + "|";
        }
    }
    if (confirm('Are you sure you want to activate the selected rows?')) {
        gridRowCheckBoxes.activateSelectedRows(selectedIDs, dataElement);
    } else {
        // Do nothing!
    }
}

//Custom Code End
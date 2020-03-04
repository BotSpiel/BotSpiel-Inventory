var GridHeaderCheckBox = /** @class */ (function () {
    function GridHeaderCheckBox() {
    }
    GridHeaderCheckBox.prototype.checkAll = function (rowCheckBoxes) {
        for (var rowCheckBox in rowCheckBoxes) {
            if (isNaN(parseInt((rowCheckBoxes[rowCheckBox].id).toString().replace("Check_", ""))) === false) {
                rowCheckBoxes[rowCheckBox].checked = true;
            }
        }
    };
    GridHeaderCheckBox.prototype.uncheckAll = function (rowCheckBoxes) {
        for (var rowCheckBox in rowCheckBoxes) {
            rowCheckBoxes[rowCheckBox].checked = false;
        }
    };
    return GridHeaderCheckBox;
}());
var GridRowCheckBoxes = /** @class */ (function () {
    function GridRowCheckBoxes() {
    }
    GridRowCheckBoxes.prototype.deleteSelectedRows = function (selectedIDs, dataElement) {
        var xhr = new XMLHttpRequest();
        var elRefresh = document.getElementById("refresh-grid");
        xhr.open("POST", dataElement + "/MultiRowDelete?Ids=" + selectedIDs);
        xhr.onload = function () {
            var sResponse = xhr.response;
            //if (xhr.status === 200 && JSON.parse(sResponse) === selectedIDs) {
            //    alert("The following IDs have been successfully deleted " + JSON.parse(sResponse).replace("|", " "));
            //}
            if (xhr.status === 200) {
                alert("The following IDs have been successfully deleted " + JSON.parse(sResponse).replace(",", ""));
                simulateClick(elRefresh);
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    };
    //Custom Code Start | Added Code Block 
    GridRowCheckBoxes.prototype.allocateSelectedRows = function (selectedIDs, dataElement) {
        var xhr = new XMLHttpRequest();
        var elRefresh = document.getElementById("refresh-grid");
        xhr.open("POST", dataElement + "/MultiRowPickBatchAllocate?Ids=" + selectedIDs);
        xhr.onload = function () {
            var sResponse = xhr.response;
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
    };
    GridRowCheckBoxes.prototype.activateSelectedRows = function (selectedIDs, dataElement) {
        var xhr = new XMLHttpRequest();
        var elRefresh = document.getElementById("refresh-grid");
        xhr.open("POST", dataElement + "/MultiRowPickBatchActivate?Ids=" + selectedIDs);
        xhr.onload = function () {
            var sResponse = xhr.response;
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
    };
    return GridRowCheckBoxes;
}());
//Custom Code Start | Added Code Block 
var CountrySubDivisionsForCountries = /** @class */ (function () {
    function CountrySubDivisionsForCountries() {
    }
    CountrySubDivisionsForCountries.prototype.getCountrySubDivisionsForCountries = function (Id) {
        var xhr = new XMLHttpRequest();
        var dropDown = document.getElementById("ixStateOrProvince");
        dropDown.innerHTML = '';
        xhr.open("POST", "/Addresses/getCountrySubDivisionsForCountries?Id=" + Id);
        xhr.onload = function () {
            var sResponse = xhr.response;
            if (xhr.status === 200) {
                var data = JSON.parse(sResponse);
                var option = void 0;
                for (var i = 0; i < data.length; i++) {
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
    };
    return CountrySubDivisionsForCountries;
}());
var BaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration = /** @class */ (function () {
    function BaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration() {
    }
    BaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration.prototype.getBaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration = function (Id, nUnits) {
        var xhr = new XMLHttpRequest();
        var nBaseUnitQuantityReceived = document.getElementById("nBaseUnitQuantityReceived");
        nBaseUnitQuantityReceived.value = '';
        xhr.open("POST", "/Receiving/getBaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration?Id=" + Id + "&nUnits=" + nUnits);
        xhr.onload = function () {
            var sResponse = xhr.response;
            if (xhr.status === 200) {
                var data = JSON.parse(sResponse);
                var option = void 0;
                for (var i = 0; i < data.length; i++) {
                    nBaseUnitQuantityReceived.value = data[i].nBaseUnitQuantityReceived;
                }
                nBaseUnitQuantityReceived.readOnly = true;
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    };
    return BaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration;
}());
var BaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration = /** @class */ (function () {
    function BaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration() {
    }
    BaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration.prototype.getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration = function (Id, nUnits) {
        var xhr = new XMLHttpRequest();
        var nBaseUnitQuantityExpected = document.getElementById("nBaseUnitQuantityExpected");
        nBaseUnitQuantityExpected.value = '';
        xhr.open("POST", "/InboundOrderLines/getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration?Id=" + Id + "&nUnits=" + nUnits);
        xhr.onload = function () {
            var sResponse = xhr.response;
            if (xhr.status === 200) {
                var data = JSON.parse(sResponse);
                var option = void 0;
                for (var i = 0; i < data.length; i++) {
                    nBaseUnitQuantityExpected.value = data[i].nBaseUnitQuantityExpected;
                }
                nBaseUnitQuantityExpected.readOnly = true;
            }
            else if (xhr.status !== 200) {
                alert("The request failed - the returned status code is " + xhr.status);
            }
        };
        xhr.send();
    };
    return BaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration;
}());
var MaterialHandlingUnitConfigurationsForMaterial = /** @class */ (function () {
    function MaterialHandlingUnitConfigurationsForMaterial() {
    }
    MaterialHandlingUnitConfigurationsForMaterial.prototype.getMaterialHandlingUnitConfigurationsForMaterial = function (Id) {
        var xhr = new XMLHttpRequest();
        var dropDown = document.getElementById("ixMaterialHandlingUnitConfiguration");
        dropDown.innerHTML = '';
        xhr.open("POST", "/Receiving/getMaterialHandlingUnitConfigurationsForMaterial?Id=" + Id);
        xhr.onload = function () {
            var sResponse = xhr.response;
            if (xhr.status === 200) {
                var data = JSON.parse(sResponse);
                var option = void 0;
                for (var i = 0; i < data.length; i++) {
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
    };
    return MaterialHandlingUnitConfigurationsForMaterial;
}());
var MaterialsForInboundOrders = /** @class */ (function () {
    function MaterialsForInboundOrders() {
    }
    MaterialsForInboundOrders.prototype.getMaterialsForInboundOrders = function (Id) {
        var xhr = new XMLHttpRequest();
        var dropDown = document.getElementById("ixMaterial");
        dropDown.innerHTML = '';
        xhr.open("POST", "/Receiving/getMaterialsForInboundOrders?Id=" + Id);
        xhr.onload = function () {
            var sResponse = xhr.response;
            if (xhr.status === 200) {
                var data = JSON.parse(sResponse);
                var option = void 0;
                for (var i = 0; i < data.length; i++) {
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
    };
    return MaterialsForInboundOrders;
}());
//Custom Code End
function toggleAll() {
    var gridHeaderCheckBox = new GridHeaderCheckBox;
    var elCheckAll = document.getElementById("CheckAll");
    var rowCheckBoxes = document.getElementsByClassName("rowCheckBox");
    if (elCheckAll.checked) {
        gridHeaderCheckBox.checkAll(rowCheckBoxes);
    }
    else {
        gridHeaderCheckBox.uncheckAll(rowCheckBoxes);
    }
}
function deleteRows(dataElement) {
    var selectedIDs = "|";
    var nID;
    var gridRowCheckBoxes = new GridRowCheckBoxes();
    var rowCheckBoxes = document.getElementsByClassName("rowCheckBox");
    for (var rowCheckBox in rowCheckBoxes) {
        if (rowCheckBoxes[rowCheckBox].checked) {
            nID = parseInt((rowCheckBoxes[rowCheckBox].id).toString().replace("Check_", ""));
            selectedIDs = selectedIDs + nID.toString() + "|";
        }
    }
    if (confirm('Are you sure you want to delete the selected rows?')) {
        gridRowCheckBoxes.deleteSelectedRows(selectedIDs, dataElement);
    }
    else {
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
    var ixCountry = document.getElementById("ixCountry");
    var nID;
    var countrySubDivisionsForCountries = new CountrySubDivisionsForCountries();
    if (isNaN(parseInt(ixCountry.options[ixCountry.selectedIndex].value)) === false) {
        countrySubDivisionsForCountries.getCountrySubDivisionsForCountries(ixCountry.options[ixCountry.selectedIndex].value);
    }
    else {
        var dropDown = document.getElementById("ixStateOrProvince");
        dropDown.innerHTML = '';
    }
}
function getBaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration() {
    var ixMaterialHandlingUnitConfiguration = document.getElementById("ixMaterialHandlingUnitConfiguration");
    var nHandlingUnitQuantity = document.getElementById("nHandlingUnitQuantity").value;
    var nID;
    var baseUnitQuantityReceivedForMaterialHandlingUnitConfiguration = new BaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration();
    if (isNaN(parseInt(ixMaterialHandlingUnitConfiguration.options[ixMaterialHandlingUnitConfiguration.selectedIndex].value)) === false) {
        baseUnitQuantityReceivedForMaterialHandlingUnitConfiguration.getBaseUnitQuantityReceivedForMaterialHandlingUnitConfiguration(ixMaterialHandlingUnitConfiguration.options[ixMaterialHandlingUnitConfiguration.selectedIndex].value, nHandlingUnitQuantity);
    }
}
function getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration() {
    var ixMaterialHandlingUnitConfiguration = document.getElementById("ixMaterialHandlingUnitConfiguration");
    var nHandlingUnitQuantity = document.getElementById("nHandlingUnitQuantity").value;
    var nID;
    var baseUnitQuantityExpectedForMaterialHandlingUnitConfiguration = new BaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration();
    if (isNaN(parseInt(ixMaterialHandlingUnitConfiguration.options[ixMaterialHandlingUnitConfiguration.selectedIndex].value)) === false) {
        baseUnitQuantityExpectedForMaterialHandlingUnitConfiguration.getBaseUnitQuantityExpectedForMaterialHandlingUnitConfiguration(ixMaterialHandlingUnitConfiguration.options[ixMaterialHandlingUnitConfiguration.selectedIndex].value, nHandlingUnitQuantity);
    }
}
function getMaterialsForInboundOrders() {
    var ixInboundOrder = document.getElementById("ixInboundOrder");
    var nID;
    var materialsForInboundOrders = new MaterialsForInboundOrders();
    if (isNaN(parseInt(ixInboundOrder.options[ixInboundOrder.selectedIndex].value)) === false) {
        materialsForInboundOrders.getMaterialsForInboundOrders(ixInboundOrder.options[ixInboundOrder.selectedIndex].value);
    }
    else {
        var dropDown = document.getElementById("ixMaterial");
        dropDown.innerHTML = '';
    }
}
function getMaterialHandlingUnitConfigurationsForMaterial() {
    var ixMaterial = document.getElementById("ixMaterial");
    var nID;
    var materialHandlingUnitConfigurationsForMaterial = new MaterialHandlingUnitConfigurationsForMaterial();
    if (isNaN(parseInt(ixMaterial.options[ixMaterial.selectedIndex].value)) === false) {
        materialHandlingUnitConfigurationsForMaterial.getMaterialHandlingUnitConfigurationsForMaterial(ixMaterial.options[ixMaterial.selectedIndex].value);
    }
    else {
        var dropDown = document.getElementById("ixMaterialHandlingUnitConfiguration");
        dropDown.innerHTML = '';
    }
}
function allocateRows(dataElement) {
    var selectedIDs = "|";
    var nID;
    var gridRowCheckBoxes = new GridRowCheckBoxes();
    var rowCheckBoxes = document.getElementsByClassName("rowCheckBox");
    for (var rowCheckBox in rowCheckBoxes) {
        if (rowCheckBoxes[rowCheckBox].checked) {
            nID = parseInt((rowCheckBoxes[rowCheckBox].id).toString().replace("Check_", ""));
            selectedIDs = selectedIDs + nID.toString() + "|";
        }
    }
    if (confirm('Are you sure you want to allocate the selected rows?')) {
        gridRowCheckBoxes.allocateSelectedRows(selectedIDs, dataElement);
    }
    else {
        // Do nothing!
    }
}
function activateRows(dataElement) {
    var selectedIDs = "|";
    var nID;
    var gridRowCheckBoxes = new GridRowCheckBoxes();
    var rowCheckBoxes = document.getElementsByClassName("rowCheckBox");
    for (var rowCheckBox in rowCheckBoxes) {
        if (rowCheckBoxes[rowCheckBox].checked) {
            nID = parseInt((rowCheckBoxes[rowCheckBox].id).toString().replace("Check_", ""));
            selectedIDs = selectedIDs + nID.toString() + "|";
        }
    }
    if (confirm('Are you sure you want to activate the selected rows?')) {
        gridRowCheckBoxes.activateSelectedRows(selectedIDs, dataElement);
    }
    else {
        // Do nothing!
    }
}
//Custom Code End
//# sourceMappingURL=botspiel-actions.js.map
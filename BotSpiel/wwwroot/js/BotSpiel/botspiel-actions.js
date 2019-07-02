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
//Custom Code End
//# sourceMappingURL=botspiel-actions.js.map
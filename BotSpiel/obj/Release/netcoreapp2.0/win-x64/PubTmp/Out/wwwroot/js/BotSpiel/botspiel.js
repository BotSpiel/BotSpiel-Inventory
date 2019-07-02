$('.content-refresh').on('click', function () {
    $('.mvc-grid').mvcgrid('reload');
});

$('.mvc-grid').mvcgrid({
    sourceUrl: 'IndexGrid', // Grid source url string
    query: '?search=test', // Grid query string
    requestType: 'get', // Ajax grid request type
    data: {
        // Ajax grid query data
    },
    filters: {
        // Grid filters to extend or use
        'Boolean': new CustomBooleanFilter()
    },
    loadingDelay: 300, // Loading block visiblity delay on ajax grid
    showLoading: true, // Indicates if grid loading block should be shown after loading delay
    reload: false, // Grid reload indicator
    rowClicked: function (row, data, e) {
        // this - grid instance which invoked the event
        // row - DOM element
        // data - clicked row's data from all bound columns
        // e - native row click event
    },
    reloadStarted: function () {
        // this - grid instance which invoked the event
    },
    reloadEnded: function () {
        // this - grid instance which invoked the event
    },
    reloadFailed: function (result) {
        // this - grid instance which invoked the event
        // result - failed ajax response result
    }
});


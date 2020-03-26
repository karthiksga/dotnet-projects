/// <reference name="MicrosoftAjax.js" />
/// <reference name="MicrosoftAjaxTemplates.js" />

var imageList, imageData, detailView, fetchParameters = { orderby: "Name" };

var dataContext = $create(
        Sys.Data.DataContext,
        {
            serviceUri: "../Services/ImagesWcfService.svc",
            saveOperation: "SaveImages"
        });

Sys.Application.add_init(appInit);

function onRendering(sender, args) {
    imageList = sender;
    imageData = args.get_data();
    Sys.Observer.makeObservable(imageData);
}

function onSort(orderby) {
    fetchParameters.orderby = orderby;
    imageList.fetchData();
}

function onInsert() {
    var newImage = { Name: "Name", Description: "Description", Contributor: "Contributor", Uri: "../images/question.jpg" };

    dataContext.insertEntity(newImage);
    imageData.add(newImage);

    var newIndex = imageData.length - 1;
    imageList.set_selectedIndex(newIndex);
    imageList.get_contexts()[newIndex].getElementById("listItem").scrollIntoView();
}

function onDetailViewCommand(sender, args) {
    switch (args.get_commandName()) {
        // A custom command   
        case "Delete":
            var index = imageList.get_selectedIndex();
            var deletedImage = imageData[index];

            dataContext.removeEntity(deletedImage);
            imageData.remove(deletedImage);

            if (index >= imageData.length) index--;
            imageList.set_selectedIndex(index);
    }
}

function appInit() {
    imageList = $create(
        Sys.UI.DataView,
        {
            autoFetch: true,
            dataProvider: dataContext,
            fetchOperation: "GetImages",
            fetchParameters: fetchParameters,
            initialSelectedIndex: 0,
            selectedItemClass: "selecteditem"
        },
        {
            rendering: onRendering
        },
        null,
        $get("imagesListView")
    );

    detailView = $create(
        Sys.UI.DataView,
        null,
        {
            command: onDetailViewCommand
        },
        null,
        $get("detailView")
    );

    $create(
        Sys.Binding,
        {
            defaultValue: null,
            source: imageList,
            path: "selectedData",
            target: detailView,
            targetProperty: "data"
        }
    );
}

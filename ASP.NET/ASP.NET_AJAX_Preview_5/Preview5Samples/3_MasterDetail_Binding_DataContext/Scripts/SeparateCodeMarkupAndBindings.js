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
            rendering: onRendering,
            itemRendered: onMasterItemRendered
        },
        null,
        $get("imagesListView")
    );

    detailView = $create(
        Sys.UI.DataView,
        null,
        {
            command: onDetailViewCommand,
            itemRendered: onDetailRendered
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

function onMasterItemRendered(sender, args) {
    var context = args.get_itemContext(),
        dataItem = args.get_dataItem();
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Contributor",
            target: context.keys.contributor,
            targetProperty: "innerHTML"
        }
    );
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Name",
            target: context.keys.name,
            targetProperty: "value"
        }
    );
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Uri",
            target: context.keys.uri,
            targetProperty: "src"
        }
    );
}

function onDetailRendered(sender, args) {
    var context = args.get_itemContext(),
        dataItem = args.get_dataItem();
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Name",
            target: context.getElementById('img'),
            targetProperty: "alt"
        }
    );
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Uri",
            target: context.getElementById('img'),
            targetProperty: "src"
        }
    );
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Name",
            target: context.getElementById('title'),
            targetProperty: "innerHTML"
        }
    );
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Name",
            target: context.getElementById('name'),
            targetProperty: "value"
        }
    );
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Description",
            target: context.getElementById('description'),
            targetProperty: "value"
        }
    );
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Contributor",
            target: context.getElementById('contributor'),
            targetProperty: "value"
        }
    );
    $create(
        Sys.Binding,
        {
            source: dataItem,
            path: "Uri",
            target: context.getElementById('uri'),
            targetProperty: "value"
        }
    );
}


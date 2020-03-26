function onSort(orderby) {
    Uc.fetchParameters.orderby = orderby;
    Uc.imageList.fetchData();
}

function onInsert() {
    var newImage = { Name: "Name", Description: "Description", Contributor: "Contributor", Uri: "../images/question.jpg" };

    Uc.imagesDataContext.insertEntity(newImage);
    Uc.imageData.add(newImage);

    var newIndex = Uc.imageData.length - 1;
    Uc.imageList.set_selectedIndex(newIndex);
    Uc.imageList.get_contexts()[newIndex].keys["listItem"].scrollIntoView();
}

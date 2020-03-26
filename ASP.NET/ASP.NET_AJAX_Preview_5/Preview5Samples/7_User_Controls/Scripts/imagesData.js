Type.registerNamespace('Uc');

Uc.fetchParameters = { orderby: "Name" }
Uc.imageList = null;
Uc.imageData = null;

Uc.imagesDataContext = $create(
    Sys.Data.DataContext,
    {
        serviceUri: "../Services/ImagesWcfService.svc",
        saveOperation: "SaveImages"
    }
);

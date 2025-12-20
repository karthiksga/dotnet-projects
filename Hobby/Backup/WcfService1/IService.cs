using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using WcfService1.Model;
using System.Drawing;

namespace MaFamilleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]    
    public interface IService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        List<PhotoModel> GetPhotos(string album);

        [OperationContract]
        List<TsunamiModel> GetTsunamiPhotos();

        [OperationContract(Name="GetPhoto")]
        byte[] GetPhoto(string albumName,string fileName);        

        [OperationContract]
        byte[] GetThumbnailPhoto(string albumName, string fileName);

        //[OperationContract(Name = "GetPhotoObject")]
        //AlbumModel GetPhotoObject(string fileName);

        [OperationContract]
        List<AlbumModel> GetAlbums();

        [OperationContract]
        byte[] GetDefaultAlbumImage();

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        FileUploadModel UploadPhoto(string albumName, string fileName,byte[] content,int id,bool append);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        int RemovePhoto(string fileName,int id);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        bool IsFileExists(string albumName,string fileName,long length);

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        AlbumModel IsAlbumExists(string albumName, string oldAlbumName, bool isThumbnailsLoaded);

        [OperationContract(IsOneWay=true)]
        void DeleteAlbum(string albumName);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}

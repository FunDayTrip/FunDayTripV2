using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace homepage.ViewModel
{
    public class CShowLocations
    {
        //放tLocation跟坐標，坐標要設定DisplayFormat
        public virtual tLocation Location { get; set; }
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> fX_Coordinate_i { get; set; }

        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> fY_Coordinate_i { get; set; }

        public virtual tCoordinate Coordinate { get; set; }

        public virtual tPhoto Photo { get; set; }

    }
   

    public class CCreateLocation
    {

        public HttpPostedFileBase PostImage { get; set; }


    }

    public class CLocation
    {
        public int ID { get; set; }
        public string fId_Location { get; set; }
        public int fId_Role { get; set; }
        public int fId_Coordinate { get; set; }
        public int fId_ShareAuth { get; set; }
        public int fId_Icon { get; set; }
        public string fType_Location { get; set; }
        public string fName_Location { get; set; }
        public string fDescript_Location { get; set; }
        public Nullable<System.DateTime> fTime_Location { get; set; }
        public string fAdd_Location { get; set; }
        public int fDelete_Location { get; set; }
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> fX_Coordinate { get; set; }
        [DisplayFormat(DataFormatString = "{0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> fY_Coordinate { get; set; }
        public string fAuth_ShareAuth { get; set; }
        public string fNickName_Role { get; set; }


        public CLocation(tLocation location)
        {
            this.fAdd_Location = location.fAdd_Location;
            this.fAuth_ShareAuth = location.tShareAuth.fAuth_ShareAuth;
            this.fDelete_Location = location.fDelete_Location;
            this.fDescript_Location = location.fDescript_Location;
            this.fId_Coordinate = location.fId_Coordinate;
            this.fId_Icon = location.fId_Icon;
            this.fId_Location = location.fId_Location;
            this.fId_Role = location.fId_Role;
            this.fId_ShareAuth = location.fId_ShareAuth;
            this.fName_Location = location.fName_Location;
            this.fNickName_Role = location.tRole.fNickName_Role;
            this.fTime_Location = location.fTime_Location;
            this.fType_Location = location.fType_Location;
            this.fX_Coordinate = location.tCoordinate.fX_Coordinate;
            this.fY_Coordinate = location.tCoordinate.fY_Coordinate;
        }
    }
    public class CRoute
    {
        public string fId_Route { get; set; }
        public Nullable<int> fId_Role { get; set; }
        public Nullable<int> fId_ShareAuth { get; set; }
        public Nullable<int> fId_Icon { get; set; }
        public string fType_Route { get; set; }
        public string fName_Route { get; set; }
        public string fDescript_Route { get; set; }
        public Nullable<System.DateTime> fTime_Route { get; set; }
        public Nullable<int> fDelete_Route { get; set; }
        public string fPath_Route { get; set; }
        public string fAuth_ShareAuth { get; set; }
        public string fNickName_Role { get; set; }
        public CRoute() { }
        public CRoute(tRoute route)
        {
            this.fAuth_ShareAuth = route.tShareAuth.fAuth_ShareAuth;
            this.fDelete_Route = route.fDelete_Route;
            this.fDescript_Route = route.fDescript_Route;
            this.fId_Icon = route.fId_Icon;
            this.fId_Role = route.fId_Role;
            this.fId_Route = route.fId_Route;
            this.fId_ShareAuth = route.fId_ShareAuth;
            this.fName_Route = route.fName_Route;
            this.fNickName_Role = route.tRole.fNickName_Role;
            this.fPath_Route = route.fPath_Route;
            this.fTime_Route = route.fTime_Route;
            this.fType_Route = route.fType_Route;
        }
    }

    public class CPhoto
    {
        public int ID { get; set; }
        public string fId_Photo { get; set; }
        public string fId_Location { get; set; }
        public int fId_Role { get; set; }
        public string fTitle_Photo { get; set; }
        public string fDescript_Photo { get; set; }
        public string fPath_Photo { get; set; }
        public Nullable<System.DateTime> fTime_Photo { get; set; }
        public string fNickName_Role { get; set; }
        public CPhoto() { }
        public CPhoto(tPhoto photo)
        {
            this.fDescript_Photo = photo.fDescript_Photo;
            this.fId_Location = photo.fId_Location;
            this.fId_Photo = photo.fId_Photo;
            this.fId_Role = photo.fId_Role;
            this.fNickName_Role = photo.tRole.fNickName_Role;
            this.fPath_Photo = photo.fPath_Photo;
            this.fTime_Photo = photo.fTime_Photo;
            this.fTitle_Photo = photo.fTitle_Photo;
        }

    }

    public class CCoordinate
    {
        public int fId_Coordinate { get; set; }
        public string fName_Coordinate { get; set; }
        public Nullable<decimal> fX_Coordinate { get; set; }
        public Nullable<decimal> fY_Coordinate { get; set; }
        public CCoordinate() { }
        public CCoordinate(tCoordinate coordinate)
        {
            this.fId_Coordinate = coordinate.fId_Coordinate;
            this.fName_Coordinate = coordinate.fName_Coordinate;
            this.fX_Coordinate = coordinate.fX_Coordinate;
            this.fY_Coordinate = coordinate.fY_Coordinate;
        }
    }

    public class CMapItem
    {
        //最大的class，包含所有單個table的class
        public List<CLocation> LocationList { get; set; }
        public List<CRoute> RouteList { get; set; }
        public List<tLR_Relation> LR_RelationsList { get; set; }
        public List<CPhoto> PhotoList { get; set; }
        public List<CCoordinate> CoordinateList { get; set; }


    }
}
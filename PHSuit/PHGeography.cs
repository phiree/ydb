using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Device.Location;
namespace PHSuit
{
   public  class PHGeography
    {
       public double GetDistance(double lat1,double lon1,double lat2,double lon2 )
       {
           GeoCoordinate one = new GeoCoordinate(lat1,lon1);
           GeoCoordinate two=new GeoCoordinate(lat2,lon2);
           double distance=one.GetDistanceTo(two);
           return distance;
       }
    }
}

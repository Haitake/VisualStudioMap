using Foundation;
using System;
using UIKit;
using MapKit;
using CoreLocation;
using CoreGraphics;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace VisualNavAPP
{
    public partial class ViewController : UIViewController
    {
       
        CLLocationManager LocationManager = new CLLocationManager();
        public ViewController(IntPtr handle) : base(handle)
        {
            
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            searchBar.SearchButtonClicked += search;

        }

        void search(object sender, EventArgs e)
        {
            var searcRequest = new MKLocalSearchRequest();

            searcRequest.NaturalLanguageQuery = searchBar.Text;
            searcRequest.Region = new MKCoordinateRegion(map.UserLocation.Coordinate, new MKCoordinateSpan(0.25, 0.25));
            var activeSearch = new MKLocalSearch(request: searcRequest);
            activeSearch.Start(delegate (MKLocalSearchResponse response, NSError error) {


                if (response != null && error == null) {
                    double latitude = response.Region.Center.Latitude;
                    double longitude = response.Region.Center.Longitude;
                    var annotation = new MKPointAnnotation();
                    annotation.Title = searchBar.Text;
                    annotation.Coordinate = new CLLocationCoordinate2D(latitude, longitude);
                    map.AddAnnotation(annotation);
                    map.SetRegion(response.Region, animated: true);

                }
                else
                {
                    Console.WriteLine("ERROR");
                }


            });
        }
    }
}
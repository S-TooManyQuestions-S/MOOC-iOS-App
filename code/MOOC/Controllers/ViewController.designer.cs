// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MOOC
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISearchBar CourseSearch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton DataScience { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView DataTable { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Python { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Sharp { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl Specification { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Xamarin { get; set; }

        [Action ("DataScience_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DataScience_TouchUpInside (UIKit.UIButton sender);

        [Action ("Python_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Python_TouchUpInside (UIKit.UIButton sender);

        [Action ("Sharp_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Sharp_TouchUpInside (UIKit.UIButton sender);

        [Action ("ValueChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ValueChanged (UIKit.UISegmentedControl sender);

        [Action ("Xamarin_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Xamarin_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (CourseSearch != null) {
                CourseSearch.Dispose ();
                CourseSearch = null;
            }

            if (DataScience != null) {
                DataScience.Dispose ();
                DataScience = null;
            }

            if (DataTable != null) {
                DataTable.Dispose ();
                DataTable = null;
            }

            if (Python != null) {
                Python.Dispose ();
                Python = null;
            }

            if (Sharp != null) {
                Sharp.Dispose ();
                Sharp = null;
            }

            if (Specification != null) {
                Specification.Dispose ();
                Specification = null;
            }

            if (Xamarin != null) {
                Xamarin.Dispose ();
                Xamarin = null;
            }
        }
    }
}
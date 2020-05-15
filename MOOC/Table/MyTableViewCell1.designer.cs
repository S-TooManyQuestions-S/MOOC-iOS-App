// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace MOOC.Controllers
{
    [Register ("MyTableViewCell1")]
    partial class MyTableViewCell1
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView CourseImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel CourseTitle { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView Language { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CourseImage != null) {
                CourseImage.Dispose ();
                CourseImage = null;
            }

            if (CourseTitle != null) {
                CourseTitle.Dispose ();
                CourseTitle = null;
            }

            if (Language != null) {
                Language.Dispose ();
                Language = null;
            }
        }
    }
}
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

namespace MOOC.Controllers
{
    [Register ("DetailsController")]
    partial class DetailsController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISegmentedControl DataPicker { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView InnerImage { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Like { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton Link { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView Logo { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView Name { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView Rating { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView Text { get; set; }

        [Action ("Like_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Like_TouchUpInside (UIKit.UIButton sender);

        [Action ("UIButton56234_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void UIButton56234_TouchUpInside (UIKit.UIButton sender);

        [Action ("ValueChanged:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void ValueChanged (UIKit.UISegmentedControl sender);

        void ReleaseDesignerOutlets ()
        {
            if (DataPicker != null) {
                DataPicker.Dispose ();
                DataPicker = null;
            }

            if (InnerImage != null) {
                InnerImage.Dispose ();
                InnerImage = null;
            }

            if (Like != null) {
                Like.Dispose ();
                Like = null;
            }

            if (Link != null) {
                Link.Dispose ();
                Link = null;
            }

            if (Logo != null) {
                Logo.Dispose ();
                Logo = null;
            }

            if (Name != null) {
                Name.Dispose ();
                Name = null;
            }

            if (Rating != null) {
                Rating.Dispose ();
                Rating = null;
            }

            if (Text != null) {
                Text.Dispose ();
                Text = null;
            }
        }
    }
}
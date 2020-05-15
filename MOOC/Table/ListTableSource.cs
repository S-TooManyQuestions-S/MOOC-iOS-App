using System;
using System.Collections.Generic;
using Foundation;
using System.Threading;
using UIKit;
using System.Net;
using MOOC.DataLibrary;

namespace MOOC.Controllers
{
    public class ListTableSource : UITableViewSource
    {
        
        public ListTableSource(List<Course> courseList )
        {
            courses = courseList;
        }
        public ListTableSource() { courses = new List<Course>(); }

        
        public List<Course> courses;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Cellid", indexPath) as MyTableViewCell1;
            cell.UpdateData(courses[indexPath.Row]);
            return cell;

        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (courses == null)
                return 0;
            return courses.Count;
        }

        
    }
}

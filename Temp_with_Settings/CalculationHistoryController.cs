using Foundation;
using System;
using UIKit;
using System.Collections.Generic;

namespace Temp_with_Settings
{
    public partial class CalculationHistoryController : UITableViewController
    {
        public CalculationHistoryController (IntPtr handle) : base (handle)
		{ }

        ViewController controller;
        public CalculationHistoryController(ViewController controller)
        {
            this.controller = controller;
        }

        private List<TableItem> coreData = new List<TableItem>();
        private static List<TableItem> data = new List<TableItem>();
        
		public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            HistoryTable.Source = new TableSource(coreData);
        }

        //has to be a better way
        public static void AddData(string dat, bool cel)
        {
            if (cel)
                data.Add(new TableItem(dat + "C"));
            else
                data.Add(new TableItem(dat + "F"));
            
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            coreData.AddRange(data);
            HistoryTable.ReloadData();
        }
    }


	/* Class taken from xamarin example: 
	 * https://docs.microsoft.com/en-us/xamarin/ios/user-interface/controls/tables/populating-a-table-with-data
     */
	public class TableSource : UITableViewSource
    {

        List<TableItem> tableItems;  
        protected string cellIdentifier = "TableCell";  
  
        public TableSource(List<TableItem> items)  
        {  
            tableItems = items;  
        }  
  
        public override nint RowsInSection(UITableView tableview, nint section)  
        {  
            return tableItems.Count;  
        }  
  
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)  
        {  
            // request a recycled cell to save memory  
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);  
  
  
            var cellStyle = UITableViewCellStyle.Default;  
  
            // if there are no cells to reuse, create a new one  
            if (cell == null)  
            {  
                cell = new UITableViewCell(cellStyle, cellIdentifier);  
            }  
  
            cell.TextLabel.Text = tableItems[indexPath.Row].Title;    
            return cell;  
        }  
  
        public override nint NumberOfSections(UITableView tableView)  
        {  
            return 1;  
        }  
 
 
        #region  editing methods 
  
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)  
        {  
            switch (editingStyle)  
            {  
                case UITableViewCellEditingStyle.Insert:  
                    tableItems.Insert(indexPath.Row, new TableItem("(inserted)"));  
                    tableView.InsertRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);  
                    break;  
  
                case UITableViewCellEditingStyle.None:  
                    Console.WriteLine("CommitEditingStyle:None called");  
                    break;  
            }  
        }  
  
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)  
        {  
            return false;   
        }  

  
        public void WillBeginTableEditing(UITableView tableView)  
        {  
            tableView.BeginUpdates();  
  
            tableView.InsertRows(new NSIndexPath[] {  
                    NSIndexPath.FromRowSection (tableView.NumberOfRowsInSection (0), 0)  
                }, UITableViewRowAnimation.Fade);  
            tableItems.Add(new TableItem("(add new)"));  
  
            tableView.EndUpdates();  
        }  
 
        #endregion  
    }

    public class TableItem  
    {  
        public string Title { get; set; }  
  
        public string ImageName { get; set; }  
  
        public UITableViewCellStyle CellStyle  
        {  
            get { return cellStyle; }  
            set { cellStyle = value; }  
        }  
        protected UITableViewCellStyle cellStyle = UITableViewCellStyle.Default;  
  
        public UITableViewCellAccessory CellAccessory  
        {  
            get { return cellAccessory; }  
            set { cellAccessory = value; }  
        }  
        protected UITableViewCellAccessory cellAccessory = UITableViewCellAccessory.None;  
  
        public TableItem() { }  
  
        public TableItem(string title)  
        { Title = title; }  
  
    }  
}
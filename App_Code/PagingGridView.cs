using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Fadrian.Web.Control
{
    public class PagingGridView : GridView
    {
        public PagingGridView()
            : base()
        {
            this.AllowPaging = true;
            this.AllowSorting = true;
            this.PagerSettings.Mode = PagerButtons.NumericFirstLast;
        }

        #region Custom properties
        [Browsable(true), Category("NewDynamic")]
        [Description("Set the virtual item count for this grid")]
        public int VirtualItemCount
        {
            get
            {
                if (ViewState["pgv_vitemcount"] == null)
                    ViewState["pgv_vitemcount"] = -1;
                return Convert.ToInt32(ViewState["pgv_vitemcount"]);
            }
            set
            {
                ViewState["pgv_vitemcount"] = value;
            }
        }

        [Browsable(true), Category("NewDynamic")]
        [Description("Get the order by string to use for this grid when sorting event is triggered")]
        public string OrderBy
        {
            get
            {
                if (ViewState["pgv_orderby"] == null)
                    ViewState["pgv_orderby"] = string.Empty;
                return ViewState["pgv_orderby"].ToString();
            }
            protected set
            {
                ViewState["pgv_orderby"] = value;
            }
        }

        private string GridViewSortExpression
        {

            get { return ViewState["GridViewSortExpression"] as string ?? string.Empty; }

            set { ViewState["GridViewSortExpression"] = value; }

        }


        private string GridViewSortDirection
        {

            get { return ViewState["GridViewSortDirection"] as string ?? string.Empty; }

            set { ViewState["GridViewSortDirection"] = value; }

        }

        private int CurrentPageIndex
        {
            get
            {
                if (ViewState["pgv_pageindex"] == null)
                    ViewState["pgv_pageindex"] = 0;
                return Convert.ToInt32(ViewState["pgv_pageindex"]);
            }
            set
            {
                ViewState["pgv_pageindex"] = value;
            }
        }
        public override int PageIndex
        {
            get
            {
                return CurrentPageIndex;
            }
            set
            {
                CurrentPageIndex = value;
                //base.PageIndex = value;
            }
        }
        private bool CustomPaging
        {
            get
            {
                return (VirtualItemCount != -1);
            }
        }
        #endregion

        #region Overriding the parent methods
        public override object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;
                // we store the page index here so we dont lost it in databind
                CurrentPageIndex = PageIndex;
            }
        }

        //protected override void OnSorting(GridViewSortEventArgs e)
        //{
        //    // We store the direction for each field so that we can work out whether next sort should be asc or desc order
        //    SortDirection direction = SortDirection.Ascending;
        //    if (ViewState[e.SortExpression] != null && (SortDirection)ViewState[e.SortExpression] == SortDirection.Ascending)
        //    {
        //        direction = SortDirection.Descending;
        //    }
        //    ViewState[e.SortExpression] = direction;
        //    OrderBy = string.Format("{0} {1}", e.SortExpression, (direction == SortDirection.Descending ? "DESC" : "ASC"));
        //    base.OnSorting(e);
        //}

        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            // This method is called to initialise the pager on the grid. We intercepted this and override
            // the values of pagedDataSource to achieve the custom paging using the default pager supplied
            if (CustomPaging)
            {
                pagedDataSource.AllowCustomPaging = true;
                pagedDataSource.VirtualCount = VirtualItemCount;
                pagedDataSource.CurrentPageIndex = CurrentPageIndex;
            }
            base.InitializePager(row, columnSpan, pagedDataSource);
        }

        #endregion


        #region Properties
        /// <summary>
        /// Enable/Disable MultiColumn Sorting.
        /// </summary>
        [
        Description("Whether Sorting On more than one column is enabled"),
        Category("Behavior"),
        DefaultValue("false"),
        ]
        public bool AllowMultiColumnSorting
        {
            get
            {
                object o = ViewState["EnableMultiColumnSorting"];
                return (o != null ? (bool)o : false);
            }
            set
            {
                AllowSorting = true;
                ViewState["EnableMultiColumnSorting"] = value;
            }
        }
        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Image to display for Ascending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),

        ]
        public string SortAscImageUrl
        {
            get
            {
                object o = ViewState["SortImageAsc"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["SortImageAsc"] = value;
            }
        }
        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Image to display for Descending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string SortDescImageUrl
        {
            get
            {
                object o = ViewState["SortImageDesc"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["SortImageDesc"] = value;
            }
        }
        #endregion
        #region Life Cycle


        protected override void OnSorting(GridViewSortEventArgs e)
        {
            if (AllowMultiColumnSorting)
                e.SortExpression = GetSortExpression(e);
            GridViewSortExpression = e.SortExpression;
            this.OrderBy = e.SortExpression;
            base.OnSorting(e);
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (GridViewSortExpression != String.Empty)
                    DisplaySortOrderImages(GridViewSortExpression, e.Row);
            }
            base.OnRowCreated(e);
        }

        #endregion
        #region Protected Methods
        /// <summary>
        ///  Get Sort Expression by Looking up the existing Grid View Sort Expression 
        /// </summary>
        protected string GetSortExpression(GridViewSortEventArgs e)
        {
            string[] sortColumns = null;
            string sortAttribute = GridViewSortExpression;

            //Check to See if we have an existing Sort Order already in the Grid View.	
            //If so get the Sort Columns into an array
            if (sortAttribute != String.Empty)
            {
                sortColumns = sortAttribute.Split(",".ToCharArray());
            }

            //if User clicked on the columns in the existing sort sequence.
            //Toggle the sort order or remove the column from sort appropriately

            if (sortAttribute.IndexOf(e.SortExpression) > 0 || sortAttribute.StartsWith(e.SortExpression))
                sortAttribute = ModifySortExpression(sortColumns, e.SortExpression);
            else
                sortAttribute += String.Concat(",", e.SortExpression, " ASC ");
            return sortAttribute.TrimStart(",".ToCharArray()).TrimEnd(",".ToCharArray());

        }
        /// <summary>
        ///  Toggle the sort order or remove the column from sort appropriately
        /// </summary>
        protected string ModifySortExpression(string[] sortColumns, string sortExpression)
        {
            string ascSortExpression = String.Concat(sortExpression, " ASC ");
            string descSortExpression = String.Concat(sortExpression, " DESC ");

            for (int i = 0; i < sortColumns.Length; i++)
            {

                if (ascSortExpression.Equals(sortColumns[i]))
                {
                    sortColumns[i] = descSortExpression;
                }

                else if (descSortExpression.Equals(sortColumns[i]))
                {
                    Array.Clear(sortColumns, i, 1);
                }
            }

            return String.Join(",", sortColumns).Replace(",,", ",").TrimStart(",".ToCharArray());

        }
        /// <summary>
        ///  Lookup the Current Sort Expression to determine the Order of a specific item.
        /// </summary>
        protected void SearchSortExpression(string[] sortColumns, string sortColumn, out string sortOrder, out int sortOrderNo)
        {
            sortOrder = "";
            sortOrderNo = -1;
            for (int i = 0; i < sortColumns.Length; i++)
            {
                if (sortColumns[i].StartsWith(sortColumn))
                {
                    sortOrderNo = i + 1;
                    if (AllowMultiColumnSorting)
                        sortOrder = sortColumns[i].Substring(sortColumn.Length).Trim();
                    else
                        sortOrder = ((SortDirection == SortDirection.Ascending) ? "ASC" : "DESC");
                }
            }
        }
        /// <summary>
        ///  Display a graphic image for the Sort Order along with the sort sequence no.
        /// </summary>
        protected void DisplaySortOrderImages(string sortExpression, GridViewRow dgItem)
        {
            string[] sortColumns = GridViewSortExpression.Split(",".ToCharArray());

            for (int i = 0; i < dgItem.Cells.Count; i++)
            {
                if (dgItem.Cells[i].Controls.Count > 0 && dgItem.Cells[i].Controls[0] is LinkButton)
                {
                    string sortOrder;
                    int sortOrderNo;
                    string column = ((LinkButton)dgItem.Cells[i].Controls[0]).CommandArgument;
                    SearchSortExpression(sortColumns, column, out sortOrder, out sortOrderNo);
                    if (sortOrderNo > 0)
                    {
                        string sortImgLoc = (sortOrder.Equals("ASC") ? SortAscImageUrl : SortDescImageUrl);

                        if (sortImgLoc != String.Empty)
                        {
                            Image imgSortDirection = new Image();
                            imgSortDirection.ImageUrl = sortImgLoc;
                            dgItem.Cells[i].Controls.Add(imgSortDirection);
                            Label lblSortOrder = new Label();                        
                            lblSortOrder.Text = sortOrderNo.ToString();
                            dgItem.Cells[i].Controls.Add(lblSortOrder);

                        }
                        else
                        {

                            Label lblSortDirection = new Label();
                            lblSortDirection.Font.Size = FontUnit.XSmall;
                            lblSortDirection.Font.Name = "webdings";
                            lblSortDirection.EnableTheming = false;
                            lblSortDirection.Text = (sortOrder.Equals("ASC") ? "5" : "6");
                            dgItem.Cells[i].Controls.Add(lblSortDirection);

                            if (AllowMultiColumnSorting)
                            {
                                Literal litSortSeq = new Literal();
                                litSortSeq.Text = sortOrderNo.ToString();
                                dgItem.Cells[i].Controls.Add(litSortSeq);

                            }


                        }




                    }

                }
            }

        }
        #endregion
    }
}

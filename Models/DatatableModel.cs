namespace NewBrainfieldNetCore.Models
{
    public class DatatableModel
    {
        public string sEcho { get; set; }
        public string sSearch { get; set; }
        public string sSearch1 { get; set; }
        public string sSearch2 { get; set; }
        public string sSearch_3 { get; set; }
        public string sSearch_4 { get; set; }

        public string sSearch5 { get; set; }
        public string sSearch6 { get; set; }
        public string sSearch7 { get; set; }
        public string sSearch8 { get; set; }
        public string sSearch9 { get; set; }
        public string sSearch10 { get; set; }
        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }
    }
}

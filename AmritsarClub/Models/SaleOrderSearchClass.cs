using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class SaleOrderSearchClass
    {
        public string Find_Group_Order { get; set; }
        public string Find_Work_Order { get; set; }
        public string Find_Batch_No { get; set; }
        public string Combi_Code { get; set; }
        public string PartyWise_Search { get; set; }
        public string SectionWise_Search { get; set; }
        public string ItemName { get; set; }
        public DateTime date_wise_search1 { get; set; }
        public DateTime date_wise_search2 { get; set; }
        public string Group_orderList { get; set; }

        //===designer=====RM
        //public sale_order saleorder { get; set; }
        //public designer_image designerImage { get; set; }
        //public designer_main designermain { get; set; }
        //public tbl_Component_details tbl_Component_Details { get; set; }
        //public tblblockingofstock2 tblblockingofstock2_ { get; set; }
        //public po_details po_Details { get; set; }

        //========Production=========
        //public tblrequisition tblrequisition_ { get; set; }

        //public unit_master unitMaster { get; set; }
        //public tbl_cr_issuerecord tblcrissuerecord { get; set; }
    }
}
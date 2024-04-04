using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmritsarClub.Models
{
    public class MemberModel
    {
        public int ID { get; set; }
        public string MemberID { get; set; }
        public string Name { get; set; }
        public string Father_Name { get; set; }
        public int Age { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public string Contact_No { get; set; }
        public string IDProof { get; set; }
        public string IDProof_Path { get; set; }

        public string Spouse_Name { get; set; }
        public int Spouse_Age { get; set; }
        public DateTime Spouse_DOB { get; set; }
        public string Spouse_PhotoID_Proof { get; set; }
        public string Spouse_Sex { get; set; }
        public string Spouse_Relation { get; set; }

        public string Child1_Name { get; set; }
        public int Childe1_Age { get; set; }
        public DateTime Childe1_DOB { get; set; }
        public string Childe1_PhotoID_Proof { get; set; }
        public string Childe1_Sex { get; set; }
        public string Childe1_Relation { get; set; }

        public string Child2_Name { get; set; }
        public int Childe2_Age { get; set; }
        public DateTime Childe2_DOB { get; set; }
        public string Childe2_PhotoID_Proof { get; set; }
        public string Childe2_Sex { get; set; }
        public string Childe2_Relation { get; set; }

        public string Child3_Name { get; set; }
        public int Childe3_Age { get; set; }
        public DateTime Childe3_DOB { get; set; }
        public string Childe3_PhotoID_Proof { get; set; }
        public string Childe3_Sex { get; set; }
        public string Childe3_Relation { get; set; }

        public string Child4_Name { get; set; }
        public int Childe4_Age { get; set; }
        public DateTime Childe4_DOB { get; set; }
        public string Childe4_PhotoID_Proof { get; set; }
        public string Childe4_Sex { get; set; }
        public string Childe4_Relation { get; set; }

        public string Child5_Name { get; set; }
        public int Childe5_Age { get; set; }
        public DateTime Childe5_DOB { get; set; }
        public string Childe5_PhotoID_Proof { get; set; }
        public string Childe5_Sex { get; set; }
        public string Childe5_Relation { get; set; }
    }
}
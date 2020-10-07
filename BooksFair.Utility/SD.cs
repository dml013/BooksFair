using System;
using System.Collections.Generic;
using System.Text;

namespace BooksFair.Utility {
    public static class SD { //static details
        public const string ProcCoverTypeCreate = "usp_CreateCoverType";
        public const string ProcCoverTypeGet = "usp_GetCoverType";
        public const string ProcCoverTypeGetAll= "usp_GetCoverTypes";
        public const string ProcCoverTypeUpdate = "usp_UpdateCoverType";
        public const string ProcCoverTypeDelete = "usp_DeleteCoverType";

        public const string RoleUserIndi = "Individual Customer";
        public const string RoleUserComp = "Company Customer";
        public const string RoleAdmin = "Admin";
        public const string RoleEmployee = "Employee";

    }
}

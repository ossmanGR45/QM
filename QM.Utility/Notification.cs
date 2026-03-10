using QM.Models;
using QM.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static QM.Models.Enums;

namespace QM.Utility
{
    public class Notification
    {
        int Id { get; set; }
        RequestStatus RequestStatus { get; set; }
        requestType RequestType { get; set; }
        public Notification(int id, RequestStatus status, requestType requestType) {
            Id = id;
            RequestStatus = status;
            RequestType = requestType;
        }
        

    }
}

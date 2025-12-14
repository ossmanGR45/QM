using QM.Models.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static QM.Models.Enums;


namespace QM.Models
{
    public class Request : EntityBase
    {

        public DateTime Year { get; set; }

        
        public Likelihood Likelihood { get; set; }

        
        public Impact Impact { get; set; }

        
        public DateTime ExpectedTime { get; set; }

        
        public string Responsible { get; set; }

        public string Description { get; set; }

        
        public RequestStatus Status { get; set; }

        public bool Occured { get; set; }




        #region navigation Properties
        
        public ICollection<RequestActionMapping> RequestAction { get; set; }
        public ICollection<RequestRiskMapping> RequestRisk { get; set; }



        #endregion
    }
}
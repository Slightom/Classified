using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClassifiedMVC.Models
{
    public class AllAtributesModel
    {
        public int AllAtributesModelID { get; set; }
        public string fuel { get; set; }
        public double Engine_power { get; set; }
        public double Enginie_capacity { get; set; }
        public string Body_type { get; set; }
        public string Transmission { get; set; }
        public string Country_of_origin { get; set; }
        public double Mileage { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }


        public int Number_of_pages { get; set; }

        public int Publication_Year { get; set; }
    }

    public class UploadModel
    {

        public int UploadModelID { get; set; }

        [DataType(DataType.Upload)]
        HttpPostedFileBase upload { get; set; }
    }
}